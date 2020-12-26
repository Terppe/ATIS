@model Atis.Domain.Models.Tbl51Infrafamily

       <!-- Edit Skriptdatum:  19.06.2018  10:32    -->

@{
    ViewBag.Title = Tbl51InfrafamiliesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl51InfrafamiliesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
