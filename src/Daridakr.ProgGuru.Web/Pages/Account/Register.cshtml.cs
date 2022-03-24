using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Email;
using Daridakr.ProgGuru.Enums.Users;
using Daridakr.ProgGuru.Helpers;
using Daridakr.ProgGuru.Localization;
using Daridakr.ProgGuru.Users.Specializations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Settings;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Daridakr.ProgGuru.Web.Pages.Account
{
    public class RegisterModel : AccountPageModel
    {

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        [BindProperty]
        public PostInput Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsExternalLogin { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ExternalLoginAuthSchema { get; set; }

        public List<SelectListItem> Specializations { get; set; }

        private readonly ISpecializationAppService _specializationAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        private readonly EmailService _emailService;

        private IdentityUser _abpIdentityUser;

        public RegisterModel(
            IAccountAppService accountAppService,
            ISpecializationAppService specializationAppService,
            IStringLocalizer<ProgGuruResource> localizer,
            EmailService emailService)
        {
            AccountAppService = accountAppService;
            _specializationAppService = specializationAppService;
            _localizer = localizer;
            _emailService = emailService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            await CheckSelfRegistrationAsync();
            await TrySetEmailAsync();

            var specializationsLookup = await _specializationAppService.GetSpecializationLookupAsync();
            Specializations = specializationsLookup.Items
                .Select(s => new SelectListItem(s.Name, s.Id.ToString()))
                .ToList();
            return Page();
        }

        private async Task TrySetEmailAsync()
        {
            if (IsExternalLogin)
            {
                var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
                if (externalLoginInfo == null)
                {
                    return;
                }

                if (!externalLoginInfo.Principal.Identities.Any())
                {
                    return;
                }

                var identity = externalLoginInfo.Principal.Identities.First();
                var emailClaim = identity.FindFirst(ClaimTypes.Email);

                if (emailClaim == null)
                {
                    return;
                }

                Input = new PostInput { EmailAddress = emailClaim.Value };
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await CheckSelfRegistrationAsync();

                if (IsExternalLogin)
                {
                    var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        Logger.LogWarning("External login info is not available");
                        return RedirectToPage("./Login");
                    }

                    await RegisterExternalUserAsync(externalLoginInfo, Input.EmailAddress);
                }
                else
                {
                    await RegisterLocalUserAsync();
                }

                if (UserManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = Input.EmailAddress, returnUrl = ReturnUrl });
                }
                else
                { 
                    await SignInManager.SignInAsync(_abpIdentityUser, isPersistent: true);
                    return LocalRedirect(ReturnUrl);
                }

                //return Redirect(ReturnUrl ?? "~/"); //TODO: How to ensure safety? IdentityServer requires it however it should be checked somehow!
            }
            catch (BusinessException e)
            {
                Alerts.Danger(GetLocalizeExceptionMessage(e));
                return await OnGetAsync();
                //return Page();
            }
            catch (AbpValidationException e)
            {
                Alerts.Danger(string.Join(",", e.ValidationErrors.Select(a => a.ErrorMessage).ToList()));
                return await OnGetAsync();
            }
        }

        private void ValidateBornDate()
        {
            int userAge = ProgGuruService.CalculateAge(Input.BornDate, DateTime.Now);
            if (userAge < 14)
            {
                throw new UserFriendlyException(_localizer[$"You must be at least 14 years old"]);
            }
            int minBornYear = DateTime.Now.Year - 120;
            if (Input.BornDate.Year < minBornYear)
            {
                throw new UserFriendlyException(_localizer["The year of born cannot be less than {0}", minBornYear]);
            }
        }

        private void ValidateBeginningYear()
        {
            int nowYear = DateTime.Now.Year;
            int startYear = nowYear - 90;
            if (Input.BeginningYear > nowYear || Input.BeginningYear < startYear)
            {
                throw new UserFriendlyException(_localizer["Beginning year must be between {0} and {1}", startYear, nowYear]);
            }
        }

        protected virtual async Task RegisterLocalUserAsync()
        {
            ValidateModel();

            await CheckSelfRegistrationAsync();
            await IdentityOptions.SetAsync();
            var user = new IdentityUser(GuidGenerator.Create(), Input.UserName, Input.EmailAddress, CurrentTenant.Id);

            ValidateBornDate();
            ValidateBeginningYear();

            user.Name = Input.Name;
            user.Surname = Input.Surname;
            user.SetPhoneNumber(Input.PhoneNumber, false);
            user.ExtraProperties["GithubUsername"] = Input.GithubUsername;
            user.ExtraProperties["TelegramUsername"] = Input.TelegramUsername;
            user.ExtraProperties["DiscordCode"] = Input.DiscordCode;
            user.ExtraProperties["Category"] = Input.Category;
            user.ExtraProperties["Status"] = Input.Status;
            user.ExtraProperties["BornLocation"] = Input.BornLocation;
            user.ExtraProperties["CurrentLocation"] = Input.CurrentLocation;
            user.ExtraProperties["BornDate"] = Input.BornDate;
            user.ExtraProperties["Education"] = Input.Education;
            user.ExtraProperties["Specialization"] = Input.Specialization;
            user.ExtraProperties["BeginningYear"] = Input.BeginningYear;
            user.ExtraProperties["Bio"] = Input.Bio;

            await _specializationAppService.CreateAsync(new SpecializationDto { Name = Input.Specialization, CreatorId = user.Id });

            (await UserManager.CreateAsync(user, Input.Password)).CheckErrors();
            await UserManager.SetEmailAsync(user, Input.EmailAddress);
            await UserManager.AddDefaultRolesAsync(user);

            _abpIdentityUser = user;

            await SendEmailToAskForEmailConfirmationAsync(_abpIdentityUser);
        }

        protected virtual async Task RegisterExternalUserAsync(ExternalLoginInfo externalLoginInfo, string emailAddress)
        {
            await CheckSelfRegistrationAsync();
            await IdentityOptions.SetAsync();
            var user = new IdentityUser(GuidGenerator.Create(), Input.UserName, emailAddress, CurrentTenant.Id);

            ValidateBornDate();
            ValidateBeginningYear();

            user.Name = Input.Name;
            user.Surname = Input.Surname;
            user.SetPhoneNumber(Input.PhoneNumber, false);
            user.ExtraProperties["GithubUsername"] = Input.GithubUsername;
            user.ExtraProperties["TelegramUsername"] = Input.TelegramUsername;
            user.ExtraProperties["DiscordCode"] = Input.DiscordCode;
            user.ExtraProperties["Category"] = Input.Category;
            user.ExtraProperties["Status"] = Input.Status;
            user.ExtraProperties["BornLocation"] = Input.BornLocation;
            user.ExtraProperties["CurrentLocation"] = Input.CurrentLocation;
            user.ExtraProperties["BornDate"] = Input.BornDate;
            user.ExtraProperties["Education"] = Input.Education;
            user.ExtraProperties["Specialization"] = Input.Specialization;
            user.ExtraProperties["BeginningYear"] = Input.BeginningYear;
            user.ExtraProperties["Bio"] = Input.Bio;

            await _specializationAppService.CreateAsync(new SpecializationDto { Name = Input.Specialization, CreatorId = user.Id });

            (await UserManager.CreateAsync(user, Input.Password)).CheckErrors();
            (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

            var userLoginAlreadyExists = user.Logins.Any(x =>
                x.TenantId == user.TenantId &&
                x.LoginProvider == externalLoginInfo.LoginProvider &&
                x.ProviderKey == externalLoginInfo.ProviderKey);

            if (!userLoginAlreadyExists)
            {
                (await UserManager.AddLoginAsync(user, new UserLoginInfo(
                    externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey,
                    externalLoginInfo.ProviderDisplayName
                ))).CheckErrors();
            }

            _abpIdentityUser = user;

            await SendEmailToAskForEmailConfirmationAsync(_abpIdentityUser);
        }

        protected virtual async Task CheckSelfRegistrationAsync()
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled) ||
                !await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }
        }

        private async Task SendEmailToAskForEmailConfirmationAsync(IdentityUser user)
        {
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);
            await _emailService.SendEmailAsync(Input.EmailAddress, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        public class PostInput
        {
            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
            public string Name { get; set; }

            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
            public string Surname { get; set; }

            [Required]
            [StringLength(UserConsts.MaxLocationLength)]
            public string BornLocation { get; set; }

            [DataType(DataType.Date)]
            [Required]
            public DateTime BornDate { get; set; }

            [StringLength(UserConsts.MaxSpecializationLength)]
            [SelectItems(nameof(Specializations))]
            public string Specialization { get; set; }

            [Required]
            public UserCategory Category { get; set; } = UserCategory.Trainee;

            [Required]
            public int BeginningYear { get; set; }

            [Required]
            public UserEducation Education { get; set; } = UserEducation.Secondary;

            [Required]
            [StringLength(UserConsts.MaxLocationLength)]
            public string CurrentLocation { get; set; }

            [Required]
            public UserStatus Status { get; set; } = UserStatus.Undefined;

            [TextArea]
            public string Bio { get; set; }

            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
            public string EmailAddress { get; set; }

            [Required]
            [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
            [DataType(DataType.Password)]
            [DisableAuditing]
            public string Password { get; set; }

            [Required]
            [StringLength(UserConsts.MaxSocialNetworkUsernameLength)]
            public string GithubUsername { get; set; }

            [Required]
            [StringLength(UserConsts.MaxSocialNetworkUsernameLength)]
            public string TelegramUsername { get; set; }

            [Required]
            [StringLength(UserConsts.DiscordCodeLength)]
            public string DiscordCode { get; set; }
        }
    }
}
