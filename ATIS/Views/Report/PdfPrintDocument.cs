using System;
using System.Drawing;
using System.Drawing.Printing;
using BitMiracle.Docotic.Pdf;
namespace ATIS.Ui.Views.Report
{
    enum PrintSize
    {
        FitPage,
        ActualSize
    }

     class PdfPrintDocument : IDisposable
    {
        private readonly PrintDocument _mPrintDocument;
        private readonly PrintSize _mPrintSize;

        private readonly PdfDocument _mPdf;

        private PrintAction _mPrintAction;
        private int _mPageIndex;
        private int _mLastPageIndex;
        private RectangleF _mPrintableAreaInPoints;

        public PdfPrintDocument(PdfDocument pdf, PrintSize printSize)
        {
            if (pdf == null)
                throw new ArgumentNullException("pdf");

            _mPdf = pdf;
            _mPrintSize = printSize;

            _mPrintDocument = new PrintDocument();
            _mPrintDocument.BeginPrint += printDocument_BeginPrint;
            _mPrintDocument.QueryPageSettings += printDocument_QueryPageSettings;
            _mPrintDocument.PrintPage += printDocument_PrintPage;
            _mPrintDocument.EndPrint += printDocument_EndPrint;
        }

        public PrintDocument PrintDocument => _mPrintDocument;

        public void Dispose()
        {
            _mPrintDocument.Dispose();
        }

        public void Print(PrinterSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            _mPrintDocument.PrinterSettings = settings;
            _mPrintDocument.Print();
        }

        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            PrintDocument printDocument = (PrintDocument)sender;
            printDocument.OriginAtMargins = false;

            _mPrintAction = e.PrintAction;

            switch (printDocument.PrinterSettings.PrintRange)
            {
                case PrintRange.Selection:
                case PrintRange.CurrentPage:
                    {
                        _mPageIndex = 0;
                        _mLastPageIndex = 0;
                        break;
                    }

                case PrintRange.SomePages:
                    {
                        _mPageIndex = Math.Max(0, printDocument.PrinterSettings.FromPage - 1);
                        _mLastPageIndex = Math.Min(_mPdf.PageCount - 1, printDocument.PrinterSettings.ToPage - 1);
                        break;
                    }

                case PrintRange.AllPages:
                default:
                    {
                        _mPageIndex = 0;
                        _mLastPageIndex = _mPdf.PageCount - 1;
                        break;
                    }
            }
        }

        private void printDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            PdfPage page = _mPdf.Pages[_mPageIndex];

            // Auto-detect portrait/landscape orientation.
            // Printer settings for orientation are ignored in this sample.
            PdfSize pageSize = GetPageSizeInPoints(page);
            e.PageSettings.Landscape = pageSize.Width > pageSize.Height;

            _mPrintableAreaInPoints = GetPrintableAreaInPoints(e.PageSettings);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics gr = e.Graphics;

            // Work in points to have consistent units for all contexts:
            // 1. Printer
            // 2. Print preview
            // 3. PDF
            gr.PageUnit = GraphicsUnit.Point;

            if (_mPrintAction == PrintAction.PrintToPreview)
            {
                gr.Clear(Color.LightGray);
                gr.FillRectangle(Brushes.White, _mPrintableAreaInPoints);
                gr.IntersectClip(_mPrintableAreaInPoints);

                gr.TranslateTransform(_mPrintableAreaInPoints.X, _mPrintableAreaInPoints.Y);
            }

            PdfPage page = _mPdf.Pages[_mPageIndex];
            PdfSize pageSizeInPoints = GetPageSizeInPoints(page);

            if (_mPrintSize == PrintSize.FitPage)
            {
                float sx = (float)(_mPrintableAreaInPoints.Width / pageSizeInPoints.Width);
                float sy = (float)(_mPrintableAreaInPoints.Height / pageSizeInPoints.Height);
                float scaleFactor = Math.Min(sx, sy);

                CenterContentInPrintableArea(gr, pageSizeInPoints, scaleFactor);
                gr.ScaleTransform(scaleFactor, scaleFactor);
            }
            else if (_mPrintSize == PrintSize.ActualSize)
            {
                CenterContentInPrintableArea(gr, pageSizeInPoints, 1);
            }

            page.Draw(gr);

            ++_mPageIndex;
            e.HasMorePages = (_mPageIndex <= _mLastPageIndex);
        }

        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
        }

        private void CenterContentInPrintableArea(Graphics gr, PdfSize contentSizeInPoints, float scaleFactor)
        {
            float xDiff = (float)(_mPrintableAreaInPoints.Width - contentSizeInPoints.Width * scaleFactor);
            float yDiff = (float)(_mPrintableAreaInPoints.Height - contentSizeInPoints.Height * scaleFactor);
            if (Math.Abs(xDiff) > 0 || Math.Abs(yDiff) > 0)
                gr.TranslateTransform(xDiff / 2, yDiff / 2);
        }

        private static PdfBox GetPageBox(PdfPage page)
        {
            // Emit Adobe Reader behavior - prefer CropBox, but use MediaBox bounds when
            // some CropBox bound is out of MediaBox area.

            PdfBox mediaBox = page.MediaBox;
            PdfBox cropBox = page.CropBox;
            double left = cropBox.Left;
            double bottom = cropBox.Bottom;
            double right = cropBox.Right;
            double top = cropBox.Top;

            if (left < mediaBox.Left || left > mediaBox.Right)
                left = mediaBox.Left;

            if (bottom < mediaBox.Bottom || bottom > mediaBox.Top)
                bottom = mediaBox.Bottom;

            if (right > mediaBox.Right || right < mediaBox.Left)
                right = mediaBox.Right;

            if (top > mediaBox.Top || top < mediaBox.Bottom)
                top = mediaBox.Top;

            return new PdfBox(left, bottom, right, top);
        }

        private static PdfSize GetPageSizeInPoints(PdfPage page)
        {
            PdfBox pageArea = GetPageBox(page);
            if (page.Rotation == PdfRotation.Rotate90 || page.Rotation == PdfRotation.Rotate270)
                return new PdfSize(pageArea.Height, pageArea.Width);

            return pageArea.Size;
        }

        private static RectangleF GetPrintableAreaInPoints(PageSettings pageSettings)
        {
            RectangleF printableArea = pageSettings.PrintableArea;
            if (pageSettings.Landscape)
            {
                float tmp = printableArea.Width;
                printableArea.Width = printableArea.Height;
                printableArea.Height = tmp;
            }

            // PrintableArea is expressed in hundredths of an inch
            const float printerSpaceToPoint = 72.0f / 100.0f;
            return new RectangleF(
                printableArea.X * printerSpaceToPoint,
                printableArea.Y * printerSpaceToPoint,
                printableArea.Width * printerSpaceToPoint,
                printableArea.Height * printerSpaceToPoint
            );
        }
    }
}
