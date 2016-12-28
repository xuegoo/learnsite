using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_mysite : System.Web.UI.Page
{
    private string currentwebpath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            LearnSite.Common.CookieHelp.KickStudent();
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            if (!IsPostBack)
            {
                GetHomePath();
                ShowStudent();
                ShowInfo();                
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }
    /// <summary>
    /// 显示信息
    /// </summary>
    private void ShowInfo()
    {
        string Wnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        int MySgrade = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString());
        int MySclass = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString());
        int MySyear = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString());

        LearnSite.Model.Webstudy wmodel = new LearnSite.Model.Webstudy();
        LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
        wmodel = ws.GetModelByWnum(Wnum);
        string wurl = "~/FtpSpace/" + MySyear.ToString() + "/" + MySclass.ToString() + "/" + Wnum;
        if (wmodel != null)
        {
            if (currentwebpath != "")
                wurl = wurl + "/" + currentwebpath;
            HyperLinksite.NavigateUrl = wurl;
            LabelWegg.Text = wmodel.Wegg.ToString();
            LabelWvote.Text = wmodel.Wvote.ToString();
            int Wused = wmodel.WquotaCurrent.Value / 1024;
            LabelWquota.Text = Wused.ToString() + "KB";
            LabelWquota.ToolTip = "详细描述：我的主页空间已占用" + wmodel.WquotaCurrent.Value + "字节";
            DataListvote.DataSource = ws.GetAllSite(MySgrade, MySclass, Wnum);//绑定全班数据
            DataListvote.DataBind();
        }
        else
        {
            if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
            {
                if (currentwebpath != "")
                    wurl = wurl + "/" + currentwebpath;
                HyperLinksite.NavigateUrl = wurl;
                LabelWegg.Text = "2";
                LabelWvote.Text = "5";
                int Wused = 30;
                LabelWquota.Text = Wused.ToString() + "KB";
                LabelWquota.ToolTip = "详细描述：我的主页空间已占用0字节";
                DataListvote.DataSource = ws.GetAllSite(MySgrade, MySclass, Wnum);//绑定全班数据
                DataListvote.DataBind();
            }
            else
            {
                Labelmsg.Text = "你没有网页制作账号，请咨询老师创建Ftp账号！";
            }
        }
    }
    protected void DataListvote_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "vote")
        {
            string Wnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
            int iv = ws.GetMyWegg(Wnum);
            if (iv > 0)
            {
                string Wname = ((HyperLink)e.Item.FindControl("Hypername")).Text;
                if (!CheckVotedName(Wname))//检测是否给该同学投过票，如果未投过则。。。
                {
                    string Wid = DataListvote.DataKeys[e.Item.ItemIndex].ToString();
                    ws.UpdateWvote(Int32.Parse(Wid));
                    iv = iv - 1;
                    ws.UpdateMyWegg(Wnum);
                    AddVoteName(Wname);//将该同学姓名添加到已投过票的同学列表中
                    System.Threading.Thread.Sleep(200);
                    Labelmsg.Text = "给" + Wname + "同学的网站投票成功！";
                    ShowInfo();
                }
                else
                {
                    Labelmsg.Text = "你已经给" + Wname + "同学的网站投过票了！";
                }
            }
            else
            {
                Labelmsg.Text = "你的投票次数已经用完！";
            }
        }
    }
    /// <summary>
    /// 检测是否给该同学投过票
    /// </summary>
    /// <param name="am"></param>
    /// <returns></returns>
    private bool CheckVotedName(string am)
    {
        bool isVoted = false;
        if (Session["SiteVotedNameList"] != null)
        {
            string vnl = Session["SiteVotedNameList"].ToString();
            if (vnl.IndexOf(am) > -1)
            {
                isVoted = true;
            }
        }
        return isVoted;
    }
    /// <summary>
    /// 将该同学姓名添加到已投过票的同学列表中
    /// </summary>
    /// <param name="am"></param>
    private void AddVoteName(string am)
    {
        if (Session["SiteVotedNameList"] != null)
        {
            Session["SiteVotedNameList"] = Session["SiteVotedNameList"] + "|" + am;
        }
        else
        {
            Session["SiteVotedNameList"] = am;
        }
    }
    private void ShowStudent()
    {
        int mySid = Int32.Parse(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString());
        Labelip.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        snum.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString());
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        sclass.Text = Sgrade + "." + Sclass + "班";
        sname.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString());
        string ssex = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sex"].ToString());
        sscore.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sscore"].ToString();
        sattitude.Text = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sattitude"].ToString();
        LearnSite.BLL.Students dbll = new LearnSite.BLL.Students();
        sleadername.Text = Server.UrlDecode(dbll.GetLeader(mySid));
        string murl = LearnSite.Common.Photo.GetStudentPhotoUrl(snum.Text, ssex);
        Imageface.ImageUrl = murl + "?temp=" + DateTime.Now.Millisecond.ToString();
        LabelRank.Text = Server.UrlDecode(Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["RankImage"].ToString());
        int myscores = int.Parse(sscore.Text);
        LabelRank.ToolTip = "你当前的等级为：" + myscores / 3 + "级";
        HyperLinksite.Text = sname.Text;
    }
    protected void DataListvote_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        bool webvotelimit = LearnSite.Common.XmlHelp.WebVote_Limit();
        string wdate = ((Label)e.Item.FindControl("LabelWupdate")).Text;
        if (wdate != "")
        {
            DateTime dt = DateTime.Now;
            string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
            DateTime nowdayDt = Convert.ToDateTime(today);
            DateTime updateDt = Convert.ToDateTime(wdate);
            if (updateDt > nowdayDt)
            {
                ((LinkButton)e.Item.FindControl("Linkvote")).BackColor = Lbc.BackColor;
            }
        }
        if (!webvotelimit)
        {
            ((LinkButton)e.Item.FindControl("Linkvote")).Enabled = true;
        }
        else
        {
            ((LinkButton)e.Item.FindControl("Linkvote")).Enabled = false;
            Labelmsg.Text = "现在还不可以进行网站投票，请努力制作好你的网站吧！";
        }
        string mystr = "网站现评价得分为：" + ((Label)e.Item.FindControl("LabelWscore")).Text + "分";
        HyperLink hlk = (HyperLink)e.Item.FindControl("Hypername");
        hlk.ToolTip = mystr;
        if (currentwebpath != "")
            hlk.NavigateUrl = hlk.NavigateUrl + "/" + currentwebpath;//替换为当前学生空间工作目录
    }

    /// <summary>
    /// 获取当前学生空间工作目录
    /// </summary>
    /// <returns></returns>
    private void GetHomePath()
    {
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
        currentwebpath = tbll.GetHpathfroStu(Int32.Parse(Sgrade), Int32.Parse(Sclass));
    }
}
