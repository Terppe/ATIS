@model Atis.Domain.Models.Tbl30Legio

       <!-- Edit Skriptdatum:  21.12.2017  10:32    -->

@{
    ViewBag.Title = Tbl30LegiosRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl30LegiosRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
