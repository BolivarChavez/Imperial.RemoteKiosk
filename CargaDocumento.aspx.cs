using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using RemoteKiosk.Modelos;
using System.Net;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace RemoteKiosk
{
    public partial class CargaDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaDocumentos();
            }
        }

        private void SetInitialRow(DataTable table)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(byte[])));

            if (table.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 0;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = null;
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    dr = dt.NewRow();
                    dr["RowNumber"] = row["do_codigo"];
                    dr["Column1"] = row["do_descripcion"];
                    dr["Column2"] = row["do_ruta"];
                    dr["Column3"] = null;
                    dt.Rows.Add(dr);
                }
            }

            ViewState["CurrentTable"] = dt;
            grdDocs.DataSource = dt;
            grdDocs.DataBind();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)grdDocs.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)grdDocs.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        HiddenField hfFile = (HiddenField)grdDocs.Rows[rowIndex].Cells[3].FindControl("hfFileByte");
                        FileUpload fuUpload = (FileUpload)grdDocs.Rows[rowIndex].Cells[3].FindControl("fuUpload");

                        byte[] bytes = null;

                        if (fuUpload.HasFile)
                        {
                            BinaryReader br = new BinaryReader(fuUpload.PostedFile.InputStream);
                            bytes = br.ReadBytes((int)fuUpload.PostedFile.InputStream.Length);
                            hfFile.Value = Convert.ToBase64String(bytes);
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = 0; 

                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = fuUpload.HasFile ? Path.GetFileName(fuUpload.PostedFile.FileName) : box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = fuUpload.HasFile ? bytes : Convert.FromBase64String(hfFile.Value);
                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdDocs.DataSource = dtCurrentTable;
                    grdDocs.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)grdDocs.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)grdDocs.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        HiddenField hfFile = (HiddenField)grdDocs.Rows[rowIndex].Cells[3].FindControl("hfFileByte");

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        hfFile.Value = !Convert.IsDBNull(dt.Rows[i]["Column3"]) ? Convert.ToBase64String((byte[])dt.Rows[i]["Column3"]) : "";

                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void AddDoc_ServerClick(object sender, EventArgs e)
        {
            string pathdoc;
            pathdoc = ConfigurationManager.AppSettings["PathDocs"];

            foreach (GridViewRow row in grdDocs.Rows)
            {
                string descripcion = (row.FindControl("TextBox1") as TextBox).Text;
                string archivo = (row.FindControl("TextBox2") as TextBox).Text;

                // Retriving Uploaded File value from Hiddenfield.
                string base64String = (row.FindControl("hfFileByte") as HiddenField).Value;
                byte[] bytes = Convert.FromBase64String(base64String);
                string filePath = Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + archivo;

                if (!string.IsNullOrEmpty(descripcion) && !string.IsNullOrEmpty(archivo) && bytes != null)
                {
                    if (int.Parse(row.Cells[0].Text) == 0)
                    {
                        File.WriteAllBytes(filePath, bytes);
                        InsertaDocumento(descripcion, archivo);
                    }
                }
            }

            CargaDocumentos();
        }

        private void InsertaDocumento(string descripcion, string archivo)
        {
            Documento parametros = new Documento();
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];

            parametros.do_cliente = int.Parse(Session["IdCliente"].ToString().Trim());
            parametros.do_codigo = 0;
            parametros.do_tipo = "XX";
            parametros.do_descripcion = decript.Encriptar(descripcion);
            parametros.do_ruta = decript.Encriptar(archivo);
            parametros.do_estado = "A";
            parametros.do_operador = decript.Encriptar(Session["UserAdmin"].ToString().Trim());

            var request = (HttpWebRequest)WebRequest.Create(url + "Documento/Registro");
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

        private void ActualizaDocumento(int codigo, string descripcion, string archivo)
        {
            Documento parametros = new Documento();
            Cifrado decript = new Cifrado();
            string url;

            url = ConfigurationManager.AppSettings["PathUrlSec"];

            parametros.do_cliente = int.Parse(Session["IdCliente"].ToString().Trim());
            parametros.do_codigo = codigo;
            parametros.do_tipo = "XX";
            parametros.do_descripcion = decript.Encriptar(descripcion);
            parametros.do_ruta = decript.Encriptar(archivo);
            parametros.do_estado = decript.Encriptar("A");
            parametros.do_operador = decript.Encriptar(Session["UserAdmin"].ToString().Trim());

            var request = (HttpWebRequest)WebRequest.Create(url + "Documento/Actualizacion");
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

            SetPreviousData();
        }

        protected void DelDoc_ServerClick(object sender, EventArgs e)
        {

        }
    }
}