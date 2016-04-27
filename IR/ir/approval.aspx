<%@ Page Title="Approval of Incident Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="approval.aspx.cs" Inherits="IR.ir.approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>Approval of Incident Report</h5>
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Search..."></asp:TextBox>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" data-provide="datepicker" placeholder="From Date"></asp:TextBox>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" data-provide="datepicker" placeholder="To Date"></asp:TextBox>
                            <asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">-- All Status --</asp:ListItem>
                                <asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                <asp:ListItem Value="Disapproved">Disapproved</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch"
                                runat="server"
                                CssClass="btn btn-primary"
                                Text="Go"
                                OnClick="btnSearch_Click" />
                        </div>
                        <div class="pull-right">
                            <asp:Button ID="btnExport"
                                runat="server"
                                Text="Export to Excel"
                                OnClick="btnExport_Click"
                                CssClass="btn btn-default btn-sm" />
                        </div>
                    </div>
                    <br />
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upIR" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvIR"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="True"
                                    DataKeyNames="Id"
                                    OnRowCommand="gvIR_RowCommand"
                                    OnRowDataBound="gvIR_RowDataBound"
                                    DataSourceID="IRDataSource">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IR No" SortExpression="TicketNo">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblTicketNo" runat="server" Text='<%# Eval("TicketNo") %>' CommandName="editRecord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CrisisName" HeaderText="Crisis" SortExpression="CrisisName" />
                                        <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                                        <asp:BoundField DataField="Room" HeaderText="Location" SortExpression="Room" />
                                        <asp:BoundField DataField="IncidentDate" HeaderText="Incident Date" SortExpression="IncidentDate" />
                                        <asp:BoundField DataField="DateSolved" HeaderText="Solved Date" SortExpression="DateSolved" DataFormatString="{0:d}" />

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnApproval" runat="server" Text="Approval" CommandName="approvalRecord" CssClass="btn btn-default" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

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
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- Approval Modal -->
    <div id="approveModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Approve IR</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to approve this IR ?
                            <asp:HiddenField ID="hfIRId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmApprove"
                                runat="server"
                                CssClass="btn btn-success"
                                Text="Approve"
                                OnClick="btnConfirmApprove_Click" />
                            <asp:Button ID="btnConfirmDisapprove"
                                runat="server"
                                CssClass="btn btn-warning"
                                Text="Disapprove"
                                OnClick="btnConfirmDisapprove_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConfirmApprove" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
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
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this record ?
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

    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <asp:LinqDataSource ID="IRDataSource"
        OnSelecting="IRDataSource_Selecting"
        runat="server">
    </asp:LinqDataSource>
</asp:Content>
