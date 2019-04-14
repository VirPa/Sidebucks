using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sidebucks.BLL.v1.DataManagers.Interface;
using Sidebucks.BLL.v1.OtherServices.Interface;
using Sidebucks.BLL.v1.Repositories.Interface;
using Sidebucks.DAL.v1.Entities.Mobile;
using Sidebucks.DAL.v1.Identity;
using Sidebucks.DAL.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sidebucks.BLL.v1.Repositories {
    public class MyUser : IMyUser {
        #region Initialization

        private readonly List<string> _infos = new List<string>();

        private readonly IOptions<Manifest> _options;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUsersDataManager _usersDataManager;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SidebucksContext _context;

        #endregion

        #region Constructor

        public MyUser(IOptions<Manifest> options,
                      IEmailSender emailSender,
                      IMapper mapper,
                      IUsersDataManager usersDataManager,
                      UserManager<ApplicationUser> userManager,
                      SidebucksContext context) {

            _options = options;
            _emailSender = emailSender;
            _mapper = mapper;
            _usersDataManager = usersDataManager;

            _userManager = userManager;
            _context = context;
        }

        #endregion

        #region Get

        public async Task<CustomResponse<UserResponse>> GetUser(GetUserModel model) {

            return await Task.Run(() => {

                var user = _usersDataManager.GetUser(new GetUserModel { UserId = model.UserId });

                #region Validate User

                if (user == null) {
                    _infos.Add("User not exist.");

                    return new CustomResponse<UserResponse> {
                        Message = _infos
                    };
                };

                #endregion

                return new CustomResponse<UserResponse> {
                    Succeed = true,
                    Data = new UserResponse {
                        Detail = new UserDetails {
                            Id = user.UserId,
                            UserName = user.UserName,
                            Email = user.Email,
                            Fullname = user.Fullname,
                            Role = user.Role,
                            MobileNumber = user.MobileNumber,
                            BackgroundSummary = user.BackgroundSummary,
                            Skill = user.Skill,
                            CreatedAt = user.CreatedAt,
                            UpdatedAt = user.UpdatedAt
                        },
                        ProfilePicture = GetProfilePicture(user.UserId),
                        Location = GetUserLocation(user.UserId)
                    }
                };
            });
        }

        public async Task<CustomResponse<GetUsersModel>> GetUserList(GetUserModel model) {

            var user = await _userManager.FindByEmailAsync(model.Email);

            var users = _usersDataManager.GetUsers(new GetUserModel() { UserId = user.Id });

            return new CustomResponse<GetUsersModel> {
                Succeed = true,
                Data = users
            };
        }

        public GetFilesListResponse GetProfilePicture(string userId) {

            var profilePicture = _context.Files.FirstOrDefault(p => p.UserId == userId && p.Type == 3);

            if (profilePicture == null) return null;

            return new GetFilesListResponse {
                Id = profilePicture.Id,
                Name = profilePicture.Name,
                CodeName = profilePicture.CodeName,
                Extension = profilePicture.Extension,
                FilePath = profilePicture.FilePath,
                Type = profilePicture.Type ?? 3,
                CreatedAt = profilePicture.CreatedAt
            };
        }

        public PinLocationDetailResponseModel GetUserLocation(string userId) {

            var location = _context.Location.FirstOrDefault(p => p.UserId == userId);

            return location == null ? null : _mapper.Map<PinLocationDetailResponseModel>(location);
        }

        #endregion

        #region Post Users

        public async Task<CustomResponse<CreateUserResponseModel>> CreateUser(CreateUserModel inputs) {

            inputs.UserName = inputs.UserName ?? inputs.Email;
            
            #region Validattion
            var user = await _userManager.FindByEmailAsync(inputs.Email);
            var user2 = await _userManager.FindByNameAsync(inputs.UserName);

            if (user != null || user2 != null) {

                _infos.Add("Email/Username address already exist.");

                return new CustomResponse<CreateUserResponseModel> {
                    Message = _infos
                };
            }

            if (inputs.Role != "Provider") {
                if (inputs.Role != "Customer") {
                    _infos.Add("Role value is invalid.");

                    return new CustomResponse<CreateUserResponseModel> {
                        Message = _infos
                    };
                }
            }
            #endregion

            var newUser = _mapper.Map<ApplicationUser>(inputs);

            var newUserCreated = await _userManager.CreateAsync(newUser, inputs.Password);

            if (!newUserCreated.Succeeded) _infos.Add(string.Join("xx | xx", newUserCreated.Errors));

            await _userManager.AddToRoleAsync(newUser, inputs.Role);

            await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, "Client"));

            return new CustomResponse<CreateUserResponseModel> {
                Succeed = newUserCreated.Succeeded,
                Data = new CreateUserResponseModel {
                    User = GetUser(new GetUserModel {
                        UserId = newUser.Id
                    }).Result.Data
                }
            };
        }

        public async Task<CustomResponse<UserResponse>> UpdateUser(UpdateUserModel model) {

            var user = _context.AspNetUsers.FirstOrDefault(u => u.Id == model.UserId);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<UserResponse> {
                    Message = _infos
                };
            };

            #endregion

            user.Fullname = string.IsNullOrEmpty(model.Fullname) ? user.Fullname : model.Fullname;
            user.MobileNumber = string.IsNullOrEmpty(model.MobileNumber) ? user.MobileNumber : model.MobileNumber;
            user.UpdatedAt = DateTime.UtcNow;
            user.BackgroundSummary = model.BackgroundSummary;

            _context.Update(user);

            await _context.SaveChangesAsync();

            return new CustomResponse<UserResponse> {
                Succeed = true,
                Data = GetUser(new GetUserModel {
                    UserId = user.Id
                }).Result.Data
            };
        }

        public async Task<CustomResponse<ConfirmEmailModel>> SendEmailConfirmation(SendEmailConfirmation model) {

            var user = await _userManager.FindByEmailAsync(model.Email);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<ConfirmEmailModel> {
                    Message = _infos
                };
            };

            #endregion

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (_options.Value.SendEmail) {

                await _emailSender.SendEmailWithTemplateAsync(SendEmailWrapper());
            }

            return new CustomResponse<ConfirmEmailModel> {
                Succeed = true,
                Data = new ConfirmEmailModel {
                    UserId = user.Id,
                    Token = token
                }
            };

            #region Local Methods

            SendEmailWithTemplateModel SendEmailWrapper() {

                return new SendEmailWithTemplateModel {
                    Recipient = model.Email,
                    FullName = user.Fullname,
                    Subject = "Email Confirmation",
                    Template = "Confirm-Email-Template",
                    Link = model.Uri + $"/user/confirm-email?id={user.Id}&token={token}&mode=confirm"
                };
            }

            #endregion
        }

        public async Task<CustomResponse<string>> ConfirmEmail(ConfirmEmailModel model) {

            var user = await _userManager.FindByIdAsync(model.UserId);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<string> {
                    Message = _infos
                };
            };

            #endregion

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (!result.Succeeded) _infos.Add("Failed to confirm email.");

            return new CustomResponse<string> {
                Succeed = result.Succeeded,
                Data = null,
                Message = _infos
            };
        }

        public async Task<CustomResponse<string>> ChangePassword(ChangePasswordModel model) {

            var user = await _userManager.FindByEmailAsync(model.Email);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<string> {
                    Message = _infos
                };
            };

            #endregion

            var isPasswordGood = await _userManager.CheckPasswordAsync(user, model.OldPassword);

            if (!isPasswordGood) _infos.Add("You entered wrong Old password.");

            return new CustomResponse<string> {
                Succeed = isPasswordGood,
                Data = null,
                Message = _infos
            };
        }

        public async Task<CustomResponse<ForgotPasswordResponseModel>> ForgotPassword(ForgotPasswordModel model) {

            var user = await _userManager.FindByEmailAsync(model.Email);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<ForgotPasswordResponseModel> {
                    Message = _infos
                };
            };

            #endregion

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (_options.Value.SendEmail) {

                await _emailSender.SendEmailWithTemplateAsync(SendEmailWrapper());
            }

            return new CustomResponse<ForgotPasswordResponseModel> {
                Succeed = true,
                Data = new ForgotPasswordResponseModel {
                    UserId = user.Id,
                    Token = token
                }
            };

            #region Local Methods

            SendEmailWithTemplateModel SendEmailWrapper() {

                return new SendEmailWithTemplateModel {
                    Recipient = model.Email,
                    FullName = user.Fullname,
                    Subject = "Reset Password",
                    Template = "Forgot-Password-Template",
                    Link = model.Uri + $"/user/reset-password?id={user.Id}&token={token}&mode=forgot"
                };
            }

            #endregion
        }

        public async Task<CustomResponse<string>> ResetPassword(ResetPasswordModel model) {

            var user = await _userManager.FindByIdAsync(model.UserId);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<string> {
                    Message = _infos
                };
            };

            #endregion

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded) _infos.Add("Failed to reset password.");

            return new CustomResponse<string> {
                Succeed = result.Succeeded,
                Data = null,
                Message = _infos
            };
        }

        public async Task<CustomResponse<UserResponse>> UpdateBackgroundSummary(UpdateBackgroundSummaryModel model) {

            var user = _context.AspNetUsers.FirstOrDefault(u => u.Email == model.Email);

            #region Validate User

            if (user == null) {
                _infos.Add("User not exist.");

                return new CustomResponse<UserResponse> {
                    Message = _infos
                };
            };

            #endregion

            user.BackgroundSummary = model.BackgroundSummary;

            _context.Update(user);

            await _context.SaveChangesAsync();

            return new CustomResponse<UserResponse> {
                Succeed = true,
                Data = GetUser(new GetUserModel {
                    UserId = user.Id
                }).Result.Data,
                Message = _infos
            };
        }

        #endregion
    }
}
