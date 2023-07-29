using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class Documento
    {
        public Int32 do_cliente { get; set; }
        public Int32 do_codigo { get; set; }
        [StringLength(2)]
        public string do_tipo { get; set; }
        [StringLength(400)]
        public string do_descripcion { get; set; }
        [StringLength(400)]
        public string do_ruta { get; set; }
        [StringLength(1)]
        public string do_estado { get; set; }
        [StringLength(25)]
        public string do_operador { get; set; }
    }
}