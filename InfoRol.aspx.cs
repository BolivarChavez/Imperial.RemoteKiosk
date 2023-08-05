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
            double totalIngresos, totalDescuentos, netoPagar;

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
                        hcell1.Text = "EGRESO";
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

            totalIngresos = 0;
            totalDescuentos = 0;

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
                            c.Controls.Add(new LiteralControl(t.pr_ingreso == 0? "" : t.pr_ingreso.ToString("#,##0.00")));
                            c.HorizontalAlign = HorizontalAlign.Right;
                            c.Style.Add("border-left-style", "solid");
                            c.Style.Add("border-left-width", "1px");
                            c.Style.Add("border-right-style", "solid");
                            c.Style.Add("border-right-width", "1px");
                            totalIngresos += t.pr_ingreso;
                            break;

                        case 3:
                            c.Controls.Add(new LiteralControl(t.pr_descuento == 0? "" : t.pr_descuento.ToString("#,##0.00")));
                            c.HorizontalAlign = HorizontalAlign.Right;
                            c.Style.Add("border-right-style", "solid");
                            c.Style.Add("border-right-width", "1px");
                            totalDescuentos += t.pr_descuento;
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

            TableRow rTotal = new TableRow();

            for (i = 1; i <= 3; i++)
            {
                TableCell cTotal = new TableCell();

                switch (i)
                {
                    case 1:
                        cTotal.Controls.Add(new LiteralControl("TOTAL DE INGRESOS Y EGRESOS"));
                        cTotal.HorizontalAlign = HorizontalAlign.Left;
                        break;

                    case 2:
                        cTotal.Controls.Add(new LiteralControl(totalIngresos.ToString("#,##0.00")));
                        cTotal.HorizontalAlign = HorizontalAlign.Right;
                        cTotal.Font.Bold = true;
                        break;

                    case 3:
                        cTotal.Controls.Add(new LiteralControl(totalDescuentos.ToString("#,##0.00")));
                        cTotal.HorizontalAlign = HorizontalAlign.Right;
                        cTotal.Font.Bold = true;
                        break;
                }

                cTotal.Height = Unit.Pixel(20);
                cTotal.Font.Size = FontUnit.Point(9);
                cTotal.Style.Add("padding-right", "10px");
                cTotal.Style.Add("padding-left", "10px");
                cTotal.BorderStyle = BorderStyle.Solid;
                cTotal.BorderWidth = Unit.Pixel(1);

                rTotal.Cells.Add(cTotal);
            }
                
            table2.Rows.Add(rTotal);

            netoPagar = totalIngresos - totalDescuentos;

            TableRow rNeto = new TableRow();

            for (i = 1; i <= 3; i++)
            {
                TableCell cNeto = new TableCell();

                switch (i)
                {
                    case 1:
                        cNeto.Controls.Add(new LiteralControl("NETO A PAGAR"));
                        cNeto.HorizontalAlign = HorizontalAlign.Left;
                        break;

                    case 2:
                        cNeto.Controls.Add(new LiteralControl(""));
                        cNeto.HorizontalAlign = HorizontalAlign.Right;
                        break;

                    case 3:
                        cNeto.Controls.Add(new LiteralControl(netoPagar.ToString("#,##0.00")));
                        cNeto.HorizontalAlign = HorizontalAlign.Right;
                        cNeto.Font.Bold = true;
                        break;
                }

                cNeto.Height = Unit.Pixel(20);
                cNeto.Font.Size = FontUnit.Point(9);
                cNeto.Style.Add("padding-right", "10px");
                cNeto.Style.Add("padding-left", "10px");
                rNeto.Cells.Add(cNeto);
            }

            table2.Rows.Add(rNeto);

            Detalle.Controls.Add(table2);
        }

        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            ImprimeSobre();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "anioactual", "ImprimirDiv('ContentPlaceHolder2_ListaRoles')", true);
        }
    }
}