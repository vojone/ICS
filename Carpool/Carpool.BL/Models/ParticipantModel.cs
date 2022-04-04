using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record ParticipantModel(
        Guid UserId,
        Guid RideId,
        bool hasUserRated
    ) : ModelBase
    {
        public Guid UserId { get; set; } = UserId;
        public Guid RideId { get; set;} = RideId;
        public bool HasUserRated { get; set; } = hasUserRated;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ParticipantEntity, ParticipantModel>();
            }
        }
        
    }
}
