using System;
using System.Collections.Generic;
using System.Text;
using Daridakr.ProgGuru.Localization;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru;

/* Inherit your application services from this class.
 */
public abstract class ProgGuruAppService : ApplicationService
{
    protected ProgGuruAppService()
    {
        LocalizationResource = typeof(ProgGuruResource);
    }
}
