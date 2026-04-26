<%@ Page Title="RefServices" Language="VB" AutoEventWireup="false" CodeFile="RefServicesAdd.aspx.vb"
    Inherits="Secured_Reference_RefServicesAdd" Theme="Skins"
    MasterPageFile="~/MasterPage/Admin.master" %>

<%@ Register Src="~/Include/wucConfirmBoxBS5.ascx" TagName="wucConfirmBox" TagPrefix="wucConfirmBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpConTent" runat="Server">

    <div class="card">
        <asp:UpdatePanel ID="updatePanel2" runat="server">
            <ContentTemplate>
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-4">
                            <button runat="server" id="btnHome" class="btn btn-primary"><i class="bi bi-chevron-double-left"></i>&nbsp;Back</button>
                        </div>
                        <div class="col-lg-4">

                            <h2 class="text-success">Educational Attainment Details</h2>
                        </div>

                    </div>

                </div>

                <div class="card-body" style="padding-bottom: 0px;">
                    <div class="container-fluid">

                        <br />
                       
                        <div class="row mb-1">

                            <div class="col-lg-12">
                                <div class="input-group">
                                    <label class="input-group-text">Description</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" Style="text-transform:uppercase;" />
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Description is required" ValidationGroup="DOC" />
                            </div>
                        </div>

                        <div class="row">
                             <div class="col-lg-6">
                                <div class="input-group">
                                    <label class="input-group-text">Sort Order</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtSortOrder" TextMode="Number" />
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription" SetFocusOnError="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Sort Order is required" ValidationGroup="DOC" />
                            </div>

                            <div class="col-md-6 mb-3">
                                 <div class="input-group">
                                     <label class="input-group-text">Is Active?</label>
                                     <asp:RadioButtonList runat="server" ID="rblIsactive" CssClass="form-control " RepeatDirection="Horizontal">
                                    <asp:ListItem Text="&nbsp;Yes&nbsp;&nbsp;&nbsp;" Value="Y" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp;No" Value="N"></asp:ListItem>
                                </asp:RadioButtonList>
                                 </div>
                            </div>

                        </div>

                        <div class="card-footer">
                            <asp:Button runat="server" Text="Save" class="btn btn-success" ID="btnSave" ValidationGroup="DOC" />
                            <%--<asp:Button runat="server" Text="Cance" class="btn btn-success" ID="Button1" ValidationGroup="DOC" />--%>
                        </div>

                    </div>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


    <asp:UpdatePanel ID="updatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfTransId"></asp:HiddenField>
            <wucConfirmBox:wucConfirmBox runat="server" ID="thisMsgBox" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
