namespace Sql.Models;

public class Enrollments
{
    private int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }

}

public class EnrollmentCreateDto
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
}