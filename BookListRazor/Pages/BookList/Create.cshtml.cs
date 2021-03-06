using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
	public class CreateModel : PageModel
	{

		private readonly ApplicationDbContext _db;

		public CreateModel(ApplicationDbContext db)
		{
			_db = db;
		}

		[BindProperty]
		public Book Book { get; set; }


		public void OnGet()
		{
		}

		//Book Object Not Required if BindProperty is set above.
		//public async Task<IActionResult> OnPost(Book bookObj) { }

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				//Add Book object to Queue.
				await _db.Book.AddAsync(Book);

				//Save Data from Queue to Database.
				await _db.SaveChangesAsync();

				//Redirect back to Index page.
				return RedirectToPage("Index");
			}
			else
			{
				return Page();
			}

		}
	}
}
