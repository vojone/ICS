using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record ParticipantModel(
        Guid UserId,
        string UserName,
        string UserSurname,
        uint UserRating,
        bool HasUserRated = false
    ) : ModelBase
    {
        public Guid UserId { get; set; } = UserId;
        public string UserName { get; set; } = UserName;
        public string UserSurname { get; set; } = UserSurname;
        public uint UserRating { get; set; } = UserRating;
        public bool HasUserRated { get; set; } = HasUserRated;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ParticipantEntity, ParticipantModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.User, expression => expression.Ignore())
                    .ForMember(entity => entity.Ride, expression => expression.Ignore())
                    .ForMember(entity => entity.RideId, expression => expression.Ignore());
            }
        }
        
    }
}
