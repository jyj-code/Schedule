using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 排课系统.teacher
{
    public partial class studentmanadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["username"] == null)
                {
                    WebMessageBox.Show("请登录","../Default.aspx");
                }
                Label1.Text = string.Format("{0} 管理 欢迎您,", Session["username"].ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text == "")
            {
                WebMessageBox.Show("请输入学生学号"); return;
            }
            if (Operation.getDatatable("select * from t_student where studentId='" + txtUserNo.Text + "'").Rows.Count > 0)
            {
                WebMessageBox.Show("此学生学号已经存在"); return;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                WebMessageBox.Show("请输入学生姓名"); return;
            }
            if (string.IsNullOrEmpty(txtmajor.Text))
            {
                WebMessageBox.Show("请输入学生专业"); return;
            }
            if (string.IsNullOrEmpty(txtgrade.Text))
            {
                WebMessageBox.Show("请输入学生所在年级"); return;
            }
            var sql=string.Format("insert into t_student(studentId,name,grade,major,pwd) values('{0}','{1}','{2}','{3}','000000')",txtUserNo.Text,txtUserName.Text,txtgrade.Text,txtmajor.Text);
            Operation.runSql(sql);
            WebMessageBox.Show("添加完成", "studentman.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("studentman.aspx");
        }
    }
}