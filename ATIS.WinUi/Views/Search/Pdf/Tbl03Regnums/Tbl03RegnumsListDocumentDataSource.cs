﻿using System.Collections.ObjectModel;
using ATIS.WinUi.Models;
using ATIS.WinUi.Views.Report.Pdf.Tbl03Regnums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ATIS.WinUi.Views.Search.Pdf.Tbl03Regnums;
public class Tbl03RegnumsListDocumentDataSource : ObservableObject
{
    private static readonly Tbl03Regnum RegnumSingle = new();

    public static Tbl03RegnumsPdfModel GetRegnumListDetails(ObservableCollection<Tbl03Regnum> regnumsCollection)
    {

        RegnumsCollection = regnumsCollection;

        return new Tbl03RegnumsPdfModel
        {
            RegnumName = RegnumSingle.RegnumName,
            Subregnum = RegnumSingle.Subregnum,
            CountId = RegnumSingle.CountId,
            Synonym = RegnumSingle.Synonym,
            Author = RegnumSingle.Author,
            AuthorYear = RegnumSingle.AuthorYear,
            Info = RegnumSingle.Info,
            Valid = RegnumSingle.Valid,
            ValidYear = RegnumSingle.ValidYear,
            EngName = RegnumSingle.EngName,
            GerName = RegnumSingle.GerName,
            FraName = RegnumSingle.FraName,
            PorName = RegnumSingle.PorName,
            Memo = RegnumSingle.Memo,
            WriterDate = RegnumSingle.WriterDate,
            UpdaterDate = RegnumSingle.UpdaterDate,
        };
    }

    #region "Collections"

    public static ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; } = null!;
    #endregion "Collections"

}
