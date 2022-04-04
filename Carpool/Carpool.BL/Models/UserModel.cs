using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record UserModel(
        string Name,
        string Surname,
        Guid? PhotoId,
        string? Country,
        uint Rating) : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid? PhotoId { get; set; }
        public uint Rating { get; set; }
        public string Country { get; set; }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserModel>();
            }
        }
    }
}
