using System;
using System.Collections.Generic;
using System.Text;

namespace VendorImport.Service.Models.ViewModels
{
    public class VendorResults<T>
    {
        public List<T> AddedItems { get; set; } = new List<T>();
        public List<T> RejectedItems { get; set; } = new List<T>();
        public int TotalCount { get; set; } = 0;
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "Not Implemented";
    }
}
