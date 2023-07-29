using RemoteKiosk.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteKiosk
{
    public partial class VistaDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaDocumentos();
            }
        }

        private void SetInitialRow(DataTable tabla)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            dt.Columns.Add(new DataColumn("Archivo", typeof(string)));

            if (tabla.Rows.Count > 0)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    dr = dt.NewRow();
                    dr["RowNumber"] = row["do_codigo"];
                    dr["Descripcion"] = row["do_descripcion"];
                    dr["Archivo"] = row["do_ruta"];
                    dt.Rows.Add(dr);
                }
            }

            grdDocs.DataSource = dt;
            grdDocs.DataBind();
        }

        private async void CargaDocumentos()
        {
            HttpClient _client = new HttpClient();
            string encriptado;
            Cifrado decript = new Cifrado();
            string url;
            ListConverter Listdt = new ListConverter();
            DataTable dt = new DataTable();

            url = ConfigurationManager.AppSettings["PathUrlSec"];
            var uri = new Uri(string.Format(url + "Documento/GetDocsConsulta/{0}", Session["IdCliente"].ToString().Trim()));

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenAuth);
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                encriptado = await response.Content.ReadAsStringAsync();
                var content = decript.DesEncriptar(encriptado);
                var serializer = new JavaScriptSerializer();
                List<Documento> l_documento = serializer.Deserialize<List<Documento>>(content);
                dt = Listdt.ToDataTable(l_documento);
                SetInitialRow(dt);
            }
        }

        protected void grdDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string filename;
            string pathdoc;
            pathdoc = Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\";

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvRow = grdDocs.Rows[index];
            filename = gvRow.Cells[2].Text;

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.WriteFile(pathdoc + filename);
            Response.End();
        }
    }
}