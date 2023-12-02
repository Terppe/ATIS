using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl30Legios;
using ATIS.WinUi.Views.Report.Pdf.Tbl45Families;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl45Families;
internal class Tbl45FamiliesListDocumentDataSource : ObservableObject
{
    private static readonly Tbl45Family FamilySingle = new();

    public static Tbl45FamiliesPdfModel GetFamilyListDetails(ObservableCollection<Tbl45Family> familiesCollection)
    {

        FamiliesCollection = familiesCollection;

        return new Tbl45FamiliesPdfModel
        {
            FamilyName = FamilySingle.FamilyName,
            CountId = FamilySingle.CountId,
            Synonym = FamilySingle.Synonym,
            Author = FamilySingle.Author,
            AuthorYear = FamilySingle.AuthorYear,
            Info = FamilySingle.Info,
            Valid = FamilySingle.Valid,
            ValidYear = FamilySingle.ValidYear,
            EngName = FamilySingle.EngName,
            GerName = FamilySingle.GerName,
            FraName = FamilySingle.FraName,
            PorName = FamilySingle.PorName,
            Memo = FamilySingle.Memo,
            WriterDate = FamilySingle.WriterDate,
            UpdaterDate = FamilySingle.UpdaterDate,
        };
    }

    #region "Collections"

    public static ObservableCollection<Tbl45Family> FamiliesCollection { get; set; } = null!;
    #endregion "Collections"

}
