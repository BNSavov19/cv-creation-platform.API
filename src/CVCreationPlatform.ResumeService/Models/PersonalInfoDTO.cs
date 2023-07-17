﻿using Data.Models.CV;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models;

public class PersonalInfoDTO
{
    public PersonalInfoDTO()
    {

    }
    public PersonalInfoDTO(PersonalInfo personalInfo)
    {
        if (personalInfo != null)
        {
            this.PhotoUrl = personalInfo.PhotoUrl;
            this.FirstName = personalInfo.FirstName;
            this.MiddleName = personalInfo.MiddleName;
            this.LastName = personalInfo.LastName;
            this.Description = personalInfo.Description;
            this.Address = personalInfo.Address;
            this.PhoneNumber = personalInfo.PhoneNumber;
            this.Email = personalInfo.Email;
        }
    }

    public IFormFile? Photo { get; set; }

    public string? PhotoUrl { get; set; }

	public string? FirstName { get; set; }

	public string? MiddleName { get; set; }

	public string? LastName { get; set; }

	public string? Description { get; set; }

	public string? Address { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Email { get; set; }
}