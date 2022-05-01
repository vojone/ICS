using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Carpool.App.Wrapper;
using Carpool.BL.Models;


namespace Carpool.App.Converters
{
    public class RideHistoryButtonTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return "";
            }

            RideListModel ride = (RideListModel)values[0];
            Guid currentUserId = (Guid)values[1];

            if (ride.DriverId == currentUserId)
            {
                return "Your ride";
            }
            else
            {
                ParticipantModel currentParticipant = GetParticipant(ride, currentUserId);
                if (!currentParticipant.HasUserRated)
                {
                    return "Add ⭐";
                }
                else
                {
                    return "Rated";
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private ParticipantModel? GetParticipant(RideListModel ride, Guid userId)
        {
            return ride.Participants.FirstOrDefault(p => p.UserId == userId);
        }
    }
}
