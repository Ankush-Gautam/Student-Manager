namespace StudentManager.Models;

public class AddStudentVm
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public bool IsSubscribed { get; set; }
}