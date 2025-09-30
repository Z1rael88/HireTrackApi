namespace Application.Dtos.Resume;

public class CandidateDto
{
    public required string Firstname { get; set; } 
    public required string Lastname { get; set; } 
    public required int Age { get; set; }
    public required string Bio { get; set; }
    public required string Email { get; set; } 
}