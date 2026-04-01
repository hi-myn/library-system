namespace library_system.Models;
public class Loan
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);           
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned => ReturnDate.HasValue;

}
