﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Object>" %>
<% if (Model == null) { %>
    <%= ViewData.ModelMetadata.NullDisplayText %>
<% } else if (ViewData.TemplateInfo.TemplateDepth > 1) { %>
    <%= ViewData.ModelMetadata.SimpleDisplayText %>
<% } else { %>
	<fieldset>
		<legend><%= ViewData.ModelMetadata.GetDisplayName() %></legend>
        <% foreach (var prop in ViewData.ModelMetadata.Properties.Where(pm => pm.ShowForDisplay
                             && !ViewData.TemplateInfo.Visited(pm))) { %>
            <% if (prop.HideSurroundingHtml) { %>
                <%= Html.Display(prop.PropertyName) %>
            <% } else if (prop.Model != null) { %>
                <% if (!String.IsNullOrEmpty(prop.GetDisplayName())) { %>
                    <div class="display-label"><%= prop.GetDisplayName() %></div>
                <% } %>
                <div class="display-field"><%= Html.Display(prop.PropertyName) %></div>
            <% } %>
        <% } %>
	</fieldset>
<% } %>
