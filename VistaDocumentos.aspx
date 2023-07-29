<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="VistaDocumentos.aspx.cs" Inherits="RemoteKiosk.VistaDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="content" class="p-4 p-md-5 pt-5" style="overflow-x: auto; width: 100%;  overflow: auto;">
        <h2 class="mb-4">Consulta de documentos</h2>
        <p>Permite descargar los documentos que la empresa pone a disposicion de sus colaboradores</p>
        <br />
        <br />
        <asp:GridView ID="grdDocs" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdDocs_RowCommand">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="Registro No." >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" >
                <ItemStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField DataField="Archivo" HeaderText="Nombre del archivo" >
                <ItemStyle Width="40%" />
                </asp:BoundField>
                <asp:ButtonField ButtonType="Link" CommandName="Select" ControlStyle-ForeColor="Black" HeaderText="Ver documento" ItemStyle-Width="150" Text="<i class='fa fa-file-text' aria-hidden='true'></i>" >
                    <ControlStyle ForeColor="Black"></ControlStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
