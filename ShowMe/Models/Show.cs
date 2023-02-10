namespace ShowMe.Models;
public class Show {
	public Guid Id { get; set; }
	public Guid MovieId { get; set; }
	public Guid Screen { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
