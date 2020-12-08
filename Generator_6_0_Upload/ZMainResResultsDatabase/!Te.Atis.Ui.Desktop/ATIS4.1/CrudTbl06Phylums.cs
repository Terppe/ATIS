@model Atis.Domain.Models.Tbl06Phylum

       <!-- Edit Skriptdatum:  13.06.2018  12:32    -->

@{
    ViewBag.Title = Tbl06PhylumsRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl06PhylumsRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
