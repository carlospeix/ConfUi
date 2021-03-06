﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Listado de entidades
</asp:Content>

<asp:Content ID="subTitle" ContentPlaceHolderID="SubTitleContent" runat="server">
    Listado
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
	    <a id="linkCreate" href="#create">Nuevo</a>
    </p>

    <div id="list">
    </div>

    <div id="popupForm" style="display:none;">
    </div>

    <script type="text/javascript">
	    $(document).ready(function () {
		    $('#popupForm').dialog({
			    title: 'Item form',
			    closeText: 'Cerrar',
			    autoOpen: false,
			    modal: true
		    });

		    $('#linkCreate').click(function (e) {
			    e.preventDefault();
			    openModalForm('<%= Url.Action("Create") %>');
		    });

		    updateList();
	    });

	    function updateList() {
		    $('#list').html('<p>Por favor espere...</p>');
		    $('#list').load('<%= Url.Action("List") %>', wireListeners);
	    }

	    function wireListeners() {
		    $(".linkEdit").each(function () {
			    $(this).click(function (e) {
				    e.preventDefault();
				    var id = $(this).parent().attr('item-id');
				    if (id == undefined)
					    return;
				    openModalForm('<%= Url.Action("Edit") %>?id=' + id);
			    });
		    });

		    $(".linkDetails").each(function () {
			    $(this).click(function (e) {
				    e.preventDefault();
				    var id = $(this).parent().attr('item-id');
				    if (id == undefined)
					    return;
				    openModalForm('<%= Url.Action("Details") %>?id=' + id);
			    });
		    });

		    $(".linkDelete").each(function () {
			    $(this).click(function (e) {
				    e.preventDefault();
				    var id = $(this).parent().attr('item-id');
				    if (id == undefined)
					    return;
				    openModalForm('<%= Url.Action("Delete") %>?id=' + id);
			    });
		    });
	    }

	    function openModalForm(loadUrl) {
		    $('#popupForm').html('<p>Por favor espere...</p>');
		    $('#popupForm').dialog('open').load(loadUrl);
	    }

	    function wireAjaxForm() {
		    $('form').submit(function (e) {
			    e.preventDefault();
			    $.post(this.action, $(this).serialize(), function (data) {
				    $('#popupForm').html(data);
			    });
		    });
	    }
    </script>
</asp:Content>
