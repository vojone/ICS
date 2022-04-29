using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.IdentityModel.Tokens;

namespace Carpool.App.Services
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
        {
            bool isValid = !(value == null || (string) value == String.Empty);

            return new ValidationResult(isValid, "Invalid value!");
        }
    }
}
