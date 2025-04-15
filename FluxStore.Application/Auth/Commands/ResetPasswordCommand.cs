
using FluxStore.Application.Common;
using MediatR;

namespace FluxStore.Application.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(
        string Email, string Token, string NewPassword, string ConfirmPassword)
        : IRequest<Result<string>>;
}

