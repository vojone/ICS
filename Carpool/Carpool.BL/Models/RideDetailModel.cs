using AutoMapper;
using Carpool.Common;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record RideDetailModel(
        string DepartureL,
        string ArrivalL,
        DateTime DepartureT,
        DateTime ArrivalT,
        uint InitialCapacity,
        uint Capacity,
        RideState State,
        Guid CarId,
        Guid DriverId) : ModelBase
    {
        public string DepartureL { get; set; } = DepartureL;
        public string ArrivalL { get; set; } = ArrivalL;
        public DateTime DepartureT { get; set; } = DepartureT;
        public DateTime ArrivalT { get; set;} = ArrivalT;
        public uint InitialCapacity { get; set; } = InitialCapacity;
        public uint Capacity { get; set; } = Capacity;
        public RideState State { get; set;} = State;
        public Guid CarId { get; set; } = CarId;
        public Guid DriverId { get; set; } = DriverId;
        public CarDetailModel? Car { get; set; }
        public UserDetailModel? Driver { get; set; }
        public List<ParticipantModel> Participants { get; init; } = new();
        

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<RideEntity, RideDetailModel>()
                    .ReverseMap();
            }
        }

        public static RideDetailModel Empty => 
            new(string.Empty,
                string.Empty, 
                DateTime.Now,
                DateTime.Now.AddHours(1.0),
                default,
                default,
                RideState.None,
                default,
                default);
    }
}

