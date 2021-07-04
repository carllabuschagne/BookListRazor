using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
	public class EditModel : PageModel
	{

		private readonly ApplicationDbContext _db;

		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}

		[BindProperty]
		public Book Book { get; set; }

		//async Task is required for await for the OnGet Handler.
		public async Task OnGet(int id)
		{
			//Load Book object with books from DB
			Book = await _db.Book.FindAsync(id);

		}


		public async Task<IActionResult> OnPost()
		{

			if (ModelState.IsValid)
			{

				var BookFromDb = await _db.Book.FindAsync(Book.Id);

				BookFromDb.Name = Book.Name;
				BookFromDb.Author = Book.Author;
				BookFromDb.ISBN = Book.ISBN;

				await _db.SaveChangesAsync();

				return RedirectToPage("Index");
			}
			else
			{
				return Page();
			}


		}
	}
}
