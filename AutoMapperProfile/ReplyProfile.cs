using AutoMapper;
using Enigmachan.Models;
using enigmachan_backend.Models;

namespace Tickets
{
    public class ReplyProfile: Profile
    {
        public ReplyProfile()
        {
            CreateMap<postReplyRequest, Reply>();
        }
        
    }
}