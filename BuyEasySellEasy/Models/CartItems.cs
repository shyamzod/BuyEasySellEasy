using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BuyEasySellEasy.Models
{
    public class CartItems
    {
        [Key]
        public long CartItemId { get; set; }

        [ForeignKey("AspNetUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }

        [ForeignKey("Products")]
        public long ProductId { get; set; }

        public Products Products { get; set; }
    }
}