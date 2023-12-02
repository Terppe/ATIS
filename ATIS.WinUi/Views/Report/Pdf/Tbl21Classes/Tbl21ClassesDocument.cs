﻿using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace ATIS.WinUi.Views.Report.Pdf.Tbl21Classes;

public class Tbl21ClassesDocument : IDocument
{
    private Tbl21ClassesPdfModel Model
    {
        get;
    }

    private static int _tabLeftRel;
    private static int _tabRightRel = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ReportPdfRightTab")); //150
    private const int TabLeftRel4 = 4;
    private const int TabLeftRel8 = 8;
    private static int _tabLeftLastRel;
    private static int _tabRightLastRel;
    private static readonly int TabRightRel8 = _tabRightRel - 8;
    private const int TextHight = 14;

    public Tbl21ClassesDocument(Tbl21ClassesPdfModel model)
    {
        Model = model;
    }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.MarginVertical(50);
                page.MarginLeft(30);
                page.MarginRight(10);
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
            //Taxonomy and Nomenclature
            column.Item().Element(ComposeHeaderTaxoNomen);
            column.Spacing(5);
            column.Item().Element(ComposeTaxoNomenContent);
            column.Spacing(5);

            //Taxonomy Hierarchy
            column.Item().Element(ComposeHeaderTaxoHiera);
            column.Spacing(5);
            column.Item().Element(ComposeTaxoHieraContent);
            column.Spacing(5);

            //Direct Children
            column.Item().Element(ComposeHeaderChildren);
            column.Spacing(5);
            column.Item().Element(ComposeChildrenContent);
            column.Spacing(5);

            //Refernces
            column.Item().Element(ComposeHeaderReferences);
            column.Spacing(5);
            //Refernces Experts
            column.Item().Element(ComposeHeaderReferencesExperts);
            column.Item().Element(ComposeReferencesExperts);
            column.Spacing(5);
            //Refernces Sources
            column.Item().Element(ComposeHeaderReferencesOtherSources);
            column.Item().Element(ComposeReferencesOtherSources);
            column.Spacing(5);
            //Refernces Authors
            column.Item().Element(ComposeHeaderReferencesPublications);
            column.Item().Element(ComposeReferencesPublications);
            column.Spacing(5);
            //Comments
            column.Item().Element(ComposeHeaderComments);
            column.Item().Element(ComposeComments);
        });
    }

    private void ComposeHeaderTaxoNomen(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Span("ATIS  ").FontSize(24).SemiBold();
                    text.Span(CultRes.StringsRes.AtisHeader).FontSize(TextHight).SemiBold();
                    text.Span("        ");
                    text.Span(DateTime.Now.ToString(CultureInfo.InvariantCulture));

                });
                column.Item().Text(text =>
                {
                    text.Span($"{Model.ClassName}").FontSize(TextHight).SemiBold().Italic();
                    text.Span($" {Model.Author}").FontSize(TextHight).SemiBold();
                    text.Span($" {Model.AuthorYear}").FontSize(TextHight).SemiBold();
                });
                column.Item().Text(text =>
                {
                    text.Span(CultRes.StringsRes.ReportTaxonomicId);
                    text.Span($"{Model.CountId}");
                });
            });

        });

    }

    private void ComposeTaxoNomenContent(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel4);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportTaxoNomen).FontSize(TextHight).SemiBold();
                });

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.Regnum);
                    table.Cell().Text($@"{Model.RegnumTree?.RegnumName} {Model.RegnumTree?.Subregnum}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportTaxoRank);
                    table.Cell().Text(CultRes.StringsRes.Class);
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportSynonyms);
                    table.Cell().Text($"{Model.Synonym}").WrapAnywhere();
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportNames);
                    table.Cell().Text($"{Model.EngName}[ENG] {Model.GerName}[GER] {Model.FraName}[FRA] {Model.PorName}[SPA]").WrapAnywhere();
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportTaxoStatus).SemiBold();
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportCurrentStand);
                    table.Cell().Text($"{Model.Valid} {Model.ValidYear}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportDataQualiIndicator).SemiBold();
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportRecordCrediRate);
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportRecordUpdate);
                    table.Cell().Text($"{Model.UpdaterDate:d}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportInfo);
                    table.Cell().Text($"{Model.Info}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.ConstantColumn(TabRightRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportMemo);
                    table.Cell().Text($"{Model.Memo}");
                });
            });
        });
    }

    private void ComposeHeaderTaxoHiera(IContainer container)
    {
        CompHeaders.HeaderTaxoHiera(container, TabLeftRel4, TextHight);
    }

    private void ComposeTaxoHieraContent(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                if (Model.RegnumTree != null && !string.IsNullOrWhiteSpace(Model.RegnumTree!.RegnumName))
                {
                    _tabLeftRel += 8;
                    _tabRightRel -= 8;
                    CompTaxoHieras.ComposeTaxoHierRegnum(column, _tabLeftRel, _tabRightRel, Model.RegnumTree);
                }

                if (Model.PhylumTree != null && !string.IsNullOrWhiteSpace(Model.PhylumTree!.PhylumName))
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierPhylum(column, _tabLeftRel, _tabRightRel, Model.PhylumTree);
                }

                if (Model.DivisionTree != null && !string.IsNullOrWhiteSpace(Model.DivisionTree!.DivisionName))
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierDivision(column, _tabLeftRel, _tabRightRel, Model.DivisionTree);
                }

                if (Model.SubphylumTree != null && !string.IsNullOrWhiteSpace(Model.SubphylumTree!.SubphylumName))
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSubphylum(column, _tabLeftRel, _tabRightRel, Model.SubphylumTree);
                }
                if (Model.SubdivisionTree != null && !string.IsNullOrWhiteSpace(Model.SubdivisionTree!.SubdivisionName))
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSubdivision(column, _tabLeftRel, _tabRightRel, Model.SubdivisionTree);
                }
                if (Model.SuperclassTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSuperclass(column, _tabLeftRel, _tabRightRel, Model.SuperclassTree);
                }
                if (Model.ClassTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    _tabLeftLastRel = _tabLeftRel;
                    _tabRightLastRel = _tabRightRel;
                    CompTaxoHieras.ComposeTaxoHierClass(column, _tabLeftRel, _tabRightRel, Model.ClassTree);
                }
            });
            _tabRightRel = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ReportPdfRightTab"));
            _tabLeftRel = 0;
        });
    }

    private void ComposeHeaderChildren(IContainer container)
    {
        if (Tbl21ClassesDocumentDataSource.SubclassesCollection.Count == 0)
        {
            return;
        }

        CompHeaders.HeaderChildren(container, _tabRightRel, TextHight);
    }

    private void ComposeChildrenContent(IContainer container)
    {
        if (Tbl21ClassesDocumentDataSource.SubclassesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl21ClassesDocumentDataSource.SubclassesCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(_tabLeftLastRel + 4);
                    columns.ConstantColumn(_tabRightLastRel - 4);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text(CultRes.StringsRes.Subclass);
                table.Cell().Text($"{item.SubclassName} {item.Author} {item.AuthorYear}").WrapAnywhere();
            }
        });
    }

    private void ComposeHeaderReferences(IContainer container)
    {
        CompHeaders.HeaderHeaderReferences(container, TabLeftRel4, TextHight);
    }

    private void ComposeHeaderReferencesExperts(IContainer container)
    {
        CompHeaders.HeaderReferencesExperts(container, TabLeftRel8);
    }

    private void ComposeReferencesExperts(IContainer container)
    {
        if (Tbl21ClassesDocumentDataSource.ExpertsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl21ClassesDocumentDataSource.ExpertsCollection)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportExpert);
                        table.Cell().Text(item.Tbl90RefExperts.RefExpertName);
                    }
                    if (i == 1)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportNotes);
                        table.Cell().Text(item.Tbl90RefExperts.Notes);
                    }
                    if (i == 2)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportRefFor);
                        table.Cell().Text(item.Info);
                    }
                    if (i == 3)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                    }
                }
            }
        });
    }

    private void ComposeHeaderReferencesOtherSources(IContainer container)
    {
        CompHeaders.HeaderReferencesOtherSources(container, TabLeftRel8);
    }

    private void ComposeReferencesOtherSources(IContainer container)
    {
        if (Tbl21ClassesDocumentDataSource.SourcesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl21ClassesDocumentDataSource.SourcesCollection)
            {
                for (var i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportSource);
                        table.Cell().Text(item.Tbl90RefSources.RefSourceName);
                    }
                    if (i == 1)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportAcquired);
                        table.Cell().Text(item.Tbl90RefSources.SourceYear);
                    }
                    if (i == 2)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportNotes);
                        table.Cell().Text(item.Tbl90RefSources.Notes);
                    }
                    if (i == 3)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportRefFor);
                        table.Cell().Text(item.Info);
                    }
                    if (i == 4)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                    }
                }
            }
        });
    }

    private void ComposeHeaderReferencesPublications(IContainer container)
    {
        CompHeaders.HeaderReferencesPublications(container, TabLeftRel8);
    }

    private void ComposeReferencesPublications(IContainer container)
    {
        if (Tbl21ClassesDocumentDataSource.AuthorsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl21ClassesDocumentDataSource.AuthorsCollection)
            {
                for (var i = 0; i < 11; i++)
                {
                    if (i == 0)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportAuthor);
                        table.Cell().Text(item.Tbl90RefAuthors.RefAuthorName);
                    }
                    if (i == 1)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportPublicationDate);
                        table.Cell().Text(item.Tbl90RefAuthors.PublicationYear);
                    }
                    if (i == 2)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportArticleTitle);
                        table.Cell().Text(item.Tbl90RefAuthors.ArticelTitle);
                    }
                    if (i == 3)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportBookName);
                        table.Cell().Text(item.Tbl90RefAuthors.BookName);
                    }
                    if (i == 4)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportPages);
                        table.Cell().Text(item.Tbl90RefAuthors.Page1);
                    }
                    if (i == 5)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportPublisher);
                        table.Cell().Text(item.Tbl90RefAuthors.Publisher);
                    }
                    if (i == 6)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportPublicationPlace);
                        table.Cell().Text(item.Tbl90RefAuthors.PublicationPlace);
                    }
                    if (i == 7)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportIsbn);
                        table.Cell().Text(item.Tbl90RefAuthors.ISBN);
                    }
                    if (i == 8)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportNotes);
                        table.Cell().Text(item.Tbl90RefAuthors.Notes);
                    }
                    if (i == 9)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportRefFor);
                        table.Cell().Text(item.Info);
                    }
                    if (i == 10)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                    }
                }
            }
        });
    }

    private void ComposeHeaderComments(IContainer container)
    {
        CompHeaders.HeaderComments(container, TabLeftRel4, TextHight);
    }

    private void ComposeComments(IContainer container)
    {
        if (Tbl21ClassesDocumentDataSource.CommentsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl21ClassesDocumentDataSource.CommentsCollection)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportInfo);
                        table.Cell().Text(item.Info);
                    }
                    if (i == 1)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportMemo);
                        table.Cell().Text(item.Memo);
                    }
                    if (i == 2)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(TabLeftRel8);
                            columns.ConstantColumn(TabRightRel8);
                            columns.RelativeColumn();
                        });
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                        table.Cell().Text(" ");
                    }
                }
            }
        });
    }

}
