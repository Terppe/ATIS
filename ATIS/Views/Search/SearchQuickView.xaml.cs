using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.Layout.Properties;
//using iText.Kernel.Pdf;		    //PdfWriter, PdfDocument
//using iText.Kernel.Geom;	    //PageSize

namespace ATIS.Ui.Views.Search
{
    /// <summary>
    /// Interaktionslogik für SearchQuickView.xaml
    /// </summary>
    public partial class SearchQuickView : UserControl
    {

        public SearchQuickView()
        {
            DataContext = new SearchQuickViewModel();

            InitializeComponent();

        }
        public SearchQuickView(string un)
        {
            DataContext = new SearchQuickViewModel(un);
            InitializeComponent();

        }

        //private void HTML2PDF(string outFile, string htmlDoc)
        //{
        //    System.IO.FileInfo fi = new FileInfo(outFile);	//例 outFile = "d:\newQuotation.pdf"
        //    PdfWriter writer = new PdfWriter(fi);

        //    //HtmlConverter.ConvertToPdf(htmlDoc, writer);

        //    //用法2 -- 自定义页面大小、留白尺寸
        //    PdfDocument pdf = new PdfDocument(writer);
        //    PageSize a6 = PageSize.A6;
        //    a6.ApplyMargins(20, 20, 20, 20, false);		//if true the rectangle will expand, otherwise it will shrink
        //    pdf.SetDefaultPageSize(a6);
        //    ConverterProperties prop = new ConverterProperties();
        //    HtmlConverter.ConvertToPdf(htmlDoc, pdf, prop);

        //}
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
                //           printDialog.PrintDocument(((IDocumentPaginatorSource)FlowDocument).DocumentPaginator, "This is a Flow Document");
                printDialog.PrintVisual(LayoutRoot, "Landscape broken Grid print");


            /*
            Configure printer dialog box
            System.Windows.Controls.PrintDialog dlg = new System.Windows.Controls.PrintDialog();
            dlg.PageRangeSelection = PageRangeSelection.AllPages;
            dlg.UserPageRangeEnabled = true;

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Print document
            }
            //----------------------------------------------
            private void PrintButton_Click(object sender, EventArgs e) 

  { 

 /*   PrintDialog printDlg = new PrintDialog(); 

    PrintDocument printDoc = new PrintDocument(); 

    printDoc.DocumentName = "Print Document"; 

    printDlg.Document = printDoc; 

    printDlg.AllowSelection = true; 

    printDlg.AllowSomePages = true; 

    //Call ShowDialog 

    if (printDlg.ShowDialog() == DialogResult.OK) 

        printDoc.Print(); 

  

} 

*/
            /*  var printDialog = new PrintDialog();
              printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
              printDialog.PrintVisual(LayoutRoot, "Landscape broken Grid print");
  */
            /*
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                DrawingVisual dv = new DrawingVisual();

                var dc = dv.RenderOpen();

                var rect = new Rect(new System.Windows.Point(20, 20), new System.Windows.Size(350, 240));
                dc.DrawRoundedRectangle(System.Windows.Media.Brushes.Yellow, new Pen(Brushes.Purple, 2), rect, 20, 20);

                dc.DrawImage(new BitmapImage(new Uri("pack://application:,,,/Media/XAMLguy.png", UriKind.Absolute)), new Rect(50, 50, 100, 100));

                dc.DrawText(new FormattedText("WPF Printing", CultureInfo.CurrentCulture, FlowDirection,
                      new Typeface(new System.Windows.Media.FontFamily("Courier New"), FontStyles.Normal, FontWeights.Bold,
                          FontStretches.Normal), 13, System.Windows.Media.Brushes.Black), new System.Windows.Point(50, 180));

                dc.DrawGeometry(Brushes.Green, new Pen(Brushes.Gray, 2), new RectangleGeometry(new Rect(270, 110, 40, 100)));

                dc.DrawEllipse(Brushes.Red, (System.Windows.Media.Pen)null, new Point(290, 90), 50, 50);
                dc.DrawEllipse(Brushes.Blue, (System.Windows.Media.Pen)null, new Point(280, 85), 14, 18);
                dc.DrawEllipse(Brushes.Blue, (System.Windows.Media.Pen)null, new Point(320, 85), 14, 18);

                rect = new Rect(new System.Windows.Point(240, 50), new System.Windows.Size(100, 30));
                dc.DrawRectangle(System.Windows.Media.Brushes.Black, (System.Windows.Media.Pen)null, rect);

                dc.DrawLine(new Pen(Brushes.Black, 2), new Point(230, 140), new Point(350, 200));

                dc.DrawDrawing(CreateGeometryDrawing());

                dc.DrawGlyphRun(Brushes.Red, CreateGlyphRun());

                dc.Close();

                printDialog.PrintVisual(dv, "Print");

                var bmp = new RenderTargetBitmap(600, 350, 120, 96, PixelFormats.Pbgra32);
                bmp.Render(dv);
                var img = new Image { Width = 100, Height = 100, Source = bmp, Stretch = Stretch.Fill };

                Width = 500;
                Height = 400;

                var r = new Rectangle { Fill = new ImageBrush(bmp) };
                r.SetValue(Grid.RowProperty, 1);
                r.SetValue(Panel.ZIndexProperty, -1);
                Grid1.Children.Add(r);
            }
                    public IDocumentPaginatorSource FlowDocument { get; set; }

        private void Reader_LostFocus(object sender, RoutedEventArgs e)
        {
                    Width = Reader.Width + 10;
        }

            */
        }
        //----------------------PDF-------------

        //private void Pdf_Click(object sender, RoutedEventArgs e)
        //{
        //    /*
        //    //Set the output dir and file name in folder Documents
        //    string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    string file = "CreadedBySharp.pdf";

        //    PrintDocument pDoc = new PrintDocument()
        //    {
        //        PrinterSettings = new PrinterSettings()
        //        {
        //            PrinterName = "Microsoft Print to PDF",
        //            PrintToFile = true,
        //            PrintFileName = System.IO.Path.Combine(directory, file)
        //        }
        //    };

        //    pDoc.PrintPage += new PrintPageEventHandler(Print_Page);
        //    pDoc.Print();

        //    */
        //    //Einfaches Beispiel PDF

        //    //var stream = new MemoryStream();
        //    //var writer = new PdfWriter(stream);
        //    //var pdf = new PdfDocument(writer);
        //    //var document = new Document(pdf);

        //    //document.Add(new Paragraph("Hello world!"));
        //    //document.Close();

        //     //Einfaches Beispiel PDF
        //    float margin = Utilities.MillimetersToPoints(Convert.ToSingle(20));

 
        //    Document doc = new Document(iTextSharp.text.PageSize.A4, margin, margin, margin, margin);

        //    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("D:\\Temp\\Test.pdf", FileMode.Create));
        //    writer.SetFullCompression();
        //    writer.CloseStream = true;

        //    doc.Open();
        //    doc.NewPage();

        //    doc.Add(new Chunk("Hallo, dies ist ein einfacher Text :)"));
        //    doc.Add(iTextSharp.text.Image.GetInstance("D:\\Temp\\thT7UR41DT.jpg"));

        //    DataTable tableToAdd = GetFilledDataTable();
        //    PdfPTable pdfTable = new PdfPTable(tableToAdd.Columns.Count);
        //    pdfTable.WidthPercentage = 100;

        //    foreach (DataColumn col in tableToAdd.Columns)
        //    {
        //        var pdfcell = new PdfPCell();
        //        pdfcell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //        pdfcell.Phrase = new Phrase(col.ColumnName, new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD));
        //        pdfTable.AddCell(pdfcell);
        //    }

        //    foreach (DataRow row in tableToAdd.Rows)
        //    {
        //        foreach (DataColumn col in tableToAdd.Columns)
        //        {
        //            var pdfcell = new PdfPCell();
        //            pdfcell.Phrase = new Phrase(row[col.ColumnName].ToString());
        //            pdfTable.AddCell(pdfcell);
        //        }
        //    }

        //    doc.Add(pdfTable);

        //    doc.Close();
        //    doc = null;
        //    Process.Start("D:\\Temp\\Test.pdf");


        //}

        //void Print_Page(object sender, PrintPageEventArgs e)
        //{
        //    //here you can play with the font style
        //    //(and much more, this is just an ultra basic example)
        //    //           Font fnt = new Font("Courier New", 12);

        //    // INSERT THE DESIRED TEXT INTO THE pdf FILE
        //    //            e.Graphics.DrawString("When hothing goes right, go left", fnt, System.Drawing.Brushes.Black, 0, 0);
        //}

        private DataTable GetFilledDataTable()
        {
            var result = new DataTable();
            result.Columns.Add("ID", typeof(int));
            result.Columns.Add("Spalte1");
            result.Columns.Add("Spalte2");

            for (int i = 0; i < 20; i++)
            {
                DataRow row = result.NewRow();
                row["ID"] = i;
                row["Spalte1"] = "Zelle 2 in Reihe " + i.ToString();
                row["Spalte2"] = "Zelle 3 in Reihe " + i.ToString();
                result.Rows.Add(row);
            }

            return result;

        }

        //------------------------------------------------------------
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
  //          var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
  //          rp.Show();
        }

        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
 //           var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
  //          rp.Show();
        }

        //private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
        //    rp.Show();
        //}

        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
 //           var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
  //          rp.Show();
        }

        //private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl15Subdivisions");
        //    rp.Show();
        //}

        //private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
        //    rp.Show();
        //}

        //private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
        //    rp.Show();
        //}

        //private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
        //    rp.Show();
        //}

        //private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
        //    rp.Show();
        //}

        //private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
        //    rp.Show();
        //}

        //private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
        //    rp.Show();
        //}

        //private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
        //    rp.Show();
        //}

        //private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
        //    rp.Show();
        //}

        //private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
        //    rp.Show();
        //}

        //private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
        //    rp.Show();
        //}

        //private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
        //    rp.Show();
        //}

        //private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
        //    rp.Show();
        //}

        //private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
        //    rp.Show();
        //}

        //private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
        //    rp.Show();
        //}

        //private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
        //    rp.Show();
        //}

        //private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl63InfratribussesWindow(id, "Tbl63Infratribusses");
        //    rp.Show();
        //}

        //private void HyperlinkGenus_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl66GenussesWindow(id, "Tbl66Genusses");
        //    rp.Show();
        //}

        private void HyperlinkSpeciesgroup_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void HyperlinkFiSpecies_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl69FiSpeciessesWindow(id, "Tbl69FiSpeciesses");
        //    rp.Show();
        //}

        //private void HyperlinkPlSpecies_Click(object sender, RoutedEventArgs e)
        //{
        //    var tagValue = ((Hyperlink)sender).Tag;
        //    var id = Convert.ToInt32(tagValue);
        //    var rp = new ReportTbl72PlSpeciessesWindow(id, "Tbl72PlSpeciesses");
        //    rp.Show();
        //}

        private void HyperlinkName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperlinkSynonym_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
