<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="InfoRol.aspx.cs" Inherits="RemoteKiosk.InfoRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Detalle de informacion de rol de pago</h2>
        <p>Informacion General</p>
        <br />
        <div id="Cabecera" runat="server">
        </div>
        <br />

        <p>Detalle de rol de pago</p>
        <br />
        <div id="Detalle" runat="server">
        </div>
        <br />
        <button type="button" runat="server" id="Buscar" style="width:15%" >Imprimir rol de pago</button>
    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
