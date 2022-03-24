using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Data;

public interface IProgGuruDbSchemaMigrator
{
    Task MigrateAsync();
}
