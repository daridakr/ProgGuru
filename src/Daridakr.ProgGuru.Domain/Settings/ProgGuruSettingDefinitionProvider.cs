using Volo.Abp.Settings;

namespace Daridakr.ProgGuru.Settings;

public class ProgGuruSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ProgGuruSettings.MySetting1));
    }
}
