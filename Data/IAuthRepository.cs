using System.Threading.Tasks;
using dotnet_rzp.Models;

namespace dotnet_rzp.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> RegisterUser(User user, string password);
         Task<ServiceResponse<string>> Login(string username, string password);
         Task<bool> isRegisted(string username);
    }
}