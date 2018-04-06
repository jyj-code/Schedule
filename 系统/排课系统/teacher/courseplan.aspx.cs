using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI;
using NPOI.HSSF;
using NPOI.HPSF;
using NPOI.SS;
using NPOI.Util;
using NPOI.SS.Util;
namespace 排课系统.teacher
{
    public partial class coursetask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Default.aspx");
                }
                else
                    Label1.Text = string.Format("{0} 老师欢迎您,", Session["teachname"].ToString());
                DataTable dt1 = Operation.getDatatable("select DISTINCT major from t_coursetask");
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = "select t_coursetask.*,t1.name as teachnamez,t2.name as teachnamef,t3.name as teachnames from ((t_coursetask left join t_teacher t1 on teachidz=t1.teachid) left join t_teacher t2 on teachidf=t2.teachid) left join t_teacher t3 on teachids=t3.teachid" +
                " where 1=1  and t1.name='"+ Session["teachname"].ToString() + "'";
            DataSourceDataTable2= Operation.getDatatable(sqlstr);
            GridView1.DataSource = DataSourceDataTable2;
            GridView1.DataKeyNames = new string[] { "id" };//主键
            GridView1.DataBind();
        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sqlstr = "delete from t_coursetask where id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            Operation.runSql(sqlstr);
            bind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //新增
            Response.Redirect("courseplanadd.aspx");
        }
        public void CreateExcel(DataTable dt, string fileName)
        {
            HttpResponse resp;
            resp = Page.Response;

            resp.Buffer = true;
            resp.ClearContent();
            resp.ClearHeaders();
            resp.Charset = "GB2312";


            //  resp.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            resp.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文   
            resp.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。



            string colHeaders = "", ls_item = "";

            ////定义表对象与行对象，同时用DataSet对其值进行初始化
            //DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的

            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\n";
                }
                else
                {
                    colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\t";
                }

            }
            resp.Write(colHeaders);

            //向HTTP输出流中写入取得的数据信息

            //逐行处理数据 
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据   
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += row[i].ToString().Trim() + "\n";
                    }
                    else
                    {
                        ls_item += row[i].ToString().Trim() + "\t";
                    }

                }
                resp.Write(ls_item);
                ls_item = "";

            }
            resp.End();
        }
        public static DataTable DataSourceDataTable2 { get; set; }
        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = DataSourceDataTable2;
            CreateExcel(dt, "课表.xls");
        }
    }
}