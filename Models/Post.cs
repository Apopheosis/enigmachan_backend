using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enigmachan.Models
{
    public class Post
    {
        public Post(long post_id, long thread_id)
        {
            this.post_id = post_id;
            this.thread_id = thread_id;
        }
        [Key] [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int key { get; set; }
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long post_id { get; set; }
        public long thread_id { get; set; }
    }
}