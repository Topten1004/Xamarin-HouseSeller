namespace Immowert4You.Presentation.Common.Validators
{
    public class ParsableToFloatRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = "Wert ist keine Zahl.";

        public bool Check(T value)
        {
            try
            {
                _ = float.Parse(value.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
