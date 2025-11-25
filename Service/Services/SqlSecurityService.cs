using Service.Interfaces;
using System.Text;

namespace Service.Services;

public class SqlSecurityService : ISqlSecurityService
{
    private string Quote(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return name;
        return name.Replace("]", "]]");
    }

    private string Bracket(string name)
    {
        return $"[{Quote(name)}]";
    }

    // ---------------------------
    // LOGIN MANAGEMENT
    // ---------------------------

    public string CreateLogin(string loginName, string password, bool isSqlLogin)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = '{Quote(loginName)}')");
        sb.AppendLine("BEGIN");

        if (isSqlLogin)
        {
            sb.AppendLine($"    CREATE LOGIN {Bracket(loginName)} WITH PASSWORD = '{password}', CHECK_POLICY = ON;");
        }
        else
        {
            sb.AppendLine($"    CREATE LOGIN {Bracket(loginName)} FROM WINDOWS;");
        }

        sb.AppendLine("END");
        return sb.ToString();
    }

    public string DropLogin(string loginName)
    {
        return $@"
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = '{Quote(loginName)}')
BEGIN
    DROP LOGIN {Bracket(loginName)};
END
";
    }

    public string ChangeLoginPassword(string loginName, string newPassword)
    {
        return $@"
IF EXISTS (SELECT * FROM sys.sql_logins WHERE name = '{Quote(loginName)}')
BEGIN
    ALTER LOGIN {Bracket(loginName)} WITH PASSWORD = '{newPassword}';
END
";
    }

    // ---------------------------
    // USER MANAGEMENT (Database)
    // ---------------------------

    public string CreateUser(string dbName, string userName, string loginName)
    {
        return $@"
USE {Bracket(dbName)};
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = '{Quote(userName)}')
BEGIN
    CREATE USER {Bracket(userName)} FOR LOGIN {Bracket(loginName)};
END
";
    }

    public string DropUser(string dbName, string userName)
    {
        return $@"
USE {Bracket(dbName)};
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = '{Quote(userName)}')
BEGIN
    DROP USER {Bracket(userName)};
END
";
    }

    // ---------------------------
    // ROLE ASSIGNMENT
    // ---------------------------

    public string AddUserToRole(string dbName, string userName, string roleName)
    {
        return $@"
USE {Bracket(dbName)};
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = '{Quote(userName)}')
BEGIN
    EXEC sp_addrolemember '{Quote(roleName)}', '{Quote(userName)}';
END
";
    }

    public string AddLoginToServerRole(string loginName, string roleName)
    {
        return $@"
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = '{Quote(loginName)}')
BEGIN
    ALTER SERVER ROLE {Bracket(roleName)} ADD MEMBER {Bracket(loginName)};
END
";
    }

    // ---------------------------
    // PERMISSIONS
    // ---------------------------

    public string GrantDatabasePermission(string dbName, string userName, string permission)
    {
        return $@"
USE {Bracket(dbName)};
GRANT {permission} TO {Bracket(userName)};
";
    }

    public string GrantObjectPermission(string dbName, string userName, string permission, string schemaName, string objectName)
    {
        return $@"
USE {Bracket(dbName)};
GRANT {permission} ON OBJECT::{Bracket(schemaName)}.{Bracket(objectName)} TO {Bracket(userName)};
";
    }

    public string RevokeObjectPermission(string dbName, string userName, string permission, string schemaName, string objectName)
    {
        return $@"
USE {Bracket(dbName)};
REVOKE {permission} ON OBJECT::{Bracket(schemaName)}.{Bracket(objectName)} FROM {Bracket(userName)};
";
    }
}
