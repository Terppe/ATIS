<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Atis.Domain.Entities.Tbl06Phylum>" %>

    <!-- Summary  Skriptdatum:  12.03.2014  12:32    -->

        <tr > 
            <td><%: Html.ActionLink(Model.PhylumName, "Edit",  new { Model.PhylumID }) %></td>
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
                <% using (Html.BeginForm("Details", "Tbl06Phylums")) { %>
                    <%: Html.Hidden("PhylumID", Model.PhylumID) %>
                    <input type="submit" value="Details" />
                <% } %>
            </td>
            <td>
                <% using (Html.BeginForm("List", "Tbl12Subphylums")) { %>
                    <input type="submit" value="Subphylums" />
                <% } %>
            </td>
            <td><%: Html.ActionLink(Tbl06PhylumsRes.StringsRes.ActionLnkDelete, "Delete", new { Model.PhylumID })%> </td>
        </tr>
    
