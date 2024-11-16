using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManager.Data;
using StudentManager.Models;
using StudentManager.Models.Entities;

namespace StudentManager.Controllers;

public class StudentsController : Controller
{
    private readonly AppDbContext _dbContext;
    
    public StudentsController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddStudentVm vm)
    {
        var newStudent = new Student
        {
            Name = vm.Name,
            Email = vm.Email,
            Phone = vm.Phone,
            IsSubscribed = vm.IsSubscribed
        };

        await _dbContext.Students.AddAsync(newStudent);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("List", "Students");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var students = await _dbContext.Students.ToListAsync();

        return View(students);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var student = await _dbContext.Students.FindAsync(id);
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Student vm)
    {
        var student = await _dbContext.Students.FindAsync(vm.Id);

        if (student is null) return RedirectToAction("List", "Students");
        
        student.Name = vm.Name;
        student.Email = vm.Email;
        student.Phone = vm.Phone;
        student.IsSubscribed = vm.IsSubscribed;

        await _dbContext.SaveChangesAsync();

        return RedirectToAction("List", "Students");
    }

    [HttpPost] 
    [Route("Students/Delete")]
    public async Task<IActionResult> Delete(Student vm)
    {
        var student = await _dbContext.Students.FindAsync(vm.Id);

        if (student is null) return RedirectToAction("List", "Students");
        
        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();
        
        return RedirectToAction("List", "Students");
    }
}