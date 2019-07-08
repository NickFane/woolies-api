using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Persistence.Db
{
    public class WooliesApiDbContext : IDbContext
    {
        private readonly List<User> _users;

        public WooliesApiDbContext(List<User> users)
        {
            _users = users;
        }

        public async Task<User> GetUserAsync(string token = null)
        {
            // For challenge purposes if no token is passed in
            // Just default to the first user
            if (string.IsNullOrEmpty(token))
            {
                return _users.First();
            }

            return _users.Single(s => s.Token == token);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return _users.ToList();
        }
    }

    public interface IDbContext
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(string token = null);
    }
}
