using RemoteKiosk.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteKiosk
{
    public partial class InfoRol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImprimeSobre();
            }
        }

        private string ConsultaTran()
        {
            HttpClient _client = new HttpClient();
            string respuesta;
            string url;
            string stoken = Session["SesionToken"].ToString().Trim();
            string cedula = Session["EmplCedula"].ToString().Trim();
            string periodo, rol, anio, mes;

            anio = Session["Anio"].ToString().Trim();
            periodo = Session["Periodo"].ToString().Trim();
            rol = Session["Rol"].ToString().Trim();
            mes = Session["Mes"].ToString().Trim();

            url = ConfigurationManager.AppSettings["PathUrlTrn"];
            var uri = new Uri(string.Format(url + "Transaccion/GetTranExtDet/{0}/{1}/{2}/{3}/{4}", cedula, anio, mes, periodo, rol));

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

        private void ImprimeSobre()
        {
            Table table1 = new Table();
            Table table2 = new Table();
            int i, j;
            string sTitulo, sValor;

            var transaccion = ConsultaTran();

            var serializer = new JavaScriptSerializer();
            List<TransaccionExt> l_tran = serializer.Deserialize<List<TransaccionExt>>(transaccion);

            table1.Width = Unit.Percentage(100);
            table1.Style.Value = "font-size:12px";
            table1.BorderStyle = BorderStyle.Solid;
            table1.BorderWidth = Unit.Pixel(1);

            sTitulo = "";
            sValor = "";

            for (i=1; i<=5; i++)
            {
                TableRow r = new TableRow();

                switch (i)
                {
                    case 1:
                        sTitulo = "Nombre del colaborador";
                        sValor = Session["EmplNombre"].ToString().Trim();
                        break;

                    case 2:
                        sTitulo = "Rol de pago";
                        sValor = Session["Rol"].ToString().Trim();
                        break;

                    case 3:
                        sTitulo = "Año";
                        sValor = Session["Anio"].ToString().Trim();
                        break;

                    case 4:
                        sTitulo = "Mes";
                        sValor = Session["DescMes"].ToString().Trim();
                        break;

                    case 5:
                        sTitulo = "Periodo";
                        sValor = Session["Periodo"].ToString().Trim();
                        break;
                }

                for (j = 1; j <= 2; j++)
                {
                    TableCell c = new TableCell();

                    if (j == 1)
                    {
                        c.Width = Unit.Percentage(20);
                        c.Font.Bold = true;
                        c.Controls.Add(new LiteralControl(sTitulo.ToUpper()));
                    }
                    else
                    {
                        c.Width = Unit.Percentage(80);
                        c.Controls.Add(new LiteralControl(sValor));
                    }

                    r.Cells.Add(c);
                }

                table1.Rows.Add(r);
            }

            Cabecera.Controls.Add(table1);

            table2.Width = Unit.Percentage(100);
            table2.Style.Value = "font-size:12px";
            table2.BorderStyle = BorderStyle.Solid;
            table2.BorderWidth = Unit.Pixel(1);

            TableHeaderRow h1 = new TableHeaderRow();

            for (i = 1; i <= 3; i++)
            {
                TableHeaderCell hcell1 = new TableHeaderCell();

                switch (i)
                {
                    case 1:
                        hcell1.Width = Unit.Percentage(60);
                        hcell1.HorizontalAlign = HorizontalAlign.Center;
                        hcell1.Text = "CONCEPTO";
                        break;

                    case 2:
                        hcell1.Width = Unit.Percentage(20);
                        hcell1.HorizontalAlign = HorizontalAlign.Center;
                        hcell1.Text = "INGRESO";
                        break;

                    case 3:
                        hcell1.Width = Unit.Percentage(20);
                        hcell1.HorizontalAlign = HorizontalAlign.Center;
                        hcell1.Text = "DESCUENTO";
                        break;
                }

                hcell1.BorderStyle = BorderStyle.Solid;
                hcell1.BorderWidth = Unit.Pixel(1);
                hcell1.Font.Bold = true;
                hcell1.Style.Add("padding-right", "10px");
                hcell1.Style.Add("padding-left", "10px");
                h1.Cells.Add(hcell1);
            }

            table2.Rows.Add(h1);

            foreach (var t in l_tran)
            {
                TableRow r = new TableRow();

                for (i = 1; i <= 3; i++)
                {
                    TableCell c = new TableCell();

                    switch (i)
                    {
                        case 1:
                            c.Controls.Add(new LiteralControl(t.pr_concepto));
                            c.HorizontalAlign = HorizontalAlign.Left;
                            break;

                        case 2:
                            c.Controls.Add(new LiteralControl(t.pr_ingreso.ToString()));
                            c.HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case 3:
                            c.Controls.Add(new LiteralControl(t.pr_descuento.ToString()));
                            c.HorizontalAlign = HorizontalAlign.Right;
                            break;
                    }

                    c.Height = Unit.Pixel(20);
                    c.Font.Size = FontUnit.Point(9);
                    c.Style.Add("padding-right", "10px");
                    c.Style.Add("padding-left", "10px");
                    r.Cells.Add(c);
                }

                table2.Rows.Add(r);
            }

            Detalle.Controls.Add(table2);
        }
    }
}