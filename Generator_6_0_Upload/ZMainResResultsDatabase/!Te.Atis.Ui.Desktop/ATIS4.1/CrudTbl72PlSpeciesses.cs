@model Atis.Domain.Models.Tbl72PlSpecies

       <!-- Edit Skriptdatum:  30.06.2018  12:32    -->

@{
    ViewBag.Title = Tbl72PlSpeciessesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl72PlSpeciessesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
