<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Student_Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Krous_Ex.Contact" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/fontawesome.min.css" />
    <link href="Assests/main/css/Contact.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">

     <div class="form-holder">
        <h1>Contact Us</h1>
        <div class="col-md-12">
            <div class="row">
                <div class=" faq-panel col-lg-6 col-12 text-center">
                    <i id="iconFAQ" class="fas fa-question fa-5x mb-lg"></i>
                    <h2 class="text-center mb-2" style="color: #6996F3">FAQs</h2>
                    <h3 class="text-center desc">Check our FAQs to find out more</h3>
                    <asp:Button ID="btnFAQ" runat="server" CssClass="btn-FAQ" Text="Enter FAQs Page" />
                </div>

                <div class="col-lg-6 col-12 text-center py-md-4 py-3">
                    <i id="iconChat" class="fa fa-comments fa-7x mb-lg"></i>
                    <h2 class="text-center text-uppercase mb-2" style="color: #6996F3">Chat</h2>
                    <h3 class="text-center desc">Come and chat with us now</h3>
                    <asp:Button ID="btnChat" runat="server" CssClass="btn-Chat" Text="Start Chatting" />
                </div>
            </div>

            <div><hr class="solid"></div>

            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-4">
                        <div class="dbox w-100 text-center">
                            <div class="icon d-flex align-items-center justify-content-center">
                                <span class="fas fa-phone-alt circle-icon"></span>
                                <div class="tel-details">
                                    <p><span>Telephone</span></p>
                                        +03 22849910
                                </div>
                            </div> 
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="dbox w-100 text-center">
                            <div class="icon d-flex align-items-center justify-content-center">
                                <span class="fas fa-fax circle-icon"></span>
                                <div class="fax-details">
                                    <p><span>Fax</span></p>
                                        +03 22849910
                                </div>
                            </div> 
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="dbox w-100 text-center">
                            <div class="icon d-flex align-items-center justify-content-center">
                                <span class="fas fa-paper-plane circle-icon"></span>
                                <div class="email-details">
                                    <p><span>Email</span></p>
                                        contact@krousEx.com
                                </div>
                            </div> 
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-8">
                <div class="contact-wrap">
                    <h3>Keep in touch with us</h3>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-grp">
                                <div class="form-holder">
                                    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" placeholder="Full name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-grp">
                                <div class="form-holder">
                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-grp">
                                <div class="form-holder">
                                    <asp:TextBox ID="txtSubject" CssClass="form-control" runat="server" placeholder="Subject"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-grp">
                                <div class="form-holder">
                                    <textarea name="message" class="form-control" id="message" cols="30" rows="8" placeholder="Message"></textarea>
                                </div>
                            </div>
                            <div class="form-grp">
                                <div class="form-holder">
                                     <asp:Button ID="btnSubmit" runat="server" type="submit" CssClass="submit-btn" Text="Submit"></asp:Button>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

       
</asp:Content>
