using System.ComponentModel.DataAnnotations;

namespace ShowMe.Models;

public class Theater {
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string City { get; set; }
	public ICollection<Screen> Screens { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}