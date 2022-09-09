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
        DateTime RegistrationDate,
        string? PhotoUrl = null,
        string? Country = null,
        uint Rating = 0) : ModelBase
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string? PhotoUrl { get; set; } = PhotoUrl;
        public uint Rating { get; set; } = Rating;
        public string? Country { get; set; } = Country;

        public DateTime RegistrationDate { get; set; } = RegistrationDate;

        public List<CarDetailModel> Cars { get; init; } = new();

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserDetailModel>().ReverseMap();
            }
        }

        public static UserDetailModel Empty => 
            new(string.Empty,
                string.Empty,
                DateTime.Now,
                null,
                string.Empty);


        public bool DataEquals(UserDetailModel model)
        {
            return this.Country == model.Country &&
                   this.Name == model.Name &&
                   this.PhotoUrl == model.PhotoUrl &&
                   this.Surname == model.Surname &&
                   this.RegistrationDate == model.RegistrationDate &&
                   this.Rating == model.Rating;
        }
    }
}
