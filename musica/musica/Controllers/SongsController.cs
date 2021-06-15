using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using musica.Data;
using musica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musica.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext db;

        public SongsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Songs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await db.Songs.FirstOrDefaultAsync(s => s.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }
        public IActionResult CreateSong()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Song song)
        {
            if (ModelState.IsValid)
            {
                db.Add(song);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await db.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Song song)
        {
            if (id != song.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(song);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(song);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await db.Songs.FirstOrDefaultAsync(s => s.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await db.Songs.FindAsync(id);
            db.Songs.Remove(song);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
