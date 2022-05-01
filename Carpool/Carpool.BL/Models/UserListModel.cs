using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record UserListModel(
        string Name,
        string Surname,
        uint Rating = 0) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public uint Rating { get; set; } = Rating;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserListModel>();
            }
        }

        public override string ToString()
        {
            return Name + " " + Surname + " (⭐ " + Rating + ")";
        }
    }
}
