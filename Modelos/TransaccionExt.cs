using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class TransaccionExt
    {
        public string pr_cedula { get; set; }
        public int pr_anio { get; set; }
        public int pr_mes { get; set; }
        public int pr_periodo { get; set; }
        public DateTime pr_desde { get; set; }
        public DateTime pr_hasta { get; set; }
        public string pr_concepto { get; set; }
        public double pr_ingreso { get; set; }
        public double pr_descuento { get; set; }
        public string pr_rol { get; set; }
    }
}