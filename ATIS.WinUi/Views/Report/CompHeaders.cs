using System.Globalization;
using QuestPDF.Fluent;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace ATIS.WinUi.Views.Report;
public class CompHeaders
{
    public static void Header(IContainer container, int textHight)
    {
        // evtl icon
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Span(CultRes.StringsRes.Atis).FontSize(24).SemiBold();
                    text.Span(CultRes.StringsRes.AtisHeader).FontSize(textHight).SemiBold();
                    text.Span("      ");
                    text.Span(DateTime.Now.ToString(CultureInfo.InstalledUICulture)).NormalPosition();

                });
            });
        });
    }
    public static void HeaderSynonym(IContainer container, int tabLeftRel, int tabRightRel)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel);
                        columns.ConstantColumn(tabRightRel);
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportSynonyms);
                });

            });
        });
    }
    public static void HeaderName(IContainer container, int tabLeftRel, int tabRightRel)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel);
                        columns.ConstantColumn(tabRightRel);
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportNames);
                });

            });
        });
    }

    public static void HeaderTaxoHiera(IContainer container, int tabLeftRel4, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportTaxoHiera).FontSize(textHight).SemiBold();
                });
            });
        });
    }
    public static void HeaderChildren(IContainer container, int tabRightRel, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabRightRel);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportDirectChild).FontSize(textHight).SemiBold();
                });
            });
        });
    }
    public static void HeaderHeaderReferences(IContainer container, int tabLeftRel4, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportReferences).FontSize(textHight).SemiBold();
                });
            });
        });
    }
    public static void HeaderReferencesExperts(IContainer container, int tabLeftRel8)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportExperts).SemiBold();
                });
            });
        });
    }
    public static void HeaderReferencesOtherSources(IContainer container, int tabLeftRel8)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportOtherSources).SemiBold();
                });
            });
        });
    }
    public static void HeaderReferencesPublications(IContainer container, int tabLeftRel8)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportPublications).SemiBold();
                });
            });
        });
    }
    public static void HeaderGeographics(IContainer container, int tabLeftRel4, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportGeographics).FontSize(textHight).SemiBold();
                });
            });
        });
    }
    public static void HeaderImages(IContainer container, int tabLeftRel4, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportImages).FontSize(textHight).SemiBold();
                });
            });
        });
    }

    public static void HeaderUsefulInfos(IContainer container, int tabLeftRel4, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportUseful).FontSize(textHight).SemiBold();
                });
            });
        });
    }
    public static void HeaderComments(IContainer container, int tabLeftRel4, int textHight)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(tabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportComments).FontSize(textHight).SemiBold();
                });
            });
        });
    }

}
