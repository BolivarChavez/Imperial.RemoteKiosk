using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class PeriodoTran
    {
        public string pr_rol { get; set; }
        public int pr_mes { get; set; }
        public int pr_periodo { get; set; }
        public string pr_desde { get; set; }
        public string pr_hasta { get; set; }
        public string DescMes { get; set; }
    }
}