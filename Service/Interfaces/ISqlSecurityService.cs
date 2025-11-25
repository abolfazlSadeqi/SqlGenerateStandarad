namespace Service.Interfaces;

public interface  ISqlSecurityService
{


     string CreateLogin(string loginName, string password, bool isSqlLogin);
     string DropLogin(string loginName);
     string ChangeLoginPassword(string loginName, string newPassword);
     string CreateUser(string dbName, string userName, string loginName);
     string DropUser(string dbName, string userName);
     string AddUserToRole(string dbName, string userName, string roleName);
     string AddLoginToServerRole(string loginName, string roleName);
     string GrantDatabasePermission(string dbName, string userName, string permission);
     string GrantObjectPermission(string dbName, string userName, string permission, string schemaName, string objectName);
     string RevokeObjectPermission(string dbName, string userName, string permission, string schemaName, string objectName);
}
