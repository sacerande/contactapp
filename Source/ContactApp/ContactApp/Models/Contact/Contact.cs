using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
    public enum status { InActive, Active }
    public class Contact
    {        
        public int Id { get; set; }

        [Required]
        [Display(Name="First name")]
        [StringLength(20, MinimumLength = 2)]
        [MaxLength(20, ErrorMessage = "First name can be of max 20 characters.")]
        public String firstName { get; set; }

        [Required]
        [StringLength(20,MinimumLength=2)]
        [MaxLength(20, ErrorMessage = "Last Name can be of max 20 characters.")]
        [Display(Name = "Last name")]
        public String lastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress.")]
        public String email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [RegularExpression(@"^\+\(?([0-9]{1,3})\)?[-. ]?([0-9]{4,15})$", ErrorMessage = "Phone number must have format '+Country code-Number' (e.g. for India, +91-7709862546)")]
        [Display(Name = "Phone number")]
        public String phoneNumber { get; set; }

        [UIHint("Enum")]
        [Display(Name="Status")]
        public status status { get; set; }
    }
}