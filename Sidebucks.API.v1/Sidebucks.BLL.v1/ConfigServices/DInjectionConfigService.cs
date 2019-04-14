using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sidebucks.BLL.v1.DataManagers;
using Sidebucks.BLL.v1.DataManagers.Interface;
using Sidebucks.BLL.v1.Helpers;
using Sidebucks.BLL.v1.Methods;
using Sidebucks.BLL.v1.OtherServices;
using Sidebucks.BLL.v1.OtherServices.Interface;
using Sidebucks.BLL.v1.Repositories;
using Sidebucks.BLL.v1.Repositories.Interface;
using Sidebucks.BLL.v1.Validation;
using Sidebucks.DAL.v1.Entities.Mobile;

namespace Sidebucks.BLL.v1.ConfigServices {
    public static class DInjectionConfigService {
        public static IServiceCollection RegisterDInjection(this IServiceCollection services) {

            services.AddTransient<EmailDaemon>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IMyUser, MyUser>();

            services.AddTransient<IMyAuthentication, MyAuthentication>();

            services.AddTransient<IMyFiles, MyFiles>();

            services.AddTransient<IMySkills, MySkills>();

            //services.AddTransient<IMyLocation, MyLocation>();

            services.AddTransient<SidebucksContext>();

            services.AddTransient<ResponseBadRequest>();

            //DATA MANAGERS
            services.AddTransient<IUsersDataManager, UsersDataManager>();

            //OTHER SERVICES
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IProcessRefreshToken, ProcessRefreshToken>();

            //VALIDATION
            services.AddTransient<UserModelValidator>();
            services.AddTransient<UpdateUserModelValidator>();
            services.AddTransient<SendEmailConfirmationValidator>();
            services.AddTransient<ConfirmEmailValidator>();
            services.AddTransient<ChangePasswordValidator>();
            services.AddTransient<ForgotPasswordValidator>();
            services.AddTransient<ResetPasswordValidator>();

            services.AddTransient<SignInModelValidator>();
            services.AddTransient<SignOutModelValidator>();
            services.AddTransient<GenerateTokenModelValidator>();

            //services.AddTransient<FileBase64ModelValidator>();

            //services.AddTransient<LocationModelValidator>();

            return services;
        }
    }
}
