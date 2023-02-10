namespace ShowMe.Models;
public class Movie {
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }
	public string Director { get; set; }
	public TimeSpan Duration { get; set; }
	public DateOnly ReleaseDate { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
