@model Atis.Domain.Models.Tbl81Image

       <!-- Edit Skriptdatum:  12.07.2018  10:32    -->

@{
    ViewBag.Title = Tbl81ImagesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl81ImagesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
