﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sidebucks.BLL.v1.Helpers;
using Sidebucks.BLL.v1.Repositories.Interface;
using Sidebucks.BLL.v1.Validation;
using Sidebucks.DAL.v1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sidebucks.API.v1.Controllers {
    [Route("user")]
    [ApiVersion("1.0")]
    public class UserController : BaseController {

        #region Initialization

        private readonly List<string> _infos = new List<string>();

        private readonly IMyUser _user;
        private readonly IMyFiles _myFiles;

        private readonly ResponseBadRequest _badRequest;
        private readonly UserModelValidator _userModelValidator;
        private readonly UpdateUserModelValidator _updateUserModelValidator;
        private readonly SendEmailConfirmationValidator _sendEmailConfirmationValidator;
        private readonly ConfirmEmailValidator _confirmEmailValidator;
        private readonly ChangePasswordValidator _changePasswordValidator;
        private readonly ForgotPasswordValidator _forgotPasswordValidator;
        private readonly ResetPasswordValidator _resetPasswordValidator;

        #endregion

        #region Constructor

        public UserController(IMyUser user,
            IMyFiles myFiles,
            UserModelValidator userModelValidator,
            UpdateUserModelValidator updateUserModelValidator,
            SendEmailConfirmationValidator sendEmailConfirmationValidator,
            ConfirmEmailValidator confirmEmailValidator,
            ChangePasswordValidator changePasswordValidator,
            ForgotPasswordValidator forgotPasswordValidator,
            ResetPasswordValidator resetPasswordValidator,
            ResponseBadRequest badRequest) {

            _user = user;
            _myFiles = myFiles;

            _badRequest = badRequest;
            _userModelValidator = userModelValidator;
            _updateUserModelValidator = updateUserModelValidator;
            _sendEmailConfirmationValidator = sendEmailConfirmationValidator;
            _confirmEmailValidator = confirmEmailValidator;
            _changePasswordValidator = changePasswordValidator;
            _forgotPasswordValidator = forgotPasswordValidator;
            _resetPasswordValidator = resetPasswordValidator;
        }

        #endregion

        #region Get Users
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUser(GetUserModel model) {

            var gotUser = await _user.GetUser(model);

            return Ok(gotUser);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list")]
        public async Task<IActionResult> GetUsers() {

            var model = new GetUserModel {
                Email = UserEmail
            };

            var fetchedUsers = await _user.GetUserList(model);

            return Ok(fetchedUsers);
        }

        #endregion

        #region Post Users

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model) {

            #region Validate Model

            var userInputValidated = _userModelValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var createdUser = await _user.CreateUser(model);

            return Ok(createdUser);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model) {

            #region Validate Model

            var userInputValidated = _updateUserModelValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var updatedUser = await _user.UpdateUser(model);

            return Ok(updatedUser);
        }

        [HttpPost("send-email-confirmation")]
        public async Task<IActionResult> GenerateEmailConfirmation([FromBody] SendEmailConfirmation model) {

            #region Validate Model

            var userInputValidated = _sendEmailConfirmationValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var sentEmailConfirmation = await _user.SendEmailConfirmation(model);

            return Ok(sentEmailConfirmation);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model) {

            #region Validate Model

            var userInputValidated = _confirmEmailValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var confirmedEmail = await _user.ConfirmEmail(model);

            return Ok(confirmedEmail);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model) {

            #region Validate Model

            var userInputValidated = _changePasswordValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var changedPassword = await _user.ChangePassword(model);

            return Ok(changedPassword);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model) {

            #region Validate Model

            var userInputValidated = _forgotPasswordValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var sentResetPassword = await _user.ForgotPassword(model);

            return Ok(sentResetPassword);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model) {

            #region Validate Model

            var userInputValidated = _resetPasswordValidator.Validate(model);

            if (!userInputValidated.IsValid) {
                _infos.Add(_badRequest.ShowError(int.Parse(userInputValidated.Errors[0].ErrorMessage)).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var resetPassword = await _user.ResetPassword(model);

            return Ok(resetPassword);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("profile-picture/change")]
        public async Task<IActionResult> ChangeProfilePicture([FromBody] PostFilesModel postModel) {

            var model = new FileBase64Model {
                Files = new List<FileDetails> {
                    new FileDetails {
                        Name = postModel.File.Name,
                        Base64 = postModel.File.Base64
                    }
                },
                Email = UserEmail,
                FileId = postModel.FileId,
                Type = 3
            };

            #region Validate Model

            if (model.Files == null) {

                _infos.Add(_badRequest.ShowError(ResponseBadRequest.ErrFileEmpty).Message);

                return BadRequest(new CustomResponse<string> {
                    Message = _infos
                });
            }

            #endregion

            var savedFiles = await _myFiles.SaveFiles(model);

            return Ok(savedFiles);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("update-background-summary")]
        public async Task<IActionResult> UpdateBackgroundSummary([FromBody] UpdateBackgroundSummaryModel model) {

            model.Email = UserEmail;

            var updatedUser = await _user.UpdateBackgroundSummary(model);

            return Ok(updatedUser);
        }

        #endregion
    }
}
