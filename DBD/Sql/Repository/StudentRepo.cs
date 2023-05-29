using System.Data;
using System.Data.SqlClient;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class StudentRepo : IStudentRepo
{
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

    public void AddStudents(StudentCreateDto create)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "INSERT INTO Students (firstName, lastName) VALUES ('"+create.FirstName+"','"+create.LastName+"')";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
    
    public void EditStudents(int id, StudentUpdateDto update)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "UPDATE Students SET firstName = '"+update.FirstName+"', lastName = '"+update.LastName+"' Where id = '"+id+"'";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
    
    public void Delete(int studentId)
    {
        SqlConnection con = Connector.GetConnection();
        SqlCommand command = new SqlCommand("DELETE FROM Students WHERE id = '"+studentId+"'", con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
}