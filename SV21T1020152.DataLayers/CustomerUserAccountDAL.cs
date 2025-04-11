using Dapper;
using SV21T1020152.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020152.DataLayers
{
    public class CustomerUserAccountDAL : BaseDAL
    {
        public CustomerUserAccountDAL(string connectionString) : base(connectionString)
        {
        }

        public UserAccount? Authorize(string username, string password)
        {
            UserAccount? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select CustomerID as UserId,
		                    Email as UserName,
		                    CustomerName as DisplayName,
		                    N'' as Photo,
		                    N'' as RoleNames
                    from Customers
                    where Email = @Email and Password = @Password";
                var parameters = new
                {
                    Email = username,
                    Password = password
                };
                data = connection.QueryFirstOrDefault<UserAccount>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            ;
            return data;
        }

        public bool ChangePassword(string username, string oldpassword, string newpassword)
        {
            bool result = false;
            // so sánh mật khẩu cũ trong database
            UserAccount userAccount = Authorize(username, oldpassword);
            if (userAccount == null) return result;
            using (var connection = OpenConnection())
            {
                // cập nhật mật khẩu mới
                var sql = @"UPDATE  Customers
                    SET     Password = @Password
                    WHERE   Email = @Email";

                var parameters = new
                {
                    Email = username,
                    Password = newpassword
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
        public UserAccount? GetUserProfile(string username)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"
            SELECT  CustomerID as UserId,
                    Email as UserName,
                    CustomerName as DisplayName,
                    Phone,
                    Province,
                    ContactName,
                    N'' as Photo,
                    N'' as RoleNames
            FROM Customers
            WHERE Email = @Email";

                var parameters = new { Email = username };

                return connection.QueryFirstOrDefault<UserAccount>(sql, parameters, commandType: CommandType.Text);
            }
        }

        public bool RegisterCustomer(string email, string password, string displayName, string phone, string province, string contactName)
        {
            using (var connection = OpenConnection())
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingUser = GetUserProfile(email);
                if (existingUser != null)
                {
                    return false; // Email đã tồn tại
                }

                var sql = @"
            INSERT INTO Customers (Email, Password, CustomerName, Phone, Province, ContactName)
            VALUES (@Email, @Password, @CustomerName, @Phone, @Province, @ContactName)";

                var parameters = new
                {
                    Email = email,
                    Password = password, // Nếu cần, hãy mã hóa mật khẩu trước khi lưu
                    CustomerName = displayName,
                    Phone = phone,
                    Province = province,
                    ContactName = contactName
                };

                int affectedRows = connection.Execute(sql, parameters, commandType: CommandType.Text);
                return affectedRows > 0; // Trả về true nếu đăng ký thành công
            }
        }

    }
}
