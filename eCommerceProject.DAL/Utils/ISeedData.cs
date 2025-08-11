
namespace eCommerceProject.DAL.Utilts
{
    public interface ISeedData
    {
        Task DataSeedingAsync();
        Task IdentityDataSeedingAsync();
    }
}