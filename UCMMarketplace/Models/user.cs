//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UCMMarketplace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.items = new HashSet<item>();
        }

        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(255, ErrorMessage = "Must be minimum 8 characters", MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        //[Required]
        [RegularExpression(@"^(?!00000)[0-9]{9,9}$", ErrorMessage = "Enter valid Student ID")]
        public Nullable<int> StudentId { get; set; }
        [Display(Name = "Email Address")]
        // [Required(ErrorMessage = "Email is required.")]
        // [RegularExpression(@"^[a-zA-Z0-9\-\.]+\ucmo.(EDU)$", ErrorMessage = "Enter valid University Email Addres")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item> items { get; set; }
        // public virtual ICollection<wishlist> wishlists { get; set; }
    }
}
