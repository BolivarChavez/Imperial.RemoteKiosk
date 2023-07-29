<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="RemoteKiosk.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Sports Camp Registration Form Flat Responsive widget Template :: w3layouts</title>
<!-- metatags-->
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="Sports Camp Registration Form a Flat Responsive Widget,Login form widgets, Sign up Web 	forms , Login signup Responsive web form,Flat Pricing table,Flat Drop downs,Registration Forms,News letter Forms,Elements" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false);
function hideURLbar(){ window.scrollTo(0,1); } </script>
<!-- Meta tag Keywords -->
<!-- css files -->
<link rel="stylesheet" href="css/jquery-ui.css"/>
<link href="css/stylereg.css" rel="stylesheet" type="text/css" media="all"/><!--stylesheet-css-->
<%--<link href="//fonts.googleapis.com/css?family=Josefin+Sans:100,100i,300,300i,400,400i,600,600i,700,700i" rel="stylesheet" />
<link href="//fonts.googleapis.com/css?family=PT+Sans:400,400i,700,700i" rel="stylesheet" />--%>
	<link href="//fonts.googleapis.com/css?family=Raleway:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet"/>

<!-- //css files -->
</head>
<body>
	<h1>Formulario de resgistro</h1>
<div class="w3l-main">
	<div class="w3l-from">
		<form runat="server">	
			<div class="w3l-details1">
				<div class="w3l-options2">
					<label class="head">Tipo de identificacion<span class="w3l-star"> * </span></label>
					<asp:DropDownList ID="DropDownList2" runat="server" class="category2">
						<asp:ListItem>CEDULA</asp:ListItem>
						<asp:ListItem>PASAPORTE</asp:ListItem>
						<asp:ListItem>RUC</asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="w3l-num">
					<label class="head">Numero de identificacion<span class="w3l-star"> * </span></label>
					<input type="text"  name="phone number" placeholder="" required="" />
				</div>
			</div>
			<div class="clear"></div>
			<div class="w3l-user">
				<label class="head">Nombre completo o razon social<span class="w3l-star"> * </span></label>
				<input type="text" name="Athlete's Name" placeholder="" required="" />
			</div>
			<div class="clear"></div>
			<div class="w3l-details1">
				<div class="gender">
					<label class="head">Pais<span class="w3l-star"> * </span></label>
					<asp:DropDownList ID="DropDownList3" runat="server" class="form-control">
						<asp:ListItem>CEDULA</asp:ListItem>
						<asp:ListItem>PASAPORTE</asp:ListItem>
						<asp:ListItem>RUC</asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="w3l-options2">
					<label class="head">Provincia<span class="w3l-star"> * </span></label>
					<asp:DropDownList ID="DropDownList4" runat="server" class="category2">
						<asp:ListItem>CEDULA</asp:ListItem>
						<asp:ListItem>PASAPORTE</asp:ListItem>
						<asp:ListItem>RUC</asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="clear"></div>
				<div class="gender">
					<label class="head">Canton<span class="w3l-star"> * </span></label>
					<asp:DropDownList ID="DropDownList5" runat="server" class="form-control">
						<asp:ListItem>CEDULA</asp:ListItem>
						<asp:ListItem>PASAPORTE</asp:ListItem>
						<asp:ListItem>RUC</asp:ListItem>
					</asp:DropDownList>
				</div>
			</div>
			<div class="clear"></div>
			<div class="w3l-user">
				<label class="head">Direccion<span class="w3l-star"> * </span></label>
				<input type="text" name="Athlete's Name" placeholder="" required="" />
			</div>
			<div class="clear"></div>
			<div class="w3l-user">
				<label class="head">Telefono<span class="w3l-star"> * </span></label>
				<input type="text" name="Athlete's Name" placeholder="" required="" />
			</div>
			<div class="clear"></div>
			<div class="w3l-mail">
				<label class="head">Correo electronico<span class="w3l-star"> * </span></label>
				<input type="email" name="email" placeholder="" required="" />
			</div>
			<div class="clear"></div>
			<div class="w3l-lef1">
				<h3>Usuario Administrador</h3>
					<div class="w3l-user">
						<label class="head">Nombre de usuario<span class="w3l-star"> * </span></label>
						<input type="text" name="Username" placeholder="" required="" />
					</div>
					<div class="w3l-user">
						<label class="head">Contraseña<span class="w3l-star"> * </span></label>
						<input type="text" name="Username" placeholder="" required="" />
					</div>
					<div class="w3l-user">
						<label class="head">Confirmar Contraseña<span class="w3l-star"> * </span></label>
						<input type="text" name="Username" placeholder="" required="" />
					</div>
					<div class="w3l-user">
						<label class="head">Clave de autorizacion de ingreso<span class="w3l-star"> * </span></label>
						<input type="text" name="Username" placeholder="" required="" />
						<label class="head">Para obtener una clave de autorizacion favor contactarse al correo: info@gmail.com</label>
					</div>
			<div class="clear"></div>
			</div>	
				<div class="btn">
					<input type="submit" name="submit" value="Submit"/>
				</div>
			<div class="clear"></div>
		</form>
	</div>
</div>
	<footer>&copy; 2018 Sports Camp Registration Form. All Rights Reserved | Design by <a href="http://w3layouts.com/"> W3layouts</a>
	</footer>
	<!-- Default-JavaScript --> <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>

<!-- Calendar -->
<script src="js/jquery-ui.js"></script>
	<script>
		$(function() {
		$( "#datepicker,#datepicker1" ).datepicker();
		});
    </script>
<!-- //Calendar -->

    <script type="text/javascript">
        function disableBack() { window.history.forward(); }
        setTimeout("disableBack()", 0);
        window.onunload = function () { null };
    </script>

</body>
</html>
