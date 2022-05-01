using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Carpool.App.Services;
using Carpool.BL.Models;

namespace Carpool.App.Wrapper;

public class UserWrapper : ModelWrapper<UserDetailModel>
{
    public UserWrapper(UserDetailModel model) : base(model)
    {
    }

    public override void Validate(string? propertyName = null)
    {
        if (propertyName is null or nameof(Name))
        {
            if (Name == string.Empty)
            {
                AddError(nameof(Name), "The user name cannot be empty!");
            }
        }

        if (propertyName is null or nameof(Surname))
        {
            if (Surname == string.Empty)
            {
                AddError(nameof(Surname), "The user surname cannot be empty!");
            }
        }
    }


    public string? Name
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string? Surname
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public DateTime RegistrationDate
    {
        get => GetValue<DateTime>();
        set => SetValue(value);
    }

    public string? PhotoUrl
    {
        get => GetValue<string>(); 
        set => SetValue(value);
    }

    public uint Rating
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    public string? Country
    {
        get => GetValue<string>(); 
        set => SetValue(value);
    }
    public ObservableCollection<CarListModel> Cars { get; init; } = new();

    public static implicit operator UserWrapper(UserDetailModel detailModel) => new(detailModel);


    
}
