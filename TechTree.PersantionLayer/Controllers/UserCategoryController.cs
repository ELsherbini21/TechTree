using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Helpers;
using TechTree.PersantionLayer.Helpers.interfaces;
using TechTree.PersantionLayer.ViewModels;
using TechTree.PersantionLayer.ViewModels;

namespace TechTree.PersantionLayer.Controllers
{
    public class UserCategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataFunctions _dataFunctions;

        public UserCategoryController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IDataFunctions dataFunctions
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            _dataFunctions = dataFunctions;
        }

        #region Index
        public async Task<IActionResult> Index(int? Id)
        {
            IEnumerable<UserCategory> Model;

            if (Id is null)
                Model = await unitOfWork.UserCategoryRepository.GetAllAsync();
            else
                Model = unitOfWork.UserCategoryRepository.GetAllAsync().Result
                    .Where(userCateg => userCateg.CategoryId == Id.Value);


            var viewModel = mapper.Map<IEnumerable<UserCategory>, IEnumerable<UserCategoryViewModel>>(Model);

            return View(viewModel);


        }



        public async Task<IActionResult> SelectCourse(string userId)
        {

            if (userId is null)
                return BadRequest();

            var appUser = await userManager.FindByIdAsync(userId);

            ViewBag.appUser = appUser;

            if (appUser is null)
                return NotFound();

            var coursesToUser = new List<CoursesToUserViewModel>();

            foreach (var category in await GetCategoriesThatHaveContent())
            {
                var courseToUser = new CoursesToUserViewModel()
                {
                    CategoryId = category.Id,
                    CategoryTitle = category.Title
                };

                if (unitOfWork.UserCategoryRepository.GetAllAsync().Result.Any(userCateg => userCateg.ApplicationUserId == userId && userCateg.CategoryId == category.Id))
                    courseToUser.IsSelected = true;
                else
                    courseToUser.IsSelected = false;

                coursesToUser.Add(courseToUser);
            }

            return View(coursesToUser);
        }

        [HttpPost]
        public async Task<IActionResult> SelectCourse(List<CoursesToUserViewModel> courses, string userId)
        {
            if (userId is null || courses.Count() <= 0)
                return BadRequest();

            var appUser = await userManager.FindByIdAsync(userId);

            if (ModelState.IsValid)
            {
                var userCategories = await unitOfWork.UserCategoryRepository.GetAllAsync();

                foreach (var item in courses)
                {
                    var userCategoryObject = userCategories.FirstOrDefault(userCateg => userCateg.ApplicationUserId == userId && userCateg.CategoryId == item.CategoryId);

                    var userCategory = new UserCategory() { ApplicationUserId = userId, CategoryId = item.CategoryId };

                    if (item.IsSelected == true && !(userCategories.Any(userCateg => userCateg.ApplicationUserId == userId && userCateg.CategoryId == item.CategoryId)))
                    {
                        unitOfWork.UserCategoryRepository.Add(userCategory);
                    }
                    else if ((item.IsSelected == false && (userCategories.Any(userCateg => userCateg.ApplicationUserId == userId && userCateg.CategoryId == item.CategoryId))))
                    {
                        unitOfWork.UserCategoryRepository.Delete(userCategoryObject);
                    }

                }

                await unitOfWork.Complete();

                return RedirectToAction("Index", "Home");
            }

            return View(courses);
        }


        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var Model = await unitOfWork.CategoryItemRepository.GetByIdAsync(id.Value);

            if (Model == null)
                return NotFound();

            var ViewModel =
                    mapper.Map<CategoryItem, CategoryItemViewModel>(Model);

            return View(ViewModel);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(string applicationUserId, int? categoryId, UserCategoryViewModel viewModel)
        {

            if (applicationUserId is not null && categoryId is not null)
            {
                var condition = unitOfWork.UserCategoryRepository.GetAllAsync()
                    .Result.Any(
                    categ => categ.CategoryId == categoryId.Value &&
                    categ.ApplicationUserId == applicationUserId
                    );
                if (!condition)
                {

                    var Model = new UserCategory
                    { CategoryId = categoryId.Value, ApplicationUserId = applicationUserId };

                    unitOfWork.UserCategoryRepository.Add(Model);

                    await unitOfWork.Complete();

                    return RedirectToAction(nameof(Index), new { Id = viewModel.CategoryId });
                }

                else
                    return View(viewModel);

            }
            return View(viewModel);

        }

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var Model = await unitOfWork.CategoryItemRepository.GetByIdAsync(id.Value);

            if (Model == null)
                return NotFound();

            var ViewModel =
                    mapper.Map<CategoryItem, CategoryItemViewModel>(Model);

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, UserCategoryViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var Model = mapper.Map<UserCategoryViewModel, UserCategory>(viewModel);

                    unitOfWork.UserCategoryRepository.Update(Model);

                    await unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(viewModel);

        }

        #endregion


        private async Task<List<Category>> GetCategoriesThatHaveContent()
        {
            var categoriesThatHaveContent = (from category in await unitOfWork.CategoryRepository.GetAllAsync()

                                             join categoryItem in await unitOfWork.CategoryItemRepository.GetAllAsync()
                                             on category.Id equals categoryItem.CategoryId

                                             join content in await unitOfWork.ContentRepository.GetAllAsync()
                                             on categoryItem.Id equals content.CategoryItem.Id

                                             select new Category()
                                             {
                                                 Id = category.Id,
                                                 Title = category.Title,
                                                 Description = category.Description,

                                             }
                                                  ).DistinctBy(categ => categ.Title).ToList();

            return categoriesThatHaveContent;
        }


    }
}
