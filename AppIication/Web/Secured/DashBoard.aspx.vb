Imports System.Data
Partial Class Secured_DashBoard
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase
   
    Dim _dtGVForInspection As New DataTable
    Dim _dtGVReturnInspection As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim thisYear As Integer = DateTime.Now.Year + 1

            For i = thisYear To (thisYear - 4) Step -1
                ddlChartYear.Items.Add(New ListItem(i, i))
            Next

            ddlChartYear.SelectedValue = DateTime.Now.Year

            getDetails()
           
            getColumnChartCategory(ddlCntBy.SelectedValue)

        End If

    End Sub



    Protected Sub ddlAPPFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAPPFilter.SelectedIndexChanged
        lblAPPCnt.Text = getStatusCnt(ddlAPPFilter.SelectedValue)
        lblAPPFilter.InnerText = ddlAPPFilter.SelectedItem.Text
    End Sub


    Private Sub getDetails()

        ddlAPPFilter.SelectedValue = "Today"


        lblAPPCnt.Text = getStatusCnt(ddlAPPFilter.SelectedValue)
        lblAPPFilter.InnerText = ddlAPPFilter.SelectedItem.Text


    End Sub


    Private Function getStatusCnt(ByVal _thisPeriod As String) As Integer

        Dim _clsDB As New clsDatabase

        Dim sqlWhere As String = ""

        If _thisPeriod = "Today" Then
            sqlWhere += " AND service_date = '" & DateTime.Now.ToString("yyyy-MM-dd") & "' "
        ElseIf _thisPeriod = "Month" Then
            sqlWhere += " AND DATE_FORMAT(service_date,'%Y-%m') = '" & DateTime.Now.ToString("yyyy-MM") & "' "
        End If

        Dim dt As New DataTable
        Dim _cnt As Integer = 0

        Dim sql As String = ""

        sql = "SELECT COUNT(*) FROM tbl_client_services " & _
              "WHERE is_active = 'Y' " & sqlWhere

        dt = _clsDB.Fill_DataTable(sql)

        _cnt = dt.Rows(0)(0)

        Return _cnt

    End Function

    Protected Sub btnApp_ServerClick(sender As Object, e As EventArgs) Handles btnApp.ServerClick

        'Session("TAGSTATUS") = "APPROVED"
        Response.Redirect("Services/ClientRecord.aspx")

    End Sub


    Private Sub getColumnChartCategory(ByVal _thisCat As String)

        Dim sql As String = ""
        Dim dt As New DataTable

        Dim cat As String = ""
        Dim catCnt As String = ""

        Dim sqlWhere As String = ""

        If ddlChartYear.SelectedValue <> "" Then
            sqlWhere += " AND YEAR(service_date) = '" & ddlChartYear.SelectedValue & "' "
        End If

        If _thisCat = "BARANGAY" Then

            sql = "SELECT tbl_ref_barangay.barangay AS cat, " & _
                  "COUNT(*) AS catCnt " & _
                  "FROM tbl_client_services " & _
                  "INNER JOIN tbl_client_info ON tbl_client_services.client_id = tbl_client_info.trans_id " & _
                  "INNER JOIN tbl_ref_barangay ON tbl_client_info.barangay = tbl_ref_barangay.barangay_code " & _
                  "WHERE tbl_client_services.is_active = 'Y' " & sqlWhere & _
                  "GROUP BY tbl_ref_barangay.barangay " & _
                  "ORDER BY tbl_ref_barangay.barangay"

        ElseIf _thisCat = "MONTH" Then

            sql = "SELECT MONTH(tbl_client_services.service_date) AS cat, " & _
                  "COUNT(*) AS catCnt " & _
                  "FROM tbl_client_services " & _
                  "WHERE tbl_client_services.is_active = 'Y' " & sqlWhere & _
                  "GROUP BY MONTH(tbl_client_services.service_date) " & _
                  "ORDER BY MONTH(tbl_client_services.service_date) "

        ElseIf _thisCat = "SERVICES" Then
            sql = "SELECT tbl_ref_services.service_desc AS cat, " & _
                  "COUNT(*) AS catCnt " & _
                  "FROM tbl_client_services " & _
                  "INNER JOIN tbl_ref_services ON tbl_client_services.service = tbl_ref_services.trans_id " & _
                  "WHERE tbl_client_services.is_active = 'Y' " & sqlWhere & _
                  "GROUP BY tbl_ref_services.trans_id " & _
                  "ORDER BY tbl_ref_services.sort_order"

        End If

        dt = _clsDB.Fill_DataTable(sql)

        Dim cnt As Integer = 0
        Dim prefix As String = ""

        For Each dr As DataRow In dt.Rows

            If cnt > 0 Then
                prefix = ","
            Else
                prefix = ""
            End If

            If _thisCat = "MONTH" Then
                cat += prefix & "'" & DateAndTime.MonthName(CInt(dr("cat"))) & "'"
            Else
                cat += prefix & "'" & dr("cat") & "'"
            End If



            catCnt += prefix & dr("catCnt")

            cnt += 1
        Next

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "columnChart", "" & _
                                                                  "document.addEventListener('DOMContentLoaded', () => { " & _
                                                                  "new ApexCharts(document.querySelector('#columnChart'), { " & _
                                                                  "series: [{" & _
                                                                  "name: 'Count'," & _
                                                                  "data: [" & catCnt & "] " & _
                                                                  "}], " & _
                                                                  "chart: { " & _
                                                                  "type: 'bar', " & _
                                                                  "height: 350 " & _
                                                                  "}, " & _
                                                                  "plotOptions:{ " & _
                                                                  "bar: { " & _
                                                                  "horizontal: false, " & _
                                                                  "columnWidth: '55%', " & _
                                                                  "endingShape: 'rounded' " & _
                                                                  "} " & _
                                                                  "}, " & _
                                                                  "dataLabels: { " & _
                                                                  "enabled: false " & _
                                                                  "}, " & _
                                                                  "stroke: { " & _
                                                                  "show: true, " & _
                                                                  "width: 2, " & _
                                                                  "colors: ['transparent'] " & _
                                                                  "}, " & _
                                                                  "xaxis: { " & _
                                                                  "categories: [" & cat & "] " & _
                                                                  "}, " & _
                                                                  "yaxis: { " & _
                                                                  "title: { " & _
                                                                  "text: 'Validated Counts' " & _
                                                                  "} " & _
                                                                  "}, " & _
                                                                  "fill: { " & _
                                                                  "opacity: 1 " & _
                                                                  "}, " & _
                                                                  "tooltip: { " & _
                                                                  "y: { " & _
                                                                  "formatter: function(val) { " & _
                                                                  "return val; " & _
                                                                  "} " & _
                                                                  "} " & _
                                                                  "} " & _
                                                                  "}).render();" & _
                                                                  "});", True)

    End Sub


    Protected Sub ddlChartYear_TextChanged(sender As Object, e As EventArgs) Handles ddlChartYear.TextChanged

        'Select Case ddlCntBy.SelectedValue
        '    Case "STATUS"
        '        getColumnChartStatus()
        '    Case "BARANGAY"
        '        getColumnChartCategory()
        'End Select

        getColumnChartCategory(ddlCntBy.SelectedValue)

    End Sub

    Protected Sub ddlCntBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCntBy.SelectedIndexChanged
        'Select Case ddlCntBy.SelectedValue
        '    Case "STATUS"
        '        getColumnChartStatus()
        '    Case "BARANGAY"
        '        getColumnChartCategory()
        'End Select

        getColumnChartCategory(ddlCntBy.SelectedValue)

    End Sub

 
End Class
