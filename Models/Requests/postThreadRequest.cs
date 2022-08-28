using System;

namespace enigmachan_backend.Models
{
    public class postThreadRequest
    {
        public string text { get; set; }
        public string[] image_urls { get; set; }
        public DateTime postDateTime { get; set; } 
        public string[]? reply_to { get; set; }
    }
}