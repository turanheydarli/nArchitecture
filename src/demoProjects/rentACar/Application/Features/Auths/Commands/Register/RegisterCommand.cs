using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using MediatR;

namespace Application.Features.Auths.Commands.Register;

public class RegisterCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string  IpAddress { get; set; }
}