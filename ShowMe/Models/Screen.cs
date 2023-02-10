namespace ShowMe.Models;
public class Screen {
	public Guid Id { get; set; }
	public string Name { get; set; }
	public short NumberOfRows { get; set; }
	public short NumberOfColumns { get; set; }
	public Guid TheaterId { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
