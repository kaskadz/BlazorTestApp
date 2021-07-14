using System.Threading.Tasks;
using BlazorTestApp.Frontend.Domain;

namespace BlazorTestApp.Frontend.Services
{
    public interface IUserInfoService
    {
        public Task<UserInfoDisplayData> GetUserInfoDisplayData();
    }
}