namespace ShowMe.Models;

public class MovieDto {
	public string Title { get; set; }
	public string? Description { get; set; }
	public string Director { get; set; }
	public TimeSpan Duration { get; set; }
	public DateOnly ReleaseDate { get; set; }
}
