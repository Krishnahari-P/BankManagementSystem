using BankManagementSystem.Client.Dto;
using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BankManagementSystem.Client.Controllers
{
    public class StudentController : Controller
    {
        private readonly IGenericHttpClient _client;

        public StudentController(IGenericHttpClient client)
        {
            _client = client;
        }
        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            List<StudentResponse> students = new List<StudentResponse>();
            students = await _client.GetAsync<List<StudentResponse>>(ApiConstant.GetAllStudents);
            return View(students);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            StudentResponse student = new StudentResponse();
            student=await _client.GetAsync<StudentResponse>($"{ApiConstant.GetStudentById}?id={id}");
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> Create(StudentResponse studentResponse)
        {
            try
            {
                await _client.PostAsync<StudentResponse>(ApiConstant.AddStudent, studentResponse);
                return RedirectToAction(nameof(Index));
                //Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            StudentResponse student=new StudentResponse();
            student=await _client.GetAsync<StudentResponse>($"{ApiConstant.GetStudentById}?id={id}");
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<ActionResult> ConfirmEdit(StudentResponse studentResponse)
        {
            try
            {
                await _client.PutAsync<StudentResponse>(ApiConstant.UpdateStudent, studentResponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            StudentResponse student = new StudentResponse();
            student = await _client.GetAsync<StudentResponse>($"{ApiConstant.GetStudentById}?id={id}");
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            try
            {
                StudentResponse student = new StudentResponse();
                student = await _client.DeleteAsync<StudentResponse>($"{ApiConstant.DeleteStudent}?id={id}");
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
