<%@ Page Title="Positions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="position.aspx.cs" Inherits="IR.admin.position" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5><span class="glyphicon glyphicon-hand-up"></span> Positions</h5>
                </div>

                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
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
                                CssClass="btn btn-default btn-sm"
                                OnClick="btnExport_Click" />
                        </div>
                    </div>

                    <br />

                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upPositions" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvPositions"
                                    runat="server"
                                    CssClass="table table-striped table-hover"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    AllowSorting="True"
                                    DataKeyNames="Id"
                                    EmptyDataText="No Record(s) found"
                                    OnRowCommand="gvPositions_RowCommand"
                                    OnRowDataBound="gvPositions_RowDataBound"
                                    DataSourceID="PositionsDataSource">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Edit" CommandName="editRecord" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" 
                                                    runat="server" 
                                                    Text="Delete"
                                                    CssClass="btn btn-danger btn-sm"
                                                    CommandName="deleteRecord" 
                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <!-- Trigger the modal with a button -->
                                <div class="pull-right">
                                    <asp:Button ID="btnOpenModal"
                                        runat="server"
                                        CssClass="btn btn-info btn-sm"
                                        Text="Add Position"
                                        OnClick="btnOpenModal_Click"
                                        CausesValidation="false" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvPositions" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Add Modal -->
    <div id="addModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Position</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddPosition">Position Name</label>
                                    <asp:TextBox ID="txtAddPosition" runat="server" CssClass="form-control" placeholder="Position Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddPosition"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Position is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAdd" OnClick="btnSave_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Edit Modal -->
    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Position</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">

                                <asp:HiddenField ID="hfEditId" runat="server" />

                                <div class="form-group">
                                    <label for="txtEditPosition">Position Name</label>
                                    <asp:TextBox ID="txtEditPosition" runat="server" CssClass="form-control" placeholder="Position Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditPosition"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="Position is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvPositions" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
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

    <asp:LinqDataSource ID="PositionsDataSource"
        OnSelecting="PositionsDataSource_Selecting"
        runat="server">
    </asp:LinqDataSource>

</asp:Content>
