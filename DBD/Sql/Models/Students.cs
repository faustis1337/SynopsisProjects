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