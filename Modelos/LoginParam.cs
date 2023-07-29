using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class LoginParam
    {
        [StringLength(15)]
        public string usuario { get; set; }
        [StringLength(200)]
        public string password { get; set; }
    }
}