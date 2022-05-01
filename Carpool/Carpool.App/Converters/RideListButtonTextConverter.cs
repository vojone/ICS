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
    //From example project "CookBook"
    public class RideListButtonTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return "";
            }
            RideListModel ride = (RideListModel) values[0];
            Guid currentUserId = (Guid)values[1];

            if (ride.DriverId == currentUserId)
            {
                return "Edit ride";
            }
            else
            {
                if (ParticipantModel.IsParticipant(ride, currentUserId))
                {
                    return "Leave";
                }
                else
                {
                    return "Book";

                }
            }
            return ride.Driver.Name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
