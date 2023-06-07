using System.Data.SqlClient;
using Sql.Connection;

namespace Sql.Repository;

public class PopulateTable
{
    public void CreateDatabase()
    {
        SqlConnection con = Connector.ConnectionStart();
        
        string query = "DROP DATABASE IF EXISTS SqlProject;" +
                        " CREATE DATABASE SqlProject;";
        
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
    public void PopulateDatabase()
    {
        SqlConnection con = Connector.GetConnection();

        string query = "DROP TABLE IF EXISTS enrollments;" +
                        "DROP TABLE IF EXISTS classes;" +
                        "DROP TABLE IF EXISTS students;" +
                        "CREATE TABLE Students (studentId INT IDENTITY(1,1) PRIMARY KEY,firstName VARCHAR(50),lastName VARCHAR(50));" +
                        "CREATE TABLE Classes (classId INT IDENTITY(1,1) PRIMARY KEY, className VARCHAR(50), classInfo NVARCHAR(100));" +
                        "CREATE TABLE Enrollments (enrollmentId INT IDENTITY(1,1) PRIMARY KEY, student_id INT,class_id INT, FOREIGN KEY (student_id) REFERENCES Students (studentId) ON DELETE CASCADE,FOREIGN KEY (class_id) REFERENCES Classes (classId) ON DELETE CASCADE);" +

                        "INSERT INTO Students (firstName, lastName) VALUES ('Mike','Wazosky'),('Amanda','Daniel')," +
                        "('Johnny','Carey'),('Leone','Charlie'),('Juliet','Roberts')" +

                        "INSERT INTO Classes (className, classInfo) VALUES " +
                        "('Maths 1','Advanced level maths'),('Maths 2','Lower level maths')," +
                        "('English 1','English as a first language'),('English 2','English as a second language')," +
                        "('Science 1','Advanced level science'),('Science 2','Basic science')" +

                        "INSERT INTO Enrollments(student_id, class_id) VALUES " +
                        "(1,1),(1,4),(1,5),(2,2),(2,3),(2,5),(3,1 ),(3,3 )" +
                        ",(3,6),(4,2),(4,3),(4,5),(5,1),(5,4),(5,5)";

        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }

}