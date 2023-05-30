namespace Sql.Models;

public class Students
{
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class StudentCreateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
public class StudentUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class StudentsClasses
{
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Classes> ClassesList { get; set; } = new List<Classes>();
}