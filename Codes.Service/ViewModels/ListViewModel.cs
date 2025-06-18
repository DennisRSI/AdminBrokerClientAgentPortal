using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels
{
    public class ListViewModel<T>
    {
        [JsonProperty(PropertyName = "items")]
        public List<T> Items { get; set; } = new List<T>();
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; } = 0;
        [JsonProperty(PropertyName = "total_pages", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalPages { get; set; } = null;
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = "";
        [JsonProperty(PropertyName = "is_success")]
        public bool IsSuccess { get { return Message.ToUpper().IndexOf("ERROR") == -1 ? true : false; } }
        [JsonIgnore]
        public string Reference { get; set; } = null;
    }
}
