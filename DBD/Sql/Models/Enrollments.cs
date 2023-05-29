namespace Sql.Models;

public class Enrollments
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }

}

public class EnrollmentCreateDto
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
}

public class EnrollmentUpdateDto
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
}