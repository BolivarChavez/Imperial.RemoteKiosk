using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class Cliente
    {
        public Int32 cl_codigo { get; set; }
        [StringLength(1)]
        public string cl_tipo_id { get; set; }
        [StringLength(15)]
        public string cl_numero_id { get; set; }
        [StringLength(250)]
        public string cl_nombre { get; set; }
        public Int16 cl_pais { get; set; }
        [StringLength(80)]
        public string cl_provincia { get; set; }
        [StringLength(80)]
        public string cl_ciudad { get; set; }
        [StringLength(400)]
        public string cl_direccion { get; set; }
        [StringLength(100)]
        public string cl_telefono { get; set; }
        [StringLength(150)]
        public string cl_email { get; set; }
        [StringLength(15)]
        public string cl_usuario { get; set; }
        [StringLength(200)]
        public string cl_password { get; set; }
        [StringLength(1)]
        public string cl_estado { get; set; }
        [StringLength(10)]
        public string cl_fecha_vigencia { get; set; }
    }
}