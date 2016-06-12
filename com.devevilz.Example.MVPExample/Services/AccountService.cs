using com.devevilz.Example.MVPExample.Model;
using System;

namespace com.devevilz.Example.MVPExample.Services
{
    public class AccountService
    {
        public Account getAccount(string username, string password)
        {
            Account toReturn = null;

            //Immaginiamo di recuperare un account, sfruttando le informazioni passate in input,
            //attraverso chiamate al data service layer implementato, ad esempio, attraverso un ORM
            //come hibernate

            if (!String.IsNullOrEmpty(username))
            {
                if (username.ToLower() == "devevil")
                    toReturn = new Account { Username = "devevil", Password = "pwd", IsEnabled=true };
                else if (username.ToLower() == "username")
                    toReturn = new Account { Username = "username", Password = "pwd", IsEnabled = false };
            }
            else
                throw new Exception("AccountService - Username is null");

            return toReturn;
        }
    }
}
