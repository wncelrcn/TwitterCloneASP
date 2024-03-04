using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterCloneASP.App.Posts;

namespace TwitterCloneASP
{
    public partial class Default : System.Web.UI.Page
    {
        public IEnumerable<Post> posts = new List<Post>()
        {
            

        };

        protected void Page_Load(object sender, EventArgs e)
        {
            var repository = new PostRepository();
            
            //PostRepeater.DataSource = posts;
            //PostRepeater.DataBind();

            repository.CreatePost(new Post()
            {
                Content = "ang lamig",
                PostedBy = "ecniw",
                PostedOn = DateTime.Now,
            });

            posts = repository.GetPostOfUser("ecniw");
        }
    }
}