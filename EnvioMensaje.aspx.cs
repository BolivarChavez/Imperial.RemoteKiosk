using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace RemoteKiosk
{
    public partial class EnvioMensaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtMensaje.Text = "";
            }
        }

        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            var fromAddress = new MailAddress("bolivarchavezg@gmail.com", "From Name");
            var toAddress = new MailAddress(txtEmail.Text.Trim(), "To Name");
            string fromPassword = "yeduufzqvmzrfzqr";
            string subject = "Comentario remitido por : " + txtNombre.Text.Trim();
            string body = txtMensaje.Text.Trim();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}