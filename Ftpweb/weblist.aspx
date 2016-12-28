<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/Manage.master"  StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="weblist.aspx.cs" Inherits="Manager_weblist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold"> 
        <div style="margin: auto; text-align: center" >        
        <asp:GridView ID="GVuser" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GVuser_PageIndexChanging"
            PageSize="20" Width="98%"  onrowdatabound="GVuser_RowDataBound" AllowPaging="True" SkinID="GVSliver">
            <Columns>
                <asp:BoundField />
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="User" HeaderText="Ftp账号"  />
                <asp:BoundField DataField="HomeDir" HeaderText="Ftp地址"  />
                <asp:BoundField DataField="Expiration" HeaderText="到期时间"  />
                <asp:BoundField DataField="QuotaMax" HeaderText="空间大小" />
                <asp:BoundField DataField="QuotaCurrent" HeaderText="空间使用" />
            </Columns>
            <PagerTemplate>
                <div  class="pagediv">
                    第<asp:Label ID="lblPageIndex" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>'></asp:Label>页
                    共<asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount  %>'></asp:Label>页
                    <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                        CommandName="Page" Font-Underline="False" ForeColor="Black" Text="首页"></asp:LinkButton>
                    <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                        CommandName="Page" Font-Underline="False" ForeColor="Black" Text="上一页"></asp:LinkButton>
                    <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                        CommandName="Page" Font-Underline="False" ForeColor="Black" Text="下一页"></asp:LinkButton>
                    <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                        CommandName="Page" Font-Underline="False" ForeColor="Black" Text="尾页"></asp:LinkButton>
                </div>
            </PagerTemplate>
        </asp:GridView>
    </div>    
    <br />
    <asp:Button ID="Buttonreturn" runat="server"  OnClick="Buttonreturn_Click" Text="返回"  SkinID="BtnNormal" />
    <br />
<br />
</div>
</asp:Content>

