using AutoMapper;
using Enigmachan.Models;
using enigmachan_backend.Models;

namespace Tickets
{
    public class ThreadProfile: Profile
    {
        public ThreadProfile()
        {
            CreateMap<postThreadRequest, Enigmachan.Models.Thread>();
            CreateMap<Enigmachan.Models.Thread, Reply>()
                .ForMember(dest => dest.mainPostId, opt => opt.MapFrom(src => src.post_id));
        } 
    }
}