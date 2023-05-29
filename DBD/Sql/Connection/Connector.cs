using System.Data.SqlClient;

namespace Sql.Connection;

public class Connector
{
    public static SqlConnection GetConnection()
    {
        const string connectionString = "Data Source=localhost; Initial Catalog=SqlProject; Integrated Security=true"; //This is localhost to connect to the DB, i created a DB with the sql query
        var connection = new SqlConnection(connectionString);
        return connection;
    }
}