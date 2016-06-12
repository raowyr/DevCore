using System;
using System.Text.RegularExpressions;

namespace com.devevil.Common.Utils.Validator
{
    public static class StringValidator
    {
        public static bool IsValidMail(this string prmEmail)
        {
            if (!String.IsNullOrEmpty(prmEmail))
                return Regex.IsMatch(prmEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            else
                return false;
        }

        public static bool CheckIsValidUsername(string prmUsername)
        {
            if (!String.IsNullOrEmpty(prmUsername))
                return true;
            else
                return false;
        }
    }
}
