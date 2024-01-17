using TracksifyAPI.Models;

namespace TracksifyAPI.Interfaces
{
    //creating an interface for the tokenrepository
    public interface ITokenRepository
    {
        string CreateJWTToken(User user);
    }
}
