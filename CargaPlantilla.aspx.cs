using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RemoteKiosk.Modelos;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.OleDb;
using System.Data;

namespace RemoteKiosk
{
    public partial class CargaPlantilla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CargaPersona(string filename)
        {
            string constr;
            string[] nombres;
            string[] apellidos;
            Cifrado decript = new Cifrado();

            constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                filename.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [COLABORADORES$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            con.Close();

            foreach (DataRow row in data.Rows)
            {
                ColaboradorExt parametros1 = new ColaboradorExt();

                parametros1.co_empresa = int.Parse(Session["IdEmpresa"].ToString().Trim());
                parametros1.pc_cedula = decript.Encriptar(String.Format("{0:0000000000}", long.Parse(row[2].ToString())));

                apellidos = row[3].ToString().Split(' ');
                parametros1.pc_apellido1 = decript.Encriptar(apellidos[0]);
                parametros1.pc_apellido2 = decript.Encriptar(apellidos[1]);

                nombres = row[4].ToString().Split(' ');
                parametros1.pc_nombre1 = decript.Encriptar(nombres[0]);
                parametros1.pc_nombre2 = decript.Encriptar(nombres[1]);

                parametros1.pc_fecha_ingreso = DateTime.Parse(row[15].ToString());
                parametros1.pc_cargo = decript.Encriptar("-");
                parametros1.pc_division = decript.Encriptar(row[1].ToString());
                parametros1.pc_area = decript.Encriptar("-");
                parametros1.pc_sueldo = double.Parse(row[19].ToString());
                parametros1.pc_tipo_colaborador = decript.Encriptar("-");
                parametros1.pc_tipo_contrato = decript.Encriptar(row[16].ToString());
                parametros1.pc_ciudad_labores = decript.Encriptar("-");
                parametros1.pc_ciudad_residencia = decript.Encriptar("-");
                parametros1.pc_domicilio = decript.Encriptar(row[10].ToString());
                parametros1.pc_telefono = decript.Encriptar(row[9].ToString());
                parametros1.pc_email = decript.Encriptar(row[11].ToString());
                parametros1.pc_fecha_actualizacion = DateTime.Today;
                parametros1.pc_estado = decript.Encriptar("A");
                parametros1.pc_operador = decript.Encriptar(Session["UserAdmin"].ToString().Trim());
                parametros1.pc_fecha_registro = DateTime.Today;

                GrabarPersona(parametros1);
            }
        }

        private void CargaRoles(string filename)
        {
            string constr;
            string cedula;
            int anio, mes, periodo, i;
            DateTime desde, hasta;

            Cifrado decript = new Cifrado();

            constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                filename.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [ROLES$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            con.Close();

            foreach (DataRow row in data.Rows)
            {
                cedula = row[0].ToString();
                anio = int.Parse(row[0].ToString());
                mes = int.Parse(row[0].ToString());
                periodo = int.Parse(row[0].ToString());
                desde = DateTime.Parse(row[0].ToString());
                hasta = DateTime.Parse(row[0].ToString());

                for(i=1; i<=7; i++)
                {
                    TransaccionExt parametros1 = new TransaccionExt();

                    parametros1.pr_cedula = decript.Encriptar(String.Format("{0:0000000000}", long.Parse(cedula)));
                    parametros1.pr_anio = anio;
                    parametros1.pr_mes = mes;
                    parametros1.pr_periodo = periodo;
                    parametros1.pr_desde = desde;
                    parametros1.pr_hasta = hasta;
                    parametros1.pr_concepto = "CONCEPTO";
                    parametros1.pr_ingreso = double.Parse(row[1 + i].ToString());
                    parametros1.pr_descuento = 0;
                    parametros1.pr_rol = "ROL DE PAGO";

                    GrabarTransaccion(parametros1);
                }

                for (i = 1; i <= 9; i++)
                {
                    TransaccionExt parametros1 = new TransaccionExt();

                    parametros1.pr_cedula = decript.Encriptar(String.Format("{0:0000000000}", long.Parse(cedula)));
                    parametros1.pr_anio = anio;
                    parametros1.pr_mes = mes;
                    parametros1.pr_periodo = periodo;
                    parametros1.pr_desde = desde;
                    parametros1.pr_hasta = hasta;
                    parametros1.pr_concepto = "CONCEPTO";
                    parametros1.pr_ingreso = 0;
                    parametros1.pr_descuento = double.Parse(row[9 + i].ToString());
                    parametros1.pr_rol = "ROL DE PAGO";

                    GrabarTransaccion(parametros1);
                }
            }
        }

        private void GrabarPersona(ColaboradorExt colaborador)
        {
            string url, stoken;

            stoken = Session["SesionToken"].ToString().Trim();
            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var request = (HttpWebRequest)WebRequest.Create(url + "Colaborador/RegistroEmplExt");
            request.ContentType = "application/json";
            request.Method = "POST";
            //request.Headers.Add("Authorization", "Bearer " + stoken);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(colaborador);
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

        private void GrabarTransaccion(TransaccionExt transaccion)
        {
            string url, stoken;

            stoken = Session["SesionToken"].ToString().Trim();
            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var request = (HttpWebRequest)WebRequest.Create(url + "Transaccion/RegistroTranExt");
            request.ContentType = "application/json";
            request.Method = "POST";
            //request.Headers.Add("Authorization", "Bearer " + stoken);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(transaccion);
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

        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            string sArchivo1, sArchivo2;
 
            if (FileUpload1.HasFile)
            {
                sArchivo1 = FileUpload1.FileName.Trim();
                FileUpload1.SaveAs(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + sArchivo1);
                CargaPersona(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + sArchivo1);
            }

            if (FileUpload2.HasFile)
            {
                sArchivo2 = FileUpload2.FileName.Trim();
                FileUpload2.SaveAs(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + sArchivo2);
                CargaRoles(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + sArchivo2);
            }
        }
    }
}
