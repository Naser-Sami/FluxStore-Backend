using MediatR;
using FluxStore.Application.Common;

namespace FluxStore.Application.Auth.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest<Result<string>>;
}