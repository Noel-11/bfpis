
Partial Class MasterPage_Applicant
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select", "$('.ddlSel').select2({theme: 'bootstrap-5',dropdownAutoWidth:'true',});", True)

        ''Modal DDL

        '' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select2", "$('.ddlSelM').select2({theme: 'bootstrap-5',dropdownParent: $('.modalHead'),width: $( this ).data( 'width' ) ? $( this ).data( 'width' ) : $( this ).hasClass( 'w-100' ) ? '100%' : 'style',placeholder: $( this ).data( 'placeholder' ),});", True)

        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select2", "$('.ddlSelM').select2({theme: 'bootstrap-5',dropdownParent: $('.modalHead'),dropdownAutoWidth:'true',});", True)

        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select3", "$('.ddlSelM2').select2({theme: 'bootstrap-5',dropdownParent: $('.modalHead2'),dropdownAutoWidth:'true',});", True)

        'User Details
        If Not Page.IsPostBack Then
            setMenuActiveCSS()

        End If

    End Sub

    Private Sub setMenuActiveCSS()

        Dim _activeMenu As String = "btnDashboard"

        If Not IsNothing(Session("ACTIVEMENU")) Then
            _activeMenu = Session("ACTIVEMENU")
            
        End If

        For Each ctrl As Control In divMenu.Controls

            If TypeOf ctrl Is Button Then

                Dim _ctrlButton As Button = CType(ctrl, Button)

                _ctrlButton.Attributes.Add("class", "")

                If _activeMenu = _ctrlButton.ID Then
                    _ctrlButton.Attributes("class") = "btn btn-outline-success w-100"
                Else
                    _ctrlButton.Attributes("class") = "btn btn-green w-100"
                End If

            End If

        Next

    End Sub

    Protected Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Session("ACTIVEMENU") = CType(sender, Button).ID
        Response.Redirect("~/Secured/Applicant/AppDashBoard.aspx")

    End Sub

    Protected Sub btnAvailable_Click(sender As Object, e As EventArgs) Handles btnAvailable.Click
        Session("ACTIVEMENU") = CType(sender, Button).ID
        Response.Redirect("~/Secured/Applicant/AppAvailable.aspx")
    End Sub

    Protected Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click
        Session("ACTIVEMENU") = CType(sender, Button).ID
        Response.Redirect("~/Secured/Applicant/AppComplete.aspx")
    End Sub

    Protected Sub btnUpcoming_Click(sender As Object, e As EventArgs) Handles btnUpcoming.Click
        Session("ACTIVEMENU") = CType(sender, Button).ID
        Response.Redirect("~/Secured/Applicant/AppUpcoming.aspx")
    End Sub

    Protected Sub aProfile_ServerClick(sender As Object, e As EventArgs) Handles aProfile.ServerClick
        Response.Redirect("~/Secured/Applicant/AppProfile.aspx")
    End Sub

    Protected Sub aLogout_ServerClick(sender As Object, e As EventArgs) Handles aLogout.ServerClick
        Session.Abandon()
        Response.Redirect("~/Login.aspx")
    End Sub
   
End Class

