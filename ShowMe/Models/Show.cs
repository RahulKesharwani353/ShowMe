using System.ComponentModel.DataAnnotations;

namespace ShowMe.Models;
public class Show {
	[Key]
	public Guid Id { get; set; }
	public Movie Movie { get; set; }
	public Screen Screen { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
