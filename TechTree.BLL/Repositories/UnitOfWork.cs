using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Data;

namespace TechTree.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(
            AppDbContext dbContext,
            ICategoryRepository categoryRepository,
            ICategoryItemRepository categoryItemRepository,
            IMediaTypeRepository mediaTypeRepository,
            IContentRepository contentRepository,
            IUserCategoryRepository userCategoryRepository
            )
        {
            _dbContext = dbContext;
            CategoryRepository = categoryRepository;
            CategoryItemRepository = categoryItemRepository;
            MediaTypeRepository = mediaTypeRepository;
            ContentRepository = contentRepository;
            UserCategoryRepository = userCategoryRepository;
        }


        public ICategoryRepository CategoryRepository { get; set; }

        public ICategoryItemRepository CategoryItemRepository { get; set; }

        public IContentRepository ContentRepository { get; set; }

        public IMediaTypeRepository MediaTypeRepository { get; set; }

        public IUserCategoryRepository UserCategoryRepository { get; set; }


        public async Task Complete() => await _dbContext.SaveChangesAsync();

        public async void Dispose() => await _dbContext.DisposeAsync();

        public async Task BeginTransaction() => await _dbContext.Database.BeginTransactionAsync();

        public async Task CommitTransaction() => await _dbContext.Database.CommitTransactionAsync();



    }
}
