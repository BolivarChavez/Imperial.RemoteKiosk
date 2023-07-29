using System;
using System.Collections.Generic;
using RemoteKiosk.Modelos;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;

namespace RemoteKiosk
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Txtusuario.Text = "";
                Txtpassword.Text = "";
            }
        }

        protected void UsrAccess_ServerClick(object sender, EventArgs e)
        {
            LoginParam parametros = new LoginParam();
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            parametros.usuario = decript.Encriptar(Txtusuario.Text.Trim());
            parametros.password = Txtpassword.Text.Trim();

            var request = (HttpWebRequest)WebRequest.Create(url + "Login/LoginUser");
            request.ContentType = "application/json";
            request.Method = "POST";
            //request.Headers.Add("Authorization", "Bearer " + AccessToken);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(parametros);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                var serializer = new JavaScriptSerializer();
                List<LoginRetorno> abcList = serializer.Deserialize<List<LoginRetorno>>(result);
                Session["SesionToken"] = "******"; //abcList[0].descripcion;
                //abcList[0].retorno 
            }

            ConsultaInfoUsuario(Txtusuario.Text.Trim(), Session["SesionToken"].ToString());
            Response.Redirect("InfoConsola.aspx");
        }

        protected void AdmAccess_ServerClick(object sender, EventArgs e)
        {
            LoginParam parametros = new LoginParam();
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            parametros.usuario = decript.Encriptar(Txtusuario.Text.Trim());
            parametros.password = Txtpassword.Text.Trim();

            var request = (HttpWebRequest)WebRequest.Create(url + "Login/LoginAdmin");
            request.ContentType = "application/json";
            request.Method = "POST";
            //request.Headers.Add("Authorization", "Bearer " + AccessToken);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(parametros);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                var serializer = new JavaScriptSerializer();
                List<LoginRetorno> abcList = serializer.Deserialize<List<LoginRetorno>>(result);
                Session["SesionToken"] = "******"; //abcList[0].descripcion;
                //abcList[0].retorno 
                ConsultaInfoCliente(Txtusuario.Text.Trim(), Session["SesionToken"].ToString());
            }

            Response.Redirect("AdminConsola.aspx");
        }

        private async void ConsultaInfoCliente(string UsrCliente, string TokenAuth)
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            var uri = new Uri(string.Format(url + "Cliente/GetCliente/{0}", UsrCliente));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                encriptado = await response.Content.ReadAsStringAsync();
                var content = decript.DesEncriptar(encriptado);
                var serializer = new JavaScriptSerializer();
                List<Cliente> l_cliente = serializer.Deserialize<List<Cliente>>(content);

                Session["IdEmpresa"] = "1";
                Session["UserAdmin"] = l_cliente.FirstOrDefault().cl_usuario.Trim();
                Session["NameAdmin"] = l_cliente.FirstOrDefault().cl_nombre.Trim();
                Session["EmailAdmin"] = l_cliente.FirstOrDefault().cl_email.Trim();
                Session["IdCliente"] = "1";
            }
        }

        private async void ConsultaInfoUsuario(string usuario, string TokenAuth)
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            var uri = new Uri(string.Format(url + "Operador/BuscaUsuarioUserName/{0}", usuario));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                encriptado = await response.Content.ReadAsStringAsync();
                var content = encriptado;
                var serializer = new JavaScriptSerializer();
                List<Operador> l_operador = serializer.Deserialize<List<Operador>>(content);

                Session["IdEmpresa"] = "1";
                Session["EmplCodigo"] = l_operador.FirstOrDefault().op_id_contacto.ToString().Trim();
                Session["IdCliente"] = "1";
            }
        }
    }
}