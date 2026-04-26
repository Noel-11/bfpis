Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsClientServices

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property clientId As String

    Public Property service As String

    Public Property serviceDate As String

    Public Property serviceAmount As String

    Public Property serviceRemarks As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _clientId = ""
        _service = ""
        _serviceDate = ""
        _serviceAmount = "0"
        _serviceRemarks = ""
        _isActive = "Y"
        _createUser = ""
        ' _createDate = ""
        _lastUser = ""
        '_lastDate = ""
    End Sub


    Public Function browseClientServices(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT tbl_client_services.trans_id, client_id, tbl_ref_services.service_desc AS service, DATE_FORMAT(service_date,'%m/%d/%Y') AS service_date, " & _
              "service_amount, tbl_client_services.is_active, tbl_client_services.create_user, tbl_client_services.create_date, service_remarks FROM tbl_client_services " & _
              "INNER JOIN tbl_ref_services ON tbl_client_services.service = tbl_ref_services.trans_id " & _
              "WHERE tbl_client_services.is_active = 'Y' AND client_id = '" & _thisId & "' " & _
              "ORDER BY service_date DESC "

        Return _clsDB.Fill_DataTable(sql, "tbl_client_services")
    End Function

    Public Function browseClientServicesForLog(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT client_id,service,service_date,service_amount,service_remarks,is_active FROM tbl_client_services " & _
               "WHERE trans_id = '" & _thisId & "' LIMIT 1 "

        Return _clsDB.Fill_DataTable(sql, "tbl_client_services")
    End Function


    Public Sub saveClientServices()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,client_id,service,service_date,service_amount,service_remarks,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_client_services", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("client_id", _clientId)
                .ADDPARAM_CMD_String("service", _service)
                .ADDPARAM_CMD_String("service_date", _serviceDate)
                .ADDPARAM_CMD_String("service_amount", _serviceAmount)
                .ADDPARAM_CMD_String("service_remarks", _serviceRemarks)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "client_id,service,service_date,service_amount,service_remarks,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_client_services", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("client_id", _clientId)
                .ADDPARAM_CMD_String("service", _service)
                .ADDPARAM_CMD_String("service_date", _serviceDate)
                .ADDPARAM_CMD_String("service_amount", _serviceAmount)
                .ADDPARAM_CMD_String("service_remarks", _serviceRemarks)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub updateIsActive()

        With _clsDB.dbUtility
            .fieldItems = "is_active,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_client_services", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub getClientServices(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_client_services WHERE trans_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _clientId = dt.Rows(0)("client_id").ToString
            _service = dt.Rows(0)("service").ToString
            _serviceDate = dt.Rows(0)("service_date").ToString
            _serviceAmount = dt.Rows(0)("service_amount").ToString
            _serviceRemarks = dt.Rows(0)("service_remarks").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
