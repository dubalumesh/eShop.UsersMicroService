

namespace UsersMicroService.Core.DTOs
{
    public record RegisterUserRequest(string Email, string? FirstName, string? LastName, string Gender, string Password, string ConfirmPassword)
    {
        public RegisterUserRequest() : this(String.Empty, default, default, String.Empty, String.Empty, String.Empty)
        {

        }
    }
}
