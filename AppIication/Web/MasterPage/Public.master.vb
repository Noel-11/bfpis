
Partial Class MasterPage_Public
    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub anHome_ServerClick(sender As Object, e As EventArgs) Handles anHome.ServerClick
        Response.Redirect("~/Default.aspx")
    End Sub

    Protected Sub anLogin_ServerClick(sender As Object, e As EventArgs) Handles anLogin.ServerClick
        Response.Redirect("~/Login.aspx")
    End Sub


    Protected Sub anRegister_ServerClick(sender As Object, e As EventArgs) Handles anRegister.ServerClick
        Response.Redirect("~/Registration.aspx")
    End Sub
End Class

