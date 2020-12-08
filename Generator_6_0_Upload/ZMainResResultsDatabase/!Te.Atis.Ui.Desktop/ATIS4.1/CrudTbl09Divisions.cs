@model Atis.Domain.Models.Tbl09Division

       <!-- Edit Skriptdatum:  13.06.2018  12:32    -->

@{
    ViewBag.Title = Tbl09DivisionsRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl09DivisionsRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
