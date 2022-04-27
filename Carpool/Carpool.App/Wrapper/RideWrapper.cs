using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.BL.Models;
using Carpool.Common;

namespace Carpool.App.Wrapper
{
    public class RideWrapper : ModelWrapper<RideDetailModel>
    {
        public RideWrapper(RideDetailModel model) : base(model)
        {

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
        
        //not 100% sure about these wrappers inside a wrapper, but hope its ok
        public UserWrapper? Driver
        {
            get => GetValue<UserWrapper>(); 
            set => SetValue(value);
        }
        //change to wrapper
        public ObservableCollection<ParticipantWrapper> Participants { get; init; } = new();

        public static implicit operator RideWrapper(RideDetailModel detailModel) => new(detailModel);
    }
}
