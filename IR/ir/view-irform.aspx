<%@ Page Title="View IR Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="view-irform.aspx.cs" Inherits="IR.ir.view_irform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="../Scripts/jquery.tosrus.min.all.js"></script>
    <link rel="stylesheet" type="text/css" href="../Content/jquery.tosrus.all.css" />

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>View Incident Report
                    <asp:HyperLink ID="hlPrintIr" runat="server">
                        <span class="glyphicon glyphicon-print pull-right"></span>
                    </asp:HyperLink>
                    </h5>
                </div>
                <div class="panel-body">

                    <div class="col-md-4">
                        <label for="txtTicketNo">IR No: </label>
                        <asp:TextBox ID="txtTicketNo" runat="server" CssClass="form-control" placeholder="IR No" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtTicketNo"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Ticket No is required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="ddlCrisis">Crisis Name: </label>
                        <asp:DropDownList ID="ddlCrisis" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlCrisis"
                            CssClass="label label-danger"
                            InitialValue="0"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Crisis Code is required"></asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="panel-body">
                    <div class="col-md-4">
                        <label for="ddlFrom">From: </label>
                        <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlFrom"
                            CssClass="label label-danger"
                            InitialValue="0"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Duty Manager is required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="txtSubject">Subject: </label>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="vgAdd"
                            ControlToValidate="txtSubject"
                            CssClass="label label-danger"
                            ErrorMessage="Subject is required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="txtRoom">Location: </label>
                        <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" placeholder="Location"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="vgAdd"
                            ControlToValidate="txtRoom"
                            CssClass="label label-danger"
                            ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-4">
                        <label for="txtDate">Date: </label>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtDate"
                            ValidationGroup="vgAdd"
                            CssClass="label label-danger"
                            ErrorMessage="Date is required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="ddlStatus">Status: </label>
                        <asp:DropDownList ID="ddlStatus"
                            runat="server"
                            CssClass="form-control">
                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                            <asp:ListItem Value="In-Progress">In-Progress</asp:ListItem>
                            <asp:ListItem Value="Solved">Solved</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="vgAdd"
                            ControlToValidate="ddlStatus"
                            CssClass="label label-danger"
                            InitialValue="0"
                            ErrorMessage="Status is required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="ddlDepartment">Department Involved: </label>
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlDepartment"
                            CssClass="label label-danger"
                            InitialValue="0"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Department Field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-4">
                        <label for="txtWhenIncident">When did the incident happen? </label>
                        <asp:TextBox ID="txtWhenIncident"
                            runat="server"
                            CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                            runat="server"
                            Display="Dynamic"
                            ValidationGroup="vgAdd"
                            ControlToValidate="txtWhenIncident"
                            CssClass="label label-danger"
                            ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="rblWhenAware">Did you know about it ? </label>
                        <asp:RadioButtonList ID="rblWhenAware"
                            runat="server"
                            CssClass="btn-group"
                            RepeatLayout="Flow"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="rblWhenAware"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-12">
                        <label for="txtWhosInvolved">Who are the parties involved? </label>
                        <asp:TextBox ID="txtWhosInvolved"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="5"
                            placeholder="Parties Involved"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtWhosInvolved"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-12">
                        <label for="txtWhatHappened">What happened ? </label>
                        <asp:TextBox ID="txtWhatHappened"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="5"
                            placeholder="What Happened"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtWhatHappened"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-12">
                        <label for="FileUpload1">Image Evidence: </label>
                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" CssClass="form-control" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                            runat="server"
                            ControlToValidate="FileUpload1"
                            Display="Dynamic"
                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$"
                            ValidationGroup="vgAdd"
                            CssClass="label label-danger"
                            ErrorMessage="Invalid image file"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <!-- Image Slideshow -->
                <div class="panel-body">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="upImageSlideshow" runat="server">
                            <ContentTemplate>
                                <div id="wrapper">
                                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ImageSlideShowDataSource">
                                        <ItemTemplate>
                                            <a href="<%# "../photo-evidence/" + Eval("IrId") + "_" + Eval("ImagePath") %>">
                                                <img src="<%# "../photo-evidence/" + Eval("IrId") + "_" + "thumb_" + Eval("ImagePath") %>" />
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <!-- Image CMS -->
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upImages" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvImages"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="True"
                                    DataKeyNames="Id"
                                    OnRowCommand="gvImages_RowCommand"
                                    DataSourceID="ImageDataSource">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ImagePath" HeaderText="Image List" SortExpression="ImagePath" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowDelete" runat="server" Text="Delete" CommandName="deleteRecord" CssClass="btn btn-danger" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvImages" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-12">
                        <label for="txtInvestigation">Investigation</label>
                        <asp:TextBox ID="txtInvestigation"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="10"
                            placeholder="Investigation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtInvestigation"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-12">
                        <label for="txtActionTaken">Actions Taken</label>
                        <asp:TextBox ID="txtActionTaken"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="10"
                            placeholder="Investigation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtActionTaken"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="col-md-12">
                        <label for="txtRecommendation">Recommendation</label>
                        <asp:TextBox ID="txtRecommendation"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="10"
                            placeholder="Investigation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="txtRecommendation"
                            CssClass="label label-danger"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <!-- prepared by-->
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtPreparedBy" class="col-sm-2 control-label">Prepared By</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPreparedBy"
                                    runat="server"
                                    Enabled="false"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtPosition" class="col-sm-2 control-label">Position</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPosition"
                                    runat="server"
                                    Enabled="false"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnUpdate"
                        runat="server"
                        Text="Update"
                        OnClick="btnUpdate_Click"
                        ValidationGroup="vgAdd"
                        CssClass="btn btn-success" />
                    <asp:LinkButton ID="lbtnCancel"
                        runat="server"
                        CssClass="btn btn-default"
                        PostBackUrl="~/ir/ir.aspx">Cancel</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <!-- Delete Modal -->
    <div id="deleteModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Image</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this image ?
                            <asp:HiddenField ID="hfDeleteId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Delete"
                                OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1"
        TargetControlID="txtInvestigation"
        EnableSanitization="false"
        runat="server">
    </ajaxToolkit:HtmlEditorExtender>
    <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender2"
        TargetControlID="txtActionTaken"
        EnableSanitization="false"
        runat="server">
    </ajaxToolkit:HtmlEditorExtender>
    <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender3"
        TargetControlID="txtRecommendation"
        EnableSanitization="false"
        runat="server">
    </ajaxToolkit:HtmlEditorExtender>


    <asp:LinqDataSource ID="ImageDataSource"
        OnSelecting="ImageDataSource_Selecting"
        runat="server"></asp:LinqDataSource>

    <asp:LinqDataSource ID="ImageSlideShowDataSource"
        OnSelecting="ImageSlideShowDataSource_Selecting" 
        runat="server"></asp:LinqDataSource>
    <script type="text/javascript">
        $(function () {
            $('#<%= txtWhenIncident.ClientID%>').datetimepicker();
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#wrapper a').tosrus();
        });
    </script>

</asp:Content>
