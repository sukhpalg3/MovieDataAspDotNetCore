using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieGuide.Data;
using MovieGuide.Models;

namespace MovieGuide.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }

        // GET: Genres-Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gen = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreID == id);
            if (gen == null)
            {
                return NotFound();
            }

            return View(gen);
        }

        // GET: Genres-Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres-Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreID,GenreName")] Genre genr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genr);
        }

        // GET: Genres-Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gen = await _context.Genres.FindAsync(id);
            if (gen == null)
            {
                return NotFound();
            }
            return View(gen);
        }

        // POST: Genres-Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreID,GenreName")] Genre gg)
        {
            if (id != gg.GenreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(gg.GenreID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gg);
        }

        // GET: Genres-Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ge = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreID == id);
            if (ge == null)
            {
                return NotFound();
            }

            return View(ge);
        }

        // POST: Genres-Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gr = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(gr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.GenreID == id);
        }
    }
}
