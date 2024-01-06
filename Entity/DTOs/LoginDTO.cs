using Entity.enums;

namespace Entity.DTOs
{
    public class LoginDTO
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public UserType UserType { get; set; }
    }
}
