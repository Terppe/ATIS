@model Atis.Domain.Models.TblCountry

       <!-- Edit Skriptdatum:   31.07.2018 12:32      -->

@{
    ViewBag.Title = TblCountriesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@TblCountriesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
