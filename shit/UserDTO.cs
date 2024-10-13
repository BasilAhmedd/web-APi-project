using System.ComponentModel.DataAnnotations;

namespace shit
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
 