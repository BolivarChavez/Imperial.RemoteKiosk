using RemoteKiosk.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteKiosk
{
    public partial class InfoConsola : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ConsultaInfoContacto(int.Parse(Session["EmplCodigo"].ToString().Trim()), Session["SesionToken"].ToString());
            }
        }

        private async void ConsultaInfoContacto(int codigo, string TokenAuth)
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            var uri = new Uri(string.Format(url + "Operador/BuscaContacto/{0}", codigo));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                encriptado = await response.Content.ReadAsStringAsync();
                var content = encriptado;
                var serializer = new JavaScriptSerializer();
                List<Contacto> l_contacto = serializer.Deserialize<List<Contacto>>(content);

                Session["EmplCedula"] = l_contacto.FirstOrDefault().co_numero_id.ToString().Trim();
                Session["EmplNombre"] = l_contacto.FirstOrDefault().co_nombre.ToString().Trim();
            }
        }
    }
}