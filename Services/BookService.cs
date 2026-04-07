using library_system.Interfaces;
using library_system.Models;
using System.Linq;

namespace library_system.Services;

public class BookService
{
    private readonly IRepository<Book> _repo;
    public BookService(IRepository<Book> repo)
    {
        _repo = repo;
    }

    //The ? indicates that it may return null.
    public Book? FindById(string id)
    {
        var book = _repo.GetById(id);
        return book;
    }

    public void MarkAsUnavailable(string id)
    {
        var book = _repo.GetById(id);
        if (book is null) return;

        if (book.IsAvailable)
        {
            book.IsAvailable = false;
            _repo.Update(book);
        }
    }

    public void AddBook(Book book)
    {
        _repo.Add(book);
        System.Console.WriteLine($"Book '{book.Title}' registered successfully");
    }

    //Book is a book. IEnumerable is a bookshelf.
    public IEnumerable<Book> GetAllBooks()
    {
        return _repo.GetAll();
    }

    public IEnumerable<Book> Search(string term)
    {
        term = term.ToLower();
        return _repo.GetAll().Where(b =>
        b.Title.ToLower().Contains(term) ||
        b.Author.ToLower().Contains(term) ||
        b.Isbn.ToLower().Contains(term));
    }

    public void MarkAsAvailable(string id)
    {
        var book = _repo.GetById(id);
        if (book is null) return;

        if (!book.IsAvailable)
        {
            book.IsAvailable = true;
            _repo.Update(book);
        }
    }

}
