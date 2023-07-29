using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class Contacto
    {
        public int co_codigo { get; set; }
        public string co_tipo { get; set; }
        public string co_tipo_id { get; set; }
        public string co_numero_id { get; set; }
        public string co_nombre { get; set; }
        public string co_nombre_alterno { get; set; }
        public string co_contacto { get; set; }
        public string co_actividad { get; set; }
        public string co_usuario { get; set; }
        public int co_relacionado { get; set; }
        public string co_estado { get; set; }
        public int co_empresa { get; set; }
        public int co_oficina { get; set; }
        public string co_operador { get; set; }
        public string co_terminal { get; set; }
        public DateTime co_fecha_act { get; set; }
    }
}