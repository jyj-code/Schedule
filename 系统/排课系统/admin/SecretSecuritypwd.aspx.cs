using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 排课系统.admin
{
    public partial class SecretSecuritypwd : System.Web.UI.Page
    {
        public static bool IsIsertSecretSecuritypwd = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachname"] != null || Session["username"] != null || Session["student"] != null)
                {
                    if (Session["teachname"] != null)
                    {
                        Session["username"] = Session["teachname"];
                    }
                    else if (Session["student"] != null)
                    {
                        Session["username"] = Session["student"];
                    }
                }
                if (Session["username"] == null)
                {
                    WebMessageBox.Show("请登录", "../Default.aspx");
                }
                Label1.Text = string.Format("{0} 管理 欢迎您,", Session["username"].ToString());
                var dt = Operation.getDatatable(string.Format("select * from t_SecretSecuritypwd where Users='{0}'", Label1.Text));
                if (dt != null && dt.Rows.Count > 0)
                {
                    TextBox1.Text = dt.Rows[0]["problem"].ToString();
                    TextBox1.Enabled = false;
                    IsIsertSecretSecuritypwd = false;
                }

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //添加
            if (TextBox1.Text == "")
            {
                WebMessageBox.Show("请输入密保问题"); return;
            }
            if (TextBox2.Text == "")
            {
                WebMessageBox.Show("请确认密保问题答案"); return;
            }
            if (TextBox3.Text != TextBox2.Text)
            {
                WebMessageBox.Show("两次输入密保问题答案不一致"); return;
            }
            if (IsIsertSecretSecuritypwd)
                Operation.runSql(string.Format("INSERT INTO t_SecretSecuritypwd (Users,problem,answer) VALUES('{0}','{1}','{2}')", Label1.Text, TextBox1.Text, TextBox2.Text));
            else
                Operation.runSql(string.Format("UPDATE t_SecretSecuritypwd SET answer='{2}' WHERE Users='{0}' AND problem='{1}'", Label1.Text, TextBox1.Text, TextBox2.Text));
            WebMessageBox.Show("密保设置完成");
        }
    }
}