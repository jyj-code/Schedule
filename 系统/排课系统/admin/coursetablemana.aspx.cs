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

using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using System.Text;

namespace 排课系统.admin
{
    public partial class coursetablemana : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    WebMessageBox.Show("请登录", "../Default.aspx");
                }
                else

                    Label1.Text = string.Format("{0} 管理 欢迎您,", Session["username"].ToString());
                DropDownList1.Items.Add("课程");
                DropDownList1.Items.Add("专业");
                DropDownList1.Items.Add("年级");
                DropDownList1.Items.Add("教室");
                DropDownList1.Items.Add("周次");
                DropDownList1.Items.Add("学号");
                DropDownList1.Items.Add("教师");
                bind();
            }
        }
        public string GetStr()
        {
            if (!string.IsNullOrEmpty(txtfindinfo.Text))
            {
                switch (DropDownList1.SelectedValue.ToString())
                {
                    case "课程": return string.Format("coursename='{0}'", txtfindinfo.Text);
                    case "专业": return string.Format("major='{0}'", txtfindinfo.Text);
                    case "年级": return string.Format("grade='{0}'", txtfindinfo.Text);
                    case "教室": return string.Format("dianjiao='{0}'", txtfindinfo.Text);
                    case "教师": return string.Format("t_teacher.name='{0}'", txtfindinfo.Text);
                    case "周次":
                        int p = 0;
                        int.TryParse(txtfindinfo.Text, out p);
                        if (p > 0)
                        {
                            StringBuilder str = new StringBuilder();
                            List<string> list = new List<string>();
                            DataTable dt = Operation.getDatatable("select distinct zhouci from t_coursetask");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                var x = dt.Rows[i]["zhouci"].ToString();
                                try
                                {
                                    for (int j = Convert.ToInt32(x.Split('-')[0]); j <= Convert.ToInt32(x.Split('-')[1]); j++)
                                    {
                                        if (j == p)
                                        {
                                            str.AppendFormat("'{0}',", x);
                                            break;
                                        }
                                    }
                                }
                                catch { }
                            }
                            return string.Format("zhouci IN ({0})", str.ToString().Substring(0, str.ToString().Length - 1));
                        }
                        return string.Format("zhouci='{0}'", txtfindinfo.Text);
                    case "学号": return string.Format("xuhao='{0}'", txtfindinfo.Text);
                }
            }
            return string.Format("1=1");
        }
        // 获得周i+1  第j+1节的课程
        private string getCourse(DataTable dt, int i, int j)
        {
            string t = "";
            for (int ii = 0; ii < dt.Rows.Count; ++ii)
            {
                if (dt.Rows[ii]["weekdays"].ToString().Equals((i + 1).ToString()) && dt.Rows[ii]["sections"].ToString().Equals((j + 1).ToString()))
                {
                    //string dianjiao = "", shuangyu = "";
                    //if (dt.Rows[ii]["dianjiao"].ToString().IndexOf("是") >= 0) dianjiao = "   (电教)";
                    //if (dt.Rows[ii]["shuangyu"].ToString().IndexOf("是") >= 0) dianjiao = "   (双语)";
                    t = dt.Rows[ii]["dianjiao"].ToString();// dt.Rows[ii]["coursename"].ToString() + "   (" + dt.Rows[ii]["zhouci"].ToString() + ")   " + dt.Rows[ii]["teachname"].ToString() + dianjiao + shuangyu;
                    break;
                }
            }
            return t;
        }
        private string getCourse2(DataTable dt, int i, int j)
        {
            string t = "";
            for (int ii = 0; ii < dt.Rows.Count; ++ii)
            {
                if (dt.Rows[ii]["weekdays"].ToString().Equals((i + 1).ToString()) && dt.Rows[ii]["sections"].ToString().Equals((j + 1).ToString()))
                {
                    t = dt.Rows[ii]["coursename"].ToString();
                    break;
                }
            }
            return t;
        }
        private string getCourse3(DataTable dt, int i, int j)
        {
            string t = "";
            for (int ii = 0; ii < dt.Rows.Count; ++ii)
            {
                if (dt.Rows[ii]["weekdays"].ToString().Equals((i + 1).ToString()) && dt.Rows[ii]["sections"].ToString().Equals((j + 1).ToString()))
                {
                    t = dt.Rows[ii]["teachname"].ToString();
                    break;
                }
            }
            return t;
        }
        private string getCourse4(DataTable dt, int i, int j)
        {
            string t = "";
            for (int ii = 0; ii < dt.Rows.Count; ++ii)
            {
                if (dt.Rows[ii]["weekdays"].ToString().Equals((i + 1).ToString()) && dt.Rows[ii]["sections"].ToString().Equals((j + 1).ToString()))
                {
                    t = dt.Rows[ii]["zhouci"].ToString();
                    break;
                }
            }
            return t;
        }



        public void bind()
        {
            //title.Text = DropDownList2.SelectedValue.ToString() + "级" + DropDownList1.SelectedValue.ToString();
            string sqlstr = "select t_coursetable.id as keyid,weekdays,sections,t_coursetask.coursename,t_coursetask.zhouci,t_teacher.name as teachname,dianjiao,shuangyu " +
                        "from (t_coursetable left join t_coursetask on taskid=t_coursetask.id) left join t_teacher on teachidz=teachid where " + GetStr() + "";
            DataTable dt = Operation.getDatatable(sqlstr);
            string[] temp = new string[20];
            string[] temp2 = new string[20];
            string[] temp3 = new string[20];
            string[] temp4 = new string[20];

            // 构造课表
            int k;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    k = i * 4 + j;  // 当前索引
                    temp[k] = getCourse(dt, i, j);
                    temp2[k] = getCourse2(dt, i, j);
                    temp3[k] = getCourse3(dt, i, j);
                    temp4[k] = getCourse4(dt, i, j);
                }
            dt = new DataTable();
            dt.Columns.Add("weekdays", Type.GetType("System.String"));
            dt.Columns.Add("sections", Type.GetType("System.String"));
            dt.Columns.Add("dianjiao", Type.GetType("System.String"));
            dt.Columns.Add("coursename", Type.GetType("System.String"));
            dt.Columns.Add("teachname", Type.GetType("System.String"));
            dt.Columns.Add("zhouci", Type.GetType("System.String"));
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    DataRow r = dt.NewRow();
                    r[0] = xingqi[i];
                    r[1] = jieci[j];
                    r[2] = temp[i * 4 + j];
                    r[3] = temp2[i * 4 + j];
                    r[4] = temp3[i * 4 + j];
                    r[5] = temp4[i * 4 + j];
                    dt.Rows.Add(r);
                }
            DataRow r1 = dt.NewRow(); r1[0] = "六";
            dt.Rows.Add(r1);
            DataRow r2 = dt.NewRow(); r2[0] = "日";
            dt.Rows.Add(r2);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            DataSourceDataTable = dt;
            GroupRows(GridView1, 0);
        }

        public static void GroupRows(GridView GridView1, int cellNum)
        {

            int i = 0, rowSpanNum = 1;
            while (i < GridView1.Rows.Count - 1)
            {
                GridViewRow gvr = GridView1.Rows[i];

                for (++i; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gvrNext = GridView1.Rows[i];
                    if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                    {
                        gvrNext.Cells[cellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;

                        rowSpanNum = 1;
                        break;
                    }

                    if (i == GridView1.Rows.Count - 1)
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;

                    }
                }
            }

        }

        string[] jieci = new string[4] { "1,2", "3,4", "5,6", "7,8" };
        string[] xingqi = new string[7] { "一", "二", "三", "四", "五", "六", "日" };
        //查询
        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
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
        public static DataTable DataSourceDataTable { get; set; }
        protected void Button2_Click(object sender, EventArgs e)
        {

            DataTable dt = DataSourceDataTable;
            CreateExcel(dt, "课表.xls");

            ////导出
            //HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            //ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

            //// 获取单元格 并设置样式
            //ICellStyle styleCell = hssfworkbook.CreateCellStyle();
            ////居中
            //styleCell.Alignment = HorizontalAlignment.Center;
            ////垂直居中
            //styleCell.VerticalAlignment = VerticalAlignment.Center;
            ////设置字体
            //IFont fontColorRed = hssfworkbook.CreateFont();
            //fontColorRed.FontHeight = 17 * 17;
            //styleCell.SetFont(fontColorRed);
            //styleCell.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin; //下边框为细线边框
            //styleCell.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;//左边框
            //styleCell.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;//上边框
            //styleCell.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;//右边框


            //ICellStyle styleCell1 = hssfworkbook.CreateCellStyle();
            //styleCell1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//居中
            //styleCell1.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            //IFont fontColorRed1 = hssfworkbook.CreateFont();
            //fontColorRed1.FontHeight = 20 * 20;
            //styleCell1.SetFont(fontColorRed1);


            //DataTable dt = getDtFromDB(DropDownList1.SelectedValue.ToString(), "");
            //int row = 0;
            //IRow row1 = sheet1.CreateRow(row++);
            //ICell cell = row1.CreateCell(0);
            //cell.CellStyle = styleCell1;
            //cell.SetCellValue("" + "级" + DropDownList1.SelectedValue.ToString());
            //sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 2));// 合并单元格
            //int k = 0;
            //for (int i = 0; i < 5; i++)
            //{
            //    int tt = row;
            //    for (int j = 0; j < 4; j++)
            //    {
            //        k = i * 4 + j;  // 当前索引
            //        row1 = sheet1.CreateRow(row++);
            //        cell = row1.CreateCell(0);
            //        cell.CellStyle = styleCell;
            //        cell.SetCellValue(dt.Rows[k][0].ToString());
            //        cell = row1.CreateCell(1);
            //        cell.CellStyle = styleCell;
            //        cell.SetCellValue(dt.Rows[k][1].ToString());
            //        cell = row1.CreateCell(2);
            //        cell.CellStyle = styleCell;
            //        cell.SetCellValue(dt.Rows[k][2].ToString());
            //    }
            //    sheet1.AddMergedRegion(new CellRangeAddress(tt, tt + 3, 0, 0));
            //}
            //++k;
            //row1 = sheet1.CreateRow(row++);
            //cell = row1.CreateCell(0);
            //cell.SetCellValue(dt.Rows[k][0].ToString());
            //cell.CellStyle = styleCell;
            //cell = row1.CreateCell(1); cell.CellStyle = styleCell; cell = row1.CreateCell(2); cell.CellStyle = styleCell;
            //++k;
            //row1 = sheet1.CreateRow(row++);
            //cell = row1.CreateCell(0);
            //cell.SetCellValue(dt.Rows[k][0].ToString());
            //cell.CellStyle = styleCell;
            //cell = row1.CreateCell(1); cell.CellStyle = styleCell; cell = row1.CreateCell(2); cell.CellStyle = styleCell;

            //// 设置行宽度
            //sheet1.SetColumnWidth(0, 20 * 256);  // 设置第二列的宽度
            //sheet1.SetColumnWidth(1, 20 * 256);  // 设置第二列的宽度
            //sheet1.SetColumnWidth(2, 100 * 256);  // 设置第二列的宽度
            //// 输出Excel
            //string filename = "coursetable.xls";
            //var context = HttpContext.Current;
            //context.Response.ContentType = "application/vnd.ms-excel";
            //context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", context.Server.UrlEncode(filename)));
            //context.Response.Clear();
            //MemoryStream file = new MemoryStream();
            //hssfworkbook.Write(file);
            //context.Response.BinaryWrite(file.GetBuffer());
            //context.Response.End();
        }
        DataTable getDtFromDB(string major, string grade)
        {
            title.Text = grade + "级" + major;
            string sqlstr = "select t_coursetable.id as keyid,weekdays,sections,t_coursetask.coursename,t_coursetask.zhouci,t_teacher.name as teachname,dianjiao,shuangyu " +
                        "from (t_coursetable left join t_coursetask on taskid=t_coursetask.id) left join t_teacher on teachidz=teachid where major='" +
                        major + "' and grade='" + grade + "'";
            DataTable dt = Operation.getDatatable(sqlstr);
            string[] temp = new string[20];
            // 构造课表
            int k;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    k = i * 4 + j;  // 当前索引
                    temp[k] = getCourse(dt, i, j);
                }
            dt = new DataTable();
            dt.Columns.Add("weekdays", Type.GetType("System.String"));
            dt.Columns.Add("sections", Type.GetType("System.String"));
            dt.Columns.Add("course", Type.GetType("System.String"));
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    DataRow r = dt.NewRow();
                    r[0] = xingqi[i];
                    r[1] = jieci[j];
                    r[2] = temp[i * 4 + j];
                    dt.Rows.Add(r);
                }
            DataRow r1 = dt.NewRow(); r1[0] = "六";
            dt.Rows.Add(r1);
            DataRow r2 = dt.NewRow(); r2[0] = "日";
            dt.Rows.Add(r2);
            return dt;
        }
    }
}