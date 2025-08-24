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
            return RedirectToAction(nameof(Details), new { id = boards.Id});
        }
        return View(boardVM);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var boardVM = await _context.Boards
            .Where(b => b.Id == id)
            .Select(b => new BoardViewModel
            {
                Id = b.Id,
                CreatedName = b.CreatedName,
                Title = b.Title,
                Body = b.Body,
                CreatedAt = b.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (boardVM == null)
        {
            return NotFound();
        }

        var repliesVM = await _context.Replies
            .Where(r => r.BoardId == id)
            .Select(r => new ReplyViewModel
            {
                RepliedName = r.RepliedName,
                Body = r.Body,
                RepliedAt = r.RepliedAt
            })
            .ToListAsync();

        var detailsVM = new BoardDetailsViewModel
        {
            Boards = boardVM,
            Replies = repliesVM
        };

        return View(detailsVM);
    }

    [HttpPost, ActionName("Details")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reply(int id, [Bind(Prefix = "Reply")] ReplyViewModel ReplyVM)
    {
        // 親スレッド存在確認
        var board = await _context.Boards
            .Where( b => b.Id == id)
            .Select( b => new BoardViewModel
            {
                Id = b.Id,
                CreatedName = b.CreatedName,
                Title = b.Title,
                Body = b.Body,
                CreatedAt = b.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (board == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var reply = new Reply
            {
                Id = ReplyVM.Id,
                BoardId = id,
                RepliedName = ReplyVM.RepliedName ?? "名無し",
                Body = ReplyVM.Body
            };
            _context.Add(reply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new {id});
        }

        var repliesVM = await _context.Replies
            .Where(r => r.BoardId == id)
            .Select(r => new ReplyViewModel
            {
                RepliedName = r.RepliedName,
                Body = r.Body,
                RepliedAt = r.RepliedAt
            })
            .ToListAsync();

        var detailsVM = new BoardDetailsViewModel
        {
            Boards = board,
            Replies = repliesVM,
            Reply = ReplyVM
        };
        return View(detailsVM);
    }
}