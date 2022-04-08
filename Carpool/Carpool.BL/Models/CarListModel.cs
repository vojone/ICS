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
    public record CarListModel(
        string Name,
        string Brand
    ) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Brand { get; set; } = Brand;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEntity, CarListModel>();
            }
        }
    }
}
