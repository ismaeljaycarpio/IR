<%@ Page Title="Incident Report Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="irform.aspx.cs" Inherits="IR.ir.irform" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


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
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="txtTicketNo" class="col-sm-3 control-label">Ticket No: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtTicketNo" runat="server" CssClass="form-control" placeholder="Ticket No"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtTicketNo"
                                    CssClass="label label-danger"
                                    ErrorMessage="Ticket No is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlFor" class="col-sm-3 control-label">For: </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlFor" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ddlFor"
                                    CssClass="label label-danger"
                                    InitialValue="0"
                                    ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlFrom" class="col-sm-3 control-label">From: </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ddlFrom"
                                    CssClass="label label-danger"
                                    InitialValue="0"
                                    ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtSubject" class="col-sm-3 control-label">Subject: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtSubject"
                                    CssClass="label label-danger"
                                    ErrorMessage="Subject is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtRoom" class="col-sm-3 control-label">Room: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtRoom" runat="server" CssClass="form-control" placeholder="Room"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtRoom"
                                    CssClass="label label-danger"
                                    ErrorMessage="Room is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtDate" class="col-sm-3 control-label">Date: </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtDate"
                                    CssClass="label label-danger"
                                    ErrorMessage="Date is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlStatus" class="col-sm-3 control-label">Status: </label>
                            <div class="col-sm-6">
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
                                    ControlToValidate="ddlStatus"
                                    CssClass="label label-danger"
                                    InitialValue="0"
                                    ErrorMessage="Status is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlDepartment" class="col-sm-3 control-label">Department Involved: </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="ddlDepartment"
                                    CssClass="label label-danger"
                                    InitialValue="0"
                                    ErrorMessage="Department Field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtWhenIncident" class="col-sm-3 control-label">When did the incident happen? </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtWhenIncident"
                                    runat="server"
                                    data-provide="datepicker"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtWhenIncident"
                                    CssClass="label label-danger"
                                    ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="rblDidyouKnow" class="col-sm-3 control-label">Did you know about it ? </label>
                            <div class="col-sm-6">
                                <asp:RadioButtonList ID="rblDidyouKnow" runat="server">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="rblDidyouKnow"
                                    CssClass="label label-danger"
                                    ErrorMessage="This field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtWhosInvolved" class="col-sm-3 control-label">Who are the parties involved? </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtWhosInvolved"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Parties Involved"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtWhatHappened" class="col-sm-3 control-label">What happened ? </label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtWhatHappened"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Height="100"
                                    placeholder="What Happened"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtWhatHappened"
                                    CssClass="label label-danger"
                                    ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtInvestigation" class="col-sm-3 control-label">Investigation</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtInvestigation"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Height="200"
                                    placeholder="Investigation"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtInvestigation"
                                    CssClass="label label-danger"
                                    ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtActionTaken" class="col-sm-3 control-label">Actions Taken</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtActionTaken"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Height="200"
                                    placeholder="Investigation"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtActionTaken"
                                    CssClass="label label-danger"
                                    ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtRecommendation" class="col-sm-3 control-label">Recommendation</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtRecommendation"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Height="200"
                                    placeholder="Investigation"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtRecommendation"
                                    CssClass="label label-danger"
                                    ErrorMessage="Field is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtPreparedBy" class="col-sm-3 control-label">Prepared By</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPreparedBy"
                                    runat="server"
                                    Enabled="false"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtPosition" class="col-sm-3 control-label">Position</label>
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
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default"/>
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
