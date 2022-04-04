using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record UserDetailModel(
        string Name,
        string Surname,
        Guid? PhotoId,
        string? Country,
        uint Rating) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public Guid? PhotoId { get; set; } = PhotoId;
        public uint Rating { get; set; } = Rating;
        public string? Country { get; set; } = Country;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserDetailModel>().ReverseMap();
            }
        }

        public static UserDetailModel Empty => new(String.Empty, String.Empty, default,String.Empty, default);
    }
}
