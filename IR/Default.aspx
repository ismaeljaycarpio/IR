<%@ page title="Home" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="IR.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">

    <script src="jquery.bxslider/jquery.bxslider.min.js" type="text/javascript"></script>
    <link href="jquery.bxslider/jquery.bxslider.css" type="text/css" rel="stylesheet" />

    <%--<div class="row">
        <div class="col-md-12">
            <ul class="bxslider">
                <li>
                    <img src="jquery.bxslider/images/azalea-baguio.jpg" alt="Azalea Boracay" />
                </li>
                <li>
                    <img src="jquery.bxslider/images/azalea-boracay-logo.jpg" alt="Azalea Boracay" />
                </li>
            </ul>
        </div>
    </div>--%>

    <script>
        $(document).ready(function () {
            $('.bxslider').bxSlider({
                adaptiveHeight: true,
                slideWidth: 1000
            });
        });
    </script>

</asp:content>
