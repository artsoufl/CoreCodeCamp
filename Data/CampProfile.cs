using AutoMapper;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCodeCamp.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>().ReverseMap(); //.MaxDepth(3).PreserveReferences();
            //.ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName));
            //this.CreateMap<Talk, TalkModel>();

            this.CreateMap<CampModel, Camp>()
                .ForPath(c => c.Location.VenueName, o => o.MapFrom(m =>m.LocationVenueName));
            //this.CreateMap<TalkModel, Talk>();
            this.CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Camp, opt => opt.Ignore()) // ignoring the mapping
                .ForMember(t => t.Speaker, opt => opt.Ignore());

            this.CreateMap<Speaker, SpeakerModel>().ReverseMap();
            //this.CreateMap<Speaker, SpeakerModel>();
        }
    }
}
