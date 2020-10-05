using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {

        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }


        public UserProfile GetUserProfileById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Name, email, imageUrl, Bio, DateCreated 
FROM UserProfile WHERE id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    
                   var userProfile = new UserProfile();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "email"),
                            ImageUrl = DbUtils.GetString(reader, "imageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "dateCreated")
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }


         }

        public List<UserProfile> GetAllUserProfile()
        {


            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "";
                }
                var myList = new List<UserProfile>();
                return myList;
            }

        }

        public void DeleteUserProfile()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                }
            }



        }


        public void UpdateUserProfile()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                }
            }


        }

        public void AddUserProfile()
        {


            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                }
            }

        }
    }
}
