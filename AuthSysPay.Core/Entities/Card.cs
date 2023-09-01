using System.ComponentModel.DataAnnotations;

namespace AuthSysPay.Core
{
    public class Card : BaseEntity
    {
        [MaxLength(15, ErrorMessage = "this field {0} needs 15 characters"),
         MinLength(15)]
        [RegularExpression(@"^[0-9]*$")]
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
