﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" Validaterequest="false" AutoEventWireup="true" CodeFile="missionedit.aspx.cs" Inherits="Teacher_missionedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="cplace">
    <div class="cleft">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 活动名称：<asp:TextBox ID="Texttitle" runat="server" SkinID="TextBoxNormal" 
            Width="200px" ></asp:TextBox>
        作品类型<asp:DropDownList ID="DDLmfiletype" runat="server" Font-Size="8pt"
            Width="60px" Font-Names="Arial">
        </asp:DropDownList>
        <asp:CheckBox ID="CheckUpload" runat="server" Text="是否提交" Checked="True" Font-Size="9pt" />
        &nbsp;<asp:CheckBox ID="CheckPublish" runat="server" Text="是否发布" 
            Checked="True" />
        <asp:CheckBox ID="CheckGroup" runat="server" Text="小组合作" />
        </div>
    <div >
        <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?Cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?Cid='+cid+'&Ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="ctl00$Content$mcontent"]', {
		            resizeType: 1,
		            newlineTag: "br", 
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false,
					afterCreate : function() {
						this.loadPlugin('autoheight');
					}		            
		        });
		    });
		</script>
    <textarea  id ="mcontent" runat ="server" style="width: 830px; height:550px;" ></textarea>  
    </div>
     <div class="placehold">
               <asp:Label ID="Labelmsg" runat="server" Width="300px"></asp:Label>
               <br />
               选择自定义评价标准：<asp:DropDownList ID="DDLMgid" runat="server" Font-Size="9pt"
            Width="120px" Font-Names="Arial">
        </asp:DropDownList>
               <br />
         <br />
              <asp:Button ID="Btnedit" runat="server"  Text="修改活动" OnClick="Btnedit_Click" SkinID="BtnNormal" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnCourse" runat="server" Text="学案返回" OnClick="BtnCourse_Click" SkinID="BtnNormal" />
              <br />
         <br />
         </div>           
        </div>
</asp:Content>

