<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="webshow.aspx.cs" Inherits="Ftpweb_webshow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="placehold"> 
        <div >        
        <div  class="chead">
&nbsp;
              <asp:DropDownList ID="DDLgrade" runat="server" Font-Size="9pt" Width="42px" 
                AutoPostBack="True" onselectedindexchanged="DDLgrade_SelectedIndexChanged"></asp:DropDownList>年级 &nbsp;
              <asp:DropDownList ID="DDLclass" runat="server" Font-Size="9pt" Width="42px" 
                AutoPostBack="True" onselectedindexchanged="DDLclass_SelectedIndexChanged"></asp:DropDownList>班级 &nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Labelnote" runat="server" Height="16px" 
                  Text="注意：网站评价方式为加分方式 A加10分 P加6分 E加2分" Width="300px"></asp:Label>
&nbsp;
              <asp:Button ID="ButtonSetP" runat="server"  OnClick="ButtonSetP_Click" Text="全评P"  SkinID="BtnSmall" Enabled="False" 
                  ToolTip="将未评的网站全评为P，全评后将不能单评" />&nbsp;&nbsp; 
            <asp:Label ID="Lbhomepath" runat="server" Visible="False"></asp:Label>
            <br />
        </div>
        <div class="centerdiv">
        <asp:GridView ID="GVuser" runat="server" AutoGenerateColumns="False"
            CellPadding="1" OnPageIndexChanging="GVuser_PageIndexChanging"
            PageSize="20" Width="100%" BorderStyle="Solid" 
                onrowdatabound="GVuser_RowDataBound" DataKeyNames="Wid"  SkinID="GridViewInfo"
                onrowcommand="GVuser_RowCommand" EnableModelValidation="True">
            <Columns>
<asp:BoundField HeaderText="序号"></asp:BoundField>
                <asp:TemplateField HeaderText="学号">
                    <ItemTemplate>
                        <asp:Label ID="LabelWnum" runat="server" Text='<%# Bind("Wnum") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Wpwd" HeaderText="密码" />
                <asp:BoundField DataField="Sname" HeaderText="姓名"  />
                <asp:BoundField DataField="Wvote" HeaderText="鲜花" />
                <asp:BoundField DataField="Wegg" HeaderText="金币" />
                <asp:BoundField DataField="WquotaCurrent" HeaderText="占用" />
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="LabelSyear" runat="server" Text='<%# Bind("Syear") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="LabelSclass" runat="server" Text='<%# Bind("Sclass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="网站">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkWurl" runat="server" NavigateUrl='<%# Eval("Wurl") %>' 
                            Target="_blank" Text="查看"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Wupdate" HeaderText="更新" />
                <asp:BoundField DataField="Wscore" HeaderText="积分" />
                <asp:TemplateField ShowHeader="False">
                <ControlStyle Width="16px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonA" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Wid") %>' 
                        CommandName="A" Text="A" ToolTip="加10分"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="评价标准">
                <ControlStyle Width="16px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonP" runat="server" CausesValidation="false" CommandName="P"
                        Text="P" CommandArgument='<%# Bind("Wid") %>' ToolTip="加6分"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ControlStyle Width="16px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonE" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Wid") %>'
                        CommandName="E" Text="E" ToolTip="加2分"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ControlStyle Width="16px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonX" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Wid") %>'
                        CommandName="X" Text="X" ToolTip="减10分"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Wcheck") %>' Text='<%# Bind("Wid") %>' ToolTip="可评或不可评状态"
                        Enabled="True" AutoPostBack="True" 
                        oncheckedchanged="CheckBox1_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>
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
    </div>    
<br />
</div>
</asp:Content>

