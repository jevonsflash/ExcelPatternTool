using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Workshop.Model.Apis;

namespace Workshop.Model.Apis
{

    public class ProcessTypeList
    {

        [JsonProperty("processId")]
        public string ProcessId { get; set; }

        [JsonProperty("processName")]
        public string ProcessName { get; set; }

        [JsonProperty("instrumentFunc")]
        public object[] InstrumentFunc { get; set; }

        [JsonProperty("dept")]
        public string Dept { get; set; }

        public ProcedureTypeInfo ProcedureTypeInfo
        {
            get
            {
                return GetProcedureTypeInfo();
            }
        }

        private ProcedureTypeInfo GetProcedureTypeInfo()
        {
            var result = new ProcedureTypeInfo();
            result.Name = this.ProcessName;
            result.Id = this.ProcessId;          
            result.Category = App.GolobelCategorys.FirstOrDefault(c => c.Code == this.Dept);
            return result;
        }
    }



    public class ProcedureTypeInfoApi: InfoApiBase
    {

        [JsonProperty("processList")]
        public ProcessTypeList[] ProcessList { get; set; }

       
    }

}
