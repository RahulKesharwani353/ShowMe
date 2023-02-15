using System.ComponentModel.DataAnnotations;

namespace ShowMe.Models;

public class Screen {
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; }
	public short NumberOfRows { get; set; }
	public short NumberOfColumns { get; set; }
	public ICollection<Show> Shows { get; set; }
	public Theater Theater { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}
