using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
{
    private readonly AuthBusinessRules _authBusinessRules;
    private IOperationClaimRepository _operationClaimRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IOperationClaimRepository operationClaimRepository,
        AuthBusinessRules authBusinessRules, IAuthService authService, IUserRepository userRepository)
    {
        _operationClaimRepository = operationClaimRepository;
        _authBusinessRules = authBusinessRules;
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.EmailCannotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

        HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out var passwordHash,
            out var passwordSalt);

        User newUser = new User
        {
            Email = request.UserForRegisterDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            FirstName = request.UserForRegisterDto.FirstName,
            LastName = request.UserForRegisterDto.LastName,
            Status = true,
        };

        User createdUser = await _userRepository.AddAsync(newUser);

        AccessToken accessToken = await _authService.CreateAccessToken(createdUser);
        RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
        RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

        RegisteredDto registeredDto = new RegisteredDto
        {
            AccessToken = accessToken,
            RefreshToken = addedRefreshToken
        };

        return registeredDto;
    }
}