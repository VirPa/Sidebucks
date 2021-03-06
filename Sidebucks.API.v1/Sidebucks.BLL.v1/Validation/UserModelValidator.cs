﻿using FluentValidation;
using Sidebucks.BLL.v1.Helpers;
using Sidebucks.DAL.v1.Models;

namespace Sidebucks.BLL.v1.Validation {
    public class UserModelValidator : AbstractValidator<CreateUserModel> {
        public UserModelValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.Fullname).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.Email).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());

            RuleFor(a => a.Password).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }

    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel> {
        public UpdateUserModelValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.UserId).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }

    public class SendEmailConfirmationValidator : AbstractValidator<SendEmailConfirmation> {
        public SendEmailConfirmationValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.Email).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());
        }
    }

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailModel> {
        public ConfirmEmailValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.UserId).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.Token).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel> {
        public ChangePasswordValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.Email).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());

            RuleFor(a => a.OldPassword).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.NewPassword).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }

    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordModel> {
        public ForgotPasswordValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.Email).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());
        }
    }

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel> {
        public ResetPasswordValidator(ResponseBadRequest badRequest) {

            RuleFor(a => a.UserId).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.Token).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.NewPassword).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }
}
