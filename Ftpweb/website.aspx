<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="website.aspx.cs" Inherits="Ftpweb_website" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="placehold"> 
        <br />       
        <div  class="website">
        <div  class="phead"> 学生网站处理</div>
            <div  class="websitediv">
                <br />
                <asp:Button ID="Buttonspace" runat="server"    Text="获取Ftp学生空间占用"  
                    SkinID="BtnLong" onclick="Buttonspace_Click" ToolTip="从Ftp数据库获取当前空间占用情况" />
                <br />
                <br />
            </div>
             <div class="websitediv">
                 <br />
                 密码长度<asp:DropDownList ID="DDLlen" runat="server" Font-Size="9pt">
                     <asp:ListItem>2</asp:ListItem>
                     <asp:ListItem>3</asp:ListItem>
                     <asp:ListItem>5</asp:ListItem>
                     <asp:ListItem>6</asp:ListItem>
                     <asp:ListItem>8</asp:ListItem>
                     <asp:ListItem>12</asp:ListItem>
                     <asp:ListItem Selected="True">16</asp:ListItem>
                 </asp:DropDownList>
                 密码类型<asp:DropDownList ID="DDLmethod" runat="server" Font-Size="9pt">
                     <asp:ListItem Value="0">单纯数字</asp:ListItem>
                     <asp:ListItem Value="1">单纯字母</asp:ListItem>
                     <asp:ListItem Selected="True" Value="2">数字字母</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;
                 <asp:Button ID="ButtonRadom" runat="server" Text="设置Ftp随机化密码"   
                     SkinID="BtnLong"  onclick="ButtonRadom_Click" 
                     ToolTip="设置全校所有ftp用户随机化密码！" />
                 <br />
                 <br />
                 <asp:Button ID="ButtonStuPwd" runat="server" Text="设置Ftp密码与学生个人登录密码一致"   
                     SkinID="BtnLong"  onclick="ButtonStuPwd_Click" 
                     ToolTip="设置全校所有ftp用户与登录密码同步" Width="300px" />
                 <br />
            </div>
             <div class="websitediv">
                 <br />
                 选择可投票数：<asp:DropDownList ID="DDLWvote" runat="server" 
                     Font-Size="9pt" Width="50px">
                     <asp:ListItem>0</asp:ListItem>
                     <asp:ListItem>3</asp:ListItem>
                     <asp:ListItem>5</asp:ListItem>
                     <asp:ListItem>8</asp:ListItem>
                     <asp:ListItem Selected="True">12</asp:ListItem>
                     <asp:ListItem>16</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;
                <asp:Button ID="ButtonVote" runat="server"  Text="上课班级网站投票重置"  SkinID="BtnLong"  onclick="ButtonVote_Click" 
                     ToolTip="每次上网页制作课都有一次投票机会，只限制重置所教的当前上课班级，不影响其他教师班级" 
                     style="margin-bottom: 0px" />
                 <br />
                 <br />
                <br />
            </div>
           <div class="websitediv">
                 <br />
                 检测网页：<asp:DropDownList ID="DDLhtmlname" runat="server" Font-Size="9pt">
                     <asp:ListItem>index.htm</asp:ListItem>
                     <asp:ListItem>1.htm</asp:ListItem>
                     <asp:ListItem>2.htm</asp:ListItem>
                     <asp:ListItem>3.htm</asp:ListItem>
                     <asp:ListItem>4.htm</asp:ListItem>
                     <asp:ListItem>5.htm</asp:ListItem>
                     <asp:ListItem>6.htm</asp:ListItem>
                     <asp:ListItem>7.htm</asp:ListItem>
                     <asp:ListItem>8.htm</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;
                <asp:Button ID="ButtonCheck" runat="server"  Text="检测学生网站更新"  SkinID="BtnLong"  onclick="ButtonCheck_Click" 
                     ToolTip="对所教的当前班级进行学生网站更新情况检测，并将更新日期记录到数据库作为评价时参考方便" />
                 <br />
                 <br />
                <br />
            </div>
            <div  class="websitediv" >
                <br />
                <asp:Button ID="Btnsite" runat="server"   Text="学生网站浏览及评价"   SkinID="BtnLong"  onclick="Btnsite_Click" />
                &nbsp;
                <asp:Button ID="BtnReBuild" runat="server"   SkinID="BtnLong"  
            onclick="BtnReBuild_Click" ToolTip="备用：当有学生变动时使用，重新更新所教班级学生网站地址" 
            Text="更新学生网站地址" />
                <br />
                <br />
            </div>   
            <div  class="websitediv" >
        <br />
                <asp:Button ID="BtnTeacher" runat="server"   SkinID="BtnLong"  
            onclick="BtnTeacher_Click" ToolTip="创建所教班级模拟学生角色时制作网页的各班Ftp空间账号" 
            Text="创建模拟学生Ftp空间" />
                <br />
        <br />
            </div>  
        <div  class="websitediv" >
                <br />
                设定学生空间当前首页位置在<asp:TextBox ID="TextBoxHome" runat="server" Width="80px"></asp:TextBox>
                目录下
&nbsp;<asp:Button ID="BtnEditHome" runat="server"   SkinID="BtnSmall"  
            onclick="BtnEditHome_Click" 
            Text="修改" />
                <br />
&nbsp;<br />
                <span style="color: #008080">说明：留空 表示在根目录，否则即设定为学生空间当前首页位置，如填web<br />
                <br />
                功能：设置此位置名称后，学生网站浏览及教师评价时，网址即为当前位置<br />
                </span><br />
                </div>          
        </div>

           <div id="Loading" style=" display:none ;text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; color: #FF0000;">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/load2.gif" />
            <input id="Textcmd" style="border-style: none" type="text" /></div>
                <br />
            <asp:Label ID="Labelmsg" runat="server" Font-Names="Arial" Font-Size="9pt" 
                ForeColor="Red" Height="18px"></asp:Label>
            <br />
            <br />
    </div>
</asp:Content>

