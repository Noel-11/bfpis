Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class Secured_Reference_RefServicesAdd
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Dim _btnOK As New HtmlButton

    Dim _btnNo As New HtmlButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            hfTransId.Value = Session("REF_SERVICE_ID")
            flllInfo()

        End If

        _btnOK = thisMsgBox.FindControl("btnMsgBoxYes")
        AddHandler _btnOK.ServerClick, AddressOf btnOK_Click

        _btnNo = thisMsgBox.FindControl("btnMsgBoxNo")
        AddHandler _btnNo.ServerClick, AddressOf btnNo_Click

    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "SAVE" Then

            saveRef()
            'thisMsgBox.setNotification("")
            Response.Redirect("RefServicesAdd.aspx")

        End If

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "SAVE" Then

        End If

    End Sub

    Protected Sub btnHome_ServerClick(sender As Object, e As EventArgs) Handles btnHome.ServerClick
        Response.Redirect("RefServices.aspx")
    End Sub

    Private Sub flllInfo()

        Dim _clsRefRecord As New clsRefServices

        With _clsRefRecord
            .getRefServices(hfTransId.Value)
            txtDescription.Text = .serviceDesc
            txtSortOrder.Text = .sortOrder
            rblIsactive.SelectedValue = .isActive
        End With

    End Sub

    Private Sub saveRef()

        Dim _clsRefTrainings As New clsRefServices

        With _clsRefTrainings

            .transId = hfTransId.Value
            .serviceDesc = txtDescription.Text.Trim.ToUpper
            .sortOrder = txtSortOrder.Text.Trim
            .isActive = rblIsactive.SelectedValue
            .lastUser = Session("UserName")
            .saveRefServices()

            Session("REF_EDUC_ID") = .transId
        End With

    End Sub

    Protected Sub btnSaveTraining_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        thisMsgBox.setModalType("SAVE")
        thisMsgBox.setConfirm(, "Are you sure to save this record?")
        thisMsgBox.showConfirmBox()
    End Sub


End Class

