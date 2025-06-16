namespace FinanceApp.Dtos
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}