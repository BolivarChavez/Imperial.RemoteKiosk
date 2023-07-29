<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="RemoteKiosk.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5">
        <h2 class="mb-4">Mantenimiento de informacion de usuarios autorizados</h2>
        <p>Seleccione el colaborador de la lista y luego edite la informacion y presione GRABAR USUARIO</p>
        <br />

        <table style="width:100%">
            <tr>
                <td style="width:15%">Seleccionar colaborador</td> 
                <td style="width:85%"><asp:DropDownList ID="cboEmpleado" runat="server" Width="600px" AutoPostBack="True" OnSelectedIndexChanged="cboEmpleado_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width:15%">Codigo de usuario</td> 
                <td style="width:85%"><asp:Label ID="lblCodigo" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width:15%">Nombre completo</td> 
                <td style="width:85%"><asp:TextBox ID="txtNombre" runat="server" Width="600px" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Nombre de usuario</td> 
                <td style="width:85%"><asp:TextBox ID="txtUsuario" runat="server" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Contraseña</td> 
                <td style="width:85%"><asp:TextBox ID="txtPassword1" runat="server" Width="300px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Confirmar contraseña</td> 
                <td style="width:85%"><asp:TextBox ID="txtPassword2" runat="server" Width="300px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%"> </td> 
                <td style="width:85%"><asp:Label ID="lblContacto" runat="server" Text="Label" Visible="False"></asp:Label></td>
            </tr>
        </table> 
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%"><button type="submit" runat="server" id="Buscar" style="width:100%" onserverclick="Buscar_ServerClick">GRABAR USUARIO</button></td>
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
