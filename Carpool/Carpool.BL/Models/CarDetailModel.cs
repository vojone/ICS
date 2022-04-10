using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.Common;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record CarDetailModel(
        string Name,
        string Brand,
        CarType Type,
        DateOnly Registration,
        uint Seats
    ) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Brand { get; set; } = Brand;
        public CarType Type { get; set; } = Type;
        public DateOnly Registration { get; set; } = Registration;
        public uint Seats { get; set; } = Seats;
        public List<CarPhotoModel> Photos { get; init; } = new();
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEntity, CarDetailModel>()
                    .ReverseMap();
            }
        }

        public static CarDetailModel Empty =>
            new(string.Empty,
                string.Empty,
                CarType.None,
                default,
                0);
    }
}
