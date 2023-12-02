using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl54Supertribusses;
using ATIS.WinUi.Views.Report.Pdf.Tbl66Genusses;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl66Genusses;
internal class Tbl66GenussesListDocumentDataSource : ObservableObject
{
    private static readonly Tbl66Genus GenusSingle = new();

    public static Tbl66GenussesPdfModel GetGenusListDetails(ObservableCollection<Tbl66Genus> genussesCollection)
    {

        GenussesCollection = genussesCollection;

        return new Tbl66GenussesPdfModel
        {
            GenusName = GenusSingle.GenusName,
            CountId = GenusSingle.CountId,
            Synonym = GenusSingle.Synonym,
            Author = GenusSingle.Author,
            AuthorYear = GenusSingle.AuthorYear,
            Info = GenusSingle.Info,
            Valid = GenusSingle.Valid,
            ValidYear = GenusSingle.ValidYear,
            EngName = GenusSingle.EngName,
            GerName = GenusSingle.GerName,
            FraName = GenusSingle.FraName,
            PorName = GenusSingle.PorName,
            Memo = GenusSingle.Memo,
            WriterDate = GenusSingle.WriterDate,
            UpdaterDate = GenusSingle.UpdaterDate,
        };
    }

    #region "Collections"

    public static ObservableCollection<Tbl66Genus> GenussesCollection { get; set; } = null!;
    #endregion "Collections"

}
