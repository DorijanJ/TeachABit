using Microsoft.AspNetCore.Identity;

namespace TeachABit.Model.Models.Uloge
{
    public class Uloga : IdentityRole
    {
        public int LevelPristupa { get; set; } = 0;
    }
}
