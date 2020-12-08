@model Atis.Domain.Models.Tbl78Name

       <!-- Edit Skriptdatum:  02.07.2018  10:32    -->

@{
    ViewBag.Title = Tbl78NamesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl78NamesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
