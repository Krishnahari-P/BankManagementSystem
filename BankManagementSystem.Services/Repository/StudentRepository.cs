//using BankManagementSystem.Entity.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BankManagementSystem.Services.Repository
//{
//    public class StudentRepository : IStudentRespository
//    {
//        private readonly AppDbContext _context;

//        public StudentRepository(AppDbContext context)
//        {
//            _context = context;
//        }
//        public async Task AddStudentAsync(Student student)
//        {
//            _context.students.Add(student);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteStudentAsync(int id)
//        {
//            var student = await _context.students.FindAsync(id);
//            _context.students.Remove(student);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<List<Student>> GetAllStudentsAsync()
//        {
//            var studentList = await _context.students.ToListAsync();
//            return studentList;

//        }

//        public async Task<Student> GetStudentByIdAsync(int id)
//        {
//            var student = await _context.students.FindAsync(id);
//            return student ?? throw new NotImplementedException();
//        }

//        public async Task UpdateStudentAsync(Student student)
//        {
//            _context.students.Update(student);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
