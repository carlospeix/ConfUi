﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Modelo.EntidadBase>" %>

<h3>¿Desea eliminar esta información?</h3>

<% using (Html.BeginForm()) { %>
    <%= Html.DisplayForModel() %>
    <div>
        <input type="submit" value="Eliminar" />
		<a id="linkClose" href="#close">Cerrar</a>
    </div>
<% } %>
<script type="text/javascript">
	$('#linkClose').click(function (e) {
		e.preventDefault();
		$('#popupForm').dialog('close');
	});
	wireAjaxForm();
</script>
