using System;
using System.Collections.Generic;
using System.Text;

namespace SalonBusinessLogic
{
    public class AuthenticationLogic
    {
        string adminUsername = "Lhynz";
        string adminPassword = "1234";

        public bool IsAdminLoginCorrect(string username, string password)
        {
            return username == adminUsername && password == adminPassword;
        }

        public bool IsCustomerNameValid(string name)
        {
            return name != "" && name != null;
        }
    }
}
