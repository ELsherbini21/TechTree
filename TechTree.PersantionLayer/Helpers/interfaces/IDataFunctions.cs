using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.Helpers.interfaces
{
    public interface IDataFunctions
    {
        Task DataFunctionsAddAndRemoveRanges(List<UserCategory> entitiesToDelete, List<UserCategory> entitiesToAdd);
    }
}
