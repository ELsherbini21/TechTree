using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Xml;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.ViewModels;
using TechTree.PersantionLayer.Helpers;
using Microsoft.AspNetCore.Identity;
using TechTree.BLL.Repositories;
using TechTree.PersantionLayer.Helpers.interfaces;

namespace TechTree.PersantionLayer.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSettings _emailSettings;

        public CategoryController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailSettings emailSettings
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSettings = emailSettings;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            var categoryModels = await unitOfWork.CategoryRepository.GetAllAsync();

            var categoryViewModels =
                    mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categoryModels);


            return View(categoryViewModels);
        }
        #endregion

        #region Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryModel = await unitOfWork.CategoryRepository.GetByIdAsync(id.Value);

            if (categoryModel == null)
                return NotFound();

            var categoryViewModel =
                    mapper.Map<Category, CategoryViewModel>(categoryModel);

            return View(categoryViewModel);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.thumbnailImagePath =
                    DocumentSettings.UploadFile(viewModel.thumbnailImagePathUploadImage, "Images");

                var Model = mapper.Map<CategoryViewModel, Category>(viewModel);

                unitOfWork.CategoryRepository.Add(Model);

                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));

            }

            return View(viewModel);
        }

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryModel = await unitOfWork.CategoryRepository.GetByIdAsync(id.Value);

            if (categoryModel == null)
                return NotFound();

            var categoryViewModel =
                    mapper.Map<Category, CategoryViewModel>(categoryModel);

            ViewData["ImgPath"] = categoryModel.thumbnailImagePath;

            return View(categoryViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, CategoryViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (viewModel.thumbnailImagePathUploadImage is not null)
                viewModel.thumbnailImagePath = DocumentSettings.UploadFile(viewModel.thumbnailImagePathUploadImage, "Images");

            if (ModelState.IsValid)
            {
                try
                {
                    var Model = mapper.Map<CategoryViewModel, Category>(viewModel);

                    unitOfWork.CategoryRepository.Update(Model);

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

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryModel = await unitOfWork.CategoryRepository.GetByIdAsync(id.Value);

            if (categoryModel == null)
                return NotFound();

            var categoryViewModel =
                    mapper.Map<Category, CategoryViewModel>(categoryModel);

            return View(categoryViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CategoryViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            var Model = mapper.Map<CategoryViewModel, Category>(viewModel);

            if (Model is null)
                return NotFound();

            try
            {
                unitOfWork.CategoryRepository.Delete(Model);

                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(viewModel);
            }

        }

        #endregion


        public async Task<IActionResult> SendEmailForUsers(int? categoryId, Email email)
        {
            if (categoryId is null)
                return BadRequest();

            var users = GetUsersByCategoryId(categoryId.Value);

            if (email.Body is not null && email.Subject is not null)
            {
                foreach (var user in users)
                {
                    email.To = user.Email;
                    _emailSettings.SendEmail(email);
                }
                ViewBag.resultOfSendEmail = "Email Has Been Send For All users ";

            }

            ViewBag.resultOfSendEmail = "There Are Problem in Sending Email .";

            return RedirectToAction(nameof(Index));

        }




        private List<ApplicationUser> GetUsersByCategoryId(int categoryId)
        {
            var joinResult = unitOfWork.UserCategoryRepository.GetAllAsync().Result.Where(userCateg => userCateg.CategoryId == categoryId)
                .Join(_userManager.Users.ToList(),
                userCategory => userCategory.ApplicationUserId,
                appUser => appUser.Id,
                (userCategories, users) => new ApplicationUser()
                {
                    Id = users.Id,
                    Email = users.Email,
                    FirstName = users.FirstName,
                    LastName = users.LastName
                }).ToList();

            return joinResult;
        }

    }
}
