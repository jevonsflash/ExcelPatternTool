using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model.Apis
{

    public class ProcessList
    {

        [JsonProperty("processName")]
        public string ProcessName { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("processId")]
        public string ProcessId { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("manhour")]
        public double Manhour { get; set; }

    }

    public class SapList
    {
        [JsonProperty("dept")]
        public string Dept { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("sapId")]
        public string SapId { get; set; }

        [JsonProperty("processList")]
        public ProcessList[] ProcessList { get; set; }

        public ProductTypeInfo ProductTypeInfo
        {
            get
            {
                return GetProductTypeInfo();
            }
        }

        private ProductTypeInfo GetProductTypeInfo()
        {
            var result = new ProductTypeInfo();
            result.Id = this.SapId;
            result.Name = this.SapId;
            result.Description = this.ProductName;
            result.ProcedureInfos = this.ProcessList.Select(c => new ProcedureInfo
            {
                ProcedureTypeInfo = new ProcedureTypeInfo()
                {
                    Id = c.ProcessId,
                    Name = c.ProcessName,


                },
                Price = c.Price,
                Duration = c.Manhour,
                Group = c.Group

            }).ToList();
            result.Category = App.GolobelCategorys.FirstOrDefault(c => c.Code == this.Dept);
            return result;
        }
    }




    public class ProductTypeInfoApi : InfoApiBase
    {

        [JsonProperty("sapList")]
        public SapList[] SapList { get; set; }





    }
}