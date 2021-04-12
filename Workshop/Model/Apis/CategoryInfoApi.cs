using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Model.Apis;

namespace Workshop.Model.Apis
{

    public class DeptList
    {
        private DateTime _nowtime;

        public DeptList()
        {
            _nowtime = DateTime.Now;
        }
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public CategoryInfo CategoryInfo
        {
            get
            {
                return GetProcedureInfo();
            }
        }
        private CategoryInfo GetProcedureInfo()
        {
            var result = new CategoryInfo();
            result.Id = this.Id;
            result.Name = this.Name;
            result.Code = this.Code;
            result.Description = this.Description;
            result.CreateTime = _nowtime;
            return result;
        }
    }



    public class CategoryInfoApi : InfoApiBase
    {

        [JsonProperty("deptList")]
        public DeptList[] DeptList { get; set; }
    }

}
