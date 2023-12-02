using System.Collections.ObjectModel;
using System.Globalization;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl06Phylums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl06Phylums;
public class Tbl06PhylumsListDocument : IDocument
{
    public Tbl06PhylumsPdfModel Model
    {
        get;
    }
    public ObservableCollection<Tbl06Phylum> ViewModelPhylumsCollection
    {
        get;
    }
    public Tbl06PhylumsListDocument(Tbl06PhylumsPdfModel model, ObservableCollection<Tbl06Phylum> viewModelPhylumsCollection)
    {
        Model = model;
        ViewModelPhylumsCollection = viewModelPhylumsCollection;
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
                text.Span(CultRes.StringsRes.Phylum).FontSize(20).SemiBold();
                text.Span("                                                                                                       ");
                text.Span(DateTime.Now.ToString(CultureInfo.InstalledUICulture)).NormalPosition();
            });
            column.Spacing(5);
            column.Item().Element(ComposeListContent);
        });
    }

    private void ComposeListContent(IContainer container)
    {
        if (ViewModelPhylumsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in ViewModelPhylumsCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(4);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text($"{item.PhylumName} {item.Author} {item.AuthorYear}" +
                                  $" {item.EngName} {item.GerName} {item.FraName} {item.PorName}").WrapAnywhere();
            }
        });
    }
}
