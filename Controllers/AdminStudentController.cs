using Microsoft.AspNetCore.Mvc;
using StudentInfo.Data;
using StudentInfo.Models.ViewModel;
using StudentInfo.Models.Domain;

namespace StudentInfo.Controllers
{
    public class AdminStudentController : Controller
    {
        private readonly StudentDbContext _studentDbContext;

        public AdminStudentController(StudentDbContext studentDbContext)
        {
            this._studentDbContext = studentDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddStudentRequest addStudentRequest)
        {
            var student = new Student
            {
                Name = addStudentRequest.Name,
                Department=addStudentRequest.Department,
                Session=addStudentRequest.Session

            };
            _studentDbContext.Students.Add(student);
            _studentDbContext.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            var Students = _studentDbContext.Students.ToList();
            return View(Students);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentDbContext.Students.FirstOrDefault(x=>x.Id==id);
            if (student != null)
            {
                var editStudentRequest = new EditStudentRequest()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Department = student.Department,
                    Session = student.Session
                };
                return View(editStudentRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditStudentRequest editStudentRequest)
        {
            var student = new Student
            {
                Id = editStudentRequest.Id,
                Name = editStudentRequest.Name,
                Department = editStudentRequest.Department,
                Session = editStudentRequest.Session
            };
            var existingStudent = _studentDbContext.Students.Find(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Department = student.Department;
                existingStudent.Session = student.Session;
                _studentDbContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delet(EditStudentRequest editStudentRequest)
        {
            var student = _studentDbContext.Students.Find(editStudentRequest.Id);
            if (student != null)
            {
                _studentDbContext.Students.Remove(student);
                _studentDbContext.SaveChanges();
            } 
            return RedirectToAction("List");
            
        }

    }

}

