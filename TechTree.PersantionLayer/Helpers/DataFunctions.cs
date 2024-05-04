using Microsoft.AspNetCore.Mvc.ModelBinding;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Helpers.interfaces;

namespace TechTree.PersantionLayer.Helpers
{



    public class DataFunctions : IDataFunctions
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataFunctions(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task DataFunctionsAddAndRemoveRanges(List<UserCategory> entitiesToDelete, List<UserCategory> entitiesToAdd)
        {
            if (entitiesToDelete is null || entitiesToAdd is null)
                return;

            using var dbContextTransActions = _unitOfWork.BeginTransaction();

            try
            {

                await _unitOfWork.UserCategoryRepository.RemoveRange(entitiesToDelete);

                await _unitOfWork.UserCategoryRepository.AddRange(entitiesToAdd);

                await _unitOfWork.CommitTransaction();

                await _unitOfWork.Complete();
            }
            catch (Exception)
            {
                dbContextTransActions.Dispose();
            }
        }
    }
}
