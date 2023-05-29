using System.Data.SqlClient;

namespace Sql.Connection;

public class Connector
{       //This is to connect to the localhost DB, i created a DB with the sql query
    public static SqlConnection GetConnection()
    {
        const string connectionString = "Data Source=localhost; Initial Catalog=SqlProject; Integrated Security=SSPI"; 
        var connection = new SqlConnection(connectionString);
        return connection;
    }
    public static SqlConnection ConnectionStart() //this is so that it can create a local DB
    {
        const string connectionString = "Server=localhost;Integrated security=SSPI;database=master";
        var connection = new SqlConnection(connectionString);
        return connection;
    }
}