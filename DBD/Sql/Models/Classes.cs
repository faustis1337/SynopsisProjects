namespace Sql.Models;

public class Classes
{
    public int ClassId { get; set; }
    public string? ClassName { get; set; }
    public string? ClassInfo { get; set; }
}

public class ClassCreateDto
{
    public string? ClassName { get; set; }
    public string? ClassInfo { get; set; }
}

public class ClassUpdateDto
{
    public string? ClassName { get; set; }
    public string? ClassInfo { get; set; }
}

public class ClassesStudent
{
    public int ClassId { get; set; }
    public string? ClassName { get; set; }
    public string? ClassInfo { get; set; }
    public List<Students> StudentsList { get; set; } = new List<Students>();
}