using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TechTree.BLL.Interfaces;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Helpers;
using TechTree.PersantionLayer.ViewModels;

namespace TechTree.PersantionLayer.Controllers
{
    public class MediaTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MediaTypeController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var Models = await unitOfWork.MediaTypeRepository.GetAllAsync();

            if (Models is null)
                return NotFound();

            var viewModels =
                    mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeViewModel>>(Models);

            return View(viewModels);
        }

        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var Model = await unitOfWork.MediaTypeRepository.GetByIdAsync(id.Value);

            if (Model is null)
                return NotFound();

            var ViewModel =
                    mapper.Map<MediaType, MediaTypeViewModel>(Model);

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
        public async Task<IActionResult> Create(MediaTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.thumbnailImagePath = DocumentSettings.UploadFile(viewModel.UploadImage, "Images");

                var Model = mapper.Map<MediaTypeViewModel, MediaType>(viewModel);

                unitOfWork.MediaTypeRepository.Add(Model);

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

            var Model = await unitOfWork.MediaTypeRepository.GetByIdAsync(id.Value);

            if (Model == null)
                return NotFound();

            var ViewModel =
                    mapper.Map<MediaType, MediaTypeViewModel>(Model);

            ViewBag.ImgPath = ViewModel.thumbnailImagePath;

            
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, MediaTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (viewModel.UploadImage is not null)
                viewModel.thumbnailImagePath = DocumentSettings.UploadFile(viewModel.UploadImage, "Images");

            if (ModelState.IsValid)
            {
                try
                {
                    var Model = mapper.Map<MediaTypeViewModel, MediaType>(viewModel);

                    unitOfWork.MediaTypeRepository.Update(Model);

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

            var Model = await unitOfWork.MediaTypeRepository.GetByIdAsync(id.Value);

            if (Model == null)
                return NotFound();

            var ViewModel =
                    mapper.Map<MediaType, MediaTypeViewModel>(Model);

            return View(ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, MediaTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            var Model = mapper.Map<MediaTypeViewModel, MediaType>(viewModel);

            if (Model is null)
                return NotFound();

            try
            {
                unitOfWork.MediaTypeRepository.Delete(Model);

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
