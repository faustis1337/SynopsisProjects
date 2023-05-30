using System.Data;
using System.Data.SqlClient;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class QueryRepo : IQueryRepo
{
    public List<Students> GetStudentsInClass(int classId) //This gets all the students in a specific class
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT studentId,firstName, lastName FROM Students s " +
                        "INNER JOIN Enrollments e on s.studentId = e.student_id " +
                        "INNER JOIN Classes c on c.classId = e.class_id " +
                        "WHERE e.class_id = '"+classId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<Students> studentsList =  new List<Students>();
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                Students student =  new Students();
                student.StudentId = Convert.ToInt32(dataTable.Rows[i]["studentId"]);
                student.FirstName = Convert.ToString(dataTable.Rows[i]["firstName"]);
                student.LastName = Convert.ToString(dataTable.Rows[i]["lastName"]);
                studentsList.Add(student);
            }
        }
        return studentsList;
    }
    
    public List<Classes> GetClassesAStudentDoes(int studentId) //this gets all the classes for a student
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT c.classId, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e on s.studentId = e.student_id " +
                        "INNER JOIN Classes c on c.classId = e.class_id " +
                        "WHERE e.student_Id = '"+studentId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<Classes> classesList =  new List<Classes>();
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                Classes classes =  new Classes();
                classes.ClassId = Convert.ToInt32(dataTable.Rows[i]["classId"]);
                classes.ClassName = Convert.ToString(dataTable.Rows[i]["className"]);
                classes.ClassInfo = Convert.ToString(dataTable.Rows[i]["classInfo"]);
                classesList.Add(classes);
            }
        }
        return classesList;
    }
    
    public List<StudentsClasses> GetClassesAStudentTakes(int studentId) //this gets a student list with the classes the student takes
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT studentId, firstName, lastName, classId, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e on s.studentId = e.student_id " +
                        "INNER JOIN Classes c on c.classId = e.class_id " +
                        "WHERE e.student_Id = '"+studentId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<StudentsClasses> studentsList =  new List<StudentsClasses>();
        
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                StudentsClasses student =  new StudentsClasses();
                student.StudentId = Convert.ToInt32(dataTable.Rows[i]["studentId"]);
                student.FirstName = Convert.ToString(dataTable.Rows[i]["firstName"]);
                student.LastName = Convert.ToString(dataTable.Rows[i]["lastName"]);
                for (int y = 0; y < dataTable.Rows.Count; y++)
                {
                    Classes classStudentHas = new Classes();
                    classStudentHas.ClassId = Convert.ToInt32(dataTable.Rows[y]["classId"]);
                    classStudentHas.ClassName = Convert.ToString(dataTable.Rows[y]["className"]);
                    classStudentHas.ClassInfo = Convert.ToString(dataTable.Rows[y]["classInfo"]);
                    student.ClassesList.Add(classStudentHas);
                }
                studentsList.Add(student);
            }
        }
        return studentsList;
    }

    public List<ClassesStudent> GetStudentsInAClass(int classId) //this gets a classes list with which students are in it
    {
        SqlConnection con =  Connector.GetConnection();
        string query = "SELECT studentId, firstName, lastName, classId, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e on s.studentId = e.student_id " +
                        "INNER JOIN Classes c on c.classId = e.class_id " +
                        "WHERE e.class_id = '"+classId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<ClassesStudent> classesList =  new List<ClassesStudent>();
        
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                ClassesStudent student =  new ClassesStudent();
                student.ClassId = Convert.ToInt32(dataTable.Rows[i]["classId"]);
                student.ClassName = Convert.ToString(dataTable.Rows[i]["className"]);
                student.ClassInfo = Convert.ToString(dataTable.Rows[i]["classInfo"]);
                for (int y = 0; y < dataTable.Rows.Count; y++)
                {
                    Students studentsInClass = new Students();
                    studentsInClass.StudentId = Convert.ToInt32(dataTable.Rows[y]["studentId"]);
                    studentsInClass.FirstName = Convert.ToString(dataTable.Rows[y]["firstName"]);
                    studentsInClass.LastName = Convert.ToString(dataTable.Rows[y]["lastName"]);
                    student.StudentsList.Add(studentsInClass);
                }
                classesList.Add(student);
            }
        }
        return classesList;
    }

    public StudentsClasses GetAStudentItem(int studentId) //this gets a student item with the classes they take under them as a list
    {
        SqlConnection con = Connector.GetConnection();
        string query = "SELECT studentId, firstName, lastName, classId, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e on s.studentId = e.student_id " +
                        "RIGHT JOIN Classes c on c.classId = e.class_id " +
                        "WHERE e.student_Id = '"+studentId+"'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        SqlCommand command = new SqlCommand(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);
        con.Open();
        var reader = command.ExecuteReader();
        
        StudentsClasses student =  new StudentsClasses();
    
        if(reader.HasRows)
            while (reader.Read())
            {
                student.StudentId = (int)reader["studentId"];
                student.FirstName = (string)reader["firstName"];
                student.LastName = (string)reader["lastName"];
                for (int y = 0; y < dataTable.Rows.Count; y++)
                {
                    Classes classStudentHas = new Classes();
                    classStudentHas.ClassId = Convert.ToInt32(dataTable.Rows[y]["classId"]);
                    classStudentHas.ClassName = Convert.ToString(dataTable.Rows[y]["className"]);
                    classStudentHas.ClassInfo = Convert.ToString(dataTable.Rows[y]["classInfo"]);
                    student.ClassesList.Add(classStudentHas);
                }
            }
        con.Close();
        return student;
    }
}