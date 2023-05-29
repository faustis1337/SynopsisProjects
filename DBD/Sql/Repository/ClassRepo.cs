using System.Data;
using System.Data.SqlClient;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class ClassRepo : IClassRepo
{
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

    public void AddClasses(ClassCreateDto classCreate)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "INSERT INTO Classes (className, classInfo) VALUES ('"+classCreate.ClassName+"','"+classCreate.ClassInfo+"')";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }

    public void EditClasses(int id, ClassUpdateDto studentsUpdate)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "UPDATE Students SET className = '"+studentsUpdate.ClassName+"', classInfo = '"+studentsUpdate.ClassInfo+"' Where id = '"+id+"'";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
    public void Delete(int classId)
    {
        SqlConnection con = Connector.GetConnection();
        SqlCommand command = new SqlCommand("DELETE FROM Classes WHERE id = '"+classId+"'", con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
}