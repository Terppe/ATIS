@model Atis.Domain.Models.Tbl93Comment

       <!-- Edit Skriptdatum:  30.07.2018  10:32    -->

@{
    ViewBag.Title = Tbl93CommentsRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl93CommentsRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
