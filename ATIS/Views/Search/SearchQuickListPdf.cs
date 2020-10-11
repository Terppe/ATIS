using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Report.PDF;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Win32;

namespace ATIS.Ui.Views.Search
{
    public class SearchQuickListPdf : ViewModelBase
    {
        public SearchQuickListPdf()
        {
            
        }

        public static void CreateMainAllPdf(string filtertext)
        {
            var searchVm = new SearchQuickViewModel(filtertext);
            searchVm.GetListAllByFilterText(filtertext);

            var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
            var saveResult = sfd.ShowDialog();
            if (saveResult != true) return;  //exit
            Document doc = null;

            //try
            //{
            //    doc = PdfHelper.HeaderMainPdf(sfd);
            //    // Add Standard Page Size
            //    PdfHelper.SetStandardPageSize(doc);
            //    // Add pages to the document
            //    PdfHelper.AddReportListMain(doc);

            //    PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, StandardFont,
            //        new Chunk("Results of search with common name : " + filtertext));
            //    PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, StandardFont, new Chunk("\n "));

            //    if (searchVm.RegnumsCollection.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Regnum));
            //        doc = AddTbl03RegnumsList(doc, searchVm.RegnumsCollection);
            //    }
            //    if (searchVm.PhylumsCollection.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Phylum));
            //        doc = AddTbl06PhylumsList(doc, searchVm.PhylumsCollection);
            //    }
            //    if (searchVm.SubphylumsCollection.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Subphylum));
            //        doc = AddTbl12SubphylumsList(doc, searchVm.SubphylumsCollection);
            //    }
            //    if (searchVm.DivisionsCollection.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Division));
            //        doc = AddTbl09DivisionsList(doc, searchVm.DivisionsCollection);
            //    }
            //    if (searchVm.Tbl15SubdivisionsList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Subdivision));
            //        doc = AddTbl15SubdivisionsList(doc, searchVm.Tbl15SubdivisionsList);
            //    }
            //    if (searchVm.Tbl18SuperclassesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Superclass));
            //        doc = AddTbl18SuperclassesList(doc, searchVm.Tbl18SuperclassesList);
            //    }
            //    if (searchVm.Tbl21ClassesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Class));
            //        doc = AddTbl21ClassesList(doc, searchVm.Tbl21ClassesList);
            //    }
            //    if (searchVm.Tbl24SubclassesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Subclass));
            //        doc = AddTbl24SubclassesList(doc, searchVm.Tbl24SubclassesList);
            //    }
            //    if (searchVm.Tbl27InfraclassesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Infraclass));
            //        doc = AddTbl27InfraclassesList(doc, searchVm.Tbl27InfraclassesList);
            //    }
            //    if (searchVm.Tbl30LegiosList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Legio));
            //        doc = AddTbl30LegiosList(doc, searchVm.Tbl30LegiosList);
            //    }
            //    if (searchVm.Tbl33OrdosList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Ordo));
            //        doc = AddTbl33OrdosList(doc, searchVm.Tbl33OrdosList);
            //    }
            //    if (searchVm.Tbl36SubordosList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Subordo));
            //        doc = AddTbl36SubordosList(doc, searchVm.Tbl36SubordosList);
            //    }
            //    if (searchVm.Tbl39InfraordosList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Infraordo));
            //        doc = AddTbl39InfraordosList(doc, searchVm.Tbl39InfraordosList);
            //    }
            //    if (searchVm.Tbl42SuperfamiliesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Superfamily));
            //        doc = AddTbl42SuperfamiliesList(doc, searchVm.Tbl42SuperfamiliesList);
            //    }
            //    if (searchVm.Tbl45FamiliesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Family));
            //        doc = AddTbl45FamiliesList(doc, searchVm.Tbl45FamiliesList);
            //    }
            //    if (searchVm.Tbl48SubfamiliesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Subfamily));
            //        doc = AddTbl48SubfamiliesList(doc, searchVm.Tbl48SubfamiliesList);
            //    }
            //    if (searchVm.Tbl51InfrafamiliesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Infrafamily));
            //        doc = AddTbl51InfrafamiliesList(doc, searchVm.Tbl51InfrafamiliesList);
            //    }
            //    if (searchVm.Tbl54SupertribussesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Supertribus));
            //        doc = AddTbl54SupertribussesList(doc, searchVm.Tbl54SupertribussesList);
            //    }
            //    if (searchVm.Tbl57TribussesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Tribus));
            //        doc = AddTbl57TribussesList(doc, searchVm.Tbl57TribussesList);
            //    }
            //    if (searchVm.Tbl60SubtribussesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Subtribus));
            //        doc = AddTbl60SubtribussesList(doc, searchVm.Tbl60SubtribussesList);
            //    }
            //    if (searchVm.Tbl63InfratribussesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Infratribus));
            //        doc = AddTbl63InfratribussesList(doc, searchVm.Tbl63InfratribussesList);
            //    }
            //    if (searchVm.Tbl66GenussesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Genus));
            //        doc = AddTbl66GenussesList(doc, searchVm.Tbl66GenussesList);
            //    }
            //    if (searchVm.Tbl69FiSpeciessesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.FiSpecies));
            //        doc = AddTbl69FiSpeciessesList(doc, searchVm.Tbl69FiSpeciessesList);
            //    }
            //    if (searchVm.Tbl72PlSpeciessesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.PlSpecies));
            //        doc = AddTbl72PlSpeciessesList(doc, searchVm.Tbl72PlSpeciessesList);
            //    }
            //    if (searchVm.Tbl78NamesList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Name));
            //        doc = AddTbl78NamesList(doc, searchVm.Tbl78NamesList);
            //    }
            //    if (searchVm.Tbl84SynonymsList.Count != 0)
            //    {
            //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, SmallBoldFont, new Chunk(CultRes.StringsRes.Synonym));
            //        doc = AddTbl84SynonymsList(doc, searchVm.Tbl84SynonymsList);
            //    }
            //}
            //catch (DocumentException)
            //{
            //    // Handle iTextSharp errors
            //}
            //finally
            //{
            //    // Clean up
            //    doc?.Close();
            //    doc = null;
            //}

        }
    }
}
