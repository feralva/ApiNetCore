using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LoginModel:IDTO
    {
        [JsonProperty(PropertyName = "user")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "pass")]
        public string Password { get; set; }
    }
}
