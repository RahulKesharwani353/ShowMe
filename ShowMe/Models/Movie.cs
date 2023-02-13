using System.ComponentModel.DataAnnotations;

namespace ShowMe.Models;
public class Movie {
	// defines the primary key for the entity
	[Key]
	// defines and used only if were using auto-incrementing values for the primary key
	// DatabaseGenerated(DatabaseGeneratedOption.Identity)
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }
	public string Director { get; set; }
	public ICollection<Show> Shows { get; set; }
	public TimeSpan Duration { get; set; }	
	public DateOnly ReleaseDate { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
