namespace StudentManager.Models.Entities;

public class Student
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public bool IsSubscribed { get; set; }
}