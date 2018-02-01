using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class _BaseViewModel
    {
        [Display(Name = "Is Active", Prompt = "Is Active")]
        public bool IsActive { get; set; } = true;
        [Display(Name = "Creation Date", Prompt = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Creator IP", Prompt = "Creator IP")]
        public string CreatorIP { get; set; }
        [Display(Name = "Deactivation Date", Prompt = "Deactivation Date")]
        public DateTime? DeactivationDate { get; set; }
        [Display(Name = "Message", Prompt = "Message")]
        public string Message { get; set; }
        [Display(Name = "Is Success", Prompt = "Is Success")]
        public bool IsSuccess { get { return Message.ToUpper().IndexOf("ERROR") == -1 ? true : false; } }
    }
}
