<%@ Page Language="C#" MasterPageFile="~/Student/Scm.master" AutoEventWireup="true" CodeFile="showactive.aspx.cs"  StylesheetTheme="Student"  Inherits="Student_showactive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
    <div  id="showcontent">
<div class="left" style="width: 800px">
<br />
    <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label><br />
   </div>
   <div class="courseother">
    <asp:Label ID="LabelSnum"  runat="server" Visible="False"></asp:Label>
			<asp:CheckBox ID="CkMupload" runat="server" Enabled="false" Visible="False" />
            <asp:CheckBox ID="CkMgroup" runat="server" Enabled="false"  Visible="False" />
            <asp:Label ID="LabelMid" runat="server" Visible="False"></asp:Label>            
            <asp:Label ID="LabelUploadType" runat="server" Visible="False"></asp:Label>
			<asp:Label ID="LabelMcid" runat="server" Visible="False"></asp:Label>
       <asp:Label ID="LabelMsort"  runat="server" Visible="False" ></asp:Label>
       <asp:Label ID="Labelinfo"  runat="server" Visible="False" ></asp:Label>
   </div>   
    <link href="../kindeditor/plugins/syntaxhighlighter/styles/shCore.css" rel="stylesheet" type="text/css" />
    <link href="../kindeditor/plugins/syntaxhighlighter/styles/shThemeRDark.css" rel="stylesheet"   type="text/css" />
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shCore.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushCss.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushJScript.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushVb.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushCSharp.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushCpp.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushPython.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushPhp.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushXml.js" type="text/javascript"></script>
    <script  type="text/javascript">        SyntaxHighlighter.all();  </script>
<div   id="Mcontent"  class="coursecontent" runat="server">	
		</div>
		<br />
		<br />
</div>
<div class="right"><br />
<center>
        <script src="../Swfupload/swfupload.js" type="text/javascript"></script>
        <script type="text/javascript">
            var swfu, twoswf;
            window.onload = function () {
                swfu = new SWFUpload({
                    // Backend Settings
                    upload_url: "uploadwork.aspx?mid=<%=LabelMid.Text %>&num=<%=LabelSnum.Text %>&info=<%=Labelinfo.Text %>",

                    // File Upload Settings
                    file_size_limit: "200 MB",
                    file_types: "<%=LabelUploadType.Text %>",
                    file_types_description: "作品文件类型",
                    file_upload_limit: '1',
                    file_queue_limit: '1',
                    // Event Handler Settings
                    file_dialog_complete_handler: function (numFilesSelected, numFilesQueued) { if (numFilesQueued === 1) this.startUpload(); },
                    upload_success_handler: function (file, responseText) {
                        //alert("作品提交成功！作品名称为：" + file.name + "提交之后的作品名称为：" + responseText);
                        alert("作品提交成功！");
                        location.reload();
                    },

                    // Button settings
                    button_image_url: "../swfupload/image/100x22gray.png",
                    button_placeholder_id: "spanButtonPlaceholder",
                    button_width: 100,
                    button_height: 22,
                    button_text: '<span class="button">提交作品</span>',
                    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt;text-align: center; width:100px;} ',
                    button_text_top_padding: 1,
                    button_text_left_padding: 5,

                    // Flash Settings
                    flash_url: "../swfupload/swfupload.swf", // Relative to this file

                    // Debug Settings
                    debug: false
                });
                twoswf = new SWFUpload({
                    // Backend Settings
                    upload_url: "uploadgroup.aspx?mid=<%=LabelMid.Text %>&num=<%=LabelSnum.Text %>&info=<%=Labelinfo.Text %>",

                    // File Upload Settings
                    file_size_limit: "200 MB",
                    file_types: "<%=LabelUploadType.Text %>",
                    file_types_description: "小组作品文件类型",

                    file_upload_limit: '1',
                    file_queue_limit: '1',

                    // Event Handler Settings
                    file_dialog_complete_handler: function (numFilesSelected, numFilesQueued) { if (numFilesQueued === 1) this.startUpload(); },
                    upload_success_handler: function (file, responseText) {
                        //alert("小组作品提交成功！作品名称为：" + file.name + "提交之后的作品名称为：" + responseText);
                        alert("小组作品提交成功！");
                        location.reload();
                    },

                    // Button settings
                    button_image_url: "../swfupload/image/100x22pink.png",
                    button_placeholder_id: "spanButtonPlaceholderTwo",
                    button_width: 100,
                    button_height: 22,
                    button_text: '<span class="button">小组合作</span>',
                    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt;text-align: center; width:100px;} ',
                    button_text_top_padding: 1,
                    button_text_left_padding: 5,

                    // Flash Settings
                    flash_url: "../swfupload/swfupload.swf", // Relative to this file

                    // Debug Settings
                    debug: false
                });
            }
	</script>
       <div ><br /><br /></div><br />
        <input type="button" class="sharedisk" id="share" value="我的网盘" onclick="showShare()" />
        <br />
        <br />
            <asp:HyperLink  ID ="VoteLink" runat="server" Target="_blank" 
                CssClass="txtszcenter" SkinID="HyperLinkPink" BackColor="#66A7FF" BorderStyle="None" 
                ToolTip="可以查看别人的作品!">作品互评</asp:HyperLink>        
        <br />
     <asp:Panel ID="Panelworks" runat="server" >
        <div>
        <br />
            <asp:Image runat="server" ID="upFileType" Visible="False" />
            <asp:HyperLink ID="upFileUrl" runat="server" Height="16px" Visible="False" 
                Target="_blank">[upFileUrl]</asp:HyperLink>
        <br /> 
        <br />                
               <br />
            <asp:Panel ID="Panelswfupload" runat="server">
            
            <div id="swfu_container" style="margin: 0px 10px;">
		    <div style="text-align: center; margin: auto">
				<span id="spanButtonPlaceholder"></span>
		    </div>
		    <div id="divFileProgressContainer" ></div>
		    <div id="divSingleFileProgress"></div>
	    </div>
            </asp:Panel>
            <br />
            <asp:Image ID="ImageType" runat="server" />
            <asp:Label ID="LabelMfiletype" runat="server"></asp:Label>
            格式<br />
    <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
            <br />
    <br />
    </div>       
    </asp:Panel>
    <br />
    <asp:Panel ID="PanelFtp" runat="server" CssClass="panelFtpcss" Visible="False">
        <div class="divcenter">
            &nbsp;&nbsp;&nbsp;<br /> <asp:Image ID="ImageHome" runat="server" ImageUrl="~/Images/home.png" />
            <asp:HyperLink ID="HyperLinkSite" runat="server" Font-Bold="True" 
                Font-Size="10pt" Font-Underline="True" Target="_blank" ToolTip="我的站点当前位置首页">我的站点</asp:HyperLink>
            &nbsp;<asp:HyperLink ID="HLftpOpen" runat="server" BorderWidth="0px" 
                Font-Underline="False" ImageUrl="~/Images/ftp.png" Target="_blank" 
                ToolTip="提示：在浏览器中打开远程网站根目录"></asp:HyperLink>
            <br />
            <br />
            <asp:HyperLink ID="HLsiteBackup" runat="server" ForeColor="Blue" 
                Target="_blank">下载网站备份</asp:HyperLink>
        </div>
        <br />
        <asp:Panel ID="Panelshow" runat="server" Visible="true">
            &nbsp;远程网站位置：<br />
        &nbsp;<asp:TextBox ID="TextFtp" runat="server" ReadOnly="True" SkinID="TextBoxFtp" 
            Width="110px"></asp:TextBox>
        <input id="ButtonFtp" class="btncopy" onclick="jsCopy('ctl00_Cpcm_TextFtp')" 
            type="button" value="复制" /><br />
         &nbsp;<br />
            &nbsp;用户名：<br />
        &nbsp;<asp:TextBox ID="TextUser" runat="server" ReadOnly="True" 
            SkinID="TextBoxFtp" Width="110px" ></asp:TextBox>
        <input id="ButtonUser" type="button" value="复制" class="btncopy" onclick="jsCopy('ctl00_Cpcm_TextUser')" /><br />
         &nbsp;<br />
            &nbsp;密码：<br />
        &nbsp;<asp:TextBox ID="TextPwd" runat="server" ReadOnly="True" 
            SkinID="TextBoxFtp" Width="110px" ForeColor="White"  ></asp:TextBox>
        <input id="ButtonPwd" type="button" value="复制" class="btncopy" onclick="jsCopy('ctl00_Cpcm_TextPwd')" /><br />
        <br />
        <div class="divcenter">            
            <asp:Button ID="Btncheck" runat="server" 
                onclick="Btncheck_Click" Text="网站备份" ToolTip="检验网站更新状态并备份，仅备份一次" Width="80px" 
                SkinID="buttonSkinPink" />
            <br />
            <br />
            <asp:Label ID="LabelSite" runat="server" Text="〖网站发布后一定要备份网站〗"></asp:Label>
            <br />
        </div>        
        </asp:Panel>
        <br />
    </asp:Panel>
    <br />
    <asp:Panel ID="Panelgroup" runat="server">
        <br />         
        <br />
        <asp:GridView ID="GVgwork" runat="server" 
            AutoGenerateColumns="False" CellPadding="3" DataKeyNames="Wid" 
            EnableModelValidation="True" 
            OnRowCommand="GVgwork_RowCommand" 
            onrowdatabound="GVgwork_RowDataBound" PageSize="15" SkinID="GridViewInfo" 
            Width="98%" Caption="小组合作面板">
            <Columns>
                <asp:TemplateField HeaderText="作品">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkWurl" runat="server" Target="_blank" Text='<%# Eval("Sname") %>' 
                            ToolTip='<%# Eval("Wurl") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ControlStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Wlscore") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ShowHeader="False">
                    <ControlStyle Width="16px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonA" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("Wid") %>' CommandName="A" Text="A"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="评价" ShowHeader="False">
                    <ControlStyle Width="16px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonP" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("Wid") %>' CommandName="P" Text="P"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ControlStyle Width="16px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonE" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("Wid") %>' CommandName="E" Text="E"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>                
            </Columns>
            
        </asp:GridView>
        <br />
        <asp:Image ID="upFileTypeGroup" runat="server" Visible="False" />
        <asp:HyperLink ID="upFileUrlGroup" runat="server" Height="16px" Target="_blank" 
            Visible="False">[upFileUrlGroup]</asp:HyperLink>
        <br /><br />
         <asp:Panel ID="PanelGroupUp" runat="server">
        <div id="swfu_containerTwo" style="margin: 0px 10px;">
		    <div style="text-align: center; margin: auto">
				<span id="spanButtonPlaceholderTwo"></span>
		    </div>
		    <div id="divFileProgressContainerTwo" ></div>
		    <div id="divSingleFileProgressTwo"></div>
	    </div>
        </asp:Panel>
        <br />
       <asp:Label ID="Labelgroupmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
    <br /> 
    </asp:Panel>
    <br />
    <br />  
    </center>
</div>   
    <br />
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function jsCopy(contentid) {
            var e = document.getElementById(contentid); //对象是content 
            e.select(); //选择对象 
            document.execCommand("Copy"); //执行浏览器复制命令 
        }
        function showShare() {
            var urlat = "../Student/groupshare.aspx";
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 600, height: 400, fixed: false, maskopacity: 60, close:true })
        }   
    </script>    
</div>
</asp:Content>