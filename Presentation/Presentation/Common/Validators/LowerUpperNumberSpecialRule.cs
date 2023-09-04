using System.Linq;

namespace Immowert4You.Presentation.Common.Validators
{
    public class LowerUpperNumberSpecialRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value.Any(char.IsUpper) &&
                    value.Any(char.IsLower) &&
                    value.Any(char.IsDigit);
        }
    }
}
