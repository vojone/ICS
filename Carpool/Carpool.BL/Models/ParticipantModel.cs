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

        public static bool IsParticipant(RideListModel ride, Guid userId)
        {
            return IsParticipant(ride.Participants, userId);
        }

        public static bool IsParticipant(RideDetailModel ride, Guid userId)
        {
            return IsParticipant(ride.Participants, userId);
        }

        public static bool IsParticipant(IEnumerable<ParticipantModel> Participants, Guid userId)
        {
            ParticipantModel? participant = Participants.FirstOrDefault(p => p.UserId == userId);
            if (participant != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
