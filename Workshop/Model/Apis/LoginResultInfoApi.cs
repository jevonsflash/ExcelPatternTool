using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model.Apis
{

    public class LoginResultInfoApi
    {

        [JsonProperty("loginTime")]
        public string LoginTime { get; set; }

        [JsonProperty("dept")]
        public string Dept { get; set; }

        [JsonProperty("errorno")]
        public int Errorno { get; set; }

        [JsonProperty("session")]
        public string Session { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string[] Role { get; set; }

    }
}
