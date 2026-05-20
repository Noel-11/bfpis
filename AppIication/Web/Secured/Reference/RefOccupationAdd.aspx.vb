Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class Secured_Reference_RefOccupationAdd
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Dim _btnOK As New HtmlButton

    Dim _btnNo As New HtmlButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            hfTransId.Value = Session("REF_OCCUPATION_ID")
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
            Response.Redirect("RefOccupationAdd.aspx")

        End If

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "SAVE" Then

        End If

    End Sub

    Protected Sub btnHome_ServerClick(sender As Object, e As EventArgs) Handles btnHome.ServerClick
        Response.Redirect("RefOccupation.aspx")
    End Sub

    Private Sub flllInfo()

        Dim _clsRefRecord As New clsRefOccupation

        With _clsRefRecord
            .getRefOccupation(hfTransId.Value)
            txtDescription.Text = .occupationDesc
            txtSortOrder.Text = .sortOrder
            txtRefPoints.Text = .refPoints
            rblIsactive.SelectedValue = .isActive
        End With

    End Sub

    Private Sub saveRef()

        Dim _clsRefTrainings As New clsRefOccupation

        With _clsRefTrainings
            .initialize()
            .transId = hfTransId.Value
            .occupationDesc = txtDescription.Text.Trim.ToUpper
            .sortOrder = txtSortOrder.Text.Trim
            .refPoints = txtRefPoints.Text.Trim
            .isActive = rblIsactive.SelectedValue
            .saveRefOccupation()

            Session("REF_OCCUPATION_ID") = .transId
        End With

    End Sub

    Protected Sub btnSaveTraining_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        thisMsgBox.setModalType("SAVE")
        thisMsgBox.setConfirm(, "Are you sure to save this record?")
        thisMsgBox.showConfirmBox()
    End Sub


End Class

