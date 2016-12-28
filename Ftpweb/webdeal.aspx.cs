using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_webdeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学生Ftp空间处理";
        ButtonFtpuser.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在创建Ftp账号中......'; document.getElementById('Loading').style.display= '';");
        ButtonMakeDir.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在创建Ftp目录中......'; document.getElementById('Loading').style.display= '';");
        if (!IsPostBack)
        {
            CheckFtpDb();
        }
    }

    protected void Buttonview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Ftpweb/weblist.aspx", false);
    }
    protected void ButtonFtpuser_Click(object sender, EventArgs e)
    {
        Labelmsg.Text = "学生人数：" + LearnSite.DBUtility.DbHelperSQL.TableCounts("Students") + " ";
        int QuotaMax = Int32.Parse(DDLspace.SelectedValue) * 1048576;
        DateTime nowtime1 = DateTime.Now;
        LearnSite.Ftp.Reg.Upgrade();//删除学生表中不存在学号的账号（根据已经清除过的学生表与网页表对比）
        System.Threading.Thread.Sleep(500);
        LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
        ws.Upgrade();//删除Webstudy中学号不在Students的记录
        System.Threading.Thread.Sleep(500);
        Labelmsg.Text = Labelmsg.Text + LearnSite.Ftp.Create.FtpUserCreate(QuotaMax);
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = Labelmsg.Text + "用时" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒<br />请在教师平台重新创建模拟学生ftp账号！";
    }
    protected void ButtonMakeDir_Click(object sender, EventArgs e)
    {
        DateTime nowtime1 = DateTime.Now;
        string msg = LearnSite.Ftp.Create.FtpDirCreate();
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = msg + "用时" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";
    }

    private void CheckFtpDb()
    {
        if (!LearnSite.Ftp.FtpHelper.DatabaseExist())
        {
            Labelmsg.Text = "您未配置好ftp数据库，请不要操作本页面！";
            ButtonFtpuser.Enabled = false;
        }
    }
}
