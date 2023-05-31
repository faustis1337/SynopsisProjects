using System.Data;
using System.Data.SqlClient;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class QueryRepo : IQueryRepo
{
    public List<Students> GetStudentsInClass(int classId) //this gets all the students in a specific class
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
    
    public List<StudentsClasses> GetClassesAStudentTakes(int studentId) //this gets a student item with the classes they take under them as a list
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
            StudentsClasses student =  new StudentsClasses();
                student.StudentId = Convert.ToInt32(dataTable.Rows[0]["studentId"]);
                student.FirstName = Convert.ToString(dataTable.Rows[0]["firstName"]);
                student.LastName = Convert.ToString(dataTable.Rows[0]["lastName"]);
                
                foreach (DataRow row in dataTable.Rows)
                {
                    Classes classStudentHas = new Classes();
                    classStudentHas.ClassId = Convert.ToInt32(row["classId"]);
                    classStudentHas.ClassName = Convert.ToString(row["className"]);
                    classStudentHas.ClassInfo = Convert.ToString(row["classInfo"]);
                    student.ClassesList.Add(classStudentHas);
                }
                studentsList.Add(student);
        }
        return studentsList;
    }

    public List<ClassesStudent> GetStudentsInAClass(int classId) //this gets a class item with the students that take that class
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
            ClassesStudent student =  new ClassesStudent();
                student.ClassId = Convert.ToInt32(dataTable.Rows[0]["classId"]);
                student.ClassName = Convert.ToString(dataTable.Rows[0]["className"]);
                student.ClassInfo = Convert.ToString(dataTable.Rows[0]["classInfo"]);
                
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
        return classesList;
    }

    public List<StudentsClasses> GetAllStudentsWithClasses() //this gets a student list with the classes the student takes
    {
        SqlConnection con = Connector.GetConnection();
        string query = "SELECT studentId, firstName, lastName, classId, className, classInfo FROM Students s " +
                        "INNER JOIN Enrollments e ON s.studentId = e.student_id " +
                        "INNER JOIN Classes c ON c.classId = e.class_id";

        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<StudentsClasses> studentsList = new List<StudentsClasses>();

        foreach (DataRow row in dataTable.Rows)
        {
            int studentId = Convert.ToInt32(row["studentId"]);
            StudentsClasses student = studentsList.FirstOrDefault(s => s.StudentId == studentId);

            if (student == null)
            {
                student = new StudentsClasses();
                student.StudentId = studentId;
                student.FirstName = Convert.ToString(row["firstName"]);
                student.LastName = Convert.ToString(row["lastName"]);
                studentsList.Add(student);
            }

            Classes classes = new Classes();
            classes.ClassId = Convert.ToInt32(row["classId"]);
            classes.ClassName = Convert.ToString(row["className"]);
            classes.ClassInfo = Convert.ToString(row["classInfo"]);
            student.ClassesList.Add(classes);
        }

        return studentsList;
    }

    public List<ClassesStudent> GetAllClassesWithStudents() //this gets a classes list with which students are in it
    {
        SqlConnection con = Connector.GetConnection();
        string query = "SELECT studentId, firstName, lastName, classId, className, classInfo FROM Classes c " +
                        "INNER JOIN Enrollments e ON c.classId = e.class_id " +
                        "INNER JOIN Students s ON s.studentId = e.student_id";

        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        List<ClassesStudent> classesList = new List<ClassesStudent>();

        foreach (DataRow row in dataTable.Rows)
        {
            int classId = Convert.ToInt32(row["classId"]);
            ClassesStudent student = classesList.FirstOrDefault(s => s.ClassId == classId);

            if (student == null)
            {
                student = new ClassesStudent();
                student.ClassId = classId;
                student.ClassName = Convert.ToString(row["className"]);
                student.ClassInfo = Convert.ToString(row["classInfo"]);
                classesList.Add(student);
            }

            Students students = new Students();
            students.StudentId = Convert.ToInt32(row["studentId"]);
            students.FirstName = Convert.ToString(row["firstName"]);
            students.LastName = Convert.ToString(row["lastName"]);
            student.StudentsList.Add(students);
        }

        return classesList;
    }
}