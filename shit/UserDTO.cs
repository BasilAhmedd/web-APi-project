using System.ComponentModel.DataAnnotations;

namespace shit
{
    public class UserDTO
    {
        [Key]
        [Required(ErrorMessage ="Id is Required")]
        public int Id { get; set; }

        [StringLength(100,MinimumLength = 3 ,ErrorMessage ="Name must be between 3 and 100 chars")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$",ErrorMessage ="Name Must Start With a Capital Letter")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Invalid Email address")]
        public string Email { get; set; }

        [RegularExpression(@"^[01]+[0-9]*{,11}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        [CreditCard(ErrorMessage ="Invalid Credit Card")]
        public string CreditCard { get; set; }

        public string? Password { get; set; }
        [Compare("Password",ErrorMessage ="Password Dont Match")]
        public string? PasswordConfirmation { get; set; }
    }
}
 