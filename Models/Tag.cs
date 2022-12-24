namespace Highlights.Models;

public class Tag
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string? Code { get; set; } = null!;
	public int TopicId { get; set; }
	public Topic Topic { get; set; } = null!;
}
