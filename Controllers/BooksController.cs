using Microsoft.AspNetCore.Mvc;
using BookManagementApi.Models;
using BookManagementApi.Services;
using System.Collections.Generic;

namespace BookManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetBooks() => _bookService.GetBooks();

        [HttpGet("{id:int}", Name = "GetBook")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null) return BadRequest("Book cannot be null");
            _bookService.AddBook(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null || id != updatedBook.Id) return BadRequest("Invalid book data");
            var book = _bookService.GetBook(id);
            if (book == null) return NotFound();
            _bookService.UpdateBook(updatedBook);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null) return NotFound();
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
