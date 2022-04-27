using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Carpool.BL.Models;

namespace Carpool.App.Wrapper;

public class UserWrapper : ModelWrapper<UserDetailModel>
{
    public UserWrapper(UserDetailModel model) : base(model)
    {}

    public string? Name
    {
        get => GetValue<String>(); 
        set => SetValue(value);
    }

    public string? Surname
    {
        get => GetValue<String>();
        set => SetValue(value);
    }

    public string? PhotoUrl
    {
        get => GetValue<String>(); 
        set => SetValue(value);
    }

    public uint Rating
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    public string? Country
    {
        get => GetValue<String>(); 
        set => SetValue(value);
    }
    public ObservableCollection<CarWrapper> Cars { get; init; } = new();

    public static implicit operator UserWrapper(UserDetailModel detailModel) => new(detailModel);
}
