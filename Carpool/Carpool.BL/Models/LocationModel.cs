using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record LocationModel(
        string State,
        string Town,
        string Street,
        string? Description) : ModelBase
    {
        public string State { get; set; } = State;
        public string Town { get; set; } = Town;
        public string Street { get; set; } = Street;
        public string? Description { get; set; } = Description;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<LocationEntity, LocationModel>();
            }
        }
    }
}
