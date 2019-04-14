using FluentValidation;
using Sidebucks.BLL.v1.Helpers;
using Sidebucks.DAL.v1.Models;

namespace Sidebucks.BLL.v1.Validation {
    public class SignInModelValidator : AbstractValidator<SignInModel> {
        public SignInModelValidator() {

            //RuleFor(a => a.Email).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());
            RuleFor(a => a.UserName).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.Password).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }

    public class SignOutModelValidator : AbstractValidator<SignOutModel> {
        public SignOutModelValidator() {

            //RuleFor(a => a.Email).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());
            RuleFor(a => a.UserName).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.RefreshToken).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());
        }
    }

    public class GenerateTokenModelValidator : AbstractValidator<GenerateTokenModel> {
        public GenerateTokenModelValidator() {

            //RuleFor(a => a.UserName).EmailAddress().WithMessage(ResponseBadRequest.ErrorInvalidEmailFormat.ToString());
            RuleFor(a => a.UserName).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.TokenResource.Token).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.TokenResource.Type).NotEmpty().WithMessage(ResponseBadRequest.ErrFieldEmpty.ToString());

            RuleFor(a => a.TokenResource.Type).Must(TypeValid).WithMessage(ResponseBadRequest.ErrorInvalidType.ToString());
        }

        private static bool TypeValid(string type) {

            return type == "session" || type == "refresh";
        }
    }
}
