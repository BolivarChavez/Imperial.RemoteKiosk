using RemoteKiosk.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteKiosk
{
    public partial class RolesPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TxtAnio.Text = DateTime.Now.Year.ToString();
                cboMes.SelectedIndex = 0;
            }
        }

        private string ConsultaPeriodos()
        {
            HttpClient _client = new HttpClient();
            string respuesta;
            string url;
            string stoken = Session["SesionToken"].ToString().Trim();
            string cedula = Session["EmplCedula"].ToString().Trim();

            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var uri = new Uri(string.Format(url + "Transaccion/GetTranExtList/{0}/{1}/{2}", cedula, TxtAnio.Text.Trim(), cboMes.SelectedIndex.ToString()));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = _client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            { 
                var responseContent = response.Content;
                respuesta = responseContent.ReadAsStringAsync().Result;
            }
            else
                respuesta = "";

            return respuesta;
        }

        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            ListConverter Listdt = new ListConverter();
            DataTable dt = new DataTable();
            var periodos = ConsultaPeriodos();

            var serializer = new JavaScriptSerializer();
            List<PeriodoTran> l_periodos = serializer.Deserialize<List<PeriodoTran>>(periodos);
            dt = Listdt.ToDataTable(l_periodos);

            grdRoles.DataSource = dt;
            grdRoles.DataBind();
        }

        protected void Consultar_ServerClick(object sender, EventArgs e)
        {
            string[] sArreg;

            sArreg = grdRoles.SelectedRow.Cells[1].Text.Split('-');

            Session["Anio"] = TxtAnio.Text.Trim();
            Session["Mes"] = sArreg[0].Trim();
            Session["Periodo"] = grdRoles.SelectedRow.Cells[3].Text;
            Session["Rol"] = grdRoles.SelectedRow.Cells[2].Text;
            Session["DescMes"] = grdRoles.SelectedRow.Cells[1].Text;

            Response.Redirect("InfoRol.aspx");
        }
    }
}