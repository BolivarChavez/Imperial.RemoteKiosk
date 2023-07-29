<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminConsola.aspx.cs" Inherits="RemoteKiosk.AdminConsola" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Consola de administracion del Kiosko de informacion de colaboradores</h2>
        <p> </p>

    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
