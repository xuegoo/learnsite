using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
public partial class Student_showactive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            if (Request.QueryString["Mid"] != null && Request.QueryString["Cid"] != null)
            {
                LearnSite.Common.CookieHelp.KickStudent();
                if (!IsPostBack)
                {
                    ShowMission();
                    ShowIpWorkDone();
                }
            }
        }
        else
        {
            LearnSite.Common.CookieHelp.JudgeStudentCookies();
        }
    }

    protected string AUTHID()
    {
        return Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Value;
    }

    private void ShowMission()
    {
        string Mid = Request.QueryString["Mid"].ToString();
        if (LearnSite.Common.WordProcess.IsNum(Mid))
        {
            LearnSite.Model.Mission model = new LearnSite.Model.Mission();
            LearnSite.BLL.Mission mn = new LearnSite.BLL.Mission();

            model = mn.GetModel(Int32.Parse(Mid));
            if (model != null)
            {
                string sSyear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
                string sSclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
                string sSnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
                string sLoginIp = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
                string sWcid = model.Mcid.ToString();
                string sWmid = Mid;
                string sWmsort = model.Msort.ToString();
                string sWfiletype = model.Mfiletype;
                LabelMtitle.Text = model.Mtitle;
                LabelMcid.Text = model.Mcid.ToString();
                Mcontent.InnerHtml = HttpUtility.HtmlDecode(model.Mcontent);
                LabelSnum.Text = sSnum;
                LabelMfiletype.Text = sWfiletype;
                bool isupload=model.Mupload;
                CkMupload.Checked = isupload;
                CkMgroup.Checked = model.Mgroup;
                LabelMsort.Text = sWmsort;
                LabelMid.Text = Mid;
                upFileTypeGroup.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
                upFileUrlGroup.Text = "小组作品";
                switch (sWfiletype)
                {
                    case "doc":
                        LabelUploadType.Text = "*.doc;*.docx";
                        break;
                    case "ppt":
                        LabelUploadType.Text = "*.ppt;*.pptx";
                        break;
                    case "xls":
                        LabelUploadType.Text = "*.xls;*.xlsx";
                        break;
                    case "office":
                        LabelUploadType.Text = "*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx";
                        break;
                    case "sb":
                        LabelUploadType.Text = "*.sb;*.sb2";
                        break;
                    case "flash":
                        LabelUploadType.Text = "*.swf;*.fla";
                        break;
                    default:
                        LabelUploadType.Text = "*." + sWfiletype;
                        break;
                }
                if (model.Mgroup)
                {
                    LearnSite.BLL.GroupWork gbll = new LearnSite.BLL.GroupWork();
                    string gurl = gbll.DoneGroupWorkUrl(sSnum, Int32.Parse(Mid));
                    upFileTypeGroup.Visible = true;
                    upFileUrlGroup.Visible = true;
                    upFileUrlGroup.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(gurl, "ls");
                    LearnSite.BLL.Students sbll = new LearnSite.BLL.Students();
                    if (sbll.IsLeader(sSnum))//如果是组长则显示小组面板
                    {
                        Panelgroup.Visible = true;
                        showgroupWork();
                        if (gurl != "")
                        {
                            if (gbll.CheckGroupWork(sSnum, Int32.Parse(Mid)))
                            {
                                PanelGroupUp.Visible = false;
                                Labelgroupmsg.Text = "小组作品已经评价！<br/>你不可以再重新提交！";
                            }
                            else
                            {
                                Labelgroupmsg.Text = "小组作品已提交但未评价！<br/>你可以修改后重新提交！";
                            }
                        }
                        else
                        {
                            upFileTypeGroup.Visible = false;
                            upFileUrlGroup.Visible = false;
                        }
                    }
                    else
                    {
                        Panelgroup.Visible = false;
                        upFileTypeGroup.Visible = false;
                    }
                }
                else
                {
                    Panelgroup.Visible = false;
                    upFileTypeGroup.Visible = false;
                }

                if (LabelMfiletype.Text == "htm")
                {
                    Labelmsg.Text = "远程网站连接设置";
                    string ipstr = LearnSite.Common.Computer.GetServerIp();
                    string ftport = LearnSite.Common.XmlHelp.GetFtpPort();
                    string ftpstr = "ftp://" + ipstr;
                    TextFtp.Text = ftpstr;
                    string userstr = sSnum;
                    TextUser.Text = userstr;
                    string pwdstr = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Ftppwd"].ToString();
                    TextPwd.Text = pwdstr;
                    string MySyear = sSyear;
                    string MySclass = sSclass;
                    string siteurl = "~/FtpSpace/" + MySyear.ToString() + "/" + MySclass.ToString() + "/" + userstr;
                    string currenthome = GetHomePath();
                    if (currenthome != "")
                        siteurl = siteurl + "/" + currenthome;
                    HyperLinkSite.NavigateUrl = siteurl;
                    PanelFtp.Visible = true;
                    Panelworks.Visible = false;
                    if (ftport != "21")
                        ipstr = ipstr + ":" + ftport;
                    HLftpOpen.NavigateUrl = "ftp://" + userstr + ":" + pwdstr + "@" + ipstr;
                    HLftpOpen.ToolTip = HLftpOpen.ToolTip + "\n" + HLftpOpen.NavigateUrl;
                    LearnSite.BLL.Signin gbll = new LearnSite.BLL.Signin();
                    string baksite = siteurl;
                    showsitebackup(baksite);//显示网站备份日期及下载超链接
                }
                else
                {
                    if (!CkMupload.Checked)
                    {
                        Panelworks.Visible = false;
                    }
                }
                ImageType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
            }
            else
            {
                Mcontent.InnerHtml = "此学案活动不存在！";
                Panelworks.Visible = false;
                PanelFtp.Visible = false;
            }
        }

    }
    private bool isTeacher(string Wid, string Snum)
    {
        if (Wid != "" && Snum.StartsWith("s"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool isTeacher(string Snum)
    {
        if (Snum.StartsWith("s"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string myWmsort()
    {
       return  LearnSite.Common.WordProcess.MathNum(LabelMsort.Text).ToString();
    }
    private void ShowIpWorkDone()
    {
        string Sid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
        string Sname = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
        string Syear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();                
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        string Snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        string Sterm = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["ThisTerm"].ToString();
        string LoginTime = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginTime"].ToString();
        string Wip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
        string info= Syear + "|" + Sgrade + "|" + Sclass + "|" + Sid + "|" + Sname + "|" + Wip + "|" + Sterm + "|" + LoginTime;
        Labelinfo.Text = HttpUtility.UrlEncode(info);
        string Wcid = Request.QueryString["Cid"].ToString();
        string Wmid = Request.QueryString["Mid"].ToString();
        LearnSite.BLL.Works ws = new LearnSite.BLL.Works();
        string Wid = ws.WorkDone(Snum, Int32.Parse(Wcid), Int32.Parse(Wmid));//返回空字符表示不存在该记录
        string SnumDone = ws.IpWorkDoneSnum(Int32.Parse(Sgrade), Int32.Parse(Sclass), Int32.Parse(Wcid), Int32.Parse(Wmid), Wip);
        string retureUrl = ws.WorkUrl(Snum, Int32.Parse(Wmid));
        VoteLink.NavigateUrl = "~/Student/myevaluate.aspx?Mid=" + Wmid + "&Cid=" + Wcid;

        if (LearnSite.Common.XmlHelp.GetWorkIpLimit())//判断有无进行IP限制
        {
            if (Snum == SnumDone || isTeacher(Wid, Snum))
            {
                if (retureUrl != "")
                {
                    upFileUrl.Visible = true;
                    upFileType.Visible = true;
                    upFileType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
                    upFileUrl.Text = Server.UrlDecode(Sname);
                    upFileUrl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(retureUrl, "ls");
                }
            }
        }
        else
        {
            if (retureUrl != "")
            {
                upFileUrl.Visible = true;
                upFileType.Visible = true;
                upFileType.ImageUrl = "~/Images/FileType/" + LabelMfiletype.Text.ToLower() + ".gif";
                upFileUrl.Text = Server.UrlDecode(Sname);
                upFileUrl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(retureUrl,"ls");
            }
        }
        if (Wid != "")//判断有无作品提交
        {
            VoteLink.Visible = true;
            bool ischeck = ws.IsChecked(Int32.Parse(Wid));
            if (ischeck)//判断作品有无评价
            {
                Labelmsg.Text = "该作品已经评分!<br/>你不可以重新提交！";
                Panelswfupload.Visible = false;
                Panelshow.Visible = false;
            }
            else
            {
                if (LearnSite.Common.XmlHelp.GetWorkIpLimit())//判断有无进行IP限制
                {
                    if (Snum == SnumDone || isTeacher(Wid, Snum))
                    {
                        Labelmsg.Text = "你已经提交该活动作品.！<br/>你可以修改作品后重新提交！";
                        Panelswfupload.Visible = true;
                        Panelshow.Visible = true;
                    }
                    else
                    {
                        Panelswfupload.Visible = false;
                        if (LabelMfiletype.Text != "htm")
                            Labelmsg.Text = SnumDone + "学号<br/>已经在该IP提交本活动作品.！";
                    }
                }
                else
                {
                    Labelmsg.Text = "你已经提交该活动作品.！！<br/>你可以修改作品后重新提交！";
                    Panelswfupload.Visible = true;
                    Panelshow.Visible = true;
                }
            }
        }
        else
        {
            if (!isTeacher(Snum))
                VoteLink.Visible = false;
            LearnSite.BLL.Mission mbll = new LearnSite.BLL.Mission();
            int minMsort = mbll.GetLastMaxMsort(Int32.Parse(Wcid), Int32.Parse(LabelMsort.Text));//任务活动中查询
            bool isExitFirstWork = ws.ExistsMyFirstWork(Int32.Parse(Wcid), Snum, minMsort);

            if (LearnSite.Common.XmlHelp.GetWorkIpLimit())//判断有无进行IP限制
            {
                if (SnumDone == "")
                {
                    if (isExitFirstWork || minMsort == 0)//如果是上个任务已经提交或是第一个任务，则显示提交按钮
                    {
                        DateTime dt = DateTime.Now;
                        string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
                        Labelmsg.Text = today;
                        Panelswfupload.Visible = true;
                        Panelshow.Visible = true;
                    }
                    else
                    {
                        Labelmsg.Text = "请先提交前面活动作品！";
                    }
                }
                else
                {
                    Panelswfupload.Visible = false;
                    if (LabelMfiletype.Text != "htm")
                        Labelmsg.Text = SnumDone + "学号<br/>已经在该IP提交本活动作品！";
                }
            }
            else
            {
                if (isExitFirstWork || minMsort == 0)//如果是上个任务已经提交或是第一个任务，则显示提交按钮
                {
                    DateTime dt = DateTime.Now;
                    string today = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day;
                    Labelmsg.Text = today;
                    Panelswfupload.Visible = true;
                    Panelshow.Visible = true;
                }
                else
                {
                    Labelmsg.Text = "请先提交前面的活动作品！";
                }
            }
        }
    }

    protected void Btncheck_Click(object sender, EventArgs e)
    {
        if (LearnSite.Common.CookieHelp.IsStudentLogin())
        {
            string aaSid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
            string aaSname = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sname"].ToString();
            string aaSnum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
            string aaSyear = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Syear"].ToString();
            string aaclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
            string aagrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
            string aaterm = LearnSite.Common.XmlHelp.GetTerm();
            string aaWip = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginIp"].ToString();
            string aaLoginTime = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["LoginTime"].ToString();
            string aaWmid = Request.QueryString["Mid"].ToString(); 

                string mysiterarpath = "~/FtpSpace/" + aaSyear + "/" + aaclass + "/" + aaSnum;//空间根目录，备份用
                string aasitepath = mysiterarpath + "/";//学生作品记录网址用
                string currenthome = GetHomePath();
                if (currenthome != "")
                    aasitepath = aasitepath + currenthome + "/";

            int sitelength = LearnSite.Common.Htmlcheck.SiteUpdateCheck(aaSnum,aasitepath);
            if (sitelength > 0)
            {
                string aaWcid = LabelMcid.Text;
                string aaMfiletype = LabelMfiletype.Text;
                string aaWmsort =LabelMsort.Text;
                DateTime aaWdate = DateTime.Now;
                string aaiplast = aaWip.Substring(aaWip.LastIndexOf(".") + 1);
                LearnSite.BLL.Works aaws = new LearnSite.BLL.Works();
                string aaWid = aaws.WorkDone(aaSnum, Int32.Parse(aaWcid), Int32.Parse(aaWmid));//返回空字符表示不存在该记录
                string aaWtime = LearnSite.Common.Computer.TimePassed().ToString();
                if (aaWid == "")
                {
                    LearnSite.Model.Works wmodel = new LearnSite.Model.Works();
                    wmodel.Wnum = aaSnum;
                    wmodel.Wcid = Int32.Parse(aaWcid);
                    wmodel.Wmid = Int32.Parse(aaWmid);
                    wmodel.Wmsort = Int32.Parse(aaWmsort);
                    wmodel.Wfilename = Server.UrlDecode(aaSname) + "的网站";
                    wmodel.Wtype = aaMfiletype;
                    wmodel.Wurl = aasitepath;
                    wmodel.Wlength = sitelength;
                    wmodel.Wdate = aaWdate;
                    wmodel.Wip = aaWip;
                    wmodel.Wtime = aaWtime;
                    wmodel.Wcan = true;
                    wmodel.Wgrade = Int32.Parse(aagrade);
                    wmodel.Wterm = Int32.Parse(aaterm);
                    wmodel.Woffice = false;
                    wmodel.Wflash = false;
                    wmodel.Werror = false;
                    wmodel.Wegg = 3;
                    wmodel.Wsid = Int32.Parse(aaSid);
                    wmodel.Wclass = Int32.Parse(aaclass);
                    wmodel.Wname = HttpUtility.UrlDecode(aaSname);
                    wmodel.Wyear = Int32.Parse(aaSyear);
                    aaws.AddWorkUp(wmodel);//添加作品提交记录
                    LearnSite.BLL.Signin sn = new LearnSite.BLL.Signin();
                    sn.UpdateQwork(Int32.Parse(aaSid), Int32.Parse(aaWcid));//更新今天签到表中的作品数量
                    string bakpath = mysiterarpath ;
                    LearnSite.Store.Package.SiteToRar(bakpath,aaWcid);//备份当前的网站为rar
                    showsitebackup(bakpath);
                    string msg = "检验网站更新并成功备份！";
                    LabelSite.Text = msg;
                    LabelSite.ForeColor = Color.Green;
                    LearnSite.Common.WordProcess.Alert(msg, this.Page);
                    Session[ "backupsite"+aaSnum] = "1";
                }
                else
                {
                    LabelSite.Text = "已经检验过网站更新！";
                    if (Session["backupsite" + aaSnum] != null)
                    {
                        int backcount = Int32.Parse(Session["backupsite" + aaSnum].ToString());
                        if (backcount < 4)
                        {
                            string bakpath = mysiterarpath;
                            LearnSite.Store.Package.SiteToRar(bakpath,aaWcid);//备份当前的网站为rar
                            showsitebackup(bakpath);
                            backcount++;
                            Session["backupsite" + aaSnum] = backcount;
                            LabelSite.Text = "网站重新备份成功！";
                        }
                        else
                        {
                            LabelSite.Text = "备份次数不能超过3次！";
                        }
                    }
                    LabelSite.ForeColor = Color.Green;
                }
            }
            else
            {
                LabelSite.Text = "检验网站更新失败！";
                LabelSite.ForeColor = Color.Red;
            }
        }
    }

    protected void GVgwork_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkWurl");
            hl.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(hl.ToolTip,"ls");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E1E8E1',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //单击行改变行背景颜色 
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#D8E0D8'; this.style.color='buttontext';this.style.cursor='default';");
        }
    }
    protected void GVgwork_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Wid = e.CommandArgument.ToString();
        int Wlscore = 0;
        if (e.CommandName == "A")
        {
            Wlscore = 10;
            updatelscore(Wid, Wlscore);
        }
        if (e.CommandName == "P")
        {
            Wlscore = 6;
            updatelscore(Wid, Wlscore);
        }
        if (e.CommandName == "E")
        {
            Wlscore = 2;
            updatelscore(Wid, Wlscore);
        }
    }

    private void updatelscore(string Wid, int Wlscore)
    {
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        wbll.Updatelscore(Int32.Parse(Wid), Wlscore);
        System.Threading.Thread.Sleep(500);
        showgroupWork();
    }
    private void showgroupWork()
    {
        string aaSid = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sid"].ToString();
        string aaWmid = Request.QueryString["Mid"].ToString(); ;
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        GVgwork.DataSource = wbll.GetGroupWorks(Int32.Parse(aaSid), Int32.Parse(aaWmid));
        GVgwork.DataBind();
    }

    private void showsitebackup(string sitepather)
    {
        string Snum = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Snum"].ToString();
        LearnSite.BLL.Works wbll = new LearnSite.BLL.Works();
        string cid = Request.QueryString["Cid"].ToString();
        if (!wbll.ExistsWcid(Int32.Parse(cid)))
        {
            cid = wbll.GetHtmCid(Snum);//如果不存在本课网站备份记录，则取最近的一次
        }
        if (!string.IsNullOrEmpty(cid))
        {
            string sdate = LearnSite.Store.Package.SiteDate(sitepather, cid);
            if (sdate != "")
            {
                HLsiteBackup.Text = sdate + "网站备份";
                string surl = sitepather + cid + ".rar";
                HLsiteBackup.NavigateUrl = "~/Plugins/download.aspx?Id=" + LearnSite.Common.EnDeCode.Encrypt(surl,"ls");
            }
        }
    }
    /// <summary>
    /// 获取当前学生空间工作目录
    /// </summary>
    /// <returns></returns>
    private string GetHomePath()
    {
        string Sgrade = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sgrade"].ToString();
        string Sclass = Request.Cookies[LearnSite.Common.CookieHelp.stuCookieNname].Values["Sclass"].ToString();
        LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
        return tbll.GetHpathfroStu(Int32.Parse(Sgrade), Int32.Parse(Sclass));
    }
}