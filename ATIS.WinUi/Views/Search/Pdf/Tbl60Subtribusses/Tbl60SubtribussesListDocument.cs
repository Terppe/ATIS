using System.Collections.ObjectModel;
using System.Globalization;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl60Subtribusses;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl60Subtribusses;
internal class Tbl60SubtribussesListDocument : IDocument
{
    public Tbl60SubtribussesPdfModel Model
    {
        get;
    }
    public ObservableCollection<Tbl60Subtribus> ViewModelSubtribussesCollection
    {
        get;
    }
    public Tbl60SubtribussesListDocument(Tbl60SubtribussesPdfModel model, ObservableCollection<Tbl60Subtribus> viewModelSubtribussesCollection)
    {
        Model = model;
        ViewModelSubtribussesCollection = viewModelSubtribussesCollection;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.MarginVertical(50);
                page.MarginLeft(30);
                page.MarginRight(20);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Size(PageSizes.A4);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            column.Item().Text(text =>
            {
                text.Span(CultRes.StringsRes.List).FontSize(20).SemiBold();
                text.Span("          ");
                text.Span(CultRes.StringsRes.Subtribus).FontSize(20).SemiBold();
                text.Span("                                                                                                       ");
                text.Span(DateTime.Now.ToString(CultureInfo.InstalledUICulture)).NormalPosition();
            });
            column.Spacing(5);
            column.Item().Element(ComposeListContent);
        });
    }

    private void ComposeListContent(IContainer container)
    {
        if (ViewModelSubtribussesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in ViewModelSubtribussesCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(4);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text($"{item.SubtribusName} {item.Author} {item.AuthorYear}" +
                                  $" {item.EngName} {item.GerName} {item.FraName} {item.PorName}").WrapAnywhere();
            }
        });
    }

}
