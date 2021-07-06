using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

		public IndexModel(ApplicationDbContext db)
		{
            _db = db;
		}


        //Object tol hold list of books
        public IEnumerable<Book> Books { get; set; }

        public string mm = "";

        //async Task is required for await for the OnGet Handler.
        public async Task OnGet()
        {
            //Load Book object with books from DB
            Books = await _db.Book.ToListAsync();

        }



        public async Task<IActionResult> OnPostDelete(int Id)
		{
            var book = await _db.Book.FindAsync(Id);

            if(book == null)
			{
                return NotFound();
			}

            _db.Book.Remove(book);

            await _db.SaveChangesAsync();

            return RedirectToPage("Index");

		}

    }
}
