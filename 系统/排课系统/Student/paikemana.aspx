<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paikemana.aspx.cs" Inherits="排课系统.Student.paikemana" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><link rel="shortcut icon" href="../favicon.ico" /><link rel="icon" href="../favicon.ico" />
    <title>后台管理</title>
    <link rel="stylesheet" type="text/css" href="../css/common.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
    <script type="text/javascript"  src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/modernizr.min.js"></script>
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
                            <a href="#"><i class="icon-font">&#xe003;</i>排课操作</a>
                            <ul class="sub-menu">
                                <li><a href="paikemana.aspx"><i class="icon-font">&#xe006;</i>课表管理</a></li>
                            </ul>
                        </li>
                        <li>
                            <a href="#"><i class="icon-font">&#xe018;</i>系统管理</a>
                            <ul class="sub-menu">
                                <li><a href="info.aspx"><i class="icon-font">&#xe017;</i>个人信息</a></li>
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
                    <div class="crumb-list"><i class="icon-font"></i><a href="index.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">专业管理</span></div>
                </div>
                <div class="search-wrap">
                    <div class="search-content">
                        <table class="search-tab">
                            <tr>
                                <th width="50"></th>
                                <th width="50"></th>
                                <th width="120"><%--已排课关键字:--%></th>
                                <td>
                                  <%--  <asp:TextBox class="common-text" placeholder="已排课关键字" ID="findinfo1" runat="server" Type="text"></asp:TextBox>--%></td>
                                <td>
                               <%--     <asp:Button ID="Button3" class="btn btn-default btn3" runat="server" Text="查询"
                                        Style="margin: auto;" OnClick="Button3_Click" />--%>

                                </td>
                                <td>
                                    <asp:Button ID="Button1" class="btn btn-success btn2" runat="server" Text="导出本课表"
                                        Style="margin: auto;" OnClick="Button2_Click" /></td>
                                <td>
                                    <input id="Button6" type="button" class="btn btn-success btn2" value="打印" onclick="printPage()" />
                                </td>
                  
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="result-wrap" id="printdiv">
                    <div align="center" style="font-size: large; font-weight: bold">已排课程列表</div>
                    <div class="result-content">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                            GridLines="None" AllowPaging="True" CssClass="result-tab"
                            DataKeyNames="id" Width="100%"
                            PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="xuhao" HeaderText="课号" ReadOnly="True" />
                                <asp:BoundField DataField="coursename" HeaderText="课程名称" ReadOnly="True" />
                                <asp:BoundField DataField="coursexingzhi" HeaderText="课程性质" ReadOnly="True" />
                                <asp:BoundField DataField="xueshiallz" HeaderText="周总学时" ReadOnly="True" />
                                <asp:BoundField DataField="xueshijiangshouz" HeaderText="周讲授学时" ReadOnly="True" />
                                <asp:BoundField DataField="xueshishiyanz" HeaderText="周实验学时" ReadOnly="True" />
                                <asp:BoundField DataField="zhouci" HeaderText="上课周次" ReadOnly="True" />
                                <asp:BoundField DataField="teachidz" HeaderText="主讲教师工号" ReadOnly="True" />
                                <asp:BoundField DataField="teachnamez" HeaderText="主讲教师" ReadOnly="True" />
                                <asp:BoundField DataField="major" HeaderText="专业" ReadOnly="True" />
                                <asp:BoundField DataField="grade" HeaderText="年级" ReadOnly="True" />
                                <asp:BoundField DataField="dianjiao" HeaderText="教室" ReadOnly="True" />
                                <asp:BoundField DataField="shuangyu" HeaderText="是否双语教学" ReadOnly="True" />
                                <asp:BoundField DataField="weekdays" HeaderText="星期" />
                                <asp:BoundField DataField="sections" HeaderText="节次" />
                                <asp:CommandField HeaderText="手动调节" ShowEditButton="True" EditText="手动调节" />
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
    </form>
</body>
</html>
