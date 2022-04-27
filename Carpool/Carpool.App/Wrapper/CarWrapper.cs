using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Carpool.BL.Models;
using Carpool.Common;

namespace Carpool.App.Wrapper
{
    public class CarWrapper : ModelWrapper<CarDetailModel>
    {
        public CarWrapper(CarDetailModel model) : base(model)
        {
            
        }

        public string? Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? Brand
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public CarType Type
        {
            get => GetValue<CarType>();
            set => SetValue(value);
        }

        public DateTime Registration
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public uint Seats
        {
            get => GetValue<uint>();
            set => SetValue(value);
        }

        /*public List<CarPhotoModel> Photos
        {
            get => GetValue<List<CarPhotoModel>>();
            set => SetValue(value);
        }*/
        private ObservableCollection<CarPhotoWrapper> Photos = new();

        public static implicit operator CarWrapper(CarDetailModel detailModel) => new(detailModel);

        public static implicit operator CarDetailModel(CarWrapper wrapper) => wrapper.Model;

    }
}


