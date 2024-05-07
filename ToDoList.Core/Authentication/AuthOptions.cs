using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ToDoList.Core.Authentication
{
    public class AuthOptions
    {
        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public required string Secret { get; set; }

        public int TokenLifeTime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
