using System;
using System.Web.UI;

namespace WebFormsClient
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // page intialization
                txtEntry.Text = "Enter the text to be sanitized";
            }
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            // notice the text value is sanitized.
            var content = txtEntry.Text;
        }
    }
}