﻿using System;
namespace FluxStore.Application.DTOs
{
	public class ResetPasswordDto
	{
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

