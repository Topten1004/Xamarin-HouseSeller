using System;
using System.Text.RegularExpressions;

namespace Immowert4You.Presentation.Common.Validators
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = "Muss eine gültige E-Mail sein.";

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
