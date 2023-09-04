namespace Immowert4You.Presentation.Common.Validators
{
    public class MaxLengthRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        private int _length;

        public MaxLengthRule(int length)
        {
            _length = length;
        }

        public bool Check(string value)
        {
            if (value == null)
            {
                return true;
            }
            if (value.Length > _length)
            {
                return false;
            }

            return true;
        }
    }
}
