namespace Highlights.Models;

public class Topic
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Premise { get; set; }
	public string Goal { get; set; } = null!;
	public DateTime CreationDate { get; set; }
}
