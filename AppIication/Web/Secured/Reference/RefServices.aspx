<%@ Page Title="RefServices" Language="VB" AutoEventWireup="false" CodeFile="RefServices.aspx.vb"
    Inherits="Secured_Reference_RefServices" Theme="Skins"
    MasterPageFile="~/MasterPage/Admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpConTent" runat="Server">

    <div class="card">

        <div class="card-header">
            <div class="row">
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4">

                    <h2 class="text-success">Reference Services/Purpose</h2>
                </div>

                <div class="col-lg-4">
                    <button runat="server" class="btn btn-success float-end" id="btnAdd"><i class="bi bi-plus-square"></i>&nbsp;Add</button>
                </div>
            </div>

        </div>

        <div class="card-body" style="padding-bottom: 0px;">
            <asp:UpdatePanel ID="updatePanel5" runat="server">
                <ContentTemplate>

                    <div class="row mt-1">
                        <div class="col-md-8 mb-1">
                            <div class="input-group mb-2">
                                <span runat="server" id="lblApplicationNameLabel" class="input-group-text border-secondary" style="background-color: white; color: black">Search</span>
                                <asp:TextBox runat="server" ID="txtSearch" CssClass="input-field form-control border-secondary" Style="text-transform: uppercase" MaxLength="100" placeholder=""></asp:TextBox>
                                <button runat="server" class="btn btn-success" id="btnSearch"><i class="bi bi-funnel"></i>&nbsp;Filter</button>
                            </div>
                        </div>

                        <div class="col-md-4 mb-1">
                            <div class="input-group mb-1">
                                <span class="input-group-text" style="background-color: white; color: black">
                                    <asp:Label runat="server" ID="lblPaging" CssClass="pull-right "></asp:Label></span>
                            </div>
                        </div>
                    </div>

                    <asp:GridView runat="server" ID="_gv" HeaderStyle-Font-Size="14px" CssClass="gridviewGreen table-bordered table-success table-striped table-hover" PageSize="15" EmptyDataText="NO RECORD FOUND"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                        GridLines="None" Font-Names="Arial" Font-Size="12px" ForeColor="#000000" AllowPaging="true">
                        <Columns>

                            <asp:BoundField DataField="sort_order" HeaderText="Sort Order" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="service_desc" HeaderText="Description" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="is_active" HeaderText="Is Active" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />

                            <asp:TemplateField HeaderText="" HeaderStyle-Width="1%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="lnkEdit" ImageUrl="~/images/editVerification.png" OnCommand="cmdGVUpdate"
                                        CommandArgument='<%# Bind("trans_id")%>' ToolTip="Click to View/Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>

</asp:Content>
