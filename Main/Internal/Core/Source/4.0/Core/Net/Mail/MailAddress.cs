namespace EyeSoft.Core.Net.Mail
{
    using System.Text.RegularExpressions;

    public static class MailAddress
    {
        public static bool IsValid(string mailAddress)
        {
            const string Format =
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            return Regex.IsMatch(mailAddress, Format, RegexOptions.IgnoreCase);
        }
    }
}