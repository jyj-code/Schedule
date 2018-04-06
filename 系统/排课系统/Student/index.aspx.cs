using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 排课系统.Student
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["student"] == null)
                {
                    WebMessageBox.Show("请登录","../Default.aspx");
                }
               Label1.Text = string.Format("{0} 同学 欢迎您,", Session["student"].ToString());
            }
        }
    }
}