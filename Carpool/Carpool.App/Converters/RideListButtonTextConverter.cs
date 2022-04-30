using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Carpool.App.Wrapper;


namespace Carpool.App.Converters
{
    //From example project "CookBook"
    public class RideListButtonTextConver : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*if (value.DriverId == parameter)
            {
                return "Edit ride";
            }
            else
            {
                if (IsParticipant(value, parameter))
                {
                    return "Leave";
                }
                else
                {
                    return "Book";

                }
            }*/
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsParticipant(RideWrapper ride, Guid userId)
        {
            ParticipantWrapper? participant = ride.Participants.FirstOrDefault(p => p.UserId == userId);
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
}
