using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [Index("Category_Index", IsUnique = true)]
        [MinLength(3)]
        [MaxLength(80)]
        public string CategorName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}