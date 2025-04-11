using SV21T1020152.DataLayers;
using SV21T1020152.DomainModels;

namespace SV21T1020152.BusinessLayers
{
    /// <summary>
    /// Các dịch vụ liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        public static EmployeeUserAccountDAL EmployeeAccountDB;
        public static CustomerUserAccountDAL CustomerAccountDB;
        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {
            string connectionString = Configuration.ConnectionString;

            EmployeeAccountDB = new EmployeeUserAccountDAL(connectionString);
            CustomerAccountDB = new CustomerUserAccountDAL(connectionString);
        }

        public static UserAccount? Authorize(UserTypes userType, string username, string password)
        {
            if (userType == UserTypes.Employee)
                return EmployeeAccountDB.Authorize(username, password);
            else if (userType == UserTypes.Customer)
                return CustomerAccountDB.Authorize(username, password);
            return null;
        }
        /// <summary>
        /// Tài khoản của nhân viên
        /// </summary>
        public static bool ChangePassword(UserTypes userType, string username, string oldpassword, string newpassword)
        {
            if (userType == UserTypes.Employee)
                return EmployeeAccountDB.ChangePassword(username, oldpassword, newpassword);
            else
                return CustomerAccountDB.ChangePassword(username, oldpassword, newpassword);
        }
        public static UserAccount? GetUserProfile(string username)
        {

            return CustomerAccountDB.GetUserProfile(username);
        }
        public static bool RegisterCustomer(string email, string password, string displayName, string phone, string province, string contactName)
        {
            return CustomerAccountDB.RegisterCustomer(email, password, displayName, phone, province, contactName);
        }
        public static UserAccount? AuthorizeCustomer(string userName, string password)
        {
            return CustomerAccountDB.Authorize(userName, password);
        }
    }

    public enum UserTypes
    {
        Employee,
        Customer
    }
}