namespace FinanceApp.Dtos
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; } = null!;
    }
}