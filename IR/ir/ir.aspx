<%@ Page Title="Incident Report List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ir.aspx.cs" Inherits="IR.ir.ir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>Incident Report</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Search..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"
                                            OnClick="btnSearch_Click" />
                                    </span>
                                    <div class="pull-right">
                                        <asp:Button ID="btnExport"
                                            runat="server"
                                            Text="Export to Excel"
                                            OnClick="btnExport_Click"
                                            CssClass="btn btn-default btn-sm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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
                                    DataSourceID="IRDataSource">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ticket No" SortExpression="TicketNo">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblTicketNo" runat="server" Text='<%# Eval("TicketNo") %>' CommandName="editRecord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CrisisName" HeaderText="Crisis" SortExpression="CrisisName" />
                                        <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                                        <asp:BoundField DataField="Room" HeaderText="Room" SortExpression="Room" />
                                        <asp:BoundField DataField="IncidentDate" HeaderText="Incident Date" DataFormatString="{0:d}" SortExpression="IncidentDate"/>
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"/>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowDelete" runat="server" Text="Delete" CommandName="deleteRecord" CssClass="btn btn-danger" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:LinkButton ID="lblCreate"
                                    runat="server"
                                    CssClass="navbar-link"
                                    PostBackUrl="~/ir/irform.aspx">Create Incident Report</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

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
        runat="server"></asp:LinqDataSource>
</asp:Content>
