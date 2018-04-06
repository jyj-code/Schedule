using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace 排课系统.Student
{
    public partial class paikemana : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["student"] == null)
                {
                    WebMessageBox.Show("请登录", "../Default.aspx");
                }
                Label1.Text = string.Format("{0} 同学 欢迎您,", Session["student"].ToString());
                bind1();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"select t_coursetable.id as keyid,taskid,weekdays,sections,t_coursetask.*,t1.name as teachnamez,
                            t2.name as teachnamef,t3.name as teachnames 
                            from (((t_coursetable left join t_coursetask on taskid=t_coursetask.id) 
                            left join t_teacher t1 on teachidz=t1.teachid) 
                            left join t_teacher t2 on teachidf=t2.teachid) 
                            left join t_teacher t3 on teachids=t3.teachid
                            left join t_student t4 on t4.grade=t_coursetask.grade and t4.major=t_coursetask.major and t_coursetask.xuhao=t4.studentId
                            where t4.studentId='{0}'";
                DataTable dt = Operation.getDatatable(string.Format(sql, Session["student"].ToString()));
                CreateExcel(dt, "已排课程列表.xls");
            }
            catch (Exception)
            { }
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
            ////   #region 强行杀死最近打开的Excel进程

            //   System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            //   System.DateTime startTime = new DateTime();
            //   int m, killId = 0;
            //   for (m = 0; m < excelProc.Length; m++)
            //   {
            //       if (startTime < excelProc[m].StartTime)
            //       {
            //           startTime = excelProc[m].StartTime;
            //           killId = m;
            //       }
            //   }
            //   if (excelProc[killId].HasExited == false)
            //   {
            //       excelProc[killId].Kill();
            //   }
            //   #endregion


        }

        public void bind1()
        {
            string sql = @"select t_coursetable.id as keyid,taskid,weekdays,sections,t_coursetask.*,t1.name as teachnamez,
                            t2.name as teachnamef,t3.name as teachnames 
                            from (((t_coursetable left join t_coursetask on taskid=t_coursetask.id) 
                            left join t_teacher t1 on teachidz=t1.teachid) 
                            left join t_teacher t2 on teachidf=t2.teachid) 
                            left join t_teacher t3 on teachids=t3.teachid
                            left join t_student t4 on t4.grade=t_coursetask.grade and t4.major=t_coursetask.major and t_coursetask.xuhao=t4.studentId
                            where t4.studentId='{0}'";
            GridView2.DataSource = Operation.getDatatable(string.Format(sql, Session["student"].ToString()));
            GridView2.DataKeyNames = new string[] { "keyid" };//主键
            GridView2.DataBind();
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // 已排课列表分页
            GridView2.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind1();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            bind1();// 已排课列表查询
        }
    }
}