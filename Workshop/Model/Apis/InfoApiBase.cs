using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model.Apis
{
    public class InfoApiBase
    {
        [JsonProperty("errorno")]
        public int Errorno { get; set; }

        [JsonProperty("errormsg")]
        public string ErrorMsg { get; set; }
    }
}
