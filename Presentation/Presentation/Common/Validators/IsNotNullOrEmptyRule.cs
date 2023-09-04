namespace Immowert4You.Presentation.Common.Validators
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = "Feld ist erforderlich.";

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value.ToString();
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
