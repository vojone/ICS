using AutoMapper;
using Carpool.Common;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record RideDetailModel(
        Guid DepartureLId,
        Guid ArrivalLId,
        DateTime DepartureT,
        DateTime ArrivalT,
        uint InitialCapacity,
        uint Capacity,
        RideState State,
        Guid CarId,
        Guid DriverId) : ModelBase
    {
        public Guid DepartureLId { get; set; } = DepartureLId;
        public Guid ArrivalLId { get; set; } = ArrivalLId;
        public DateTime DepartureT { get; set; } = DepartureT;
        public DateTime ArrivalT { get; set;} = ArrivalT;
        public uint InitialCapacity { get; set; } = InitialCapacity;
        public uint Capacity { get; set; } = Capacity;
        public RideState State { get; set;} = State;
        public Guid CarId { get; set; } = CarId;
        public Guid DriverId { get; set; } = DriverId;
        public LocationEntity? DepartureL { get; set; }
        public LocationEntity? ArrivalL { get; set; }
        public CarEntity? Car { get; set; }
        public UserEntity? Driver { get; set; }
        public ICollection<ParticipantEntity> Participants { get; set; } = new List<ParticipantEntity>();
        

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<RideEntity, RideDetailModel>().ReverseMap();
            }
        }

        public static RideDetailModel Empty => 
            new(default,
                default,
                default,
                default,
                default,
                default,
                RideState.None,
                default,
                default);
    }
}

