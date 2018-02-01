using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.ViewModels
{
    public class DataTableViewModel<T>
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }
        public int NumberOfRows { get; set; }
        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }
        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonProperty(PropertyName = "data")]
        public T[] Data { get; set; }
        [JsonProperty(PropertyName = "header_value")]
        public string HeaderValue { get; set; } = "";
        [JsonProperty(PropertyName = "role_name")]
        public string RoleName { get; set; } = "";
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = null;
        
    }
}
