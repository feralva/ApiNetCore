using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.RequestHttp
{
    public class RequestModify<T> where T: IDTO
    {
        public T Model { get; set; }
}
}
