using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kouch.App.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public List<Comment> Children { get; set; }
        [NotMapped]
        public bool HaveChildren => Children!=null&&Children.Count!=0;
    }
}
