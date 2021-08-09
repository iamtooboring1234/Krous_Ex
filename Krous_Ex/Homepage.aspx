<%@ Page Title="Homepage" Language="C#" MasterPageFile="~/Student_Site.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="Krous_Ex.Homepage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="Assests/main/css/fullpage.css" rel="stylesheet" />
    <script src="Scripts/fullpage.js"></script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">

    <div style="scroll-behavior: smooth;">
        <div class="page-cover">
            <div class="cover-bg-mask" style="background-color: rgba(26,26,26,0.3)"></div>
        </div>
        <div id="fullpage">
            <div class="section">
                <div class="section-home-cover">
                    <div class="section-center fullscreen-md">
                        <div class="section-wrapper-with-margin">
                            <div class="section-content">
                                <div class="justify-content-center">
                                    <header class="text-center text-white py-5" style="cursor:default">
                                        <h1 class="display-4 font-weight-bold mb-4"><span style="opacity:0.65;">LMS</span> <span>Krous-Ex</span></h1>
                                        <p class="lead mb-0">An Learning Management System.</p>
                                    </header>
                                </div>
                            </div>
                        </div>
                    </div>

                    <footer class="section-footer scrolldown">
                        <a class="down" style="cursor:pointer" href="#scroll">
                            <span class="txt">Scroll</span>
                            <div class="vl"></div>
                        </a>
                    </footer>
                </div>
            </div>

            <div class="section" >
                <div class="section-home-cover">
                    <div class="section-center fullscreen-md" id="scroll">
                        <div class="section-wrapper-with-margin">
                            <div class="section-content">
                                <div class="justify-content-center">
                                    <header class="text-center text-white py-5" style="cursor:default">
                                        <h1 class="display-4 font-weight-bold mb-4"><span style="opacity:0.65;">LMS</span> <span>Krous-Ex</span></h1>
                                        <p class="lead mb-0">An Learning Management System.</p>
                                    </header>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="section">
                <div class="section-home-cover">
                    <div class="section-center fullscreen-md" id="scroll">
                        <div class="section-wrapper-with-margin">
                            <div class="section-content">
                                <div class="justify-content-center">
                                    <header class="text-center text-white py-5" style="cursor:default">
                                        <h1 class="display-4 font-weight-bold mb-4"><span style="opacity:0.65;">LMS</span> <span>Krous-Ex</span></h1>
                                        <p class="lead mb-0">An Learning Management System.</p>
                                    </header>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        new fullpage('#fullpage', {
            autoScrolling: true,
            sectionsColor: ['', 'rgba(0,0,0,0.6)', '#7BAABE', 'whitesmoke', '#ccddff'],

            onLeave: function (origin, destination, direction) {
            var leavingSection = this;

                //after leaving section 2
                if (origin.index == 1 && direction == 'down') {
                }

                else if (origin.index == 0 && direction == 'down') {
                    $('.page-cover').css({
                        '-webkit - filter': 'blur(5px)',
                        'filter': 'blur(5px)',
                        'transition': '2s - webkit - filter linear'
                    });
                }

                else if (origin.index == 1 && direction == 'up'){
                    $('.page-cover').css({
                        '-webkit - filter': 'blur(0)',
                        'filter': 'blur(0)'
                    });
                }
            }
        })
    </script>

</asp:Content>