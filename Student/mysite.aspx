<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Stud.master"  StylesheetTheme="Student" AutoEventWireup="true" CodeFile="mysite.aspx.cs" Inherits="Student_mysite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cphs" Runat="Server">
    <div id="student">
<div class="left">
<div class="divcenter">
     <br />
     <asp:Image ID="ImageHome" runat="server" ImageUrl="~/Images/home.png" 
         BorderStyle="None" />
    <asp:HyperLink ID="HyperLinksite" runat="server"  ToolTip="^p^去我的网站空间踩踩！"
         Target="_blank" Height="18px" BorderStyle="None" BorderWidth="1px" 
         CssClass="txtszcenter" Width="70px" BackColor="#E1EDFF">hls</asp:HyperLink>
           &nbsp;空间已用：<asp:Label ID="LabelWquota" runat="server"  ></asp:Label>
                &nbsp;我的网站得票：<asp:Label ID="LabelWvote" runat="server" 
         Font-Bold="False" Font-Names="Arial"  ></asp:Label>
     <asp:Image ID="Imagegift" runat="server" ImageUrl="~/Images/gift.png" />
                &nbsp; 可投次数：<asp:Label ID="LabelWegg"  runat="server"  ></asp:Label>
                &nbsp;<asp:Label ID="Lbc" runat="server" BackColor="#C6E3B3" 
         Height="14px" ToolTip="网站更新指示颜色" Width="14px"></asp:Label>
                <br />
<asp:Label ID="Labelmsg" runat="server"  Height="16px" ForeColor="Red"></asp:Label>
     <br />
     <asp:DataList ID="DataListvote" runat="server" RepeatDirection="Horizontal" 
                    RepeatColumns="8" DataKeyField="Wid" 
                    OnItemCommand="DataListvote_ItemCommand" CellPadding="2" 
                     onitemdatabound="DataListvote_ItemDataBound" Width="98%">
                    <ItemTemplate>
                        <div  class="divvote"> 
                        <div class="divvotebg"><asp:HyperLink ID="Hypername" runat="server"  Text='<%# Eval("Sname") %>'  
                                Font-Underline="False" Height="18px"  ForeColor="Black"  ToolTip="瞧瞧我的网站！"   Target="_blank" 
                                NavigateUrl='<%# Eval("Wurl") %>' ></asp:HyperLink>
                                </div>
                            <asp:Label ID="WvoteLabel" runat="server" Text='<%# Eval("Wvote") %>' 
                                Height="16px" ></asp:Label>
                            <br />
                            <asp:LinkButton ID="Linkvote" runat="server" BackColor="#C4DBFF" BorderColor="#E0E0E0"
                                CommandArgument="uid" CommandName="vote" Font-Size="9pt" Font-Underline="False"
                                ForeColor="Black" Height="18px" Width="50px" ToolTip="请投我一票，谢谢！" 
                                CssClass="txtszcenter">投票</asp:LinkButton>
                            <br />
                            <asp:Label ID="LabelWscore" runat="server" Text='<%# Eval("Wscore") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LabelWupdate" runat="server" Text='<%# Eval("Wupdate") %>' Visible="false"></asp:Label>
                                </div>
                                <br />
                    </ItemTemplate>
      </asp:DataList>
     <br />
</div>
<br />        
</div>
<div class="right">
<div>    
    <asp:Image ID="Imageface" runat="server" Height="80px" Width="80px" />
    <div id="DivRank" class="divinfo" >
    <asp:Label ID="LabelRank" runat="server"></asp:Label>
    </div>
    </div> 
    <div class="divinfo">
    <div class="divinfo1">学号:</div>
    <div class="divinfo2"><asp:Label ID="snum" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">班级:</div>
    <div class="divinfo2"><asp:Label ID="sclass" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">姓名:</div>
    <div class="divinfo2"><asp:Label ID="sname" runat="server" ></asp:Label></div>
    </div>    
    <div class="divinfo">
    <div class="divinfo1">表现:</div>
    <div class="divinfo2">
        <asp:HyperLink ID="sattitude" runat="server" 
            NavigateUrl="~/Student/attituderank.aspx" Target="_blank" ToolTip="点击显示表现排行">[sattitude]</asp:HyperLink>
        </div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">学分:</div>
    <div class="divinfo2"><asp:Label ID="sscore" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">组长:</div>
    <div class="divinfo2"><asp:Label ID="sleadername" runat="server" ></asp:Label></div>
    </div>
    <div class="divinfo">
    <div class="divinfo1">MyIP:</div>
    <div class="divinfo2"><asp:Label ID="Labelip" runat="server"  SkinID="LabelSize8" ></asp:Label></div>
    </div> 
    <br />
    <br /> 
    <div>
    
    <asp:HyperLink ID="HyperLink1" runat="server"  Width="120px" SkinID="HyperLink" 
        Height="18px" NavigateUrl="~/Student/allsite.aspx" CssClass="txtszcenter" 
            Target="_self">全校同学网站浏览</asp:HyperLink>   
    </div>
    <br />
    <br />
    </div>
<br />
</div>
</asp:Content>

