using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.App.Extensions;
using Carpool.BL.Facades;
using Carpool.BL.Models;
using Carpool.Common;

namespace Carpool.App.Wrapper
{
    public class RideWrapper : ModelWrapper<RideDetailModel>
    {
        public RideWrapper(RideDetailModel model) : base(model)
        {
            InitializeCollectionProperties(model);
        }

        public string? DepartureL
        {
            get => GetValue<String>(); 
            set => SetValue(value);
        }

        public string? ArrivalL
        {
            get => GetValue<String>(); 
            set => SetValue(value);
        }

        public DateTime DepartureT
        {
            get => GetValue<DateTime>(); 
            set => SetValue(value);
        }
        public DateTime ArrivalT
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public uint InitialCapacity
        {
            get => GetValue<uint>(); 
            set => SetValue(value);
        }

        public uint Capacity
        {
            get => GetValue<uint>(); 
            set => SetValue(value);
        }

        public RideState State
        {
            get => GetValue<RideState>();
            set => SetValue(value);
        }

        public Guid CarId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public Guid DriverId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public CarWrapper? Car
        {
            get => GetValue<CarWrapper>(); 
            set => SetValue(value);
        }

        private void InitializeCollectionProperties(RideDetailModel model)
        {
            if (model.Participants == null)
            {
                throw new ArgumentException("Cars cannot be null");
            }
            Participants.AddRange(model.Participants.Select(e => new ParticipantWrapper(e)));

            RegisterCollection(Participants, model.Participants);
        }

        //not 100% sure about these wrappers inside a wrapper, but hope its ok
        public UserWrapper? Driver
        {
            get => GetValue<UserWrapper>(); 
            set => SetValue(value);
        }

        public override void Validate(string? propertyName = null)
        {
            if (propertyName is null or nameof(DepartureL))
            {
                if (DepartureL == string.Empty)
                {
                    AddError(nameof(DepartureL), "The departure location cannot be empty!");
                }
            }

            if (propertyName is null or nameof(ArrivalL))
            {
                if (ArrivalL == string.Empty)
                {
                    AddError(nameof(ArrivalL), "The destination cannot be empty!");
                }
            }

            if (propertyName is null or nameof(Car))
            {
                if (Car == CarDetailModel.Empty || Car == null)
                {
                    AddError(nameof(Car), "A car must be selected!");
                }
            }
        }

        public ObservableCollection<ParticipantWrapper> Participants { get; init; } = new();

        public static implicit operator RideWrapper(RideDetailModel detailModel) => new(detailModel);
    }
}
