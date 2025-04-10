namespace FluxStore.Application.DTOs
{
	public class CategoryDto
	{
        public int Id { get; set; }
        public required string Name { get; set; }
        public string IconName { get; set; } = string.Empty;
    }
}