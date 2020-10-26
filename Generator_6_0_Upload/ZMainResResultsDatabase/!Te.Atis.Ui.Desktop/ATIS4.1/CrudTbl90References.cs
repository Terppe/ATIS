@model Atis.Domain.Models.Tbl90Reference

       <!-- Edit Skriptdatum:  21.07.2018  10:32    -->

@{
    ViewBag.Title = Tbl90ReferencesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl90ReferencesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
