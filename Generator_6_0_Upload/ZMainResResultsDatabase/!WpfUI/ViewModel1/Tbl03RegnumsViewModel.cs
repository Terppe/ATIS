@model Atis.Domain.Models.Tbl03Regnum    	

       <!-- Delete  Skriptdatum:  06.03.2014  12:32      -->        

@{
    ViewBag.Title = Tbl03RegnumsRes.StringsRes.Delete;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@Tbl03RegnumsRes.StringsRes.Delete</h2>

    <div>
        <p>@SharedRes.StringsRes.DeleteMessage       
  
             <i>@Model.RegnumName  ?</i>   
 
         </p>     	   
    </div>
@using (Html.BeginForm()) {
    <p>
            <input name="confirmButton" type="submit" value="@SharedRes.StringsRes.ButtonDelete" /> 
        @Html.ActionLink(SharedRes.StringsRes.ActionLnkBack, "Index")

    </p>
}

    
