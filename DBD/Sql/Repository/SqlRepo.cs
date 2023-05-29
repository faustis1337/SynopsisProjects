using System.Data;
using System.Data.SqlClient;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class SqlRepo : ISqlRepo
{
    private string conString = "Data Source = .;Initial Catalog = SqlProject ; Integrated Security = true";

    public  List<Students> GetAllStudents()
    {
        SqlConnection con =  Connector.GetConnection();
        SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Students", con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);
        
        List<Students> studentsList =  new List<Students>();
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                Students student =  new Students();
                student.StudentId = Convert.ToInt32(dataTable.Rows[i]["id"]);
                student.FirstName = Convert.ToString(dataTable.Rows[i]["firstName"]);
                student.LastName = Convert.ToString(dataTable.Rows[i]["lastName"]);
                studentsList.Add(student);
            }
        }

        return studentsList;
    }

    public List<Classes> GetAllClasses()
    {
        SqlConnection con = Connector.GetConnection();
        SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Classes", con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);
        
        List<Classes> classesList =  new List<Classes>();
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                Classes classes =  new Classes();
                classes.ClassId = Convert.ToInt32(dataTable.Rows[i]["id"]);
                classes.ClassName = Convert.ToString(dataTable.Rows[i]["className"]);
                classes.ClassInfo = Convert.ToString(dataTable.Rows[i]["classInfo"]);
                classesList.Add(classes);
            }
        }

        return classesList;
    }

    public void AddStudents(StudentCreateDto studentsCreate)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "INSERT INTO Students (firstName, lastName) VALUES ('"+studentsCreate.FirstName+"','"+studentsCreate.LastName+"')";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }

    public void AddClasses(ClassCreateDto classCreate)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "INSERT INTO Classes (className, classInfo) VALUES ('"+classCreate.ClassName+"','"+classCreate.ClassInfo+"')";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();    }

    public void AddStudentToClass(EnrollmentCreateDto enrollmentsCreate)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "INSERT INTO Classes (className, classInfo) VALUES ('"+enrollmentsCreate.StudentId+"','"+enrollmentsCreate.ClassId+"')";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();        
    }
}