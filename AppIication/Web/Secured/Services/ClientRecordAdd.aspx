<%@ Page Title="Client Details" Language="VB" AutoEventWireup="false" CodeFile="ClientRecordAdd.aspx.vb"
    Inherits="Secured_Services_ClientRecordAdd" Theme="Skins"
    MasterPageFile="~/MasterPage/Admin.master" %>

<%@ Register Src="~/Include/wucConfirmBoxBS5.ascx" TagName="wucConfirmBox" TagPrefix="wucConfirmBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpConTent" runat="Server">

    <div class="card">
        <div class="card-header">
            <div class="row">

                <div class="col-lg-4">
                    <button runat="server" id="btnHome" class="btn btn-primary"><i class="bi bi-chevron-double-left"></i>&nbsp;Back</button>
                </div>

                <div class="col-lg-4">
                    <h2 class="text-success">Client Details</h2>
                </div>

            </div>

        </div>

        <div class="card-body" style="padding-bottom: 0px;">

            <div class="container-fluid">


                <div class="row">
                    <div class="col-md-12">
                        <div class="card" runat="server" id="divTrainingInfo">
                            <div class="card-header bg-success text-light">
                                <span runat="server" id="spanTainingHead" style="font-weight: bold;">BASIC INFO</span>
                            </div>
                            <div class="card-body" style="padding-bottom: 5px;">
                                <asp:UpdatePanel ID="updatePanel1" runat="server">
                                    <ContentTemplate>
                                        <br />

                                        <div class="row mb-1">
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <label class="input-group-text">Last Name</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtLastName" Style="text-transform: uppercase;" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLastName" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Last Name is required" ValidationGroup="DOC" />
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <label class="input-group-text">First Name</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtFirstName" Style="text-transform: uppercase;" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFirstName" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="First Name is required" ValidationGroup="DOC" />
                                            </div>

                                        </div>

                                        <div class="row mb-1">
                                            <div class="col-lg-8">
                                                <div class="input-group">
                                                    <label class="input-group-text">Middle Name</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtMiddleName" Style="text-transform: uppercase;" />

                                                </div>

                                            </div>

                                            <div class="col-lg-4">
                                                <div class="input-group">
                                                    <label class="input-group-text">Ext.</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtExt" Style="text-transform: uppercase;" />

                                                </div>

                                            </div>
                                        </div>

                                        <div class="row mb-1">

                                            <div class="col-lg-12">
                                                <div class="input-group">
                                                    <label class="input-group-text">Barangay</label>
                                                    <asp:DropDownList runat="server" CssClass="form-select" ID="ddlBarangay">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBarangay" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Barangay is required" ValidationGroup="DOC" />
                                            </div>

                                        </div>

                                        <div class="row mb-1">
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <label class="input-group-text">Birth Date</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="dtpBDate" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpBDate" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Birth Date is required" ValidationGroup="DOC" />
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <label class="input-group-text">CP Number</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCPNumber" MaxLength="11" />
                                                </div>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpBDate" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Birth Date is required" ValidationGroup="DOC" />--%>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-lg-4">
                                                <asp:Button runat="server" Text="Save Info" class="btn btn-success" ID="btnSaveInfo" ValidationGroup="DOC" />

                                            </div>

                                            <div class="col-lg-8">
                                                <div class="float-end">
                                                    <span class="text-success" runat="server" id="lblLastUserDetails" style="font-size: 11px; font-weight: bold;"></span>

                                                </div>

                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="card" runat="server" id="divServices">
                            <asp:UpdatePanel ID="updatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="card-header bg-success text-light">
                                        <span runat="server" id="span1" style="font-weight: bold;">SERVICE AVAILED</span>
                                        <button runat="server" class="btn btn-info" id="btnAddService" tooltip="Click to Add Service"><i class="bi bi-plus-square"></i>&nbsp;</button>

                                    </div>

                                    <div class="card-body" style="padding-bottom: 5px; max-height:700px;overflow-y:scroll;">

                                        <asp:GridView runat="server" ID="_gvServices" HeaderStyle-Font-Size="14px" CssClass="gridviewGray table-bordered table-success table-striped table-hover" PageSize="15" EmptyDataText="NO SERVICE FOUND"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                            GridLines="None" Font-Names="Arial" Font-Size="12px" ForeColor="#000000" AllowPaging="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="1%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="lnkEdit" ImageUrl="~/images/open-folder.png" OnCommand="cmdGVUpdate"
                                                            CommandArgument='<%# Bind("trans_id")%>' ToolTip="Click to View/Edit Service" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="service_date" HeaderText="Date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="service" HeaderText="Service" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
                                                 <asp:BoundField DataField="service_remarks" HeaderText="Remarks" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="left" />
                                                <asp:BoundField DataField="create_user" HeaderText="User" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="1%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="lnkRemove" ImageUrl="~/images/filedel24.png" OnCommand="cmdGVRemove"
                                                            CommandArgument='<%# Bind("trans_id")%>' serviceDesc='<%# Eval("service")%>' serviceDate='<%# Eval("service_date")%>' ToolTip="Click to Remove Service" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>

            </div>
        </div>

    </div>

    <!-- MODAL TRAINING STATUS-->
    <div id="mdlService" role="dialog" class="modal fade" aria-hidden="true" data-bs-backdrop="false" data-bs-keyboard="false">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                    <ContentTemplate>

                        <div class="modal-header bg-success">
                            <h5 class="modal-title text-light" runat="server" id="lblReturnHeaderText">Service Details</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>

                        <div class="modal-body bg-light">
                            <div class="row">
                                <div class="col-md-12 mb-1">
                                    <div class="input-group">
                                        <label class="input-group-text">Client Name:</label>
                                        <asp:Label runat="server" CssClass="form-control text-dark" Style="background-color: white" ID="lblClientName"></asp:Label>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-12 mb-1">
                                    <div class="input-group">
                                        <label class="input-group-text">Service:</label>
                                        <asp:DropDownList runat="server" ID="ddlService" CssClass="form-select"></asp:DropDownList>
                                    </div>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlService" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Service is required" ValidationGroup="DOCSERVICE" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 mb-1">
                                    <div class="input-group">
                                        <label class="input-group-text">Service Date</label>
                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" ID="dtpServDate" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpServDate" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Date is required" ValidationGroup="DOCSERVICE" />
                                </div>
                            </div>

                              <div class="row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <label class="input-group-text">Remarks</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtServiceRemarks" Style="text-transform: uppercase;" TextMode="MultiLine" Rows="3"  />
                                    </div>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dtpServDate" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Date is required" ValidationGroup="DOCSERVICE" />--%>
                                </div>
                            </div>

                        </div>

                        <div class="modal-footer">
                            <button runat="server" class="btn btn-success" id="btnSaveService" tooltip="Click to Save" validationgroup="DOCSERVICE"><i class="bi bi-bookmark-check"></i>&nbsp;Save Service</button>
                            <button type="button" class="btn btn-danger" runat="server" id="btnCloseService" data-bs-dismiss="modal">Close</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="updatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfTransId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hfServiceId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hfStatus"></asp:HiddenField>
            <wucConfirmBox:wucConfirmBox runat="server" ID="thisMsgBox" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
