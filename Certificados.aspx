<%@ Page Title="" Language="C#" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="Certificados.aspx.cs" Inherits="RemoteKiosk.Certificados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Solicitud de certificado laboral</h2>
        <p>Ingrese la informacion solicitada y luego presione el boton PROCESAR CERTIFICADO</p>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%">Fecha de impresion</td> 
                <td style="width:85%">
                    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:15%">Ciudad de origen</td> 
                <td style="width:85%"><asp:TextBox ID="txtOrigen" runat="server" Width="600px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Destinatario</td> 
                <td style="width:85%"><asp:TextBox ID="txtDestino" runat="server" Width="600px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Ciudad</td> 
                <td style="width:85%"><asp:TextBox ID="txtCiudad" runat="server" Width="600px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%"><button type="submit" runat="server" id="Buscar" style="width:100%" onserverclick="Buscar_ServerClick">Procesar Certificado</button></td>
                <td style="width:15%"><button type="submit" runat="server" id="Revisar" style="width:100%" onserverclick="Revisar_ServerClick">Ver Certificado</button></td>
                <td style="width:70%"></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
