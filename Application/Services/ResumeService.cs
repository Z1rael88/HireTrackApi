using System.Text;
using System.Text.RegularExpressions;
using Application.Dtos.Resume;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

namespace Application.Services;

public class ResumeService(IUnitOfWork unitOfWork, 
    IValidator<ResumeRequestDto> validator, IEmailService emailService, UserManager<User> userManager)
    : IResumeService
{
    private readonly IRepository<Resume> _repository = unitOfWork.Repository<Resume>();
    private readonly IRepository<VacancyResume> _vacancyResumeRepository = unitOfWork.Repository<VacancyResume>();
    private readonly IResumeRepository _resumeRepository = unitOfWork.Resumes;
    private readonly ICandidateRepository _candidateCommonRepository = unitOfWork.Candidates;
    private readonly IRepository<Candidate> _candidateRepository = unitOfWork.Repository<Candidate>();
    private readonly IVacancyRepository _vacancyRepository = unitOfWork.Vacancies;


    public async Task<ResumeResponseDto?> CreateResumeAsync(ResumeRequestDto dto)
    {
        validator.ValidateAndThrow(dto);
        var resume = dto.Adapt<Resume>();
        var existingResume = await _resumeRepository.GetResumeByCandidateEmail(resume.Candidate.Email);
        if (dto.VacancyId is not 0 && existingResume is not null)
        {
            var vacancyResumes = new VacancyResume
            {
                ResumeId = existingResume.Id,
                VacancyId = (int)dto.VacancyId!,
                Status = ResumeStatus.Sent,
            };
            await _vacancyResumeRepository.CreateAsync(vacancyResumes);
            return null;
        }

        var createdResume = await _repository.CreateAsync(resume);

        if (dto.VacancyId != 0)
        {
            var vacancyResume = new VacancyResume
            {
                VacancyId = dto.VacancyId.Value,
                ResumeId = createdResume.Id,
                Status = ResumeStatus.Sent
            };

            await _vacancyResumeRepository.CreateAsync(vacancyResume);
        }


        var user = await userManager.FindByEmailAsync(resume.Candidate.Email);
        var candidate = await _candidateCommonRepository.GetCandidateByEmailAsync(resume.Candidate.Email);
        if (user is not null && candidate is not null)
        {
            candidate.UserId = user.Id;
            await _candidateRepository.UpdateAsync(resume.Candidate);
        }

        var result = createdResume.Adapt<ResumeResponseDto>();
        return result;
    }

    public async Task<ResumeResponseDto> GetResumeByIdAsync(int resumeId)
    {
        var resume = await _resumeRepository.GetResumeById(resumeId);
        var result = resume.Adapt<ResumeResponseDto>();
        result.Status = await _vacancyRepository.GetResumeStatusByResumeIdAsync(resumeId);
        return result;
    }

    public async Task<IEnumerable<ResumeResponseDto>> GetAllResumesByVacancyIdAsync(int vacancyId)
    {
        var resumes = await _resumeRepository.GetAllResumesByVacancyId(vacancyId);

        var result = resumes.Adapt<List<ResumeResponseDto>>();

        foreach (var dto in result)
        {
            dto.Status =
                await _vacancyRepository.GetResumeStatusByResumeIdAsync(dto.Id);
        }

        return result;
    }


    public async Task ChangeStatusOfResumeAsync(int resumeId, int vacancyId, ResumeStatus status)
    {
        var vacancyResumes = await _resumeRepository.GetVacancyResumeByIds(vacancyId, resumeId);
        vacancyResumes.Status = status;
        await _vacancyResumeRepository.SaveChangesAsync();
        var resume = await GetResumeByIdAsync(resumeId);
        await emailService.SendEmailAsync(resume.Candidate.Email, "Status for your resume changed!",
            $"Hello {resume.Candidate.Firstname} !" +
            $"Status for your resume changed to {status}");
    }

    public async Task UpdateResumeAsync(ResumeRequestDto dto, int resumeId)
    {
        validator.ValidateAndThrow(dto);
        await _resumeRepository.UpdateResume(dto.Adapt<Resume>(), resumeId);
    }

    public async Task<ResumeResponseDto> GetResumeByUserIdAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user!.Email is null) throw new NotFoundException("No user found with that Id");
        var resume = await _resumeRepository.GetResumeByCandidateEmail(user.Email);
        var result = resume.Adapt<ResumeResponseDto>();
        if (resume != null) result.Status = await _vacancyRepository.GetResumeStatusByResumeIdAsync(resume.Id);
        return result;
    }

    public ResumeResponseDto UploadResumeAsync(IFormFile resume)
    {
        var pdf = new PdfLoadedDocument(resume.OpenReadStream());
        var pages = pdf.Pages;
        var allText = new StringBuilder();

        foreach (var page in pages.Cast<PdfLoadedPage>())
        {
            var extractedText = page.ExtractText(true);
            allText.AppendLine(extractedText);
        }

        return ConvertTextToResumeDto(allText.ToString());
    }

    private ResumeResponseDto ConvertTextToResumeDto(string text)
    {
        var resume = new ResumeResponseDto();
        resume.Candidate = new CandidateDto
        {
            Email = ExtractEmail(text),
            Firstname = ExtractFirstName(text),
            Lastname =  ExtractLastName(text),
            Age = ExtractAge(text),
            Bio = ExtractBio(text),
            WorkType = ExtractWorkType(text),
        };
        resume.LanguageLevels = ExtractLanguageLevel(text);
        return resume;
    }

    private string ExtractFullName(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        var lines = text
            .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim());

        var headers = new[] { "Profile", "Summary", "About Me", "Objective" };

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (headers.Any(h => line.StartsWith(h, StringComparison.OrdinalIgnoreCase)))
                continue;

            return line;
        }

        return string.Empty;
    }

    private string ExtractFirstName(string text)
    {
        var fullName = ExtractFullName(text);

        if (string.IsNullOrWhiteSpace(fullName))
            return string.Empty;

        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 0 ? parts[0] : string.Empty;
    }

    private string ExtractLastName(string text)
    {
        var fullName = ExtractFullName(text);

        if (string.IsNullOrWhiteSpace(fullName))
            return string.Empty;

        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 1 ? parts[^1] : string.Empty;
    }

    private ICollection<LanguageLevelDto> ExtractLanguageLevel(string text)
    {
        var match = Regex.Match(
            text,
            @"\bLanguages\b[\s:]*\r?\n([\s\S]*?)(?=\r?\n\b(Experience|Education|Skills|Projects|Work\s*Experience|Employment)\b|$)",
            RegexOptions.IgnoreCase);

        if (match.Success)
        {
            var lines = match.Groups[1].Value
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim());

            var languageLevels = new List<LanguageLevelDto>();

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { '–', '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                if (!Enum.TryParse<Language>(parts[0].Trim(), true, out var language))
                    continue;

                var levelText = parts[1].Trim();
                if (!Enum.TryParse<Level>(levelText, true, out var level))
                {
                    level = levelText switch
                    {
                        "A1" => Level.Beginner,
                        "A2" => Level.Beginner,
                        "B1" => Level.Intermediate,
                        "B2" => Level.Intermediate,
                        "C1" => Level.Advanced,
                        "C2" => Level.Native,
                        _ => Level.Beginner
                    };
                }

                languageLevels.Add(new LanguageLevelDto
                {
                    Language = language,
                    Level = level
                });
            }

            return languageLevels;
        }

        return new List<LanguageLevelDto>();
    }

    private ICollection<WorkType> ExtractWorkType(string text)
    {
        return new List<WorkType>();
    }

    private string ExtractEmail(string text)
    {
        var match = Regex.Match(
            text,
            @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}",
            RegexOptions.IgnoreCase);

        return match.Success ? match.Value : string.Empty;
    }

    private int ExtractAge(string text)
    {
        var match = Regex.Match(
            text,
            @"(?:age\s*:?|aged\s*|years\s*old\s*:?|age\s*is\s*)\s*(\d{1,2})",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        return match.Success ? int.Parse(match.Value) : 0;
    }

    private string ExtractBio(string text)
    {
        var match = Regex.Match(
            text,
            @"\b(Profile|Summary|About\s*Me|Objective)\b[\s:]*\r?\n?([\s\S]*?)(?=\r?\n\b(Experience|Education|Skills|Projects|Work\s*Experience|Employment)\b|$)",
            RegexOptions.IgnoreCase);

        if (!match.Success)
            return string.Empty;

        var bio = match.Groups[2].Value;

        bio = bio.Replace("\r", "")
            .Replace("\n", " ")
            .Trim();

        return bio;
    }
}