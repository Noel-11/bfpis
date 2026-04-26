Imports System.Data
Partial Class Secured_BFPIS_BrgyProfile
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Session.Remove("CLIENT_ID")


            If Session("CLIENTINFO_FILTER") <> Nothing Then

                Dim filter() As String = Split(Session("CLIENTINFO_FILTER").ToString, "|")

                txtSearchLName.Text = filter(0).Trim
                txtSearchFName.Text = filter(1).Trim

            End If

            fillGv()

        End If

    End Sub

    Protected Sub fillGv()

        Dim dt As New DataTable

        Dim _clsRecord As New clsProfileHead

        Session("CLIENTINFO_FILTER") = txtSearchLName.Text.Trim & "|" & txtSearchFName.Text.Trim

        dt = _clsRecord.browseProfileHead(Session("CLIENTINFO_FILTER"))

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
        Session("CLIENT_ID") = e.CommandArgument.ToString
        Response.Redirect("BrgyProfileAdd.aspx")
    End Sub

    Protected Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick
        Session("CLIENT_ID") = ""
        Response.Redirect("BrgyProfileAdd.aspx")
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        fillGv()
    End Sub
End Class
