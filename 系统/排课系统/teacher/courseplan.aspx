<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="courseplan.aspx.cs" Inherits="排课系统.teacher.coursetask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="../favicon.ico" />
    <link rel="icon" href="../favicon.ico" />
    <title>后台管理</title>
    <link rel="stylesheet" type="text/css" href="../css/common.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
    <script type="text/javascript" src="../Scripts/modernizr.min.js"></script>
    <script type="text/javascript" src="../Scripts/modernizr.min.js"></script>
    <script type="text/javascript">
        function printPage() {
            var newWin = window.open('printer', '', '');
            var titleHTML = document.getElementById("printdiv").innerHTML;
            newWin.document.write(titleHTML);
            newWin.document.location.reload();
            newWin.print();
            newWin.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="topbar-wrap white">
            <div class="topbar-inner clearfix">
                <div class="topbar-logo-wrap clearfix">
                    <h1 class="topbar-logo none"><a href="index.aspx" class="navbar-brand">后台管理</a></h1>
                    <ul class="navbar-list clearfix">
                        <li><a class="on" href="index.aspx">首页</a></li>
                    </ul>
                </div>
                <div class="top-info-wrap">
                    <ul class="top-info-list clearfix">
                        <li>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </li>
                        <li><a href="../Default.aspx">退出</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="container clearfix">
            <div class="sidebar-wrap">
                <div class="sidebar-title">
                    <h1>菜单</h1>
                </div>
                <div class="sidebar-content">
                    <ul class="sidebar-list">
                        <li>
                            <a href="#"><i class="icon-font">&#xe003;</i>基本操作</a>
                            <ul class="sub-menu">
                                <li class="active"><a href="studentman.aspx"><i class="icon-font">&#xe005;</i>学生管理</a></li>
                            </ul>
                        </li>
                        <li>
                            <a href="#"><i class="icon-font">&#xe003;</i>排课操作</a>
                            <ul class="sub-menu">
                                <li><a href="courseplan.aspx"><i class="icon-font">&#xe052;</i>教学计划</a></li>
                                <li><a href="SecretSecuritypwd.aspx"><i class="icon-font">&#xe006;</i>密保管理</a></li>
                            </ul>
                        </li>
                        <li>
                            <a href="#"><i class="icon-font">&#xe018;</i>系统管理</a>
                            <ul class="sub-menu">
                                <li><a href="modifypwd.aspx"><i class="icon-font">&#xe017;</i>个人密码</a></li>
                                <li><a href="SecretSecuritypwd.aspx"><i class="icon-font">&#xe006;</i>密保管理</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <!--/sidebar-->
            <!--/main-->


            <div class="main-wrap">

                <div class="crumb-wrap">
                    <div class="crumb-list"><i class="icon-font"></i><a href="index.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">教学任务</span></div>
                </div>
                <div class="result-wrap">
                    <div class="result-title">
                        <div class="result-list">
                            <asp:Button ID="Button3" class="btn btn-info btn2" runat="server" Text="新增"
                                Style="margin: auto;" OnClick="Button3_Click" />
                            &nbsp; &nbsp; &nbsp;
                                    <input id="Button6" type="button" class="btn btn-success btn2" value="打印" onclick="printPage()" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="Button2" class="btn btn-success btn2" runat="server" Text="导出本课表"
                                Style="margin: auto;" OnClick="Button2_Click" />
                        </div>
                    </div>
                    <div class="result-content" id="printdiv">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            GridLines="None" AllowPaging="True" CssClass="result-tab"
                            DataKeyNames="id" Width="100%"
                            OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDeleting="GridView1_RowDeleting" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="xuhao" HeaderText="课号" ReadOnly="True" />
                                <asp:BoundField DataField="coursename" HeaderText="课程名称" />
                                <asp:BoundField DataField="coursexingzhi" HeaderText="课程性质" />
                                <asp:BoundField DataField="xueshiall" HeaderText="总学时" />
                                <asp:BoundField DataField="xueshijiangshou" HeaderText="讲授学时" />
                                <asp:BoundField DataField="xueshishiyan" HeaderText="实验学时" />
                                <asp:BoundField DataField="xueshiallz" HeaderText="周总学时" />
                                <asp:BoundField DataField="xueshijiangshouz" HeaderText="周讲授学时" />
                                <asp:BoundField DataField="xueshishiyanz" HeaderText="周实验学时" />

                                <asp:BoundField DataField="zhouci" HeaderText="上课周次" />
                                <asp:BoundField DataField="khtype" HeaderText="考核方式" />
                                <asp:BoundField DataField="courserongliang" HeaderText="课程容量" />

                                <asp:BoundField DataField="teachnamez" HeaderText="主讲教师" />
                                <asp:BoundField DataField="teachnamef" HeaderText="辅导教师" />
                                <asp:BoundField DataField="teachnames" HeaderText="实验教师" />
                                <asp:BoundField DataField="dianjiao" HeaderText="教室" />
                                <asp:BoundField DataField="shuangyu" HeaderText="是否双语教学" />
                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
                            </Columns>
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="Black" />

                        </asp:GridView>
                    </div>

                </div>
            </div>


        </div>

    </form>
</body>
</html>
