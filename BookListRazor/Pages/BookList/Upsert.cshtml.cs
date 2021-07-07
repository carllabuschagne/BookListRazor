using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
	public class UpsertModel : PageModel
	{

		private readonly ApplicationDbContext _db;

		public UpsertModel(ApplicationDbContext db)
		{
			_db = db;
		}

		[BindProperty]
		public Book Book { get; set; }

		//async Task is required for await for the OnGet Handler.
		//Id could be NULL when creating a new record. ? nullable type
		public async Task<IActionResult> OnGet(int? id)
		{
			Book = new Book();

			if (id == null)
			{
				return Page();
			}

			//Load Book object with books from DB

			//Book = await _db.Book.FindAsync(id);

			//OR

			Book = _db.Book.FirstOrDefault<Book>(u => u.Id == id);

			if (Book == null)
			{
				return NotFound();
			}

			return Page();

		}


		public async Task<IActionResult> OnPost()
		{

			if (ModelState.IsValid)
			{

				if (Book.Id == 0)
				{
					//Add a new Book in the Database
					_db.Book.Add(Book);
				}
				else
				{
					//Update all Book fields in the Database with all the fields in the Book object.
					_db.Book.Update(Book);
				}


				//Use the below if you want to update single fields.
				
				/*
				var BookFromDb = await _db.Book.FindAsync(Book.Id);

				BookFromDb.Name = Book.Name;
				BookFromDb.Author = Book.Author;
				BookFromDb.ISBN = Book.ISBN;

				*/

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
