using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.IRepository;

namespace WebApplication2.Controllers
{
    public class DepartmentMasterIQController : Controller
    {
        private readonly IDepartmentMasterRpository_IQ _repository;

        public DepartmentMasterIQController(IDepartmentMasterRpository_IQ repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            var departments = _repository.GetAllDepartments();
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = _repository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(DepartmentMaster department)
        {
            //here i remove isActive from modelstate so it is only check DepartmentName is required
            ModelState.Remove(nameof(department.IsActive));

            if (ModelState.IsValid)
            {
                _repository.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = _repository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, DepartmentMaster department)
        {
            if (id != department.DepartmentId)
                return NotFound();

            ModelState.Remove(nameof(department.IsActive));

            if (ModelState.IsValid)
            {
                _repository.UpdateDepartments(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);

        }

        public IActionResult Delete(int id)
        {
            var department = _repository.GetDepartmentById(id);
            if ((department == null))
                return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
