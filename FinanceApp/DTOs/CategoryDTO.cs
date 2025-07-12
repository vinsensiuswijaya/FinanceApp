namespace FinanceApp.Dtos
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? UserId { get; set; }
    }
}