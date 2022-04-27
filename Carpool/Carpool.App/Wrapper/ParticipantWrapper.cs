using System;
using Carpool.BL.Models;

namespace Carpool.App.Wrapper;

public class ParticipantWrapper : ModelWrapper<ParticipantModel>
{
    public ParticipantWrapper(ParticipantModel model) : base(model)
    { }

    public Guid UserId
    {
        get => GetValue<Guid>(); 
        set => SetValue(value);
    }

    public string? UserName
    {
        get => GetValue<string>(); 
        set => SetValue(value);
    }

    public string? UserSurname
    {
        get => GetValue<string>(); 
        set => SetValue(value);
    }

    public uint UserRating
    {
        get => GetValue<uint>(); 
        set => SetValue(value);
    }

    public bool HasUserRated
    {
        get => GetValue<bool>(); 
        set => SetValue(value);
    }

    public static implicit operator ParticipantWrapper(ParticipantModel detailModel) => new(detailModel);

}
