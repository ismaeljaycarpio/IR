<%@ Page Title="Incident Report List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ir.aspx.cs" Inherits="IR.ir.ir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>Incident Report</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"
                                            OnClick="btnSearch_Click" />
                                    </span>
                                    <asp:TextBox ID="txtSearch"
                                        runat="server"
                                        CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                    <div class="pull-right">
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel"
                                            OnClick="btnExport_Click" CssClass="btn btn-default btn-sm" />
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
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="true"
                                    DataKeyNames="Id"
                                    OnPageIndexChanging="gvIR_PageIndexChanging"
                                    OnRowCommand="gvIR_RowCommand"
                                    PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ticket No">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblTicketNo" runat="server" Text='<%# Eval("TicketNo") %>' CommandName="editRecord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Code" HeaderText="Code" />
                                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                        <asp:BoundField DataField="Room" HeaderText="Room" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowDelete" runat="server" Text="Delete" CommandName="deleteRecord" CssClass="btn btn-danger" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:LinkButton ID="lblCreate" runat="server"
                                    CssClass="btn btn-default"
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
</asp:Content>
