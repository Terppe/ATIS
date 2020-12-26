@model Atis.Domain.Models.Tbl33Ordo

       <!-- Edit Skriptdatum:  19.06.2018  10:32    -->

@{
    ViewBag.Title = Tbl33OrdosRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl33OrdosRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
