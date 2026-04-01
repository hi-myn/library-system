namespace library_system.Models;
public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> ActiveLoanIds { get; set; } = new();
    public const int MaxLoans = 3;
    public bool CanBorrow => ActiveLoanIds.Count < MaxLoans;

}
