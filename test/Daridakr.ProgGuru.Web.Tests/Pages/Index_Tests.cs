using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Daridakr.ProgGuru.Pages;

public class Index_Tests : ProgGuruWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
