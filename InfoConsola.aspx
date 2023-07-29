<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="InfoConsola.aspx.cs" Inherits="RemoteKiosk.InfoConsola" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Kiosko de informacion de colaboradores</h2>
        <p> </p>

    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
