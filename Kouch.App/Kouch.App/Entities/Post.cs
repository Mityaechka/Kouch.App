using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kouch.App.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Preview { get; set; }
        public List<File> Files { get; set; }
        [NotMapped]
        public bool HasPreview => Preview != null;

        public List<File> AllFiles
        {
            get
            {
                List<File> files = new List<File>();
                if (Preview != null)
                {
                    files.Add(new File
                    {
                        Path = Preview
                    });
                }
                if (Files != null)
                {
                    files.AddRange(Files);
                }
                return files;
            }
        }
    }
    public class File
    {
        public string Path { get; set; }
    }
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
