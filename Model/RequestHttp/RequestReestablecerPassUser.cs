using System;
using System.Collections.Generic;
using System.Text;

namespace Model.RequestHttp
{
    public class RequestReestablecerPassUser
    {
        public string emailUsuario { get; set; }
        public string nuevaPass { get; set; }
        public string tokenRefresh { get; set; }
    }
}
