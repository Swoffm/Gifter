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
                    cmd.CommandText = "SELECT id, Name, email, imageUrl, Bio, DateCreated FROM UserProfile";

                    var myList = new List<UserProfile>();
                    var reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        myList.Add(new UserProfile
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "email"),
                            ImageUrl = DbUtils.GetString(reader, "imageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "dateCreated")

                        });
                    }

                    reader.Close();
                    return myList;
                }

               
               
            }

        }

        public void DeleteUserProfile(int id)
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Userprofile WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();

                }
            }



        }


        public void UpdateUserProfile(UserProfile profile)
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile SET Name = @Name, email = @email, imageUrl = @ImageUrl, datecreated = @datecreated WHERE id = @id";

                    DbUtils.AddParameter(cmd, profile.Name, "@name");
                    DbUtils.AddParameter(cmd, "@Id", profile.Id);
                    DbUtils.AddParameter(cmd, "@datecreated", profile.DateCreated);
                    DbUtils.AddParameter(cmd, profile.Email, "@Email");
                    DbUtils.AddParameter(cmd, profile.ImageUrl, "@ImageUrl");

                    cmd.ExecuteNonQuery();
                }
            }


        }

        public void AddUserProfile(UserProfile profile)
        {


            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (id, Name, email, imageUrl, DateCreated) OUTPUT INSERTED.ID VALUES (@Name, @email, @imageUrl, @DateCreated)";

                    DbUtils.AddParameter(cmd, profile.Name, "@name");
                    DbUtils.AddParameter(cmd, "@datecreated", profile.DateCreated);
                    DbUtils.AddParameter(cmd, profile.Email, "@Email");
                    DbUtils.AddParameter(cmd, profile.ImageUrl, "@ImageUrl");

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }

        }
    }
}
