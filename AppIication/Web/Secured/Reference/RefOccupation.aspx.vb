Imports System.Data
Partial Class Secured_Reference_RefOccupation
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Session.Remove("REF_OCCUPATION_ID")

            fillGv()

        End If

    End Sub

    Protected Sub fillGv()

        Dim dt As New DataTable

        Dim _clsRecord As New clsRefOccupation

        dt = _clsRecord.browseRefOccupation(txtSearch.Text.Trim)

        _gv.DataSource = dt
        _gv.DataBind()

        lblPaging.Text = setCurrentPage(0, dt)

    End Sub

    Protected Sub _gv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles _gv.PageIndexChanging
        Session("NewPageIndex") = e.NewPageIndex
        _gv.PageIndex = e.NewPageIndex
        fillGv()
    End Sub

    Protected Sub cmdGVUpdate(ByVal sender As Object, ByVal e As CommandEventArgs)
        Session("REF_OCCUPATION_ID") = e.CommandArgument.ToString
        Response.Redirect("RefOccupationAdd.aspx")
    End Sub

    Protected Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick
        Session("REF_OCCUPATION_ID") = ""
        Response.Redirect("RefOccupationAdd.aspx")
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        fillGv()
    End Sub
End Class
