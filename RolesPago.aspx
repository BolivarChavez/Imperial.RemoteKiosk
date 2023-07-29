<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="RolesPago.aspx.cs" Inherits="RemoteKiosk.RolesPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5" style="overflow-x: auto; width: 100%;  overflow: auto;">
        <h2 class="mb-4">Consulta de roles de pago</h2>
        <p>Ingresar el año, seleccionar el mes y luego presionar el boton BUSCAR PERIODOS.</p>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%">Seleccione el año</td> 
                <td style="width:85%"><asp:TextBox ID="TxtAnio" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:15%">Seleccione el mes</td> 
                <td style="width:85%"><asp:DropDownList ID="cboMes" runat="server" Width="200px">
                    <asp:ListItem Value="0">Todos los meses</asp:ListItem>
                    <asp:ListItem Value="1">Enero</asp:ListItem>
                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                    <asp:ListItem Value="4">Abril</asp:ListItem>
                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                    <asp:ListItem Value="6">Junio</asp:ListItem>
                    <asp:ListItem Value="7">Julio</asp:ListItem>
                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%"><button type="submit" runat="server" id="Buscar" style="width:100%" onserverclick="Buscar_ServerClick">Buscar periodos</button></td>
                <td style="width:15%"><button type="submit" runat="server" id="Consultar" style="width:100%" onserverclick="Consultar_ServerClick">Consultar rol</button></td>
                <td style="width:70%"></td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView ID="grdRoles" runat="server" AutoGenerateColumns="False" Width="60%" AutoGenerateSelectButton="True">
            <Columns>
                <asp:BoundField DataField="DescMes" HeaderText="Mes">
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="pr_rol" HeaderText="Rol de pago">
                <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="pr_periodo" HeaderText="Periodo">
                <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="pr_desde" HeaderText="Desde">
                <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="pr_hasta" HeaderText="Hasta">
                <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField DataField="pr_mes" HeaderText="Id_Mes" Visible="False">
                <ItemStyle Width="5%" />
                </asp:BoundField>
            </Columns>
            <SelectedRowStyle BackColor="#FF9999" />
        </asp:GridView>

    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
