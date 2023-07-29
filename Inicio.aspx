<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="RemoteKiosk.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kiosko virtual empresarial para consulta de informacion de colaboradores</title>

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="keywords" content="Kiosko, Informacion, Empresarial, Colaboradores"/>

    <link href="css/styleini.css" rel="stylesheet" type="text/css" />
    <link href="css/fontawesome-all.css" rel="stylesheet" />
    <link href="//fonts.googleapis.com/css?family=Raleway:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet"/>
</head>
<body>
    <h1 style="color: #8A0010">Ingreso al Kiosko Virtual Empresarial</h1>
    <div class="w3l-login-form">
        <h2>Ingrese sus credenciales aqui</h2>
        <form runat="server">
            <div class=" w3l-form-group">
                <label>Usuario:</label>
                <div class="group">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="Txtusuario" runat="server" class="form-control" Text=""></asp:TextBox>
                </div>
            </div>
            <div class=" w3l-form-group">
                <label>Contraseña:</label>
                <div class="group">
                    <i class="fas fa-unlock"></i>
                    <asp:TextBox ID="Txtpassword" runat="server" class="form-control" Text="" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <button type="submit" runat="server" id="UsrAccess" onserverclick="UsrAccess_ServerClick">Ingresar al Kiosko Virtual</button>
            <br />
            <br />
            <button type="submit" runat="server" id="AdmAccess" onserverclick="AdmAccess_ServerClick">Ingresar como Administrador</button>
        </form>
    </div>
    <footer>
        <p class="copyright-agileinfo"> &copy;<script>document.write(new Date().getFullYear());</script> Todos los derechos reservados.</p>
    </footer>
    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>
</body>
</html>
