<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CargaDocumento.aspx.cs" Inherits="RemoteKiosk.CargaDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />

    <div id="content" class="p-4 p-md-5 pt-5" style="overflow-x: auto; width: 100%;  overflow: auto;">
        <h2 class="mb-4">Carga de documentos para consultas</h2>
        <p>Ingresar la informacion del documento que se va a subir y luego proceder a cargar el mismo</p>
        <br />
        <table style="width:100%">
            <tr>
                <td style="width:15%"><button type="submit" runat="server" id="AddDoc" style="width:100%" onserverclick="AddDoc_ServerClick">Agregar Documento</button></td>
                <td style="width:15%"><button type="submit" runat="server" id="DelDoc" style="width:100%" onserverclick="DelDoc_ServerClick">Eliminar Documento</button></td>
                <td style="width:70%"></td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView ID="grdDocs" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="Registro No." >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Descripcion">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Ingrese descripcion" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Archivo">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Upload">
                    <ItemTemplate>
                        <asp:FileUpload ID="fuUpload" runat="server" />
                        <asp:HiddenField ID="hfFileByte" runat="server" />
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <FooterTemplate>
                        <asp:Button ID="ButtonAdd" runat="server" Text="Agregar documento" OnClick="ButtonAdd_Click"/>
                    </FooterTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
