using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBaseQ.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
        public bool  IsDelete { get; set; }
    }
}
