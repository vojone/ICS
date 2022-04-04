using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record PhotoListModel(string? Url) : ModelBase
    {
        public string? Url { get; set; } = Url;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<PhotoEntity, PhotoListModel>();
            }
        }
    }
}
