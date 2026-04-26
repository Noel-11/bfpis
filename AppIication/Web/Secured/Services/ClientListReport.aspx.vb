Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class Secured_Services_ClientListReport
    Inherits cPageInit_Secured_BS

    Dim _clsDB As New clsDatabase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Session.Remove("TRAINING_ID")

            _clsDB.populateDDLB(ddlBarangay, "barangay", "barangay_code", "tbl_ref_barangay", "barangay", , "ALL", "")

            _clsDB.populateDDLB(ddlService, "service_desc", "trans_id", "tbl_ref_services", "sort_order", , "ALL", "")
         
            dtpDateFrom.Text = DateTime.Now.Year & "-" & DateTime.Now.Month.ToString("00") & "-01"
            dtpDateTo.Text = DateTime.Now.Year & "-" & DateTime.Now.Month.ToString("00") & "-" & DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString("00")

        End If

    End Sub

   
#Region "REPORT"

    Protected Sub btnPrint_ServerClick(sender As Object, e As EventArgs) Handles btnPrint.ServerClick

        generateReport()
        lblReportHeadName.Text = "Report: Client List"
        ' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlPrintReport", "var myModal = new bootstrap.Modal(document.getElementById('mdlPrintReport'), {});  myModal.show();", True)

    End Sub


    Public Sub generateReport()
        Try
            Dim warnings() As Warning
            Dim streamIds() As String
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty
            'Dim agerange As String
            Dim rvPrint As ReportViewer = New ReportViewer

            rvPrint.ProcessingMode = ProcessingMode.Local
            rvPrint.LocalReport.ReportPath = Server.MapPath("~/Secured/Report/rptServiceList.rdlc")

            Dim dsService As New ReportDataSource("dsService", getDetails())

            rvPrint.LocalReport.DataSources.Clear()
            rvPrint.LocalReport.DataSources.Add(dsService)

            Dim datePeriod As String = ""

            If dtpDateFrom.Text.Trim = dtpDateTo.Text.Trim Then
                datePeriod = dtpDateFrom.Text.Trim
            Else
                datePeriod = CDate(dtpDateFrom.Text).ToString("MM/dd/yyyy") & "-" & CDate(dtpDateTo.Text).ToString("MM/dd/yyyy")
            End If


            Dim paramDate As New ReportParameter("paramDate", datePeriod)
          
            Dim paramBarangay As New ReportParameter("paramBarangay", IIf(ddlBarangay.SelectedValue = "", "ALL", ddlBarangay.SelectedItem.Text.ToString).ToString)
            Dim paramService As New ReportParameter("paramService", IIf(ddlService.SelectedValue = "", "ALL", ddlService.SelectedItem.Text.ToString).ToString)

            rvPrint.LocalReport.SetParameters(New ReportParameter() {paramDate, paramBarangay, paramService})

            rvPrint.LocalReport.Refresh()

            Dim bytes() As Byte = rvPrint.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamIds, warnings)

            Session("pdfBytes") = bytes

            ltEmbed.Text = String.Format("<object data=""{0}{1}"" type=""application/pdf"" width=""100%"" height=""700px""></object>", ResolveUrl("~/ReportHandler.ashx"), "")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'ReportsMsgBox.show("PAYROLL")
    End Sub

    Private Function getDetails() As DataTable

        Dim sql As String = ""

        Dim sqlWhere As String = ""

        If dtpDateFrom.Text <> "" And dtpDateTo.Text <> "" Then
            sqlWhere += "AND (service_date BETWEEN '" & dtpDateFrom.Text & "' AND '" & dtpDateTo.Text & "') "
        End If

        If ddlBarangay.SelectedValue <> "" Then
            sqlWhere += "AND  tbl_client_info.barangay = '" & ddlBarangay.SelectedValue & "' "
        End If

        If ddlService.SelectedValue <> "" Then
            sqlWhere += "AND  tbl_client_services.service = '" & ddlService.SelectedValue & "' "
        End If

     

        sql = "SELECT COALESCE(CASE WHEN tbl_ref_services.service_desc = 'OTHERS' THEN CONCAT(service_desc,' (', service_remarks,')') ELSE service_desc END) AS service_desc,+-" & _
              "DATE_FORMAT(tbl_client_services.service_date,'%m/%d/%Y') AS service_date, " & _
              "last_name,first_name, middle_name, ext_name, tbl_ref_barangay.barangay,DATE_FORMAT(birth_date,'%m/%d/%Y') AS birth_date, " & _
              "cp_number FROM tbl_client_info " & _
              "INNER JOIN tbl_ref_barangay ON tbl_client_info.barangay = tbl_ref_barangay.barangay_code " & _
              "INNER JOIN tbl_client_services ON tbl_client_info.trans_id = tbl_client_services.client_id AND " & _
              "tbl_client_services.is_active = 'Y' " & _
              "INNER JOIN tbl_ref_services ON tbl_client_services.service = tbl_ref_services.trans_id " & _
              "WHERE tbl_client_info.is_active = 'Y' " & sqlWhere & _
              "ORDER BY service_date,last_name,first_name"

        Dim dt As New DataTable

        dt = _clsDB.Fill_DataTable(sql, "tbl_applicants")

        Return dt

    End Function




#End Region


End Class
