@model Atis.Domain.Models.Tbl90RefAuthor

       <!-- Edit Skriptdatum:  14.11.2017  10:32    -->

@{
    ViewBag.Title = Tbl90RefAuthorsRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl90RefAuthorsRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
