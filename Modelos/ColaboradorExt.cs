using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class ColaboradorExt
    {
        public int co_empresa { get; set; }
        public string pc_cedula { get; set; }
        public string pc_apellido1 { get; set; }
        public string pc_apellido2 { get; set; }
        public string pc_nombre1 { get; set; }
        public string pc_nombre2 { get; set; }
        public DateTime pc_fecha_ingreso { get; set; }
        public string pc_cargo { get; set; }
        public string pc_division { get; set; }
        public string pc_area { get; set; }
        public double pc_sueldo { get; set; }
        public string pc_tipo_colaborador { get; set; }
        public string pc_tipo_contrato { get; set; }
        public string pc_ciudad_labores { get; set; }
        public string pc_ciudad_residencia { get; set; }
        public string pc_domicilio { get; set; }
        public string pc_telefono { get; set; }
        public string pc_email { get; set; }
        public DateTime pc_fecha_actualizacion { get; set; }
        public string pc_estado { get; set; }
        public string pc_operador { get; set; }
        public DateTime pc_fecha_registro { get; set; }
    }
}