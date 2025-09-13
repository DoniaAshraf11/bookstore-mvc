using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreMVC.Data;
using BookStoreMVC.Models;

namespace BookStore.Controllers
{
    public class ReadersController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();


        // GET: Readers
        public IActionResult Index()
        {
            var readers = _context.Readers
                .Include(r => r.Books)
                .ToList();
            return View(readers);
        }


        // GET: Readers/Details/5

        public IActionResult Details(int id)
        {
            var reader = _context.Readers
                .Include(r => r.Books)
                .FirstOrDefault(r => r.Id == id);

            if (reader == null) return NotFound();

            return View(reader);
        }


        // GET: Readers/Create
        public IActionResult Create()
        {
            ViewBag.AvailableBooks = new SelectList(_context.Books.Where(b => b.ReaderId == null).ToList(), "Id", "Title");
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reader reader, List<int> SelectedBookIds)
        {
            if (ModelState.IsValid)
            {
                _context.Readers.Add(reader);
                await  _context.SaveChangesAsync();

                // assign selected books
                var books = _context.Books.Where(b => SelectedBookIds.Contains(b.Id)).ToList();
                foreach (var book in books)
                {
                    book.ReaderId = reader.Id;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AvailableBooks = new SelectList(_context.Books.Where(b => b.ReaderId == null), "Id", "Title");
            return View(reader);
        }
















        //public IActionResult Create(Reader reader, int selectedBookId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Readers.Add(reader);
        //        _context.SaveChanges();

        //        // Assign the selected book (if any)
        //        if (selectedBookId > 0)
        //        {
        //            var book = _context.Books.Find(selectedBookId);
        //            if (book != null)
        //            {
        //                book.ReaderId = reader.Id;
        //                _context.Books.Update(book);
        //                _context.SaveChanges();
        //            }
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AvailableBooks = new SelectList(_context.Books.Where(b => b.ReaderId == null).ToList(), "Id", "Title");
        //    return View(reader);
        //}

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var reader = await _context.Readers.FindAsync(id);
        //    if (reader == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(reader);
        //}

        public IActionResult Edit(int id)
        {
            var reader = _context.Readers
                .Include(r => r.Books)
                .FirstOrDefault(r => r.Id == id);

            if (reader == null) return NotFound();

            return View(reader);
        }












        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber")] Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaderExists(reader.Id))
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
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reader = _context.Readers
                .Include(r => r.Books)
                .FirstOrDefault(r => r.Id == id);

            if (reader == null)
                return NotFound();

            // Unassign all books from this reader
            if (reader.Books != null && reader.Books.Any())
            {
                foreach (var book in reader.Books)
                {
                    book.ReaderId = null;
                    _context.Books.Update(book);
                }
            }

            _context.Readers.Remove(reader);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        private bool ReaderExists(int id)
        {
            return _context.Readers.Any(e => e.Id == id);
        }
    }
}
