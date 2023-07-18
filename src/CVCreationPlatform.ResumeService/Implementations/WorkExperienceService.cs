﻿using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class WorkExperienceService : IWorkExperienceService
{
    private readonly ApplicationDbContext _context;

    public WorkExperienceService(ApplicationDbContext context)
        => _context = context;

    public async Task<bool> AssignWorkExperienceToResume(Guid resumeId, WorkExperienceDTO workExperienceDTO)
    {
        var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
        if (resume == null)
            throw new ArgumentException("Invalid workExperience id");

        var workExperienceToAdd = new WorkExperience
        {
            ResumeId = resumeId,
            Resume = resume,
            CompanyName = workExperienceDTO.CompanyName,
            Position = workExperienceDTO.Position,
            StartDate = workExperienceDTO.StartDate,
            EndDate = workExperienceDTO.EndDate,
            Location = workExperienceDTO.Location,
            Description = workExperienceDTO.Description
        };

        resume.WorkExperiences.Add(workExperienceToAdd);

        await this._context.AddAsync(workExperienceToAdd);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateWorkExperience(int workExperienceId, WorkExperienceDTO newWorkExperienceDTO)
    {
        var workExperience = await _context.WorkExperiences.FindAsync(workExperienceId);
        if (workExperience == null)
            throw new ArgumentException("Invalid workExperience id");

        workExperience.CompanyName = newWorkExperienceDTO.CompanyName;
        workExperience.Position = newWorkExperienceDTO.Position;
        workExperience.StartDate = newWorkExperienceDTO.StartDate;
        workExperience.EndDate = newWorkExperienceDTO.EndDate;
        workExperience.Location = newWorkExperienceDTO.Location;
        workExperience.Description = newWorkExperienceDTO.Description;


        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSkill(int workExperienceId)
    {
        var workExperienceToRemove = await _context.WorkExperiences.FindAsync(workExperienceId);
        if (workExperienceToRemove == null)
            throw new ArgumentException("Invalid workExperience id");

        _context.WorkExperiences.Remove(workExperienceToRemove);
        await this._context.SaveChangesAsync();
        return true;
    }
}
