namespace Immowert4You.Presentation.Common.Validators
{
    public class MatchPasswordRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; } = "Passwörter müssen übereinstimmen.";
        public string PasswordToMatch { get; set; }

        public bool Check(string value)
        {
            return value == PasswordToMatch;
        }
    }
}
