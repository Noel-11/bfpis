Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class Secured_BFPIS_BrgyProfileAdd
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Dim _btnOK As New HtmlButton

    Dim _btnNo As New HtmlButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            hfTransId.Value = Session("CLIENT_ID")

            getDll()

            flllInfo()

        End If

        _btnOK = thisMsgBox.FindControl("btnMsgBoxYes")
        AddHandler _btnOK.ServerClick, AddressOf btnOK_Click

        _btnNo = thisMsgBox.FindControl("btnMsgBoxNo")
        AddHandler _btnNo.ServerClick, AddressOf btnNo_Click

    End Sub


    Private Sub getDll()

        _clsDB.populateDDLB(ddlExt, "suffix_desc", "trans_id", "tbl_ref_suffix", "sort_order", " WHERE is_active = 'Y' ", "", "")

        _clsDB.populateDDLB(ddlBarangay, "barangay", "barangay_code", "tbl_ref_barangay", "barangay", , , "")

        _clsDB.populateDDLB(ddlSex, "description", "trans_id", "tbl_ref_sex", "sort_order", " WHERE is_active = 'Y' ", , "")

        _clsDB.populateDDLB(ddlReligion, "religion_desc", "trans_id", "tbl_ref_religion", "sort_order", " WHERE is_active = 'Y' ", , "")

        _clsDB.populateDDLB(ddlOccupation, "occupation_desc", "trans_id", "tbl_ref_occupation", "sort_order", " WHERE is_active = 'Y' ", , "")

        _clsDB.populateDDLB(ddlEconomicStatus, "economic_desc", "trans_id", "tbl_ref_economic_status", "sort_order", " WHERE is_active = 'Y' ", , "")


        'MEMBER
        _clsDB.populateDDLB(ddlMemberExt, "suffix_desc", "trans_id", "tbl_ref_suffix", "sort_order", " WHERE is_active = 'Y' ", "", "")

        _clsDB.populateDDLB(ddlMemberRelation, "relation_desc", "trans_id", "tbl_ref_relation", "sort_order", " WHERE is_active = 'Y' ", , "")

        _clsDB.populateDDLB(ddlMemberSex, "description", "trans_id", "tbl_ref_sex", "sort_order", " WHERE is_active = 'Y' ", , "")


    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "SAVE INFO" Then
            saveInfo()
            Response.Redirect("BrgyProfileAdd.aspx")

        ElseIf thisMsgBox.getModalType = "SAVE MEMBER" Then
            saveMember()
            fillGvMember()

            thisMsgBox.setInfo(, "Member Saved Successfully!")
            thisMsgBox.showConfirmBox()

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "btnCloseMember", "document.getElementById('ctl00_cpConTent_btnCloseMember').click();", True)

        ElseIf thisMsgBox.getModalType = "REMOVE MEMBER" Then

            updateIsActive("N")
            fillGvMember()
            thisMsgBox.setInfo(, "Member Removed!")
            thisMsgBox.showConfirmBox()

        End If

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnHome_ServerClick(sender As Object, e As EventArgs) Handles btnHome.ServerClick
        Response.Redirect("BrgyProfile.aspx")
    End Sub

    Private Sub flllInfo()

        divFamily.Visible = False

        If hfTransId.Value <> "" Then

            divFamily.Visible = True

            spanTainingHead.InnerText = "UPDATE PROFILE"

            Dim _clsRecord As New clsProfileHead

            With _clsRecord
                .getProfileHead(hfTransId.Value)
                txtLastName.Text = .lastName
                txtFirstName.Text = .firstName
                txtMiddleName.Text = .middleName
                ddlExt.SelectedValue = .extName
                txtCPNumber.Text = .celNo
                ddlBarangay.SelectedValue = .addrBarangay
                txtAddrOther.Text = .addrOther
                dtpBDate.Text = CDate(.birthDate).ToString("yyyy-MM-dd")
                ddlSex.SelectedValue = .sex
                ddlReligion.SelectedValue = .religion
                ddlOccupation.SelectedValue = .occupation
                txtMonthlyIncome.Text = .monthlyIncome
                ddlEconomicStatus.SelectedValue = .economicStatus
                lblLastUserDetails.InnerText = "Create User: " & .createUser & " " & .createDate
            End With

            fillGvMember()

        Else
            spanTainingHead.InnerText = "ADD PROFILE"

        End If

    End Sub

    Private Sub saveInfo()

        Dim _clsRecord As New clsProfileHead

        Dim dtOld As New DataTable
        Dim dtNew As New DataTable

        Dim isNew As Boolean = True

        If hfTransId.Value <> "" Then
            isNew = False

            ' dtOld = _clsRecord.browseClientInfoForLog(hfTransId.Value)

        End If

        With _clsRecord
            .initialize()
            .transId = hfTransId.Value
            .lastName = txtLastName.Text.Trim.ToUpper
            .firstName = txtFirstName.Text.Trim.ToUpper
            .middleName = txtMiddleName.Text.Trim.ToUpper
            .extName = ddlExt.SelectedValue
            .celNo = txtCPNumber.Text.Trim
            .addrBarangay = ddlBarangay.SelectedValue
            .addrOther = txtAddrOther.Text.Trim.ToUpper
            .birthDate = CDate(dtpBDate.Text).ToString("yyyy-MM-dd")
            .sex = ddlSex.SelectedValue
            .religion = ddlReligion.SelectedValue
            .occupation = ddlOccupation.SelectedValue
            .monthlyIncome = txtMonthlyIncome.Text.Trim.ToUpper
            .economicStatus = ddlEconomicStatus.SelectedValue
            .saveProfileHead()
            hfTransId.Value = .transId

            Session("CLIENT_ID") = .transId

        End With


        'dtNew = _clsRecord.browseClientInfoForLog(hfTransId.Value)

        'Dim clsLogs As New clsAuditTrail

        'If isNew Then

        '    Dim newEntry As String = clsLogs.getTransactionNewEntry(dtNew)

        '    With clsLogs
        '        .transModule = "ADD"
        '        .transType = "PDIS : CLIENT INFO"
        '        .linkID = hfTransId.Value
        '        .loggedChanges = newEntry
        '        .transBy = Session("UserName")
        '        .saveAuditTrail()
        '    End With

        'Else

        '    Dim changes As String = clsLogs.getTransactionChanges(dtOld, dtNew)

        '    If changes <> "" Then
        '        With clsLogs
        '            .transModule = "UPDATE"
        '            .transType = "PDIS : CLIENT INFO"
        '            .linkID = hfTransId.Value
        '            .loggedChanges = changes
        '            .transBy = Session("UserName")
        '            .saveAuditTrail()
        '        End With
        '    End If

        'End If

    End Sub

    Protected Sub btnSaveInfo_Click(sender As Object, e As EventArgs) Handles btnSaveInfo.Click


        Dim dtCheck As New DataTable

        Dim sql As String = ""

        If hfTransId.Value = "" Then
            sql = "SELECT trans_id, last_name,first_name,middle_name, ext_name, tbl_ref_barangay.barangay  FROM tbl_profile_head " & _
                           "INNER JOIN tbl_ref_barangay ON tbl_profile_head.barangay = tbl_ref_barangay.barangay_code " & _
                           "WHERE last_name = '" & txtLastName.Text.Trim.ToUpper & "' AND first_name = '" & txtFirstName.Text.Trim.ToUpper & "' AND " & _
                           "tbl_client_info.birth_date = '" & CDate(dtpBDate.Text).ToString("yyyy-MM-dd") & "' AND is_active = 'Y' LIMIT 1"
        Else
            sql = "SELECT trans_id, last_name,first_name,middle_name, ext_name, tbl_ref_barangay.barangay  FROM tbl_profile_head " & _
                           "INNER JOIN tbl_ref_barangay ON tbl_profile_head.barangay = tbl_ref_barangay.barangay_code " & _
                           "WHERE last_name = '" & txtLastName.Text.Trim.ToUpper & "' AND first_name = '" & txtFirstName.Text.Trim.ToUpper & "' AND " & _
                           "tbl_client_info.birth_date = '" & CDate(dtpBDate.Text).ToString("yyyy-MM-dd") & "' AND is_active = 'Y' AND tbl_profile_head.trans_id <> '" & hfTransId.Value & "' LIMIT 1"
        End If



        dtCheck = _clsDB.Fill_DataTable(sql)

        thisMsgBox.setModalType("SAVE INFOXX")
        If dtCheck.Rows.Count > 0 Then
            thisMsgBox.setError("INFO EXISTED", "Profile already existed! <br/>" & _
                                               "Name: " & dtCheck.Rows(0)("last_name") & ", " & dtCheck.Rows(0)("first_name") & " " & dtCheck.Rows(0)("ext_name") & " " & dtCheck.Rows(0)("middle_name") & "<br/>" & _
                                               "Barangay: " & dtCheck.Rows(0)("barangay"))
        Else
            thisMsgBox.setModalType("SAVE INFO")
            thisMsgBox.setConfirm(, "Are you sure to save Profile?")
        End If

        thisMsgBox.showConfirmBox()
    End Sub

#Region "SERVICES"

    Protected Sub btnAddService_ServerClick(sender As Object, e As EventArgs) Handles btnAddService.ServerClick
        hfMemberId.Value = ""

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlMember", "var myModal = new bootstrap.Modal(document.getElementById('mdlMember'), {});  myModal.show();", True)
    End Sub

    Protected Sub btnSaveMember_ServerClick(sender As Object, e As EventArgs) Handles btnSaveMember.ServerClick

        Dim dtCheck As New DataTable

        If hfMemberId.Value = "" Then
            Dim sql As String = "SELECT trans_id,tbl_ref_relation.relation_desc FROM tbl_profile_member " & _
                                "INNER JOIN tbl_ref_relation ON tbl_ref_relation.member_relation = tbl_ref_relation.trans_id " & _
                                "WHERE member_lname = '" & txtMemberLName.Text.Trim.ToUpper & "' AND member_fname = '" & txtMemberFName.Text.Trim.ToUpper & "' AND " & _
                                "member_bdate = '" & CDate(dtpMemberBDate.Text).ToString("yyyy-MM-dd") & "' LIMIT 1 "


            dtCheck = _clsDB.Fill_DataTable(sql)
        End If


        thisMsgBox.setModalType("SAVE MEMBERXX")
        If dtCheck.Rows.Count > 0 Then

            thisMsgBox.setError("Cannot Add", "Member Already Exist! <br/> " & _
                                                "Name: " & txtMemberLName.Text.Trim.ToUpper & ", " & txtMemberFName.Text.Trim.ToUpper & " " & ddlExt.SelectedValue & " " & txtMiddleName.Text.Trim.ToUpper & "<br/>" & _
                                                "Relation: " & dtCheck.Rows(0)("relation_desc"))

            thisMsgBox.setHeader("INDIANRED", "YELLOW")

        Else
            thisMsgBox.setModalType("SAVE MEMBER")
            thisMsgBox.setConfirm(, "Are you sure to save Member? <br/> " & _
                                    "Name: " & txtMemberLName.Text.Trim.ToUpper & ", " & txtMemberFName.Text.Trim.ToUpper & " " & ddlExt.SelectedValue & " " & txtMiddleName.Text.Trim.ToUpper & "<br/>" & _
                                    "Relation: " & ddlMemberRelation.SelectedItem.Text.ToUpper)
        End If

        thisMsgBox.showConfirmBox()

    End Sub

    Private Sub saveMember()

        ' -----------------------------------------------
        Dim _clsRecord As New clsProfileMember

        Dim dtOld As New DataTable
        Dim dtNew As New DataTable

        Dim isNew As Boolean = True

        If hfMemberId.Value <> "" Then
            isNew = False

            ' dtOld = _clsRecord.browseClientServicesForLog(hfMemberId.Value)

        End If

        With _clsRecord
            .initialize()
            .transId = hfMemberId.Value
            .headId = hfTransId.Value
            .memberLname = txtMemberLName.Text.Trim.ToUpper
            .memberFname = txtMemberFName.Text.Trim.ToUpper
            .memberMname = txtMemberMName.Text.Trim.ToUpper
            .memberEname = ddlMemberExt.Text.Trim.ToUpper
            .memberRelation = ddlMemberRelation.SelectedValue
            .memberBdate = CDate(dtpMemberBDate.Text).ToString("yyyy-MM-dd")
            .memberSex = ddlMemberSex.SelectedValue
            .saveProfileMember()
            hfMemberId.Value = .transId
        End With

        'dtNew = _clsRecord.browseClientServicesForLog(hfMemberId.Value)

        'Dim clsLogs As New clsAuditTrail

        'If isNew Then

        '    Dim newEntry As String = clsLogs.getTransactionNewEntry(dtNew)

        '    With clsLogs
        '        .transModule = "ADD"
        '        .transType = "PDIS : CLIENT SERVICE"
        '        .linkID = hfMemberId.Value
        '        .loggedChanges = newEntry
        '        .transBy = Session("UserName")
        '        .saveAuditTrail()
        '    End With

        'Else

        '    Dim changes As String = clsLogs.getTransactionChanges(dtOld, dtNew)

        '    If changes <> "" Then
        '        With clsLogs
        '            .transModule = "UPDATE"
        '            .transType = "PDIS : CLIENT SERVICE"
        '            .linkID = hfMemberId.Value
        '            .loggedChanges = changes
        '            .transBy = Session("UserName")
        '            .saveAuditTrail()
        '        End With
        '    End If

        'End If


        'Dim dt As New DataTable

        'dt = _clsDB.Fill_DataTable("SELECT trans_id FROM tbl_client_services " & _
        '                           "WHERE client_id = '" & hfTransId.Value & "' AND is_active = 'Y' " & _
        '                           "ORDER BY service_date DESC LIMIT 1")

        'Dim _clsInfo As New clsClientInfo

        'For Each dr As DataRow In dt.Rows

        '    With _clsInfo
        '        .transId = hfTransId.Value
        '        .currService = dr(0)
        '        .updateCurrentService()
        '    End With

        'Next


    End Sub

    Protected Sub fillGvMember()

        Dim dt As New DataTable

        Dim _clsRecords As New clsProfileMember

        dt = _clsRecords.browseProfileMember(hfTransId.Value)

        _gvMember.DataSource = dt
        _gvMember.DataBind()

    End Sub



    Protected Sub cmdGVUpdate(ByVal sender As Object, ByVal e As CommandEventArgs)
        hfMemberId.Value = e.CommandArgument
        'lblClientName.Text = txtLastName.Text & ", " & txtFirstName.Text & " " & txtExt.Text & " " & txtMiddleName.Text
        Dim _clsRecord As New clsProfileMember

        With _clsRecord
            .getProfileMember(hfMemberId.Value)
            txtMemberLName.Text = .memberLname
            txtMemberFName.Text = .memberFname
            txtMemberMName.Text = .memberMname
            ddlMemberExt.Text = .memberEname
            ddlMemberRelation.SelectedValue = .memberRelation
            dtpMemberBDate.Text = CDate(.memberBdate).ToString("yyyy-MM-dd")
            ddlMemberSex.SelectedValue = .memberSex
        End With

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlMember", "var myModal = new bootstrap.Modal(document.getElementById('mdlMember'), {});  myModal.show();", True)
    End Sub


    Protected Sub cmdGVRemove(ByVal sender As Object, ByVal e As CommandEventArgs)

        hfMemberId.Value = e.CommandArgument

        thisMsgBox.setModalType("REMOVE MEMBER")
        thisMsgBox.setConfirm(, "Are you to remove this member? <br/>" & _
                                "Name: " & CType(sender, ImageButton).Attributes("memberName") & "<br/>" & _
                                "Relation: " & CType(sender, ImageButton).Attributes("memberRelation"))

        thisMsgBox.setHeader("RED", "YELLOW")
        thisMsgBox.showConfirmBox()
    End Sub


    Private Sub updateIsActive(ByVal _thisActive As String)

        Dim _clsRecord As New clsProfileMember

        'Dim dtOld As New DataTable
        'Dim dtNew As New DataTable

        'dtOld = _clsRecord.browseClientServicesForLog(hfMemberId.Value)

        With _clsRecord
            .transId = hfMemberId.Value
            .isActive = _thisActive
            '.lastUser = Session("UserName")
            .updateIsActive()
        End With


        'If _thisActive = "N" Then
        '    Dim _clsInfo As New clsClientInfo

        '    With _clsInfo
        '        .transId = hfTransId.Value
        '        .currService = ""
        '        .updateCurrentService()
        '    End With

        'End If


        'dtNew = _clsRecord.browseClientServicesForLog(hfMemberId.Value)

        'Dim clsLogs As New clsAuditTrail

        'Dim changes As String = clsLogs.getTransactionChanges(dtOld, dtNew)

        'If changes <> "" Then
        '    With clsLogs
        '        .transModule = "UPDATE"
        '        .transType = "PDIS : CLIENT SERVICE"
        '        .linkID = hfMemberId.Value
        '        .loggedChanges = changes
        '        .transBy = Session("UserName")
        '        .saveAuditTrail()
        '    End With
        'End If


    End Sub



#End Region

End Class

