@model Atis.Domain.Models.Tbl21Class

       <!-- Edit Skriptdatum:  19.06.2018  18:32    -->

@{
    ViewBag.Title = Tbl21ClassesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl21ClassesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
