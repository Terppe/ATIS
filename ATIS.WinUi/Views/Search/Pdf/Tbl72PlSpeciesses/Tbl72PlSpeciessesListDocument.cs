﻿using System.Collections.ObjectModel;
using System.Globalization;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl72PlSpeciesses;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl72PlSpeciesses;
internal class Tbl72PlSpeciessesListDocument : IDocument
{
    public Tbl72PlSpeciessesPdfModel Model
    {
        get;
    }
    public ObservableCollection<Tbl72PlSpecies> ViewModelPlSpeciessesCollection
    {
        get;
    }
    public Tbl72PlSpeciessesListDocument(Tbl72PlSpeciessesPdfModel model, ObservableCollection<Tbl72PlSpecies> viewModelPlSpeciessesCollection)
    {
        Model = model;
        ViewModelPlSpeciessesCollection = viewModelPlSpeciessesCollection;
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
                text.Span(CultRes.StringsRes.PlSpecies).FontSize(20).SemiBold();
                text.Span("                                                                                                       ");
                text.Span(DateTime.Now.ToString(CultureInfo.InstalledUICulture)).NormalPosition();
            });
            column.Spacing(5);
            column.Item().Element(ComposeListContent);
        });
    }

    private void ComposeListContent(IContainer container)
    {
        if (ViewModelPlSpeciessesCollection.Count == 0)
        {
            return;
        }

        container.Table(table =>
        {
            foreach (var item in ViewModelPlSpeciessesCollection)
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(4);
                    columns.RelativeColumn();
                });
                table.Cell().Text("");
                table.Cell().Text($"{item.Tbl66Genusses!.GenusName} {item.PlSpeciesName} {item.Subspecies} {item.Divers}" +
                                  $" {item.Author} {item.AuthorYear}").WrapAnywhere();
            }
        });
    }

}
