# 📚 Library Management System

A console-based library management system built in C# as a portfolio project for the **Microsoft C# Certification (freeCodeCamp)**.

## 🎯 Concepts Demonstrated

| Concept | Where |
|---|---|
| Abstract classes | `Models/Item.cs` |
| Inheritance | `Book` and `Magazine` inherit from `Item` |
| Interfaces | `ILoanable`, `IRepository<T>` |
| Generics | `InMemoryRepository<T>` |
| LINQ | Filters in `BookService`, `LoanService` |
| Nullable types | `DateTime?`, `T?` return types |
| Calculated properties | `IsReturned`, `IsOverdue`, `CanBorrow` |
| Dependency injection (manual) | Services receive repositories via constructor |

## 🚀 How to Run

```bash
# Prerequisite: .NET 8 SDK installed
git clone https://github.com/your-username/library-system
cd library-system
dotnet run
```

## 🗂 Project Structure

```
library-system/
├── Models/
│   ├── Item.cs           # Abstract base class
│   ├── Book.cs           # Inherits from Item
│   ├── Magazine.cs       # Inherits from Item
│   ├── User.cs           # Library user
│   └── Loan.cs           # Loan record with due date and fine logic
├── Interfaces/
│   ├── IRepository.cs    # Generic repository contract
│   └── ILoanable.cs      # Contract for loanable items
├── Repositories/
│   └── InMemoryRepository.cs  # Generic in-memory implementation
├── Services/
│   ├── BookService.cs    # Book business logic
│   ├── UserService.cs    # User business logic
│   └── LoanService.cs    # Loan and return logic
└── Program.cs            # Console menu + dependency wiring
```

## ✨ Features

- Register books with title, author, ISBN, publisher, pages and year
- Register users with name and email
- Create loans with automatic due date (14 days)
- Return books with overdue detection
- View all active loans
- Search books by title, author or ISBN
- Loan limit per user (max 3 active loans)
- Short 4-character IDs for easy reference in the console

## 💡 Possible Next Steps

- [ ] Persistence with **Entity Framework Core** + SQLite
- [ ] Unit tests with **xUnit**
- [ ] Web interface with **Blazor**
- [ ] Export reports as CSV
- [ ] OpenLibrary API integration for automatic book data by ISBN

## 🛠 Tech Stack

- **Language:** C# 12
- **Framework:** .NET 8
- **Storage:** In-memory (Dictionary)
- **Architecture:** Service + Repository pattern