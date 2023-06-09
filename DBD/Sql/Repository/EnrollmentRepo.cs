﻿using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Sql.Connection;
using Sql.Interfaces;
using Sql.Models;

namespace Sql.Repository;

public class EnrollmentRepo : IEnrollmentRepo
{
    public  List<Enrollments> GetAllEnrolls()
    {
        SqlConnection con =  Connector.GetConnection();
        SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Enrollments", con);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);
        
        List<Enrollments> enrollmentsList =  new List<Enrollments>();
        
        if (dataTable.Rows.Count > 0)
        {
            for (int i = 0; i < dataTable.Rows.Count ; i++)
            {
                Enrollments enrolls =  new Enrollments();
                enrolls.EnrollmentId = Convert.ToInt32(dataTable.Rows[i]["enrollmentId"]);
                enrolls.StudentId = Convert.ToInt32(dataTable.Rows[i]["student_Id"]);
                enrolls.ClassId = Convert.ToInt32(dataTable.Rows[i]["class_Id"]);
                enrollmentsList.Add(enrolls);
            }
        }
        return enrollmentsList;
    }
    
    public void AddStudentToClass(EnrollmentCreateDto create)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "INSERT INTO Enrollments (className, classInfo) VALUES ('"+create.StudentId+"','"+create.ClassId+"')";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();        
    }
    
    public void EditEnrollments(int id, EnrollmentUpdateDto update)
    {
        SqlConnection con = Connector.GetConnection();
        String query = "UPDATE Enrollments SET student_id = '"+update.StudentId+"', class_id = '"+update.ClassId+"' Where enrollmentId = '"+id+"'";
        SqlCommand command = new SqlCommand(query, con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
    
    public void Delete(int enrollmentId)
    {
        SqlConnection con = Connector.GetConnection();
        SqlCommand command = new SqlCommand("DELETE FROM Enrollments WHERE enrollmentId = '"+enrollmentId+"'", con);
        con.Open();
        command.ExecuteNonQuery();
        con.Close();
    }
}