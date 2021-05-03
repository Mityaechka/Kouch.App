using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Kouch.App.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [JsonProperty("preview_image")]
        public File Preview { get; set; }
        public List<File> Files { get; set; }
        public bool IsBookmark { get; set; } = true;
        [NotMapped]
        public bool HasPreview => Preview != null;

        public List<File> AllFiles
        {
            get
            {
                List<File> files = new List<File>();
                if (Preview != null)
                {
                    files.Add(Preview);
                }
                if (Files != null)
                {
                    files.AddRange(Files);
                }
                return files;
            }
        }
    }
}
