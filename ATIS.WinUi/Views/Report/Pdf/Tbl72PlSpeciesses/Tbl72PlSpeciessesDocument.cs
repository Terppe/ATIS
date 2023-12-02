using System.Configuration;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ATIS.WinUi.Views.Report.Pdf.Tbl72PlSpeciesses;
public class Tbl72PlSpeciessesDocument : IDocument
{
    private Tbl72PlSpeciessesPdfModel Model
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

    public Tbl72PlSpeciessesDocument(Tbl72PlSpeciessesPdfModel model)
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
            //Taxonomy and Nomenclature
            column.Item().Element(ComposeHeaderTaxoNomen);
            column.Spacing(5);
            column.Item().Element(ComposeTaxoNomenContent);

            //Direct Synonym
            column.Item().Element(ComposeHeaderSynonym);
            column.Item().Element(ComposeSynonymContent);
            //Direct Name
            column.Item().Element(ComposeHeaderName);
            column.Item().Element(ComposeNameContent);

            //Rest Taxonomen
            column.Item().Element(ComposeTaxoNomenContentRest);
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
            //Geographics
            column.Item().Element(ComposeHeaderGeographics);
            column.Item().Element(ComposeGeographics);
            //Images
            column.Item().Element(ComposeHeaderImages);
            column.Item().Element(ComposeImages);
            //Comments
            column.Item().Element(ComposeHeaderComments);
            column.Item().Element(ComposeComments);
            //usefull information
            column.Item().Element(ComposeHeaderUsefulInfos);
            column.Item().Element(ComposeUsefulInfos);
        });
    }

    private void ComposeHeaderSynonym(IContainer container)
    {
        if (Tbl72PlSpeciessesDocumentDataSource.SynonymsCollection.Count == 0)
        {
        }
        else
        {
            CompHeaders.HeaderSynonym(container, TabLeftRel8, TabRightRel8);
        }
    }

    private void ComposeSynonymContent(IContainer container)
    {
        if (Tbl72PlSpeciessesDocumentDataSource.SynonymsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.SynonymsCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(TabLeftRel8);
                    columns.ConstantColumn(TabRightRel8);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().Text($@"{item.SynonymName} {item.Author} {item.AuthorYear}").WrapAnywhere();
            }
        });
    }

    private void ComposeHeaderName(IContainer container)
    {
        if (Tbl72PlSpeciessesDocumentDataSource.NamesCollection.Count == 0)
        {
        }
        else
        {
            CompHeaders.HeaderName(container, TabLeftRel8, TabRightRel8);
        }
    }
    private void ComposeNameContent(IContainer container)
    {
        if (Tbl72PlSpeciessesDocumentDataSource.NamesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.NamesCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(TabLeftRel8);
                    columns.ConstantColumn(TabRightRel8);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text("");
                table.Cell().Text($@"{item.NameName} [{item.Language}]").WrapAnywhere();
            }
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
                    text.Span($"{Model.GenusTree?.GenusName} {Model.PlSpeciesTree?.PlSpeciesName} {Model.PlSpeciesTree?.Subspecies}" +
                              $"{Model.PlSpeciesTree?.Divers}").FontSize(TextHight).SemiBold().Italic().WrapAnywhere();
                    text.Span($" {Model.PlSpeciesTree?.Author} {Model.PlSpeciesTree?.AuthorYear}").FontSize(TextHight).SemiBold().WrapAnywhere();
                });
                column.Item().Text(text =>
                {
                    text.Span(CultRes.StringsRes.ReportTaxonomicId);
                    text.Span($" {Model.CountId}");
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
                    table.Cell().Text(CultRes.StringsRes.PlSpecies);
                });

            });
        });
    }

    private void ComposeTaxoNomenContentRest(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
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
                    table.Cell().Text(CultRes.StringsRes.ReportTradeName);
                    table.Cell().Text($"{Model.TradeName}");
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
                    table.Cell().Text($"{Model.MemoSpecies}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportImporterWithYear);
                    table.Cell().Text($"{Model.Importer} {Model.ImportingYear}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportBasinHeight);
                    table.Cell().Text($"{Model.BasinHeight}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportPlantLength);
                    table.Cell().Text($"{Model.PlantLength}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportNotes);
                    table.Cell().Text($"{Model.MemoSpecies}");
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
                    CompTaxoHieras.ComposeTaxoHierClass(column, _tabLeftRel, _tabRightRel, Model.ClassTree);
                }
                if (Model.SubclassTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSubclass(column, _tabLeftRel, _tabRightRel, Model.SubclassTree);
                }
                if (Model.InfraclassTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierInfraclass(column, _tabLeftRel, _tabRightRel, Model.InfraclassTree);
                }
                if (Model.LegioTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierLegio(column, _tabLeftRel, _tabRightRel, Model.LegioTree);
                }
                if (Model.OrdoTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierOrdo(column, _tabLeftRel, _tabRightRel, Model.OrdoTree);
                }
                if (Model.SubordoTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSubordo(column, _tabLeftRel, _tabRightRel, Model.SubordoTree);
                }
                if (Model.InfraordoTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierInfraordo(column, _tabLeftRel, _tabRightRel, Model.InfraordoTree);
                }
                if (Model.SuperfamilyTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSuperfamily(column, _tabLeftRel, _tabRightRel, Model.SuperfamilyTree);
                }
                if (Model.FamilyTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierFamily(column, _tabLeftRel, _tabRightRel, Model.FamilyTree);
                }
                if (Model.SubfamilyTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSubfamily(column, _tabLeftRel, _tabRightRel, Model.SubfamilyTree);
                }
                if (Model.InfrafamilyTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierInfrafamily(column, _tabLeftRel, _tabRightRel, Model.InfrafamilyTree);
                }
                if (Model.SupertribusTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSupertribus(column, _tabLeftRel, _tabRightRel, Model.SupertribusTree);
                }
                if (Model.TribusTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierTribus(column, _tabLeftRel, _tabRightRel, Model.TribusTree);
                }
                if (Model.SubtribusTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierSubtribus(column, _tabLeftRel, _tabRightRel, Model.SubtribusTree);
                }
                if (Model.InfratribusTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    CompTaxoHieras.ComposeTaxoHierInfratribus(column, _tabLeftRel, _tabRightRel, Model.InfratribusTree);
                }
                if (Model.GenusTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;

                    CompTaxoHieras.ComposeTaxoHierGenus(column, _tabLeftRel, _tabRightRel, Model.GenusTree);
                }
                if (Model.PlSpeciesTree != null)
                {
                    _tabLeftRel += 4;
                    _tabRightRel -= 4;
                    _tabLeftLastRel = _tabLeftRel;
                    _tabRightLastRel = _tabRightRel;

                    CompTaxoHieras.ComposeTaxoHierPlSpecies(column, _tabLeftRel, _tabRightRel, Model.PlSpeciesTree);
                }
            });
            _tabRightRel = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ReportPdfRightTab"));
            _tabLeftRel = 0;

            // row.ConstantItem(100).Height(50).Placeholder();
        });
    }

    private void ComposeHeaderChildren(IContainer container)
    {
        //   if (Tbl69FiSpeciessesDocumentDataSource.FiSpeciessesCollection.Count == 0)
        if (string.IsNullOrEmpty(Model.PlSpeciesTree?.Subspecies))
        {
            return;
        }

        CompHeaders.HeaderChildren(container, _tabRightRel, TextHight);
    }

    private void ComposeChildrenContent(IContainer container)
    {
        if (string.IsNullOrEmpty(Model.PlSpeciesTree?.Subspecies))
        {
            return;
        }
        //subspecies wird nicht gefunden
        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.PlSpeciessesCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(_tabLeftLastRel + 4);
                    columns.ConstantColumn(_tabRightLastRel - 4);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text(CultRes.StringsRes.Subspecies);
                table.Cell().Text($"{item.Tbl66Genusses.GenusName} {item.PlSpeciesName}" +
                                  $" {item.Subspecies} {item.Divers} {item.Author} {item.AuthorYear}").WrapAnywhere();

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
        if (Tbl72PlSpeciessesDocumentDataSource.ExpertsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.ExpertsCollection)
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
        if (Tbl72PlSpeciessesDocumentDataSource.SourcesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.SourcesCollection)
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
        if (Tbl72PlSpeciessesDocumentDataSource.AuthorsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.AuthorsCollection)
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

    private void ComposeHeaderGeographics(IContainer container)
    {
        CompHeaders.HeaderGeographics(container, TabLeftRel4, TextHight);
    }
    private void ComposeGeographics(IContainer container)
    {
        if (Tbl72PlSpeciessesDocumentDataSource.GeographicsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.GeographicsCollection)
            {
                for (var i = 0; i < 10; i++)
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
                        table.Cell().Text(CultRes.StringsRes.ReportAddress);
                        table.Cell().Text(item.Address);
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
                        table.Cell().Text(CultRes.StringsRes.ReportContinent);
                        table.Cell().Text(item.Continent);
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
                        table.Cell().Text(CultRes.StringsRes.ReportCountry);
                        table.Cell().Text(item.Country);
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
                        table.Cell().Text(CultRes.StringsRes.ReportHttp);
                        table.Cell().Text(item.Http);
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
                        table.Cell().Text(CultRes.StringsRes.ReportLatitudeLongitude);
                        table.Cell().Text($"{item.Latitude}  //  {item.Longitude}");
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
                        table.Cell().Text("");
                        table.Cell().Text($"{item.Latitude1}  //  {item.Longitude1}");
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
                        table.Cell().Text("");
                        table.Cell().Text($"{item.Latitude2}  //  {item.Longitude2}");
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
                        table.Cell().Text("");
                        table.Cell().Text($"{item.Latitude3}  //  {item.Longitude3}");
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
                        table.Cell().Text(CultRes.StringsRes.ReportMemo);
                        table.Cell().Text(item.Memo);
                    }
                    if (i == 9)
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

    private void ComposeHeaderImages(IContainer container)
    {
        CompHeaders.HeaderImages(container, TabLeftRel4, TextHight);
    }

    private void ComposeImages(IContainer container)
    {
        if (Tbl72PlSpeciessesDocumentDataSource.ImagesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.ImagesCollection)
            {
                for (var i = 0; i < 6; i++)
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
                        table.Cell().Text("");
                        //       table.Cell().Text(Placeholders.LoremIpsum());
                        //       table.Cell().Image(Placeholders.Image(200, 100));
                        table.Cell().Image(item.Filestream).FitArea();
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
                        table.Cell().Text(CultRes.StringsRes.ReportImageShot);
                        table.Cell().Text(item.ShotDate.ToString());
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
                        table.Cell().Text(CultRes.StringsRes.ReportImageMimeType);
                        table.Cell().Text(item.ImageMimeType);
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
                        table.Cell().Text(CultRes.StringsRes.ReportInfo);
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
                        table.Cell().Text("");
                        table.Cell().Text(CultRes.StringsRes.ReportNotes);
                        table.Cell().Text(item.Memo);
                    }
                    if (i == 5)
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
        if (Tbl72PlSpeciessesDocumentDataSource.CommentsCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in Tbl72PlSpeciessesDocumentDataSource.CommentsCollection)
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
    private void ComposeHeaderUsefulInfos(IContainer container)
    {
        CompHeaders.HeaderUsefulInfos(container, TabLeftRel4, TextHight);
    }

    private void ComposeUsefulInfos(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportTechnic).SemiBold();
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
                    table.Cell().Text(CultRes.StringsRes.ReportBasinHeight);
                    table.Cell().Text($"{Model.BasinHeight}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportPlantLength);
                    table.Cell().Text($"{Model.PlantLength}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportPh12);
                    table.Cell().Text($"{Model.Ph1}/{Model.Ph2}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportTemp12);
                    table.Cell().Text($"{Model.Temp1}/{Model.Temp2}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportHardness12);
                    table.Cell().Text($"{Model.Hardness1}/{Model.Hardness2}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportCarboHardness12);
                    table.Cell().Text($"{Model.CarboHardness1}/{Model.CarboHardness2}");
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
                    table.Cell().Text(CultRes.StringsRes.ReportMemoTechnic);
                    table.Cell().Text($"{Model.MemoTech}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportPublications).SemiBold();
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
                    table.Cell().Text($"{Model.MemoReproduction}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportCulture).SemiBold();
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
                    table.Cell().Text($"{Model.MemoColor}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportGlobal).SemiBold();
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
                    table.Cell().Text($"{Model.MemoColor}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportCulture).SemiBold();
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
                    table.Cell().Text($"{Model.MemoCulture}");
                });
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(TabLeftRel8);
                        columns.RelativeColumn();
                    });
                    table.Cell().Text("");
                    table.Cell().Text(CultRes.StringsRes.ReportGlobal).SemiBold();
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
                    table.Cell().Text($"{Model.MemoGlobal}");
                });
            });
        });
    }

}
