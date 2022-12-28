namespace Highlights.Models;

public class Highlight
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public string? Comment { get; set; }
    public int TopicId { get; set; }
    public Topic Topic { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}
