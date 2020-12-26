@model Atis.Domain.Models.Tbl42Superfamily

       <!-- Edit Skriptdatum:  19.06.2018  10:32    -->

@{
    ViewBag.Title = Tbl42SuperfamiliesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl42SuperfamiliesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
