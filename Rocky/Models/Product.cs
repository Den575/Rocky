using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1,int.MinValue)]
        public double Price { get; set; }
        [Display(Name="Category Type")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryType")]
        public virtual Category Category { get; set; }
    }
}
