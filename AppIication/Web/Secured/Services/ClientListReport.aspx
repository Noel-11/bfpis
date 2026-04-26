<%@ Page Title="Report" Language="VB" AutoEventWireup="false" CodeFile="ClientListReport.aspx.vb"
    Inherits="Secured_Services_ClientListReport" Theme="Skins"
    MasterPageFile="~/MasterPage/Admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpConTent" runat="Server">

    <div class="card">

        <div class="card-header">
            <div class="row">
                <div class="col-lg-4">
                    <%--<button runat="server" class="btn btn-success" id="btnAdd"><i class="bi bi-plus-square"></i>&nbsp;Add New Training</button>--%>
                </div>
                <div class="col-lg-4">

                    <h2 class="text-success">Report: Client List</h2>
                </div>

            </div>

        </div>

        <div class="card-body" style="padding-bottom: 0px;">
            <asp:UpdatePanel ID="updatePanel5" runat="server">
                <ContentTemplate>

                    <div class="row mt-1">
                        <div class="col-md-4 mb-1">
                            <div class="input-group">
                                <span runat="server" id="Span1" class="input-group-text border-secondary" style="background-color: white; color: black">Date From</span>
                                <asp:TextBox runat="server" ID="dtpDateFrom" CssClass="input-field form-control border-secondary" Style="text-transform: uppercase" placeholder="" TextMode="Date"></asp:TextBox>
                                <span runat="server" id="Span2" class="input-group-text border-secondary" style="background-color: white; color: black">To</span>
                                <asp:TextBox runat="server" ID="dtpDateTo" CssClass="input-field form-control border-secondary" Style="text-transform: uppercase" placeholder="" TextMode="Date"></asp:TextBox>

                            </div>

                        </div>

                        <div class="col-md-4 mb-1">
                            <div class="input-group">
                                <span runat="server" id="Span4" class="input-group-text border-secondary bg-success text-light">Service/Purpose</span>
                                <asp:DropDownList runat="server" ID="ddlService" CssClass="form-select border-secondary"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4 mb-1">
                            <div class="input-group">
                                <span runat="server" id="Span5" class="input-group-text border-secondary bg-success text-light">Barangay</span>
                                <asp:DropDownList runat="server" ID="ddlBarangay" CssClass="form-select border-secondary"></asp:DropDownList>
                                 <button runat="server" class="btn btn-info" id="btnPrint"><i class="bi bi-printer"></i>&nbsp;Generate</button>
                            <span class="text-dark" style="background-color: white; color: black">
                                <asp:Label runat="server" ID="lblPaging" CssClass="pull-right "></asp:Label></span>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                             <asp:Literal ID="ltEmbed" runat="server" />
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>

    <!-- Modal PRINT REPORT-->

    <div id="mdlPrintReport" role="dialog" class="modal fade" data-bs-backdrop="false" data-bs-keyboard="false">
        <div class="modal-dialog modal-xl">

            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="updatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <span class="glyphicon glyphicon-alt-list"></span>
                            <asp:Label runat="server" ID="lblReportHeadName" Style="font-size: 20px" Text="Attendance"></asp:Label>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>

                        <div class="modal-body">
                           <%-- <asp:Literal ID="ltEmbed" runat="server" />--%>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="Button4" runat="server" class="btn btn-danger " data-bs-dismiss="modal"><span class="glyphicon glyphicon-remove"></span>&nbsp;Close</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>

    </div>

</asp:Content>
