using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    [MetadataType(typeof(LoginModel))]
    public partial class User
    {

    }
    public class LoginModel
    {
        
        public int UserId { get; set; }
        [Display(Name = "Email address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address required")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}