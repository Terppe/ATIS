@model Atis.Domain.Models.Tbl24Subclass

       <!-- Edit Skriptdatum:  15.06.2018  18:32    -->

@{
    ViewBag.Title = Tbl24SubclassesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl24SubclassesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
