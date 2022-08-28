using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enigmachan.Models
{
    public class Thread
    {
        [Key]
        public long post_id { get; set; }
        public string[] image_urls { get; set; }
        public string text { get; set; }
        public string? title { get; set; }
        public DateTime postDateTime { get; set; }
        public List<string>? reply_to { get; set; }
        public List<string>? replies { get; set; }
        public int bumps { get; set; }
        public ICollection<Reply> threadReplies { get; set; }
    }
}