namespace Domain.Models;

public class Candidate : BaseEntity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Bio { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}