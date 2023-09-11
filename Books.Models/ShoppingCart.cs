using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models
{
    public class ShoppingCart
    {
        // Product that has been added, count of the product, and the user who added it
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1,1000,ErrorMessage = "Please Enter a value between 1 and 1000")]
        public int Count { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; } // This shopping cart is specific to only this user
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}