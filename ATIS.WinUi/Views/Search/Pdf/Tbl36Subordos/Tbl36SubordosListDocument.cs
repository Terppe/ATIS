using System.Collections.ObjectModel;
using System.Globalization;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl36Subordos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl36Subordos;
internal class Tbl36SubordosListDocument : IDocument
{
    public Tbl36SubordosPdfModel Model
    {
        get;
    }
    public ObservableCollection<Tbl36Subordo> ViewModelSubordosCollection
    {
        get;
    }
    public Tbl36SubordosListDocument(Tbl36SubordosPdfModel model, ObservableCollection<Tbl36Subordo> viewModelSubordosCollection)
    {
        Model = model;
        ViewModelSubordosCollection = viewModelSubordosCollection;
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
                text.Span(CultRes.StringsRes.Subordo).FontSize(20).SemiBold();
                text.Span("                                                                                                       ");
                text.Span(DateTime.Now.ToString(CultureInfo.InstalledUICulture)).NormalPosition();
            });
            column.Spacing(5);
            column.Item().Element(ComposeListContent);
        });
    }

    private void ComposeListContent(IContainer container)
    {
        if (ViewModelSubordosCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in ViewModelSubordosCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(4);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text($"{item.SubordoName} {item.Author} {item.AuthorYear}" +
                                  $" {item.EngName} {item.GerName} {item.FraName} {item.PorName}").WrapAnywhere();
            }
        });
    }

}
