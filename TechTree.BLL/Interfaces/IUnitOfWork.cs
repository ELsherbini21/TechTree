using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTree.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; set; }

        ICategoryItemRepository CategoryItemRepository { get; set; }

        IContentRepository ContentRepository { get; set; }

        IMediaTypeRepository MediaTypeRepository { get; set; }

        IUserCategoryRepository UserCategoryRepository { get; set; }

        Task Complete();

        Task BeginTransaction();

        Task CommitTransaction();
    }
}
