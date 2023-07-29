using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class Operador
    {
        public int op_codigo { get; set; }
        public int op_id_contacto { get; set; }
        public string op_usuario { get; set; }
        public string op_nombre { get; set; }
        public string op_password { get; set; }
        public DateTime op_fecha_validez { get; set; }
        public int op_intentos { get; set; }
        public string op_estado { get; set; }
        public string op_operador { get; set; }
        public string op_terminal { get; set; }
        public DateTime op_fecha_act { get; set; }
        public int op_cliente { get; set; }
    }
}