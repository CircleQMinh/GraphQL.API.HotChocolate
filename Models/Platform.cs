using System.ComponentModel.DataAnnotations;

namespace GraphQL.API.HotChocolate.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; } = 0;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string LicenseKey { get; set; } = string.Empty;
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}
