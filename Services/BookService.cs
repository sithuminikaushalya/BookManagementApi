using System.Collections.Generic;
using System.Linq;
using BookManagementApi.Models;

namespace BookManagementApi.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new();

        public List<Book> GetBooks() => _books;

        public Book GetBook(int id) => _books.FirstOrDefault(b => b.Id == id);

        public void AddBook(Book book)
        {
            book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
        }

        public void UpdateBook(Book updatedBook)
        {
            var book = GetBook(updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.ISBN = updatedBook.ISBN;
                book.PublicationDate = updatedBook.PublicationDate;
            }
        }

        public void DeleteBook(int id)
        {
            var book = GetBook(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}
