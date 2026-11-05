using SalonData;

namespace SalonBusinessLogic
{
    public class AuthenticationLogic
    {
        UserData userData = new UserData();

        public bool IsAdminLoginCorrect(string username, string password)
        {
            return username == userData.adminUsername && password == userData.adminPassword;
        }

        public bool IsCustomerNameValid(string name)
        {
            return name != "" && name != null;
        }
    }
}
