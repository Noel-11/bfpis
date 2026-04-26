Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class Secured_Services_ClientRecordAdd
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Dim _btnOK As New HtmlButton

    Dim _btnNo As New HtmlButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            hfTransId.Value = Session("CLIENT_ID")

            _clsDB.populateDDLB(ddlBarangay, "barangay", "barangay_code", "tbl_ref_barangay", "barangay", , , "")

            _clsDB.populateDDLB(ddlService, "service_desc", "trans_id", "tbl_ref_services", "sort_order", " WHERE is_active = 'Y' ", , "")

            flllInfo()

        End If

        _btnOK = thisMsgBox.FindControl("btnMsgBoxYes")
        AddHandler _btnOK.ServerClick, AddressOf btnOK_Click

        _btnNo = thisMsgBox.FindControl("btnMsgBoxNo")
        AddHandler _btnNo.ServerClick, AddressOf btnNo_Click

    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "SAVE INFO" Then
            saveInfo()
            Response.Redirect("ClientRecordAdd.aspx")

        ElseIf thisMsgBox.getModalType = "SAVE SERVICE" Then
            saveService()
            fillGvServices()
       
            thisMsgBox.setInfo(, "Service Saved Successfully!")
            thisMsgBox.showConfirmBox()

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "btnCloseService", "document.getElementById('ctl00_cpConTent_btnCloseService').click();", True)

        ElseIf thisMsgBox.getModalType = "REMOVE SERVICE" Then

            updateIsActive("N")
            fillGvServices()
            thisMsgBox.setInfo(, "Service Removed!")
            thisMsgBox.showConfirmBox()

        End If

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnHome_ServerClick(sender As Object, e As EventArgs) Handles btnHome.ServerClick
        Response.Redirect("ClientRecord.aspx")
    End Sub

    Private Sub flllInfo()

        divServices.Visible = False

        If hfTransId.Value <> "" Then

            divServices.Visible = True

            spanTainingHead.InnerText = "UPDATE CLIENT RECORD"

            Dim _clsRecord As New clsClientInfo

            With _clsRecord
                .getClientInfo(hfTransId.Value)
                txtLastName.Text = .lastName
                txtFirstName.Text = .firstName
                txtMiddleName.Text = .middleName
                txtExt.Text = .extName
                ddlBarangay.SelectedValue = .barangay
                dtpBDate.Text = CDate(.birthDate).ToString("yyyy-MM-dd")
                txtCPNumber.Text = .cpNumber
                lblLastUserDetails.InnerText = "Create User: " & .createUser & " " & .createDate

            End With

            fillGvServices()

        Else
            spanTainingHead.InnerText = "ADD CLIENT INFO"

        End If

    End Sub

    Private Sub saveInfo()

        Dim _clsRecord As New clsClientInfo

        Dim dtOld As New DataTable
        Dim dtNew As New DataTable

        Dim isNew As Boolean = True

        If hfTransId.Value <> "" Then
            isNew = False

            dtOld = _clsRecord.browseClientInfoForLog(hfTransId.Value)

        End If

        With _clsRecord
            .initialize()
            .transId = hfTransId.Value
            .lastName = txtLastName.Text.Trim.ToUpper
            .firstName = txtFirstName.Text.Trim.ToUpper
            .middleName = txtMiddleName.Text.Trim.ToUpper
            .extName = txtExt.Text.Trim.ToUpper
            .barangay = ddlBarangay.SelectedValue
            .birthDate = CDate(dtpBDate.Text).ToString("yyyy-MM-dd")
            .cpNumber = txtCPNumber.Text.Trim
            .lastUser = Session("UserName")
            .saveClientInfo()
            hfTransId.Value = .transId

            Session("CLIENT_ID") = .transId

        End With


        dtNew = _clsRecord.browseClientInfoForLog(hfTransId.Value)

        Dim clsLogs As New clsAuditTrail

        If isNew Then

            Dim newEntry As String = clsLogs.getTransactionNewEntry(dtNew)

            With clsLogs
                .transModule = "ADD"
                .transType = "PDIS : CLIENT INFO"
                .linkID = hfTransId.Value
                .loggedChanges = newEntry
                .transBy = Session("UserName")
                .saveAuditTrail()
            End With

        Else

            Dim changes As String = clsLogs.getTransactionChanges(dtOld, dtNew)

            If changes <> "" Then
                With clsLogs
                    .transModule = "UPDATE"
                    .transType = "PDIS : CLIENT INFO"
                    .linkID = hfTransId.Value
                    .loggedChanges = changes
                    .transBy = Session("UserName")
                    .saveAuditTrail()
                End With
            End If

        End If

    End Sub

    Protected Sub btnSaveInfo_Click(sender As Object, e As EventArgs) Handles btnSaveInfo.Click


        Dim dtCheck As New DataTable

        Dim sql As String = ""

        If hfTransId.Value = "" Then
            sql = "SELECT trans_id, last_name,first_name,middle_name, ext_name, tbl_ref_barangay.barangay  FROM tbl_client_info " & _
                           "INNER JOIN tbl_ref_barangay ON tbl_client_info.barangay = tbl_ref_barangay.barangay_code " & _
                           "WHERE last_name = '" & txtLastName.Text.Trim.ToUpper & "' AND first_name = '" & txtFirstName.Text.Trim.ToUpper & "' AND " & _
                           "tbl_client_info.birth_date = '" & CDate(dtpBDate.Text).ToString("yyyy-MM-dd") & "' AND is_active = 'Y' LIMIT 1"
        Else
            sql = "SELECT trans_id, last_name,first_name,middle_name, ext_name, tbl_ref_barangay.barangay  FROM tbl_client_info " & _
                           "INNER JOIN tbl_ref_barangay ON tbl_client_info.barangay = tbl_ref_barangay.barangay_code " & _
                           "WHERE last_name = '" & txtLastName.Text.Trim.ToUpper & "' AND first_name = '" & txtFirstName.Text.Trim.ToUpper & "' AND " & _
                           "tbl_client_info.birth_date = '" & CDate(dtpBDate.Text).ToString("yyyy-MM-dd") & "' AND is_active = 'Y' AND tbl_client_info.trans_id <> '" & hfTransId.Value & "' LIMIT 1"
        End If

       

        dtCheck = _clsDB.Fill_DataTable(sql)

        thisMsgBox.setModalType("SAVE INFOXX")
        If dtCheck.Rows.Count > 0 Then
            thisMsgBox.setError("INFO EXISTED", "Client info already existed! <br/>" & _
                                               "Name: " & dtCheck.Rows(0)("last_name") & ", " & dtCheck.Rows(0)("first_name") & " " & dtCheck.Rows(0)("ext_name") & " " & dtCheck.Rows(0)("middle_name") & "<br/>" & _
                                               "Barangay: " & dtCheck.Rows(0)("barangay"))
        Else
            thisMsgBox.setModalType("SAVE INFO")
            thisMsgBox.setConfirm(, "Are you sure to save Client Info?")
        End If

        thisMsgBox.showConfirmBox()
    End Sub

#Region "SERVICES"

    Protected Sub btnAddService_ServerClick(sender As Object, e As EventArgs) Handles btnAddService.ServerClick
        hfServiceId.Value = ""

        lblClientName.Text = txtLastName.Text & ", " & txtFirstName.Text & " " & txtExt.Text & " " & txtMiddleName.Text

        dtpServDate.Text = DateTime.Now.ToString("yyyy-MM-dd")

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlService", "var myModal = new bootstrap.Modal(document.getElementById('mdlService'), {});  myModal.show();", True)
    End Sub

    Protected Sub btnSaveService_ServerClick(sender As Object, e As EventArgs) Handles btnSaveService.ServerClick

        Dim dtCheck As New DataTable

        If hfServiceId.Value = "" Then
            Dim sql As String = "SELECT trans_id, DATE_FORMAT(service_date,'%m/%d/%Y') AS service_date FROM tbl_client_services " & _
                           "WHERE (service_date BETWEEN DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 2 MONTH),'%Y-%m-%d') AND DATE_FORMAT(NOW(),'%Y-%m-%d')) AND " & _
                           "client_id = '" & hfTransId.Value & "' AND is_active = 'Y' LIMIT 1 "


            dtCheck = _clsDB.Fill_DataTable(sql)
        End If
       
        'thisMsgBox.setModalType("SAVE SERVICEXX")
        thisMsgBox.setModalType("SAVE SERVICE")
        If dtCheck.Rows.Count > 0 Then

            thisMsgBox.setConfirm("Warning", "This client have already received service on " & dtCheck.Rows(0)("service_date") & "!.<br/>" & _
                                             "Are you sure to save Service? <br/> " & _
                                             "Description: " & ddlService.SelectedItem.Text.ToUpper)

            thisMsgBox.setHeader("INDIANRED", "YELLOW")

        Else

            thisMsgBox.setConfirm(, "Are you sure to save Service? <br/> " & _
                                    "Description: " & ddlService.SelectedItem.Text.ToUpper)
        End If

        thisMsgBox.showConfirmBox()

    End Sub

    Private Sub saveService()

        ' -----------------------------------------------
        Dim _clsRecord As New clsClientServices

        Dim dtOld As New DataTable
        Dim dtNew As New DataTable

        Dim isNew As Boolean = True

        If hfServiceId.Value <> "" Then
            isNew = False

            dtOld = _clsRecord.browseClientServicesForLog(hfServiceId.Value)

        End If

        With _clsRecord
            .initialize()
            .transId = hfServiceId.Value
            .clientId = hfTransId.Value
            .service = ddlService.SelectedValue
            .serviceDate = CDate(dtpServDate.Text).ToString("yyyy-MM-dd")
            .serviceRemarks = txtServiceRemarks.Text.Trim.ToUpper
            .lastUser = Session("UserName")
            .saveClientServices()
            hfServiceId.Value = .transId
        End With

        dtNew = _clsRecord.browseClientServicesForLog(hfServiceId.Value)

        Dim clsLogs As New clsAuditTrail

        If isNew Then

            Dim newEntry As String = clsLogs.getTransactionNewEntry(dtNew)

            With clsLogs
                .transModule = "ADD"
                .transType = "PDIS : CLIENT SERVICE"
                .linkID = hfServiceId.Value
                .loggedChanges = newEntry
                .transBy = Session("UserName")
                .saveAuditTrail()
            End With

        Else

            Dim changes As String = clsLogs.getTransactionChanges(dtOld, dtNew)

            If changes <> "" Then
                With clsLogs
                    .transModule = "UPDATE"
                    .transType = "PDIS : CLIENT SERVICE"
                    .linkID = hfServiceId.Value
                    .loggedChanges = changes
                    .transBy = Session("UserName")
                    .saveAuditTrail()
                End With
            End If

        End If


        Dim dt As New DataTable

        dt = _clsDB.Fill_DataTable("SELECT trans_id FROM tbl_client_services " & _
                                   "WHERE client_id = '" & hfTransId.Value & "' AND is_active = 'Y' " & _
                                   "ORDER BY service_date DESC LIMIT 1")

        Dim _clsInfo As New clsClientInfo

        For Each dr As DataRow In dt.Rows

            With _clsInfo
                .transId = hfTransId.Value
                .currService = dr(0)
                .updateCurrentService()
            End With

        Next


    End Sub

    Protected Sub fillGvServices()

        Dim dt As New DataTable

        Dim _clsRecords As New clsClientServices

        dt = _clsRecords.browseClientServices(hfTransId.Value)

        _gvServices.DataSource = dt
        _gvServices.DataBind()

    End Sub

    Protected Sub cmdGVUpdate(ByVal sender As Object, ByVal e As CommandEventArgs)
        hfServiceId.Value = e.CommandArgument
        lblClientName.Text = txtLastName.Text & ", " & txtFirstName.Text & " " & txtExt.Text & " " & txtMiddleName.Text
        Dim _clsRecord As New clsClientServices

        With _clsRecord
            .getClientServices(hfServiceId.Value)
            ddlService.SelectedValue = .service
            dtpServDate.Text = CDate(.serviceDate).ToString("yyyy-MM-dd")
            txtServiceRemarks.Text = .serviceRemarks
        End With

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlService", "var myModal = new bootstrap.Modal(document.getElementById('mdlService'), {});  myModal.show();", True)
    End Sub


    Protected Sub cmdGVRemove(ByVal sender As Object, ByVal e As CommandEventArgs)

        hfServiceId.Value = e.CommandArgument

        thisMsgBox.setModalType("REMOVE SERVICE")
        thisMsgBox.setConfirm(, "Are you to remove this service? <br/>" & _
                                "Service: " & CType(sender, ImageButton).Attributes("serviceDesc") & "<br/>" & _
                                "Date: " & CType(sender, ImageButton).Attributes("serviceDate"))
        thisMsgBox.setHeader("RED", "YELLOW")
        thisMsgBox.showConfirmBox()
    End Sub


    Private Sub updateIsActive(ByVal _thisActive As String)

        Dim _clsRecord As New clsClientServices

        Dim dtOld As New DataTable
        Dim dtNew As New DataTable

        dtOld = _clsRecord.browseClientServicesForLog(hfServiceId.Value)

        With _clsRecord
            .transId = hfServiceId.Value
            .isActive = _thisActive
            .lastUser = Session("UserName")
            .updateIsActive()
        End With


        If _thisActive = "N" Then
            Dim _clsInfo As New clsClientInfo

            With _clsInfo
                .transId = hfTransId.Value
                .currService = ""
                .updateCurrentService()
            End With

        End If


        dtNew = _clsRecord.browseClientServicesForLog(hfServiceId.Value)

        Dim clsLogs As New clsAuditTrail

        Dim changes As String = clsLogs.getTransactionChanges(dtOld, dtNew)

            If changes <> "" Then
                With clsLogs
                    .transModule = "UPDATE"
                    .transType = "PDIS : CLIENT SERVICE"
                    .linkID = hfServiceId.Value
                    .loggedChanges = changes
                    .transBy = Session("UserName")
                    .saveAuditTrail()
                End With
            End If

       
    End Sub



#End Region

End Class

