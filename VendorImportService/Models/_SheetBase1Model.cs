using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VendorImport.Service.Models
{
    public class _SheetBase1Model
    {
        [JsonIgnore, Key, Column(Order = 1), Required]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "confirmation"), Required, StringLength(100)]
        public string Confirmation { get; set; } = "";
        [JsonProperty(PropertyName = "property"), Required, StringLength(500)]
        public string Property { get; set; } = "";
        [JsonProperty(PropertyName = "guestFirstName"), Required, StringLength(100)]
        public string GuestFirstName { get; set; } = "";
        [JsonProperty(PropertyName = "guestLastName"), Required, StringLength(255)]
        public string GuestLastName { get; set; } = "";
        [JsonProperty(PropertyName = "creationDate"), Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [JsonIgnore, Timestamp]
        public byte[] RowVersion { get; set; }
        [NotMapped]
        public string Message { get; set; } = "";
        [JsonIgnore, Required]
        public int UploadCounter { get; set; } = 1;

        [JsonIgnore]
        public string GuestString
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    string[] str = value.Split(" ");
                    if (str.Length > 1)
                    {


                        if (str.Length == 2)
                        {
                            GuestFirstName = str[0];
                            GuestLastName = str[1];
                        }
                        else
                        {
                            GuestLastName = str[str.Length - 1];
                            string[] str1 = str.Take(str.Count() - 1).ToArray();
                            GuestFirstName = string.Join(" ", str1);
                        }
                    }
                    else
                    {
                        GuestFirstName = str[0];
                    }
                }
            }
        }
    }
}
