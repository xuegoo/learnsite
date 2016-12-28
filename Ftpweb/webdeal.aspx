<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="webdeal.aspx.cs" Inherits="Manager_webdeal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold" >
    <div  class="webdeal">
        <div style="border: 1px solid #EEEEEE; height: 18px; background-color: #EEEEEE;  margin: auto;">
                学生网页制作空间生成</div>         
             <br />
        <br />
             <div  class="webdealnote">
                 &nbsp;&nbsp;&nbsp; 学生空间是根据入学年度、学号二项分别在空间根目录<br />
                 <br />
                 下生成分级目录，使得学生升班后（如从初一升到初二）都<br />
                 <br />
                 不相互影响。所以新生导入数据中一定要有入学年度、学号<br />
                 <br />
                 二项数据。<br />
                 <br />
             &nbsp;&nbsp;&nbsp; 生成前请特别注意是否已经创建了ftp数据库，并在<br />
                 <br />
                 web.config中修改正确连接字符串中数据库名称、账号、和<br />
                 <br />
                 密码，并配置好Serv_U服务。<br />
             </div>

        <br />
        <div class="divcenter">
        <br />
        空间大小<asp:DropDownList ID="DDLspace" runat="server" Font-Size="9pt" Width="60px">
            <asp:ListItem Value="30">30MB</asp:ListItem>
            <asp:ListItem Value="50" Selected="True">50MB</asp:ListItem>
            <asp:ListItem Value="100">100MB</asp:ListItem>
            <asp:ListItem Value="300">300MB</asp:ListItem>
            <asp:ListItem Value="500">500MB</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <br />
        <asp:Button ID="ButtonFtpuser" runat="server" Text="创建Ftp账号"  SkinID="BtnLong" 
            onclick="ButtonFtpuser_Click" ToolTip="每秒创建20个用户左右，请自己估算等待时间！" />
        <br />
        <br />
        <br />
        <asp:Button ID="ButtonMakeDir" runat="server" Text="生成Ftp目录"  SkinID="BtnLong" onclick="ButtonMakeDir_Click" />
        <br />
        <br />
        <br />
        <asp:Button ID="Buttonview" runat="server" Text="浏览Ftp用户" SkinID="BtnLong" onclick="Buttonview_Click"  />
        <br />
        </div>
        <br />
        <div id="Loading" style=" display:none ;text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; color: #FF0000;">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/load2.gif" />
            <input id="Textcmd" style="border-style: none" type="text" /></div>        
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <br />
       </div>
    <br />
</div>
</asp:Content>

