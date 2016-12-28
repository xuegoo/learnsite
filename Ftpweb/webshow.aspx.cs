using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ftpweb_webshow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        LearnSite.Common.CookieHelp.JudgeTeacherCookies();
        Master.Page.Title = LearnSite.Common.CookieHelp.SetMainPageTitle() + "学生空间查询及评价页面";
        if (!IsPostBack)
        {
            GetHomePath();
            GradeClass();
            ShowWeb();
        }
    }
    protected void Buttonview_Click(object sender, EventArgs e)
    {
        GVuser.PageIndex = 0;
        ShowWeb();
        ButtonSetP.Enabled = true;
    }
    private void ShowWeb()
    {
        string Sgrade = DDLgrade.SelectedValue.ToString();
        string Sclass= DDLclass.SelectedValue.ToString();
        if (Sgrade != "" && Sclass != "")
        {
            LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
            GVuser.DataSource= ws.GetListWeb(Int32.Parse( Sgrade),Int32.Parse( Sclass));
            GVuser.AllowPaging = true;
            GVuser.DataBind();
        }
    }
    private void GradeClass()
    {
        LearnSite.BLL.Room room = new LearnSite.BLL.Room();
        DDLgrade.DataSource = room.GetGrade();
        DDLgrade.DataTextField = "Rgrade";
        DDLgrade.DataValueField = "Rgrade";
        DDLgrade.DataBind();

        DDLclass.DataSource = room.GetClass();
        DDLclass.DataTextField = "Rclass";
        DDLclass.DataValueField = "Rclass";
        DDLclass.DataBind();
    }


    protected void GVuser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;

            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {

                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        Session["SoftPageIndex"] = newPageIndex;
        ShowWeb();

    }
    protected void GVuser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(GVuser.PageIndex * GVuser.PageSize + e.Row.RowIndex + 1);
            string homepath = Lbhomepath.Text;
            if (homepath != "")
            {
                HyperLink hlk = (HyperLink)e.Row.FindControl("HyperLinkWurl");
                string curl = hlk.NavigateUrl;
                hlk.NavigateUrl = curl + "/" + homepath;//替换为当前学生空间工作目录
            }
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
    protected void GVuser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Wid = e.CommandArgument.ToString();

        if (e.CommandName == "A")
        {
            int Wscore = 10;
            updatescore(Wid, Wscore);

        }
        if (e.CommandName == "P")
        {
            int Wscore = 6;
            updatescore(Wid, Wscore);
        }
        if (e.CommandName == "E")
        {
            int Wscore = 2;
            updatescore(Wid, Wscore);
        }
        if (e.CommandName == "X")
        {
            int Wscore = -10;
            updatescore(Wid, Wscore);
        }
    }

    private void updatescore(string Wid, int Wscore)
    {
        LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
        ws.UpdateOne(Int32.Parse(Wid), Wscore);
        System.Threading.Thread.Sleep(500);
        ShowWeb();
        GVuser.DataBind();
    }
    protected void ButtonSetP_Click(object sender, EventArgs e)
    {
        int Sgrade =Int32.Parse( DDLgrade.SelectedValue.ToString());
        int Sclass =Int32.Parse( DDLclass.SelectedValue.ToString());
        LearnSite.BLL.Webstudy ws = new LearnSite.BLL.Webstudy();
        ws.UpdateOneClass(Sgrade, Sclass);
        System.Threading.Thread.Sleep(1000);
        ShowWeb();
        GVuser.DataBind();
    }
    protected void DDLgrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLgrade.SelectedItem != null)
        {
            int Rgrade = Int32.Parse(DDLgrade.SelectedValue);
            LearnSite.BLL.Room rm = new LearnSite.BLL.Room();
            DDLclass.DataSource = rm.GetLimitClass(Rgrade);
            DDLclass.DataBind();
            GVuser.PageIndex = 0;
            ShowWeb();
            ButtonSetP.Enabled = true;
        }
    }
    protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVuser.PageIndex = 0;
        ShowWeb();
        ButtonSetP.Enabled = true;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
       //bool ch=((CheckBox)sender).Checked;
        //取得当前被选中项的索引  
        string fwid = ((CheckBox)sender).Text;
        //取得当前选中项中的某个值，最好找ID。
        LearnSite.BLL.Webstudy wbll = new LearnSite.BLL.Webstudy();
        wbll.UpdateWcheck(fwid);
    }
    /// <summary>
    /// 获取当前学生空间工作目录
    /// </summary>
    /// <returns></returns>
    private void GetHomePath()
    {
        if (Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname] != null)
        {
            string Hid = Request.Cookies[LearnSite.Common.CookieHelp.teaCookieNname].Values["Hid"].ToString();
            LearnSite.BLL.Teacher tbll = new LearnSite.BLL.Teacher();
            Lbhomepath.Text = tbll.GetHpath(Hid);
        }
    }
}
