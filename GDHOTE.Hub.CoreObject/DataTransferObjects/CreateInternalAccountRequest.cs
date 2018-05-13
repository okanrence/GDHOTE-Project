using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateInternalAccountRequest
    {
        [Required(ErrorMessage = "Enter a valid bank account number")]
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Enter a valid bank account name")]
        [DisplayName("Account Name")]
        public string AccountName { get; set; }
        [Required(ErrorMessage = "Select a valid bank")]
        public int BankId { get; set; }
        [Display(Name = "Account Type")]
        public int AccountTypeId { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
    }
}
