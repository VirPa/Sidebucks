using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sidebucks.BLL.v1.Helpers;
using Sidebucks.BLL.v1.Repositories.Interface;
using Sidebucks.BLL.v1.Validation;
using Sidebucks.DAL.v1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sidebucks.API.v1.Controllers {
    [AllowAnonymous]
    [Route("auth")]
    [ApiVersion("1.0")]
    public class AuthenticationController : BaseController {

        #region Initialization

        private readonly List<string> _infos = new List<string>();

        private readonly IMyAuthentication _myAuthentication;

        private readonly ResponseBadRequest _badRequest;
        private readonly SignInModelValidator _signInModelValidator;
        private readonly SignOutModelValidator _signOutModelValidator;
        private readonly GenerateTokenModelValidator _generateTokenModelValidator;

        #endregion

        #region Constructor

        public AuthenticationController(IMyAuthentication myAuthentication,
            ResponseBadRequest badRequest,
            SignInModelValidator signInModelValidator,
            SignOutModelValidator signOutModelValidator,
            GenerateTokenModelValidator generateTokenModelValidator) {

            _myAuthentication = myAuthentication;
            _badRequest = badRequest;
            _signInModelValidator = signInModelValidator;
            _signOutModelValidator = signOutModelValidator;
            _generateTokenModelValidator = generateTokenModelValidator;
        }

        #endregion

        #region Post Auth

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model) {

            #region Validate Model

            var userInputValidated = _signInModelValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            #region Supply User Agent values

            model.ApiVersion = "1.0";
            model.UserAgent = UserAgent;
            model.AppVersion = Version;

            #endregion

            var signedIn = await _myAuthentication.SignInTheReturnUser(model);

            return Ok(signedIn);
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut([FromBody] SignOutModel model) {

            #region Validate Model

            var userInputValidated = _signOutModelValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var signedOut = await _myAuthentication.SignOut(model);

            return Ok(signedOut);
        }

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenModel model) {

            #region Validate Model

            var userInputValidated = _generateTokenModelValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            #region Supply User Agent values
            model.UserAgent = UserAgent;
            model.AppVersion = Version;
            #endregion

            var generatedToken = await _myAuthentication.GenerateToken(model);

            return Ok(generatedToken);
        }

        #endregion
    }
}
