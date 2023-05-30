using System.Data;
using System.Data.SqlClient;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class QueryRepo : IQueryRepo
{
    public List<Students> GetStudentsInClass(int classId)
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT s.id,firstName, lastName FROM Students s " +
                        "INNER JOIN Enrollments e on s.id = e.student_id " +
                        "INNER JOIN Classes c on c.id = e.class_id " +
                        "WHERE c.id = '"+classId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
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
    
    public List<Classes> GetClassesAStudentDoes(int studentId)
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT c.id, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e on s.id = e.student_id " +
                        "INNER JOIN Classes c on c.id = e.class_id " +
                        "WHERE s.id = '"+studentId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
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
    
    public List<StudentsClasses> GetStudentsInClassList(int classId)
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT s.id, firstName, lastName, c.id, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e on s.id = e.student_id " +
                        "INNER JOIN Classes c on c.id = e.class_id " +
                        "WHERE s.id = '"+classId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<StudentsClasses> studentsList =  new List<StudentsClasses>();
        
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                StudentsClasses student =  new StudentsClasses();
                student.StudentId = Convert.ToInt32(dataTable.Rows[i]["id"]);
                student.FirstName = Convert.ToString(dataTable.Rows[i]["firstName"]);
                student.LastName = Convert.ToString(dataTable.Rows[i]["lastName"]);
                for (int y = 0; y < dataTable.Rows.Count; y++)
                {
                    Classes classesOfStudent = new Classes();
                    classesOfStudent.ClassId = Convert.ToInt32(dataTable.Rows[y]["id"]);
                    classesOfStudent.ClassName = Convert.ToString(dataTable.Rows[y]["className"]);
                    classesOfStudent.ClassInfo = Convert.ToString(dataTable.Rows[y]["classInfo"]);
                    student.ClassesList.Add(classesOfStudent);
                }
                studentsList.Add(student);
            }
        }
        return studentsList;
    }
    
    // public Students GetAStudentItem(int id)
    // {
    //     SqlConnection con =  Connector.GetConnection();
    //     string query = "SELECT firstName, lastName FROM Students s " +
    //                     "INNER JOIN Enrollments e on s.id = e.student_id " +
    //                     "INNER JOIN Classes c on c.id = e.class_id " +
    //                     "WHERE c.id = '"+id+"'";
    //     SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
    //     SqlCommand command = new SqlCommand(query, con);
    //     DataTable dataTable = new DataTable();
    //     dataAdapter.Fill(dataTable);
    //
    //     var reader = command.ExecuteReader();
    //     
    //     Students student =  new Students();
    //
    //     if(reader.HasRows)
    //         while (reader.Read())
    //         {
    //             student.StudentId = (int)reader["id"];
    //             student.FirstName = (string)reader["firstName"];
    //             student.LastName = (string)reader["lastName"];
    //         }
    //     return student;
    // }
}