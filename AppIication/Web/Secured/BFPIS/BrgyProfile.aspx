<%@ Page Title="Brgy. Info" Language="VB" AutoEventWireup="false" CodeFile="BrgyProfile.aspx.vb"
    Inherits="Secured_BFPIS_BrgyProfile" Theme="Skins"
    MasterPageFile="~/MasterPage/Admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpConTent" runat="Server">

    <div class="card">

        <div class="card-header">
            <div class="row">
                <%--<div class="col-lg-4">
                </div>--%>
                <div class="col-lg-8">
                    <h2 class="text-success">Barangay Profile Record</h2>
                </div>

                <div class="col-lg-4">
                    <button runat="server" class="btn btn-success float-end" id="btnAdd"><i class="bi bi-plus-square"></i>&nbsp;Add New</button>
                </div>

            </div>

        </div>

        <div class="card-body" style="padding-bottom: 0px;">
            <asp:UpdatePanel ID="updatePanel5" runat="server">
                <ContentTemplate>

                    <div class="row mt-2">
                        <div class="col-md-4 mb-1">
                            <div class="input-group">
                                <span runat="server" id="lblApplicationNameLabel" class="input-group-text border-secondary" style="background-color: white; color: black">Last Name</span>
                                <asp:TextBox runat="server" ID="txtSearchLName" CssClass="input-field form-control border-secondary" Style="text-transform: uppercase" MaxLength="50" placeholder="" onkeyup="clickEnterSearch('ctl00_cpConTent_btnSearch');"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-4 mb-1" runat="server" visible="True">
                            <div class="input-group">
                                <span runat="server" id="Span1" class="input-group-text border-secondary" style="background-color: white; color: black">First Name</span>
                                <asp:TextBox runat="server" ID="txtSearchFName" CssClass="input-field form-control border-secondary" Style="text-transform: uppercase" MaxLength="100" placeholder="" onkeyup="clickEnterSearch('ctl00_cpConTent_btnSearch');"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-4 mb-1">
                            <span class="input-group-text" style="background-color: white; color: black">
                                <button runat="server" class="btn btn-success" id="btnSearch"><i class="bi bi-funnel"></i>&nbsp;Filter</button>
                                <asp:Label runat="server" ID="lblPaging" CssClass="pull-right "></asp:Label></span>
                        </div>
                    </div>

                    <asp:GridView runat="server" ID="_gv" HeaderStyle-Font-Size="14px" CssClass="gridviewGreen table-bordered table-success table-striped table-hover" PageSize="15" EmptyDataText="NO RECORD FOUND"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                        GridLines="None" Font-Names="Arial" Font-Size="12px" ForeColor="#000000" AllowPaging="true">
                        <Columns>
                            <%--<asp:BoundField DataField="training_date" HeaderText="Training Date" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MMM dd, yyyy}" />--%>
                            <asp:BoundField DataField="last_name" HeaderText="Last Name" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="first_name" HeaderText="First Name" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="middle_name" HeaderText="Middle Name" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ext_name" HeaderText="Ext Name" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />

                            <asp:BoundField DataField="addr_barangay" HeaderText="Barangay" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="monthly_income" HeaderText="Monthly Income" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="economic_status" HeaderText="Economic Status" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />

                            <%-- <asp:BoundField DataField="curr_service" HeaderText="Curr. Service" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                             <asp:BoundField DataField="service_date" HeaderText="Service Date " ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                             <asp:BoundField DataField="serviceDays" HeaderText="Days" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />--%>

                            <asp:TemplateField HeaderText="" HeaderStyle-Width="1%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="lnkEdit" ImageUrl="~/images/editVerification.png" OnCommand="cmdGVUpdate"
                                        CommandArgument='<%# Bind("trans_id")%>' ToolTip="Click to View/Edit Training" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>

</asp:Content>
