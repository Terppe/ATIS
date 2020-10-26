@model Atis.Domain.Models.TblUserProfile

       <!-- Edit Skriptdatum:   31.07.2018  10:32    -->

@{
    ViewBag.Title = TblUserProfilesRes.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@TblUserProfilesRes.StringsRes.Edit</h2>

@Html.Partial("_Form");

    
