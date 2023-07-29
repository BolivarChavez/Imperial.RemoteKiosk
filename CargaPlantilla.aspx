<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CargaPlantilla.aspx.cs" Inherits="RemoteKiosk.CargaPlantilla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Carga de plantillas de informacion de colaboradores y roles de pago</h2>
        <p>Seleccione la ruta del archivo respectiva para la carga y luego presionar ACTUALIZAR INFORMACION</p>
        <br />

        <table style="width:100%">
            <tr>
                <td style="width:25%">Carga archivo de informacion de colaboradores</td> 
                <td style="width:75%"><asp:FileUpload ID="FileUpload1" runat="server" Width="600px" /></td>
            </tr>
            <tr>
                <td style="width:25%">Carga archivo de informacion de roles de pago</td> 
                <td style="width:75%"><asp:FileUpload ID="FileUpload2" runat="server" Width="600px" /></td>
            </tr>
        </table>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%"><button type="submit" runat="server" id="Buscar" style="width:100%" onserverclick="Buscar_ServerClick">Actualizar Informacion</button></td>
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
