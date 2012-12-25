<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Modelo.EntidadBase>" %>

<h3>Operacion confirmada.</h3>

<%= Html.DisplayForModel() %>

<p>
	<a id="linkClose" href="#close">Cerrar</a>
</p>
<script type="text/javascript">
	$('#linkClose').click(function (e) {
		e.preventDefault();
		$('#popupForm').dialog('close');
	});
	updateList();
</script>
