using AutoMapper;
using Carpool.Common;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record RideListModel(
        DateTime DepartureT,
        DateTime ArrivalT,
        uint InitialCapacity,
        uint Capacity,
        RideState State) : ModelBase
    {
        public DateTime DepartureT { get; set; }
        public DateTime ArrivalT { get; set; }
        public uint InitialCapacity { get; set; }
        public uint Capacity { get; set; }
        public RideState State { get; set; }
        public LocationEntity? DepartureL { get; set; }
        public LocationEntity? ArrivalL { get; set; }
        public UserEntity? Driver { get; set; }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<RideEntity, RideListModel>().ReverseMap();
            }
        }

        //public static RideDetailModel Empty => new();
    }
}
