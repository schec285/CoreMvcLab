using CoreMvcLab.Data;
using CoreMvcLab.Entities;
using CoreMvcLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcLab.Controllers
{
    public class BoardController : Controller
    {
        private readonly CoreMvcLabContext _context;

        public BoardController(CoreMvcLabContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var boardVM = await _context.Boards
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BoardViewModel
                {
                    Id = b.Id,
                    CreatedName = b.CreatedName,
                    Title = b.Title,
                    Body = b.Body,
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();

            var indexVM = new BoardIndexViewModel
            {
                Boards = boardVM
            };

            return View(indexVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, CreatedName, Title, body")] Board board)
        {
            if(ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }
    }
}
