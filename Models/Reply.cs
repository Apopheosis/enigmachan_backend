using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enigmachan.Models
{
    public class Reply
    {
        [Key]
        public long post_id { get; set; }
        public string[]? image_urls { get; set; }
        public string? text { get; set; }
        public DateTime postDateTime { get; set; }
        public List<string>? reply_to { get; set; }
        public List<string>? replies { get; set; }
        [ForeignKey("Thread")]
        public long mainPostId { get; set; }
    }
}