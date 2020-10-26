<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Atis.Domain.Entities.Tbl03Regnum>" %>

    <!-- Summary  Skriptdatum:  12.03.2014  12:32      -->

        <tr > 
            <td><%: Html.ActionLink(Model.RegnumName, "Edit",  new { Model.RegnumID }) %></td>
            <td><%: Model.Subregnum %></td>
            <td><%: Html.CheckBoxFor(m => m.Valid) %></td>
            <td><%: Model.ValidYear %></td>
            <td><%: Model.Synonym %></td>
            <td><%: Model.Author %></td>
            <td><%: Model.AuthorYear %></td>   
     
            <td>
                <% using (Html.BeginForm("Details", "Tbl03Regnums")) { %>
                    <%: Html.Hidden("RegnumID", Model.RegnumID) %>
                    <input type="submit" value="Details" />
                <% } %>
            </td>
            <td>
                <% using (Html.BeginForm("List", "Tbl06Phylums")) { %>
                    <input type="submit" value="Phylums" />
                <% } %>
            </td>
            <td><%: Html.ActionLink(Tbl03RegnumsRes.StringsRes.ActionLnkDelete, "Delete", new { Model.RegnumID })%> </td>
        </tr>
    
