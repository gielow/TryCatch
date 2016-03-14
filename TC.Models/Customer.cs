using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TC.Models
{
    public class CustomerLoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid e-mail address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid e-mail address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DisplayName("Zip code")]
        public string ZipCode { get; set; }
        public List<string> Carts { get; set; }
        [Required]
        [DisplayName("House number")]
        public int HouseNumber { get; set; }

        public override bool Equals(object obj)
        {
            var obj2 = obj as Customer;
            if (obj2 == null)
                return false;

            return this.Email.Equals(obj2.Email);
        }
    }
}