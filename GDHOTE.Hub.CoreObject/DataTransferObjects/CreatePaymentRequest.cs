using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePaymentRequest
    {
        public int MemberKey { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid Amount")]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }
        [Required]
        [Display(Name = "Mode of Payment")]
        public int PaymentModeId { get; set; }
        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [Required]
        public string Narration { get; set; }
    }
}
