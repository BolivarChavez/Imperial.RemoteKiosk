<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Opciones.Master" AutoEventWireup="true" CodeBehind="InfoRol.aspx.cs" Inherits="RemoteKiosk.InfoRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="css/stylebutton.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-3.4.1.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ImprimirDiv(nombreDiv) {
            var contents = $('#' + nombreDiv).html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title>Impresion de sobre de pago del colaborador</title>');
            frameDoc.document.write('</head><body style="font-family:Poppins; font-size: 14px;">');
            frameDoc.document.write('<table style="width: 100%; border-color:black; border-width:1px; border-style:Solid;">');
            frameDoc.document.write('<tr>');
            frameDoc.document.write('<td style="width: 30%; text-align: center;"><img src="image/LogoImperial.png" width="100%"/></td>');
            frameDoc.document.write('<td style="width: 70%; text-align: left; vertical-align: top; padding: 0""><b>INDUSTRIA ALIMENTICIA IMPERIAL</b><br/> <b>Planta Principal:</b> Jujan, Km 53 Vía Durán – Babahoyo <br /> <b>Almacenera:</b> Yaguachi, Km 20 Vía Durán – Babahoyo <br /> <b>PBX:</b> 04-2748096 <br /> <b>EMAIL:</b> administracion@imperial.ec </td>');
            frameDoc.document.write('</tr>');
            frameDoc.document.write('</table>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();

            var css = frameDoc.document.createElement("link");
            css.setAttribute("href", "https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700,800,900");
            css.setAttribute("rel", "stylesheet");
            css.setAttribute("type", "text/css");
            frameDoc.document.head.appendChild(css);

            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
        }
    </script>

    <div id="content" class="p-4 p-md-5 pt-5">
        <div id="ListaRoles" runat="server" style="width:auto">
            <h2 class="mb-4">Sobre de Pago del Colaborador</h2>
            <p>Informacion General</p>
            <br />
            <div id="Cabecera" runat="server">
            </div>
            <br />
            <p>Detalle de rol de pago</p>
            <br />
            <div id="Detalle" runat="server">
            </div>
        </div>
        <br />
        <button type="button" runat="server" id="Buscar" style="width:15%" onserverclick="Buscar_ServerClick" >Imprimir rol de pago</button>
    </div>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
