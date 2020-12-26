@model Atis.Domain.Models.Tbl15Subdivision

       <!-- Edit Skriptdatum:  14.06.2018  12:32    -->

@{
    ViewBag.Title = Tbl15SubdivisionsRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl15SubdivisionsRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
