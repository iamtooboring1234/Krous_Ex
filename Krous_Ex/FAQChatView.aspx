<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.Master" AutoEventWireup="true" CodeBehind="FAQChatView.aspx.cs" Inherits="Krous_Ex.FAQChatView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Assests/main/css/inquiry.css" rel="stylesheet" />

        <script type="text/javascript">

            function enlargeImage(src) {
                var modal = document.getElementById("imgModal");
                var modalImg = document.getElementById("enlargeImg");
                modal.style.display = "block";
                modalImg.src = src;
            }

            function CloseModal() {
                var modal = document.getElementById("imgModal");
                modal.style.display = "none";
            }

            function EnterChatComfirmation() {
                if ($('#<%=hdCheckChatStatus.ClientID%>').val() == "Pending") {
                    if (confirm('Are you sure to accept the chat ? \nOnce you accept the chat, you will take charge of this chat')) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }

        </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="card">
        <div class="card-body">
            <div class="col-lg-12">

                <div class="card-header">
                    <h4 class="card-title"><strong>Chat Room</strong></h4>
                    <asp:button runat="server" CssClass="btn btn-sm btn-secondary" id="EnterChatBtn" OnClientClick="return EnterChatComfirmation();" Text="Enter Chat" OnClick="EnterChatBtn_Click"/>

                </div>

                <div class="ps-container ps-theme-default ps-active-y" id="chat-content" style="overflow-y: scroll !important; height: 520px !important;">
                    <div id="chat-content-Message">
                        <asp:Literal runat="server" ID="litMessage"></asp:Literal>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div id="imgModal" onclick="CloseModal()">
        <img class="modal-content" id="enlargeImg" src="" />
    </div>

    <asp:HiddenField runat="server" ID="hdCheckChatStatus" />

    <script>
        var myDiv = document.getElementById("chat-content");
        myDiv.scrollTop = myDiv.scrollHeight;
    </script>

</asp:Content>
