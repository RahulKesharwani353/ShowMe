using System;
namespace ShowMe.Models {
	public class Theater {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}

