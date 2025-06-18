using System;
using System.Collections.Generic;
using System.Text;

namespace VendorImport.Service.Models.ViewModels
{
    public class VendorResult<T>
    {
        public T Item { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "Not Implemented";
    }
}
