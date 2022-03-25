using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Users;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Daridakr.ProgGuru.EntityFrameworkCore;

public static class ProgGuruEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        ProgGuruGlobalFeatureConfigurator.Configure();
        ProgGuruModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
               .MapEfCoreProperty<IdentityUser, DateTime>(
                   "BornDate",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired();
                   }
               ).MapEfCoreProperty<IdentityUser, UserCategory>(
                   "Category",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired().HasDefaultValue(UserCategory.Trainee);
                   }
               ).MapEfCoreProperty<IdentityUser, UserStatus>(
                   "Status",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired().HasDefaultValue(UserStatus.Undefined);
                   }
               ).MapEfCoreProperty<IdentityUser, UserEducation>(
                   "Education",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired().HasDefaultValue(UserEducation.Secondary);
                   }
               ).MapEfCoreProperty<IdentityUser, int>(
                   "BeginningYear",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired();
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "BornLocation",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.HasMaxLength(UserConsts.MaxLocationLength).HasDefaultValue("");
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "CurrentLocation",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.HasMaxLength(UserConsts.MaxLocationLength).IsRequired().HasDefaultValue("");
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "Specialization",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.HasMaxLength(UserConsts.MaxSpecializationLength).HasDefaultValue("");
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "Bio",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.HasMaxLength(UserConsts.MaxBioLength).HasDefaultValue("");
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "ProfilePictureUrl",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.HasDefaultValue(UserConsts.DefaultProfilePicture);
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "GithubUsername",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired().HasMaxLength(UserConsts.MaxSocialNetworkUsernameLength).HasDefaultValue(""); ;
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "TelegramUsername",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired().HasMaxLength(UserConsts.MaxSocialNetworkUsernameLength).HasDefaultValue(""); ;
                   }
                ).MapEfCoreProperty<IdentityUser, string>(
                   "DiscordCode",
                   (entityBuilder, propertyBuilder) =>
                   {
                       propertyBuilder.IsRequired().HasMaxLength(UserConsts.DiscordCodeLength).HasDefaultValue(""); ;
                   }
                );
        });
    }
}
