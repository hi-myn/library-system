using library_system.Interfaces;
using library_system.Models;

namespace library_system.Services;

public class LoanService
{
    private readonly IRepository<Loan> _loanRepo;
    private readonly IRepository<User> _userRepo;
    private readonly BookService _bookService;

    public LoanService(IRepository<Loan> loanRepo, IRepository<User> userRepo, BookService bookService)
    {
        _loanRepo = loanRepo;
        _userRepo = userRepo;
        _bookService = bookService;
    }

    public bool CreateLoan(string userId, string bookId)
    {
        var user = _userRepo.GetById(userId);
        var book = _bookService.FindById(bookId);

        if (user is null || book is null)
        {
            Console.WriteLine("Error ! User or book not found");
            return false;
        }

        if (book.IsAvailable == false)
        {
            Console.WriteLine("Book is not available");
            return false;
        }

        if (user.CanBorrow == false)
        {
            Console.WriteLine("The MaxLoans has been exceeded");
            return false;
        }

        var loan = new Loan { UserId = userId, ItemId = bookId };
        _loanRepo.Add(loan);
        user.ActiveLoanIds.Add(loan.Id);
        _userRepo.Update(user);
        _bookService.MarkAsUnavailable(bookId);
        return true;
    }

    public bool ReturnLoan(string loanId)
    {
        var loan = _loanRepo.GetById(loanId);
        if (loan is null || loan.IsReturned)
        {
            Console.WriteLine("Oops, the book has already been returned or doesn't exist");
            return false;
        }

        loan.ReturnDate = DateTime.Now;
        _loanRepo.Update(loan);

        var user = _userRepo.GetById(loan.UserId);
        if (user is not null)
        {
            user.ActiveLoanIds.Remove(loan.Id);
            _userRepo.Update(user);
        }
        _bookService.MarkAsAvailable(loan.ItemId);
        Console.WriteLine("Book returned sucessfully");
        return true;
    }

    public IEnumerable<Loan> GetActiveLoans() =>
    _loanRepo.GetAll().Where(l => !l.IsReturned);

    public IEnumerable<Loan> GetOverdueLoans() =>
        _loanRepo.GetAll().Where(l => l.IsOverdue);

    public IEnumerable<Loan> GetLoansByUser(string userId) =>
        _loanRepo.GetAll().Where(l => l.UserId == userId);
}
