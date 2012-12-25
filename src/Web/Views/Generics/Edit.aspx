<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Modelo.EntidadBase>" %>

<% using (Html.BeginForm()) {%>
    <%= Html.ValidationSummary(false, "Por favor, revise los siguientes errores:") %>
	<%= Html.EditorForModel() %>
	<div>
		<input id="submitButton" type="submit" value="Grabar" />
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