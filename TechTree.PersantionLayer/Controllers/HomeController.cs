using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Helpers.interfaces;
using TechTree.PersantionLayer.ViewModels;


namespace TechTree.PersantionLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSettings _emailSettings;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper, IEmailSettings emailSettings)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _emailSettings = emailSettings;
        }

        public async Task<IActionResult> Index()
        {
            //IEnumerable<CategoryItem> categoryItemsModel = await _unitOfWork.CategoryItemRepository.GetAllAsync();

            IEnumerable<CategoryItemDetailsViewModel> categoryItemDetailsViewModel;

            IEnumerable<GroupedCategoryItemByCategoryViewModel> groupedCategoryItemByCategoryViewModel;

            CategoryDetailsViewModel categoryDetailsViewModel = new CategoryDetailsViewModel();

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user is not null)
                {
                    categoryItemDetailsViewModel = GetCategoryItemDetails(user.Id);

                    groupedCategoryItemByCategoryViewModel = GetGroupedCategoryItemByCategory(categoryItemDetailsViewModel);

                    categoryDetailsViewModel.GroupedCategoryItemByCategoryViewModels = groupedCategoryItemByCategoryViewModel;
                }
            }

            else
            {
                var categoriesModel = await GetCategoriesThatHaveContent();

                if (categoriesModel.Count() > 0)
                {
                    var categoriesViewModel = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categoriesModel);

                    categoryDetailsViewModel.Categories = categoriesViewModel;


                }

            }

            return View(categoryDetailsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string To , string Subject, string Body)
        {
            if (ModelState.IsValid == true)
            {
                var email = new Email()
                {
                    To = To,
                    Body = Body,
                    Subject = Subject
                };

                _emailSettings.SendEmail(email);


            }
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<CategoryItemDetailsViewModel> GetCategoryItemDetails(string userId)
        {
            return (from catItem in _unitOfWork.CategoryItemRepository.GetAllAsync().Result

                    join category in _unitOfWork.CategoryRepository.GetAllAsync().Result
                    on catItem.CategoryId equals category.Id

                    join content in _unitOfWork.ContentRepository.GetAllAsync().Result
                    on catItem.Id equals content.CategoryItem.Id

                    join userCategory in _unitOfWork.UserCategoryRepository.GetAllAsync().Result
                    on category.Id equals userCategory.CategoryId

                    join mediaType in _unitOfWork.MediaTypeRepository.GetAllAsync().Result
                    on catItem.MediaTypeId equals mediaType.Id

                    where userCategory.ApplicationUserId == userId

                    select new CategoryItemDetailsViewModel
                    {
                        CategoryId = category.Id,

                        CategoryTitle = category.Title,

                        CategoryItemId = catItem.Id,

                        CategoryItemTitle = catItem.Title,

                        CategoryItemDescription = catItem.Description,

                        MediaImagePath = mediaType.thumbnailImagePath

                    }
                          ).ToList();
        }

        private IEnumerable<GroupedCategoryItemByCategoryViewModel> GetGroupedCategoryItemByCategory(IEnumerable<CategoryItemDetailsViewModel> categoryItemDetailsViewModels)
        {
            return (from item in categoryItemDetailsViewModels
                    group item by item.CategoryId into catItemDet

                    select new GroupedCategoryItemByCategoryViewModel
                    {
                        Id = catItemDet.Key,
                        Title = catItemDet.Select(cat => cat.CategoryTitle).FirstOrDefault(),
                        CategoryItems = catItemDet
                    }
                    );
        }

        private async Task<List<Category>> GetCategoriesThatHaveContent()
        {
            var getCategoriesWithContent = (from category in _unitOfWork.CategoryRepository.GetAllAsync().Result

                                            join categoryItem in _unitOfWork.CategoryItemRepository.GetAllAsync().Result
                                            on category.Id equals categoryItem.CategoryId

                                            join content in _unitOfWork.ContentRepository.GetAllAsync().Result
                                            on categoryItem.Id equals content.CategoryItem.Id

                                            select new Category
                                            {
                                                Id = category.Id,
                                                Title = category.Title,
                                                Description = category.Description,
                                                thumbnailImagePath = category.thumbnailImagePath

                                            }).DistinctBy(item => item.Title).ToList();

            return getCategoriesWithContent;
        }


    }
}