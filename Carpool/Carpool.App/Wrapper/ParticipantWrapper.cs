using System;
using System.Collections.Generic;
using System.Linq;
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

    public override string ToString()
    {
        return UserName+" "+UserSurname;
    }

    public static implicit operator ParticipantWrapper(ParticipantModel detailModel) => new(detailModel);

    public static bool IsParticipant(RideWrapper model, Guid userId)
    {
        return IsParticipant(model.Participants, userId);
    }

    private static bool IsParticipant(IEnumerable<ParticipantWrapper> Participants, Guid userId)
    {
        ParticipantWrapper? participant = Participants.FirstOrDefault(p => p.UserId == userId);
        if (participant != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
