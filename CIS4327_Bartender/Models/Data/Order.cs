using CIS4327_Bartender.Models.Cart;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CIS4327_Bartender.Models.Data
{
    public class Order
    {
        [Key]
        [BindNever]
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        public bool IsReady { get; set; } 
        public decimal Price { get; set; }
    }
}
