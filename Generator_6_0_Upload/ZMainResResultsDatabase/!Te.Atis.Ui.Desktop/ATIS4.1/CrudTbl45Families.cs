@model Atis.Domain.Models.Tbl45Family

       <!-- Edit Skriptdatum:  19.06.2018  10:32    -->

@{
    ViewBag.Title = Tbl45FamiliesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl45FamiliesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
