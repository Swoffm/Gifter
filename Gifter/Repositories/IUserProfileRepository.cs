using Gifter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        public UserProfile GetUserProfileById(int id);

        public List<UserProfile> GetAllUserProfile();

        public void DeleteUserProfile();
        public void UpdateUserProfile();

        public void AddUserProfile();


    }
}
