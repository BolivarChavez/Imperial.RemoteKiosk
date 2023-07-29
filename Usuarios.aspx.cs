using Newtonsoft.Json;
using RemoteKiosk.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteKiosk
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCodigo.Text = "";
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtPassword1.Text = "";
                txtPassword2.Text = "";

                CargaEmpleados();
            }
        }

        private async void CargaEmpleados()
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            Cifrado decript = new Cifrado();
            string url;
            ListConverter Listdt = new ListConverter();
            DataTable dt = new DataTable();

            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var uri = new Uri(string.Format(url + "Colaborador/GetEmplListExt/{0}/{1}/{2}/{3}/{4}", Session["IdEmpresa"].ToString().Trim(), "*", "*", "*", "*"));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                cboEmpleado.Items.Clear();

                encriptado = await response.Content.ReadAsStringAsync();
                var content = encriptado;
                var serializer = new JavaScriptSerializer();
                List<ColaboradorLista> l_empl = serializer.Deserialize<List<ColaboradorLista>>(content);
                dt = Listdt.ToDataTable(l_empl);

                cboEmpleado.DataSource = dt;

                cboEmpleado.DataSource = dt;
                cboEmpleado.DataValueField = "pc_cedula";
                cboEmpleado.DataTextField = "nombre";

                cboEmpleado.DataBind();
            }
        }

        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            if (lblCodigo.Text.Trim() == "0")
            {
                CreaOperador(int.Parse(lblContacto.Text.Trim()));
            }
            else
            {
                CambiaPassword();
            }
        }

        protected void cboEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetContacto(cboEmpleado.SelectedValue.ToString().Trim());
            CargaOperador(cboEmpleado.SelectedValue.ToString().Trim());
        }

        private async void CargaOperador(string cedula)
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            Cifrado decript = new Cifrado();
            string url;
            ListConverter Listdt = new ListConverter();
            DataTable dt = new DataTable();

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            var uri = new Uri(string.Format(url + "Operador/BuscaUsuarioCedula/{0}/{1}", cedula, Session["IdEmpresa"].ToString().Trim()));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                encriptado = await response.Content.ReadAsStringAsync();
                var content = encriptado;
                var serializer = new JavaScriptSerializer();
                List<Operador> l_user = serializer.Deserialize<List<Operador>>(content);

                lblCodigo.Text = l_user.FirstOrDefault().op_codigo.ToString().Trim();
                txtNombre.Text = l_user.FirstOrDefault().op_nombre.ToString().Trim();
                txtUsuario.Text = l_user.FirstOrDefault().op_usuario.ToString().Trim();
            }
        }

        private async void GetContacto(string cedula)
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            string url;

            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var uri = new Uri(string.Format(url + "Colaborador/GetContacto/{0}/{1}", Session["IdEmpresa"].ToString().Trim(), cedula));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                encriptado = await response.Content.ReadAsStringAsync();
                var content = encriptado;
                var serializer = new JavaScriptSerializer();
                List<Contacto> l_user = serializer.Deserialize<List<Contacto>>(content);
                lblContacto.Text = l_user.FirstOrDefault().co_codigo.ToString().Trim();
            }
        }

        private void CreaOperador(int contacto)
        {
            Operador parametros = new Operador();
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];

            parametros.op_codigo = int.Parse(lblCodigo.Text.Trim());
            parametros.op_id_contacto = contacto;
            parametros.op_usuario = decript.Encriptar(txtUsuario.Text.Trim());
            parametros.op_nombre = decript.Encriptar(cboEmpleado.SelectedItem.Text);
            parametros.op_password = txtPassword1.Text.Trim();
            parametros.op_fecha_validez = DateTime.Today;
            parametros.op_intentos = 0;
            parametros.op_estado = "A";
            parametros.op_operador = Session["UserAdmin"].ToString().Trim();
            parametros.op_terminal = "SERVER";
            parametros.op_fecha_act = DateTime.Today;
            parametros.op_cliente = int.Parse(Session["IdCliente"].ToString().Trim());

            var request = (HttpWebRequest)WebRequest.Create(url + "Operador/Registro");
            request.ContentType = "application/json";
            request.Method = "POST";
            //request.Headers.Add("Authorization", "Bearer " + AccessToken);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(parametros);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                var serializer = new JavaScriptSerializer();
                List<ProcRetorno> l_retorno = serializer.Deserialize<List<ProcRetorno>>(result);
            }
        }

        private void CambiaPassword()
        {
            Operador parametros = new Operador();
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];

            parametros.op_codigo = int.Parse(lblCodigo.Text.Trim());
            parametros.op_id_contacto = 0;
            parametros.op_usuario = "";
            parametros.op_nombre = "";
            parametros.op_password = txtPassword1.Text.Trim();
            parametros.op_fecha_validez = DateTime.Now;
            parametros.op_intentos = 0;
            parametros.op_estado = "A";
            parametros.op_operador = Session["UserAdmin"].ToString().Trim();
            parametros.op_terminal = "SERVER";
            parametros.op_fecha_act = DateTime.Now;
            parametros.op_cliente = int.Parse(Session["IdCliente"].ToString().Trim());

            var request = (HttpWebRequest)WebRequest.Create(url + "Operador/ActPassword");
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
                List<ProcRetorno> l_retorno = serializer.Deserialize<List<ProcRetorno>>(result);
            }
        }
    }
}