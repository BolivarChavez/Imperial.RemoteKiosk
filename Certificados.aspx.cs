using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using RemoteKiosk.Modelos;

namespace RemoteKiosk
{
    public partial class Certificados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private async void CargaPlantilla()
        {
            string sFecha = string.Empty;
            Microsoft.Office.Interop.Word._Application w = new Microsoft.Office.Interop.Word.Application();
            string path = @"C:\table.doc";
            string pathexport = string.Empty;
            string sNombre = string.Empty;
            string emplinfo;
            string cedula = Session["EmplCedula"].ToString().Trim();

            emplinfo = await DatosColaborador(cedula);
            var serializer = new JavaScriptSerializer();
            List<ColaboradorExt> l_empleado = serializer.Deserialize<List<ColaboradorExt>>(emplinfo);

            sNombre = l_empleado.FirstOrDefault().pc_apellido1.Trim() + " " + l_empleado.FirstOrDefault().pc_apellido2.Trim() + " " + l_empleado.FirstOrDefault().pc_nombre1.Trim() + " " + l_empleado.FirstOrDefault().pc_nombre2.Trim();

            Microsoft.Office.Interop.Word._Document oDoc = w.Documents.Open(FileName: path, ReadOnly:false);
            oDoc.Content.Find.Execute(FindText: "<<Origen>>", ReplaceWith: txtOrigen.Text.Trim());
            oDoc.Content.Find.Execute(FindText: "<<Fecha>>", ReplaceWith: sFecha);
            oDoc.Content.Find.Execute(FindText: "<<Destinatario>>", ReplaceWith: txtDestino.Text.Trim());
            oDoc.Content.Find.Execute(FindText: "<<Ciudad>>", ReplaceWith: txtCiudad.Text.Trim());
            oDoc.Content.Find.Execute(FindText: "<<Nombre>>", ReplaceWith: sNombre);
            oDoc.Content.Find.Execute(FindText: "<<Cargo>>", ReplaceWith: l_empleado.FirstOrDefault().pc_cargo.Trim());
            oDoc.Content.Find.Execute(FindText: "<<Sueldo>>", ReplaceWith: l_empleado.FirstOrDefault().pc_sueldo.ToString());
            oDoc.Content.Find.Execute(FindText: "<<Area>>", ReplaceWith: l_empleado.FirstOrDefault().pc_area.Trim());
            oDoc.Content.Find.Execute(FindText: "<<FechaIngreso>>", ReplaceWith: l_empleado.FirstOrDefault().pc_fecha_ingreso.ToShortDateString());

            oDoc.SaveAs(pathexport + "Certificado1.pdf", Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
            oDoc.PageSetup.Orientation = 0;
            oDoc.Close();
            w.Visible = false;
            w.Quit();

            oDoc = null;
            w = null;
        }

        private async Task<string> DatosColaborador(string cedula)
        {
            HttpClient _client = new HttpClient();
            string respuesta;
            string url;
            string stoken = Session["SesionToken"].ToString().Trim();

            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var uri = new Uri(string.Format(url + "Login/LoginAdmin", cedula));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                respuesta = await response.Content.ReadAsStringAsync();
            else
                respuesta = "";

            return respuesta;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = Calendar1.SelectedDate.ToShortDateString(); 
        }

        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            CargaPlantilla();
        }

        protected void Revisar_ServerClick(object sender, EventArgs e)
        {

        }
    }
}