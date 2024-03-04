using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TwitterCloneASP.App.Posts
{
    public class PostRepository
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\itswi\Documents\Computer Science (2nd Year)\2ND TERM\IT114L\TwitterCloneASP\App_Data\twitterclone.mdf"";Integrated Security=True";

        public IEnumerable<Post> GetAllPosts()
        {


            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Posts";
                return command
                    .ExecuteReader()
                    .Cast<IDataRecord>()
                    .Select(row => new Post()
                    {
                        Content = (string)row["content"],
                        PostedBy = (string)row["postedBy"],
                        PostedOn = (DateTime)row["postedOn"]
                    })
                    .ToList();
            }
        }


        public IEnumerable<Post> GetPostOfUser(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = $"SELECT * FROM Posts WHERE postedBy = @username";

                command.Parameters.AddWithValue("username", username);

                return command
                    .ExecuteReader()
                    .Cast<IDataRecord>()
                    .Select(row => new Post()
                    {
                        Content = (string)row["content"],
                        PostedBy = (string)row["postedBy"],
                        PostedOn = (DateTime)row["postedOn"]
                    })
                    .ToList();
            }
        }

        //public IEnumerable<Post> GetAllPosts()
        //{
        //    return new List<Post> {
        //        new Post()
        //        {
        //            Content = "hello world",
        //            PostedBy = "joblipat",
        //            PostedOn = DateTime.Now,


        //        },
        //        new Post()
        //        {
        //            Content = "hello world",
        //            PostedBy = "ecniw",
        //            PostedOn = DateTime.Now,


        //        },

        //        new Post()
        //        {
        //            Content = "hello world",
        //            PostedBy = "wincee",
        //            PostedOn = DateTime.Now,


        //        }
        //    };
        // }



        public void CreatePost(Post post)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = $"INSERT INTO Posts (content, postedOn, postedBy)" +
                    $"VALUES (@content, @postedOn, @postedBy)";


                command.Parameters.AddWithValue("content", post.Content);
                command.Parameters.AddWithValue("postedOn", post.PostedOn);
                command.Parameters.AddWithValue("postedBy", post.PostedBy);

                int rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
}