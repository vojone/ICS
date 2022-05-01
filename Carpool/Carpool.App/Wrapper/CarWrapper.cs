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

        public override void Validate(string? propertyName = null)
        {
            if (propertyName is null or nameof(Name))
            {
                if (Name == string.Empty)
                {
                    AddError(nameof(Name), "The car name cannot be empty!");
                }
            }

            if (propertyName is null or nameof(Brand))
            {
                if (Brand == string.Empty)
                {
                    AddError(nameof(Brand), "The brand cannot be empty!");
                }
            }
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

        public string? Photo
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

        public Guid OwnerId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public static implicit operator CarWrapper(CarDetailModel detailModel) => new(detailModel);

        public static implicit operator CarDetailModel(CarWrapper wrapper) => wrapper.Model;

        public bool DataEquals(CarDetailModel model)
        {
            return this.Name == model.Name &&
                   this.Brand == model.Brand &&
                   this.Photo == model.Photo &&
                   this.Registration == model.Registration &&
                   this.Seats == model.Seats &&
                   this.Type == model.Type;
        }
    }
}


