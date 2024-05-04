using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.ViewModels;

namespace TechTree.PersantionLayer.Controllers
{
    public class CategoryItemController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryItemController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index(int? Id) // id =>[category Id .]
        {
            IEnumerable<CategoryItem> Models;

            if (Id is null)
            {
                Models = await unitOfWork.CategoryItemRepository.GetAllAsync();
            }
            else
            {
                Models = unitOfWork.CategoryItemRepository.GetAllAsync()
                    .Result.Where(categoryItem => categoryItem.CategoryId == Id);

                ViewBag.CategoryId = Id.Value;

                TempData["CategoryId"] = Id.Value;

                

            }

            var ViewModels =
                    mapper.Map<IEnumerable<CategoryItem>, IEnumerable<CategoryItemViewModel>>(Models);

          

            return View(ViewModels);
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
        public async Task<IActionResult> Create(int CategoryId)
        {
            var ViewModel = new CategoryItemViewModel { CategoryId = CategoryId };

            return View(ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryItemViewModel viewModel)
        {


            if (ModelState.IsValid)
            {
                var Model = mapper.Map<CategoryItemViewModel, CategoryItem>(viewModel);

                unitOfWork.CategoryItemRepository.Add(Model);

                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index), new { Id = viewModel.CategoryId });

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
        public async Task<IActionResult> Edit([FromRoute] int id, CategoryItemViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var Model = mapper.Map<CategoryItemViewModel, CategoryItem>(viewModel);

                    unitOfWork.CategoryItemRepository.Update(Model);

                    await unitOfWork.Complete();

                    return RedirectToAction(nameof(Index), new { Id = Model.CategoryId });
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

            var Model = await unitOfWork.CategoryItemRepository.GetByIdAsync(id.Value);

            if (Model == null)
                return NotFound();

            var ViewModel =
                    mapper.Map<CategoryItem, CategoryItemViewModel>(Model);

            return View(ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CategoryItemViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            var Model = mapper.Map<CategoryItemViewModel, CategoryItem>(viewModel);

            if (Model is null)
                return NotFound();

            try
            {
                unitOfWork.CategoryItemRepository.Delete(Model);

                await unitOfWork.Complete();

                return RedirectToAction(nameof(Index), new { Id = viewModel.CategoryId });
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(viewModel);
            }

        }

        #endregion

    }
}
