using System.Data.SqlClient;
using Sql.Connection;

namespace Sql.Repository;

public class PopulateTable
{
    public void CreateData()
    {
        SqlConnection con = Connector.ConnectionStart();
        
        string query = "DROP TABLE IF EXISTS enrollments;"+
                        "DROP TABLE IF EXISTS classes;"+
                        "DROP TABLE IF EXISTS students;"+
                        "CREATE TABLE Students (id INT IDENTITY(1,1) PRIMARY KEY,firstName VARCHAR(50),lastName VARCHAR(50));"+
                        "CREATE TABLE Classes (id INT IDENTITY(1,1) PRIMARY KEY, className VARCHAR(50), classInfo NVARCHAR(100));"+ 
                        " CREATE TABLE Enrollments (id INT IDENTITY(1,1) PRIMARY KEY, student_id INT,class_id INT, FOREIGN KEY (student_id) REFERENCES Students (id),FOREIGN KEY (class_id) REFERENCES Classes (id));"+
                        
                        "INSERT INTO Students (firstName, lastName) VALUES ('Mike', 'Wazosky')"+
                        "INSERT INTO Students (firstName, lastName) VALUES ('Amanda', 'Daniel')"+
                        "INSERT INTO Students (firstName, lastName) VALUES ('Johnny', 'Carey')"+
                        "INSERT INTO Students (firstName, lastName) VALUES ('Leone', 'Charlie')"+
                        "INSERT INTO Students (firstName, lastName) VALUES ('Juliet', 'Roberts')"+

                        "INSERT INTO Classes (className, classInfo) VALUES ('Maths 1', 'Advanced level maths')"+
                        "INSERT INTO Classes (className, classInfo) VALUES ('Maths 2', 'Lower level maths')"+
                        "INSERT INTO Classes (className, classInfo) VALUES ('English 1', 'English as a first language')"+
                        "INSERT INTO Classes (className, classInfo) VALUES ('English 2', 'English as a second language')"+
                        "INSERT INTO Classes (className, classInfo) VALUES ('Science 1', 'Advanced level science')"+
                        "INSERT INTO Classes (className, classInfo) VALUES ('Science 2', 'Basic science')"+

                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 1, 1)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 1,4 )"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 1,5 )"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 2,2 )"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 2,3 )"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 2, 5)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 3,1 )"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 3,3 )"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 3, 6)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 4, 2)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 4, 3)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 4, 5)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 5, 1)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 5, 4)"+
                        "INSERT INTO Enrollments(student_id, class_id) VALUES ( 5, 5)"
            ;
        
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }

}