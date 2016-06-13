<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IR.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="jquery.bxslider/jquery.bxslider.js" type="text/javascript"></script>
    <link href="jquery.bxslider/jquery.bxslider.css" type="text/css" rel="stylesheet" />

    <div class="row">
        <div class="col-md-12">
            <ul class="bxslider">
                <li>
                    <%--<img src="jquery.bxslider/images/azalea.jpg" />--%>
                    <%--<img src="jquery.bxslider/images/azalea-boracay-logo.jpg" alt="Azalea Boracay" />--%>
                </li>
            </ul>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('.bxslider').bxSlider({
                adaptiveHeight: true,
                slideWidth: 1000
            });
        });
    </script>

</asp:Content>
