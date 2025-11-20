using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.IRepository;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeMasterRepositoryController : Controller
    {
        public readonly IEmployeeMasterUSP _repository;
        public readonly IDepartmentMasterRpository_IQ _drepository;
        public readonly IDesignationMasterRepository_IQ _dsrepository;

        public EmployeeMasterRepositoryController(IEmployeeMasterUSP repository, IDepartmentMasterRpository_IQ drepository, IDesignationMasterRepository_IQ dsrepository)
        {
            _repository = repository;
            _drepository = drepository;
            _dsrepository = dsrepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var employees = _repository.GetAllEmployee();
            return View(employees);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [Authorize]
        [HttpGet]
        
        public IActionResult Create()
        {
            ViewBag.Departments = _drepository.GetAllDepartments(); // You need to implement this method
            ViewBag.Designations = _dsrepository.GetAllDesignation();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(EmployeeMaster employee)
        {
            ModelState.Remove(nameof(employee.EmpStatus));
            if (ModelState.IsValid)
            {
                _repository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _drepository.GetAllDepartments();
            ViewBag.Designations = _dsrepository.GetAllDesignation();

            return View(employee);
        }

        [Authorize]
        [HttpGet]
        
        public IActionResult Edit(int id)
        {
            ViewBag.Departments = _drepository.GetAllDepartments();
            ViewBag.Designations = _dsrepository.GetAllDesignation();
            var employee = _repository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }



       
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult Edit(int id, EmployeeMaster employee)
        {
            if (id != employee.EmpId)

                return NotFound();

            if (ModelState.IsValid)
            {
                _repository.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        [Authorize]
        [HttpGet]
        
        public IActionResult Delete(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            if(employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public IActionResult ConfirmedDelete(int id)
        {
                _repository.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
        }

    }
}
