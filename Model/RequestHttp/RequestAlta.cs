using System;
using System.Collections.Generic;
using System.Text;

namespace Model.RequestHttp
{
    public class RequestAlta<T> where T: IDTO
    {
        public T Model { get; set; }
    }
}
