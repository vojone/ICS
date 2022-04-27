using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.BL.Models;

namespace Carpool.App.Wrapper
{
    public class CarPhotoWrapper : ModelWrapper<CarPhotoModel>
    {
        public CarPhotoWrapper(CarPhotoModel model) : base(model)
        {

        }

        public String? Url
        {
            get => GetValue<String>();
            set => SetValue(value);
        }

        public static implicit operator CarPhotoWrapper(CarPhotoModel detailModel) => new(detailModel);
    }
}
