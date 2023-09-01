using System.ComponentModel.DataAnnotations;

namespace AuthSysPay.Core
{
    public class Card : BaseEntity
    {
        [MaxLength(15, ErrorMessage = "this field {0} needs 15 characters")]
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
