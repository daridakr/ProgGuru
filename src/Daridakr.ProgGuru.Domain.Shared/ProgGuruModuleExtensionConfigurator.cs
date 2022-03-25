using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Daridakr.ProgGuru.Localization;
using Daridakr.ProgGuru.Enums.Users;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Helpers;

namespace Daridakr.ProgGuru;

public static class ProgGuruModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        IdentityUserConsts.MaxEmailLength = 99;
        IdentityUserConsts.MaxUserNameLength = 99;
    }

    /// <summary>
    /// Method is called at the beginning of the application.
    /// </summary>
    private static void ConfigureExtraProperties()
    {
        // Guarantees to execute this code only one time per application, since multiple calls are unnecessary
        OneTimeRunner.Run(() =>
        {
            // Starting point to configure a module
            ObjectExtensionManager.Instance.Modules()
                // Method is used to configure the entities of the Identity Module
                .ConfigureIdentity(identity =>
                {
                        // Is used to configure the user entity of the identity module,
                        // because not all entities are designed to be extensible (since it is not needed).
                        identity.ConfigureUser(user =>
                    {
                            // Is used to add a new property to the user entity with the optional type

                            // BORN DATE
                            user.AddOrUpdateProperty<DateTime?>( //property type
                            "BornDate", //property name
                            property =>
                            {
                                //additional options for the new property (validation rules)
                                property.Attributes.Add(new RequiredAttribute());
                                property.Attributes.Add(new DataTypeAttribute(DataType.Date));
                                property.Validators.Add(context =>
                                {
                                    var localizer = context.ServiceProvider.GetRequiredService<IStringLocalizer<ProgGuruResource>>();
                                    DateTime bornDate = DateTime.Parse(context.Value.ToString());
                                    int userAge = ProgGuruService.CalculateAge(bornDate, DateTime.Now);
                                    if (userAge < 14)
                                    {
                                        context.ValidationErrors.Add(
                                            new ValidationResult(
                                                localizer[$"You must be at least 14 years old"],
                                                new[] { "extraProperties.BornDate" }
                                            )
                                        );
                                    }
                                    int minBornYear = DateTime.Now.Year - 120;
                                    if (bornDate.Year < minBornYear)
                                    {
                                        context.ValidationErrors.Add(
                                            new ValidationResult(
                                                localizer["The year of born cannot be less than {0}", minBornYear],
                                                new[] { "extraProperties.BornDate" }
                                            )
                                        );
                                    }
                                });
                                property.UI.OnEditForm.IsVisible = false;
                                property.Api.OnUpdate.IsAvailable = false;
                            }
                        );

                            // CATEGORY
                            user.AddOrUpdateProperty<UserCategory>("Category");

                            // STATUS
                            user.AddOrUpdateProperty<UserStatus>("Status");

                            // EDUCATION
                            user.AddOrUpdateProperty<UserEducation>("Education");

                        // BEGGINING YEAR
                        user.AddOrUpdateProperty<int>(
                            "BeginningYear",
                            property =>
                            {
                                property.Attributes.Add(new RequiredAttribute());
                                property.Validators.Add(context =>
                                {
                                    var localizer = context.ServiceProvider.GetRequiredService<IStringLocalizer<ProgGuruResource>>();
                                    int nowYear = DateTime.Now.Year;
                                    int startYear = nowYear - 90;
                                    if ((int)context.Value > nowYear || (int)context.Value < startYear)
                                    {
                                        context.ValidationErrors.Add(
                                            new ValidationResult(
                                                localizer["Beginning year must be between {0} and {1}", startYear, nowYear],
                                                new[] { "extraProperties.BeginningYear" }
                                            )
                                        );
                                    }
                                });
                                property.UI.OnTable.IsVisible = false;
                            }
                        );

                            // BORN LOCATION
                            user.AddOrUpdateProperty<string>(
                            "BornLocation",
                            property =>
                            {
                                property.Attributes.Add(
                                    new StringLengthAttribute(UserConsts.MaxLocationLength)
                                    {
                                        MinimumLength = UserConsts.MinLocationLength
                                    }
                                );
                                //property.UI.OnTable.IsVisible = false;
                                //property.UI.OnEditForm.IsVisible = false;
                            }
                        );

                            // CUR LOCATION
                            user.AddOrUpdateProperty<string>(
                            "CurrentLocation",
                            property =>
                            {
                                property.Attributes.Add(new RequiredAttribute());
                                property.DefaultValue = "";
                                property.Attributes.Add(
                                    new StringLengthAttribute(UserConsts.MaxLocationLength)
                                    {
                                        MinimumLength = UserConsts.MinLocationLength
                                    }
                                );
                            }
                        );

                            // SPECIALIZATION
                            user.AddOrUpdateProperty<string>(
                            "Specialization",
                            property =>
                            {
                                property.DefaultValue = "";
                                property.Attributes.Add(
                                    new StringLengthAttribute(UserConsts.MaxSpecializationLength)
                                    {
                                        MinimumLength = UserConsts.MinSpecializationLength
                                    }
                                );
                            }
                        );

                            // BIO
                            user.AddOrUpdateProperty<string>(
                            "Bio",
                            property =>
                            {
                                property.Attributes.Add(new DataTypeAttribute(DataType.Text));
                                property.Attributes.Add(
                                    new StringLengthAttribute(UserConsts.MaxBioLength)
                                    {
                                        MinimumLength = UserConsts.MinBioLength
                                    }
                                );
                                property.UI.OnTable.IsVisible = false;
                                property.UI.OnCreateForm.IsVisible = false;
                            }
                        );

                        // GITHUB
                        user.AddOrUpdateProperty<string>(
                        "GithubUsername",
                        property =>
                        {
                            property.DefaultValue = "";
                            property.Attributes.Add(new RequiredAttribute());
                            property.Attributes.Add(
                                new StringLengthAttribute(UserConsts.MaxSocialNetworkUsernameLength)
                                {
                                    MinimumLength = UserConsts.MinSocialNetworkUsernameLength
                                }
                            );
                        }
                    );

                        // TELEGRAM
                        user.AddOrUpdateProperty<string>(
                        "TelegramUsername",
                        property =>
                        {
                            property.DefaultValue = "";
                            property.Attributes.Add(new RequiredAttribute());
                            property.Attributes.Add(
                                new StringLengthAttribute(UserConsts.MaxSocialNetworkUsernameLength)
                                {
                                    MinimumLength = UserConsts.MinSocialNetworkUsernameLength
                                }
                            );
                        }
                    );

                        // DISCORD
                        user.AddOrUpdateProperty<string>(
                        "DiscordCode",
                        property =>
                        {
                            property.DefaultValue = "";
                            property.Attributes.Add(new RequiredAttribute());
                            property.Attributes.Add(
                                new StringLengthAttribute(UserConsts.DiscordCodeLength)
                                {
                                    MinimumLength = UserConsts.DiscordCodeLength
                                }
                            );
                        }
                    );


                        // PROFILE PICTURE
                        user.AddOrUpdateProperty<string>(
                            "ProfilePictureUrl",
                            property =>
                            {
                                property.Attributes.Add(new DataTypeAttribute(DataType.ImageUrl));
                                property.UI.OnTable.IsVisible = false;
                                property.UI.OnCreateForm.IsVisible = false;
                                property.UI.OnEditForm.IsVisible = false;
                                property.DefaultValue = UserConsts.DefaultProfilePicture;
                            }
                        );
                    });
                });
        });
    }
}
