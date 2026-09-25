using company.Models;
using company.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace company.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repo;

        public EmployeeController(EmployeeRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var employee = _repo.GetAll();
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employee) 
        {
            _repo.Create(employee);
            return RedirectToAction("Index");
            //return _repo.Create(employee);
        }

        public IActionResult Edit(long id) { 
            EmployeeModel employee = _repo.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel emp)
        {
            _repo.Update(emp);
            return RedirectToAction("Index");
        }
        //[HttpPost]
        public IActionResult Delete(long id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
