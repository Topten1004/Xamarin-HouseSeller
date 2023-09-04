namespace Immowert4You.Presentation.Common.Validators
{
    public class MinLengthRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private int _length;

        public MinLengthRule(int length)
        {
            _length = length;
        }

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }
            if (value.Length < _length)
            {
                return false;
            }

            return true;
        }
    }
}
