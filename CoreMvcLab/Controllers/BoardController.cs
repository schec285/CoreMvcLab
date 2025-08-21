using CoreMvcLab.Data;
using CoreMvcLab.Entities;
using CoreMvcLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Threading.Tasks;

namespace CoreMvcLab.Controllers;

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
    public async Task<IActionResult> Create([Bind("Id, CreatedName, Title, Body")] BoardViewModel boardVM)
    {
        if(ModelState.IsValid)
        {
            var boards = new Board
            {
                CreatedName = boardVM.CreatedName ?? "名無し",
                Title = boardVM.Title,
                Body = boardVM.Body
            };
            _context.Add(boards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        return View(boardVM);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var board = await _context.Boards
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (board == null)
        {
            return NotFound();
        }

        var boardVM = new BoardViewModel
        {
            Id = board.Id,
            CreatedName = board.CreatedName,
            Title = board.Title,
            Body = board.Body,
            CreatedAt = board.CreatedAt
        };

        var detailsVM = new BoardDetailsViewModel
        {
            Boards = boardVM
        };

        return View(detailsVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reply(int boardId, [Bind("Id, RepliedName, Body")] ReplyViewModel ReplyVM)
    {

        if(boardId != ReplyVM.BoardId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var replies = new Reply
            {
                Id = ReplyVM.Id,
                BoardId = boardId,
                RepliedName = ReplyVM.RepliedName ?? "名無し",
                Body = ReplyVM.Body
            };
            _context.Add(replies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }
        return View(ReplyVM);
    }
}