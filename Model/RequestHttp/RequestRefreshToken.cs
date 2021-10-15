using System;
using System.Collections.Generic;
using System.Text;

namespace Model.RequestHttp
{
    public class RequestRefreshToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
