using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Helpers;
using TechTree.PersantionLayer.ViewModels;


namespace TechTree.PersantionLayer.Controllers
{
    public class ContentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ContentController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #region Details
        public async Task<IActionResult> Details( int? CategoryItemId)
        {
            if (CategoryItemId == null )
                return View(new ContentViewModel());

            var Model = unitOfWork.ContentRepository.GetAllAsync()
                       .Result.FirstOrDefault(content => content.CategoryItemId == CategoryItemId.Value);

            if (Model == null)
                return BadRequest();

            var ViewModel =
                    mapper.Map<Content, ContentViewModel>(Model);

            return View(ViewModel);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create(int CategoryId, int CategoryItemId)
        {
            var ViewModel = new ContentViewModel()
            {
                CategoryItemId = CategoryItemId,
                CategoryId = CategoryId
            };

            return View(ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ContentViewModel viewModel)
        {


            if (ModelState.IsValid)
            {
                var Model = mapper.Map<ContentViewModel, Content>(viewModel);

                unitOfWork.ContentRepository.Add(Model);

                await unitOfWork.Complete();

                return RedirectToAction(nameof(Details),
                    new { CategoryId = viewModel.CategoryId, CategoryItemId = viewModel.CategoryItemId });

            }
            return View(viewModel);

        }

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int? CategoryId, int? CategoryItemId)
        {
            if (CategoryItemId == null || CategoryId == null)
                return NotFound();

            var Model = unitOfWork.ContentRepository.GetAllAsync()
                       .Result.FirstOrDefault(content =>
                       content.CategoryItemId == CategoryItemId.Value);

            if (Model == null)
                return BadRequest();

            var ViewModel =
                    mapper.Map<Content, ContentViewModel>(Model);

            ViewModel.CategoryId = CategoryId.Value;

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, ContentViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                
                try
                {
                    var Model = mapper.Map<ContentViewModel, Content>(viewModel);

                    Model.VideoLink = YoutubeHelper.GetYoutubeEmbedSrc(viewModel.VideoLink);

                    unitOfWork.ContentRepository.Update(Model);

                    await unitOfWork.Complete();

                    return RedirectToAction(nameof(Details),
                        new { CategoryId = viewModel.CategoryId, CategoryItemId = viewModel.CategoryItemId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
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

                return RedirectToAction(nameof(Index));
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
