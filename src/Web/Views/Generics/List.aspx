<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Modelo.EntidadBase>>" %>
<table>
    <tr>
        <th>Descripción</th>
        <th>Comandos</th>
    </tr>
    <% foreach (var item in Model) { %>
	<tr>
		<td><%= item.ToString() %></td>
		<td item-id="<%= item.Id %>">
			<a class="linkEdit" href="#edit">Editar</a> | 
			<a class="linkDetails" href="#details">Detalles</a> | 
			<a class="linkDelete" href="#delete">Borrar</a>
		</td>
	</tr>
	<% } %>
</table>