﻿<%@ Page Title="View IR Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="view-irform.aspx.cs" Inherits="IR.ir.view_irform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>Create Incident Report</h4>
                </div>
                <div class="panel-body">

                    <div class="col-md-4">
                        <label for="txtTicketNo">Ticket No: </label>
                        <asp:TextBox ID="txtTicketNo" runat="server" CssClass="form-control" placeholder="Ticket No"></asp:TextBox>
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

                    <div class="col-md-4">
                        <label for="ddlFor">For: </label>
                        <asp:DropDownList ID="ddlFor" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server"
                            Display="Dynamic"
                            ControlToValidate="ddlFor"
                            CssClass="label label-danger"
                            InitialValue="0"
                            ValidationGroup="vgAdd"
                            ErrorMessage="Manager is required"></asp:RequiredFieldValidator>
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
                        <label for="txtRoom">Room: </label>
                        <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" placeholder="Room"></asp:TextBox>
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
                            <asp:ListItem Value="0" Enabled="true">Select Status</asp:ListItem>
                            <asp:ListItem Value="InProgress">In Progress</asp:ListItem>
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
                            data-provide="datepicker"
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
</asp:Content>