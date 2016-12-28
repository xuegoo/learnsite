using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ftpweb_website : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Buttonspace.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在获取空间占用情况中......'; document.getElementById('Loading').style.display= '';");
        ButtonRadom.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在生成自定义随机密码中......'; document.getElementById('Loading').style.display= '';");
        BtnTeacher.Attributes.Add("OnClick", "document.getElementById('Textcmd').value='正在创建教师模拟学生角色Ftp空间......'; document.getElementById('Loading').style.display= '';");
        if (!IsPostBack)
        {
            Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学生网站处理页面";
            CheckFtpDb();
            GetHomePath();
        }
    }
    protected void Buttonspace_Click(object sender, EventArgs e)
    {
        Labelmsg.Text = "";
        DateTime nowtime1 = DateTime.Now;
        LearnSite.Ftp.Reg.WebUserSpace();
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = "生成空间占用情况 用时：" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";

    }
    protected void ButtonRadom_Click(object sender, EventArgs e)
    {
        int slen = Int32.Parse(DDLlen.SelectedValue);
        string smethod = DDLmethod.SelectedValue;
        DateTime nowtime1 = DateTime.Now;
        Labelmsg.Text = LearnSite.Ftp.Reg.UpdatePwdRandom(slen, smethod);//自定义密码长度和类型
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = Labelmsg.Text + "<br/> 随机" + slen.ToString() + "位密码生成 用时：" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";

    }
    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        Labelmsg.Text = "";
        DateTime nowtime1 = DateTime.Now;
        LearnSite.Ftp.Disk.DelAllOtherDir();
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = "清理不允许文件及目录 用时：" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";

    }
    protected void ButtonVote_Click(object sender, EventArgs e)
    {
        int eggs = 2;
        if (DDLWvote.SelectedValue != "")
        {
            eggs = Int32.Parse(DDLWvote.SelectedValue);
        }
        Labelmsg.Text = "";
        DateTime nowtime1 = DateTime.Now;
        LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
        ws.ResetWegg(eggs);//重置所上课班级网站投票次数
        ws.ClearNoSiteVote();//将无网站制作内容的得票清零
        DateTime nowtime2 = DateTime.Now;
        Labelmsg.Text = "重新设置学生投票次数为" + eggs.ToString() + " 用时：" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";

    }
    protected void Btnsite_Click(object sender, EventArgs e)
    {
        string url = "~/Ftpweb/webshow.aspx";
        Response.Redirect(url, false);
    }
    protected void ButtonCheck_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
            string myhomepath = tbll.GetHpath(Hid);
            DateTime nowtime1 = DateTime.Now;
            LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
            string htmlname = DDLhtmlname.SelectedValue;
            if (myhomepath != "")
            {
                htmlname = myhomepath + "/" + htmlname;
            }
            ws.WebSiteUpdateCheck(DDLhtmlname.SelectedValue);
            DateTime nowtime2 = DateTime.Now;
            Labelmsg.Text = "检测学生网站更新 用时：" + LearnSite.Common.Computer.Datagone(nowtime1, nowtime2) + "秒";

        }
    }

    protected void BtnReBuild_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.Webstudy wbll = new LearnSite.BLL.Webstudy();
            wbll.WebUpdateWurl(Int32.Parse(Hid));
        }
    }
    protected void BtnTeacher_Click(object sender, EventArgs e)
    {
        DateTime nowtime1 = DateTime.Now;
        string teapwd = getTeaPwd();
        int createnum = LearnSite.Ftp.SimulationFtp.TeacherSlftp(teapwd);
        DateTime nowtime2 = DateTime.Now;
        if (createnum > 0)
            Labelmsg.Text = "创建模拟学生Ftp空间账号:" + createnum.ToString() + "个，Ftp密码(18位)都为" + teapwd;
        else
            Labelmsg.Text = "模拟学生Ftp空间账号已经存在，更新ftp密码(18位)都为" + teapwd;
    }
    private string getTeaPwd()
    {
        string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
        return LearnSite.Common.WordProcess.GetMD5_Nbit(Hid, 18);
    }
    private void CheckFtpDb()
    {
        if (!LearnSite.Ftp.FtpHelper.DatabaseExist())
        {
            Labelmsg.Text = "您未配置好ftp数据库，请不要操作本页面！";
        }
    }
    private void GetHomePath()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
            string myhomepath = tbll.GetHpath(Hid);
            TextBoxHome.Text = myhomepath;
            Labelmsg.Text = "当前ftp端口配置为"+LearnSite.Common.XmlHelp.GetFtpPort()+"端口";
        }
    }
    protected void BtnEditHome_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            string indir = TextBoxHome.Text.Trim();
            if (LearnSite.Common.WordProcess.ExistSpecial(indir))
            {
                LearnSite.Common.WordProcess.Alert("填写内容包含特殊符号，请使用数字或字母！", this.Page);
                GetHomePath();
            }
            else
            {
                LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
                tbll.SetHpath(indir, Int32.Parse(Hid));//设定当前学生空间工作子目录
                Labelmsg.Text = "设定学生空间的网站首页位置为"+indir+"成功！";
            }
        }
    }
    protected void ButtonStuPwd_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.Ftp.Reg.UpdateStuPwd(Hid);
        }
    }
}
