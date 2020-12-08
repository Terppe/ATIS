@model Atis.Domain.Models.Tbl90RefExpert

       <!-- Edit Skriptdatum:  30.07.2018  10:32    -->

@{
    ViewBag.Title = Tbl90RefExpertsRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl90RefExpertsRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
