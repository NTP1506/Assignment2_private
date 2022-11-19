using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Interface
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAllAsync();
        public Task<byte> PostAsync(UserDTO model, User user);

    }
}
