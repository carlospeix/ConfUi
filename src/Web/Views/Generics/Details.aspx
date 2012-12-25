<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Modelo.EntidadBase>" %>

<%= Html.DisplayForModel() %>
<p>
	<a id="linkEdit" item-id="<%= Model.Id %>" href="#edit">Editar</a>
	<a id="linkClose" href="#close">Cerrar</a>
</p>
<script type="text/javascript">
	$('#linkClose').click(function (e) {
		e.preventDefault();
		$('#popupForm').dialog('close');
	});

	$('#linkEdit').click(function (e) {
		e.preventDefault();
		var id = $(this).attr('item-id');
		if (id == undefined)
			return;
		$('#popupForm').load('<%= Url.Action("Edit") %>?id=' + id);
	});
</script>
