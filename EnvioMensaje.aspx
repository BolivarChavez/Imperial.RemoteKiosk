<%@ Page Title="" Language="C#" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="EnvioMensaje.aspx.cs" Inherits="RemoteKiosk.EnvioMensaje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Envio de mensaje</h2>
        <p>Llene los campos que se muestran, luego presione el boton ENVIAR MENSAJE</p>
        <br />

        <table style="width:100%">
            <tr>
                <td style="width:15%">Nombre completo</td> 
                <td style="width:85%"><asp:TextBox ID="txtNombre" runat="server" Width="600px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Correo electronico</td> 
                <td style="width:85%"><asp:TextBox ID="txtEmail" runat="server" Width="600px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Mensaje</td> 
                <td style="width:85%"><asp:TextBox ID="txtMensaje" runat="server" Width="600px" Rows="15" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%"><button type="submit" runat="server" id="Buscar" style="width:100%" onserverclick="Buscar_ServerClick">Enviar Mensaje</button></td>
                <td style="width:85%"></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
