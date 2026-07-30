using Microsoft.AspNetCore.Identity;

namespace JemeHotelsProject.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> Roles);

    }
}
