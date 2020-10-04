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
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _context.Movies.Include(p => p.con);
            
            ViewData["CurrentFilter"] = searchString;
            var mov = from s in _context.Movies
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                mov = mov.Where(s => s.MovieName.Contains(searchString)
                                || s.con.GenreName.Contains(searchString));
            }
            return View(await mov.Include(p => p.con).AsNoTracking().ToListAsync());
        }

        // GET: Movies-Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movv = await _context.Movies
                .Include(p => p.con)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movv == null)
            {
                return NotFound();
            }

            return View(movv);
        }

        // GET: Movies-Create
        public IActionResult Create()
        {
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName");
            return View();
        }

        // POST: Movies-Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,MovieName,Description,GenreID")] Movie moviee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moviee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName", moviee.GenreID);
            return View(moviee);
        }

        // GET: Movies-Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mo = await _context.Movies.FindAsync(id);
            if (mo == null)
            {
                return NotFound();
            }
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName", mo.GenreID);
            return View(mo);
        }

        // POST: Movies-Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,MovieName,Description,GenreID")] Movie moviezz)
        {
            if (id != moviezz.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moviezz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(moviezz.MovieID))
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
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName", moviezz.GenreID);
            return View(moviezz);
        }

        // GET: Movies-Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gmov = await _context.Movies
                .Include(p => p.con)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (gmov == null)
            {
                return NotFound();
            }

            return View(gmov);
        }

        // POST: Movies-Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savMov = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(savMov);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieID == id);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ViewMovies()
        {
            var applicationDbContext = _context.Movies.Include(p => p.con)
                .OrderBy(a => Guid.NewGuid());            
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> MovieDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mz = await _context.Movies
                .Include(p => p.con)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (mz == null)
            {
                return NotFound();
            }
            else
            {

                mz.Photos = new List<Photo>();
                var photos = _context.Photos.Where(x => x.MovieID == mz.MovieID);
                foreach( var photo in photos)
                {
                    mz.Photos.Add(photo);
                }
            }

            return View(mz);
        }
    }

    
}
