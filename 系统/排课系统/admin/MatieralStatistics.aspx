<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatieralStatistics.aspx.cs" Inherits="排课系统.admin.MatieralStatistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><link rel="shortcut icon" href="../favicon.ico" /><link rel="icon" href="../favicon.ico" />
    <title>后台管理</title>
    <link rel="stylesheet" type="text/css" href="../css/common.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/modernizr.min.js"></script>
    <script type="text/javascript" src="../Scripts/highcharts.js"></script>
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
                            <li><a href="majormana.aspx"><i class="icon-font">&#xe008;</i>专业管理</a></li>
                            <li><a href="teacherman.aspx"><i class="icon-font">&#xe005;</i>教师管理</a></li>
                            <li><a href="studentman.aspx"><i class="icon-font">&#xe005;</i>学生管理</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="#"><i class="icon-font">&#xe003;</i>排课操作</a>
                        <ul class="sub-menu">
                            <li><a href="courseplan.aspx"><i class="icon-font">&#xe052;</i>教学计划</a></li>
                            <li><a href="coursetask.aspx"><i class="icon-font">&#xe033;</i>教学任务</a></li>
                            <li><a href="paikemana.aspx"><i class="icon-font">&#xe005;</i>排课管理</a></li>
                            <li><a href="coursetablemana.aspx"><i class="icon-font">&#xe006;</i>课表管理</a></li>
                            <li><a href="coursetask.aspx"><i class="icon-font">&#xe033;</i>教学任务</a></li>
                              <li><a href="coursetablemana.aspx"><i class="icon-font">&#xe006;</i>课表管理</a></li>
                              <li><a href="MatieralStatistics.aspx"><i class="icon-font">&#xe006;</i>图表</a></li>
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
                    <div class="crumb-list"><i class="icon-font"></i><a href="index.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">图表</span></div>
                </div>
                <div class="result-wrap">
                    <div>
                        <h2>排课统计</h2>
                        <div id="ms1" style="width: 48%; float: left; height: 300px;"></div>
                        <div id="ms2" style="width: 48%; float: right; height: 300px;"></div>
                        <div id="ms3" style="width: 48%; float: left; height: 300px;"></div>
                        <div id="ms4" style="width: 48%; float: right; height: 300px;"></div>
                    </div>
                    <script type="text/javascript">
                        var url = "/Control/Handler.ashx?methods=l";
                        var urlparameter;
                        var imgarr = [];
                        var major = [];
                        var grade = [];
                        var kctype = [];
                        $(document).ready(function () {
                            $.getJSON(url + 1, function (data) {
                                $.each(data, function (i, item) {
                                    imgarr.push([item.Name, item.Count]);
                                });
                                $('#ms1').highcharts({
                                    chart: {
                                        plotBackgroundColor: null,
                                        plotBorderWidth: null,
                                        plotShadow: false
                                    },
                                    title: {
                                        text: '按课程统计'
                                    },
                                    tooltip: {
                                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                    },
                                    plotOptions: {
                                        pie: {
                                            allowPointSelect: true,
                                            cursor: 'pointer',
                                            dataLabels: {
                                                enabled: true,
                                                color: '#000000',
                                                connectorColor: '#000000',
                                                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                            }
                                        }
                                    },
                                    series: [{
                                        type: 'pie',
                                        name: '占例',
                                        data: imgarr
                                    }]
                                });
                            });
                            $.getJSON(url + 2, function (data) {
                                $.each(data, function (i, item) {
                                    major.push([item.Name, item.Count]);
                                });
                                $('#ms2').highcharts({
                                    chart: {
                                        plotBackgroundColor: null,
                                        plotBorderWidth: null,
                                        plotShadow: false
                                    },
                                    title: {
                                        text: '按专业统计'
                                    },
                                    tooltip: {
                                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                    },
                                    plotOptions: {
                                        pie: {
                                            allowPointSelect: true,
                                            cursor: 'pointer',
                                            dataLabels: {
                                                enabled: true,
                                                color: '#000000',
                                                connectorColor: '#000000',
                                                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                            }
                                        }
                                    },
                                    series: [{
                                        type: 'pie',
                                        name: '占例',
                                        data: major
                                    }]
                                });
                            });
                            $.getJSON(url + 3, function (data) {
                                $.each(data, function (i, item) {
                                    grade.push([item.Name, item.Count]);
                                });
                                $('#ms3').highcharts({
                                    chart: {
                                        plotBackgroundColor: null,
                                        plotBorderWidth: null,
                                        plotShadow: false
                                    },
                                    title: {
                                        text: '按年级统计'
                                    },
                                    tooltip: {
                                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                    },
                                    plotOptions: {
                                        pie: {
                                            allowPointSelect: true,
                                            cursor: 'pointer',
                                            dataLabels: {
                                                enabled: true,
                                                color: '#000000',
                                                connectorColor: '#000000',
                                                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                            }
                                        }
                                    },
                                    series: [{
                                        type: 'pie',
                                        name: '占例',
                                        data: grade
                                    }]
                                });
                            });
                            $.getJSON(url + 4, function (data) {
                                $.each(data, function (i, item) {
                                    kctype.push([item.Name, item.Count]);
                                });
                                $('#ms4').highcharts({
                                    chart: {
                                        plotBackgroundColor: null,
                                        plotBorderWidth: null,
                                        plotShadow: false
                                    },
                                    title: {
                                        text: '按课程类型统计'
                                    },
                                    tooltip: {
                                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                    },
                                    plotOptions: {
                                        pie: {
                                            allowPointSelect: true,
                                            cursor: 'pointer',
                                            dataLabels: {
                                                enabled: true,
                                                color: '#000000',
                                                connectorColor: '#000000',
                                                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                            }
                                        }
                                    },
                                    series: [{
                                        type: 'pie',
                                        name: '占例',
                                        data: kctype
                                    }]
                                });
                            });
                        });
                    </script>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
