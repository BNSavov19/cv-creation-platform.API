﻿using CVCreationPlatform.AuthService.Contracts;
using CVCreationPlatform.AuthService.Implementations;
using CVCreationPlatform.AuthService.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CVCreationPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IJWTService _jWTService;

    public UserController(ILogger<UserController> logger, IUserService userService, IJWTService jwtService)
        => (_logger, _userService, _jWTService) = (logger, userService, jwtService);

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegistrationModel registrationModel)
    {
        try
        {
            await _userService.RegisterAsync(registrationModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await this._userService.GetUserAsync(id);
            return Ok(user);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
    {
        try
        {
            bool result = await this._userService.CheckLoginInformationAsync(loginModel);
            return Ok(this._jWTService.CreateToken(loginModel));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}