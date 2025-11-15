using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.IRepository;

namespace WebApplication2.Controllers
{
    public class DesignationMasterRepositoryController : Controller
    {
        public readonly IDesignationMasterRepository_IQ _repository;

        public DesignationMasterRepositoryController(IDesignationMasterRepository_IQ repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            var designations = _repository.GetAllDesignation();
            return View(designations);
        }

        public IActionResult Details(int id)
        {
            var designation = _repository.GetDesignationById(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(DesignationMaster designation)
        {

            ModelState.Remove(nameof(designation.IsActive));

            if (ModelState.IsValid)
            {
                _repository.AddDesignation(designation);
                return RedirectToAction(nameof(Index));
            }


            return View(designation);
        }


        public IActionResult Edit(int id)
        {
            var designation = _repository.GetDesignationById(id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, DesignationMaster designation)
        {
            if (id != designation.DesignationId)
                return NotFound();

            ModelState.Remove(nameof(designation.IsActive));

            if (ModelState.IsValid)
            {
                _repository.UpdateDesignation(designation);
                return RedirectToAction(nameof(Index));
            }

            return View(designation);
        }


        public IActionResult Delete(int id)
        {
            var designation = _repository.GetDesignationById(id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
              _repository.DeleteDesignation(id);
              return RedirectToAction(nameof(Index));
        }
    }
}
