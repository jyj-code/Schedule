using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace 排课系统.Control
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            string returnInfo = string.Empty;
            string method = context.Request.QueryString["methods"].ToLower();
            switch (method)
            {
                case "l1":
                    returnInfo = GetAjaxData();
                    break;
                case "l2":
                    returnInfo = GetMajorCount();
                    break;
                case "l3":
                    returnInfo = GetGradeCount();
                    break;
                case "l4":
                    returnInfo = GetTypeCount();
                    break;
            }
            context.Response.Write(returnInfo);
        }


        public string GetAjaxData()
        {
            string sql = @"	select coursename as Name,count(coursename) as Count
                            from (((t_coursetable left join t_coursetask on taskid=t_coursetask.id) 
                            left join t_teacher t1 on teachidz=t1.teachid) 
                            left join t_teacher t2 on teachidf=t2.teachid) 
                            left join t_teacher t3 on teachids=t3.teachid
                            left join t_student t4 on t4.grade=t_coursetask.grade and t4.major=t_coursetask.major 
							and t_coursetask.xuhao=t4.studentId
							group by coursename";
            return DataTableToJsonWithJavaScriptSerializer(Operation.getDatatable(sql));
        }
        public string GetMajorCount()
        {
            string sql = @"select t_coursetask.major as Name,count(t_coursetask.major) as Count
                            from (((t_coursetable left join t_coursetask on taskid=t_coursetask.id) 
                            left join t_teacher t1 on teachidz=t1.teachid) 
                            left join t_teacher t2 on teachidf=t2.teachid) 
                            left join t_teacher t3 on teachids=t3.teachid
                            left join t_student t4 on t4.grade=t_coursetask.grade and t4.major=t_coursetask.major 
							and t_coursetask.xuhao=t4.studentId
							group by t_coursetask.major";
            return DataTableToJsonWithJavaScriptSerializer(Operation.getDatatable(sql));
        }
        public string GetGradeCount()
        {
            string sql = @"select t_coursetask.grade as Name,count(t_coursetask.grade) as Count
                            from (((t_coursetable left join t_coursetask on taskid=t_coursetask.id) 
                            left join t_teacher t1 on teachidz=t1.teachid) 
                            left join t_teacher t2 on teachidf=t2.teachid) 
                            left join t_teacher t3 on teachids=t3.teachid
                            left join t_student t4 on t4.grade=t_coursetask.grade and t4.major=t_coursetask.major 
							and t_coursetask.xuhao=t4.studentId
							group by t_coursetask.grade";
            return DataTableToJsonWithJavaScriptSerializer(Operation.getDatatable(sql));
        }
        public string GetTypeCount()
        {
            string sql = @"
						select t_coursetask.coursexingzhi as Name,count(t_coursetask.coursexingzhi) as Count
                            from (((t_coursetable left join t_coursetask on taskid=t_coursetask.id) 
                            left join t_teacher t1 on teachidz=t1.teachid) 
                            left join t_teacher t2 on teachidf=t2.teachid) 
                            left join t_teacher t3 on teachids=t3.teachid
                            left join t_student t4 on t4.grade=t_coursetask.grade and t4.major=t_coursetask.major 
							and t_coursetask.xuhao=t4.studentId
							group by t_coursetask.coursexingzhi";
            return DataTableToJsonWithJavaScriptSerializer(Operation.getDatatable(sql));
        }
        public string DataTableToJsonWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}