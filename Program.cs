using library_system.Models;
using library_system.Repositories;
using library_system.Services;

var bookRepo = new InMemoryRepository<Book>(b => b.Id);
var userRepo = new InMemoryRepository<User>(u => u.Id);
var loanRepo = new InMemoryRepository<Loan>(l => l.Id);

var bookService = new BookService(bookRepo);
var userService = new UserService(userRepo);
var loanService = new LoanService(loanRepo, userRepo, bookService);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== LIBRARY SYSTEM ===");
    Console.WriteLine("1 - Add book");
    Console.WriteLine("2 - List books");
    Console.WriteLine("3 - Search Book");
    Console.WriteLine("4 - Add User");
    Console.WriteLine("5 - Create Loan");
    Console.WriteLine("6 - List Users");
    Console.WriteLine("7 - Return Loan");
    Console.WriteLine("8 - Active Loan");
    Console.WriteLine("0 - Exit");
    Console.Write("Choose: ");

    var choice = Console.ReadLine();

    switch (choice)
    {

        case "1": AddBook(); break;
        case "2": ListBook(); break;
        case "3": SearchBook(); break;
        case "4": AddUser(); break;
        case "5": CreateLoan(); break;
        case "6": ListUsers(); break;
        case "7": ReturnLoans(); break;
        case "8": ActiveLoan(); break;
        case "0": return;
    }
}

void AddBook()
{
    var book = new Book();
    Console.Write("Title: "); book.Title = Console.ReadLine() ?? "";
    Console.Write("Author: "); book.Author = Console.ReadLine() ?? "";
    Console.Write("ISBN: "); book.Isbn = Console.ReadLine() ?? "";
    Console.Write("Publisher: "); book.Publisher = Console.ReadLine() ?? "";
    Console.Write("Pages: "); book.Pages = int.TryParse(Console.ReadLine(), out int p) ? p : 0;
    Console.Write("Year: "); book.Year = int.TryParse(Console.ReadLine(), out int y) ? y : 0;
    bookService.AddBook(book);
    Console.ReadLine();
}

void ListBook()
{
    var books = bookService.GetAllBooks().ToList();
    if (!books.Any()) { Console.WriteLine("No books registered."); Console.ReadLine(); return; }
    foreach (var book in books)
    {
        Console.WriteLine(book.GetDescription());
        Console.WriteLine($"  ID: {book.Id[..8]}");  
        Console.WriteLine("---");
    }
    Console.ReadLine();
}

void SearchBook()
{
    Console.WriteLine("Search term: ");
    var term = Console.ReadLine() ?? "";
    var results = bookService.Search(term).ToList();

    if (!results.Any())
    {
        Console.WriteLine("No books found");
    }
    else
    {
        foreach (var book in results)
        {
            Console.WriteLine(book.GetDescription());
        }
    }
    Console.ReadLine();
}

void AddUser()
{
    var user = new User();
    Console.WriteLine("Name: "); user.Name = Console.ReadLine() ?? "";
    Console.WriteLine("Email: "); user.Email = Console.ReadLine() ?? "";
    userService.AddUser(user);
    Console.ReadLine();
}

void CreateLoan()
{
    Console.WriteLine("\nUSERS");
    Console.WriteLine($"{"ID",-6} {"Name",-20}");
    Console.WriteLine(new string('-', 26));
    foreach (var u in userService.GetAllUsers())
        Console.WriteLine($"{u.Id[..4],-6} {u.Name,-20}");

    Console.WriteLine("\nBOOKS");
    Console.WriteLine($"{"ID",-6} {"Title",-30}");
    Console.WriteLine(new string('-', 36));
    foreach (var b in bookService.GetAllBooks())
        Console.WriteLine($"{b.Id[..4],-6} {b.Title,-30}");

    Console.WriteLine();
    Console.Write("User ID: "); var userId = Console.ReadLine() ?? "";
    Console.Write("Book ID: "); var bookId = Console.ReadLine() ?? "";

    var user = userService.GetAllUsers().FirstOrDefault(u => u.Id.StartsWith(userId));
    var book = bookService.GetAllBooks().FirstOrDefault(b => b.Id.StartsWith(bookId));

    if (user is null || book is null)
    {
        Console.WriteLine("User or book not found.");
        Console.ReadLine();
        return;
    }

    loanService.CreateLoan(user.Id, book.Id);
    Console.WriteLine("Loan created successfully");
    Console.ReadLine();
}

void ListUsers()
{
    var users = userService.GetAllUsers().ToList();
    if (!users.Any()) { Console.WriteLine("No users registered."); Console.ReadLine(); return; }
    foreach (var user in users)
        Console.WriteLine(user); 
    Console.ReadLine();

}

void ReturnLoans()
{
    var active = loanService.GetActiveLoans().ToList();
    if (!active.Any())
    {
        Console.WriteLine("No active loans.");
        Console.ReadLine();
        return;
    }

    Console.WriteLine("Active loans:");
    foreach (var loan in active)
        Console.WriteLine(loan); 

    Console.Write("\nLoan ID (first 4 chars): ");
    var loanId = Console.ReadLine() ?? "";

    var found = active.FirstOrDefault(l => l.Id.StartsWith(loanId));
    if (found is null)
    {
        Console.WriteLine("Loan not found.");
        Console.ReadLine();
        return;
    }

    loanService.ReturnLoan(found.Id);
    Console.ReadLine();
}

void ActiveLoan()
{
    var loans = loanService.GetActiveLoans().ToList();
    if (!loans.Any())
    {
        Console.WriteLine("No active loans.");
    }
    else
    {
        foreach (var loan in loans) Console.WriteLine(loan);
    }
    Console.ReadLine();
}



