using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingTemple.BoardGameStore.Models
{
    public class CheckoutModel
    {
        [Required]
        public DateTime? CreditCardExpiration { get; set; }

        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CreditCardName { get; set; }

        [Required]
        public string CreditCardVerificationValue { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string ShippingAddress2 { get; set; }


        [Required(ErrorMessage = "You need to specify a city")]
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }

        [Required(ErrorMessage = "You need to specify a city")]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(12)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}