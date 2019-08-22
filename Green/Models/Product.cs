using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        [MaxLength(255)]
        public string Description { get; set; }
        [Display(Name = "Qty in Stock")]
        public int QuantityInStock { get; set; }
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public virtual Category Category { get; set; }
    }
}