Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsClientInfo

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property lastName As String

    Public Property firstName As String

    Public Property middleName As String

    Public Property extName As String

    Public Property barangay As String

    Public Property birthDate As String

    Public Property cpNumber As String

    Public Property voter As String

    Public Property currService As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _lastName = ""
        _firstName = ""
        _middleName = ""
        _extName = ""
        _barangay = "0"
        _birthDate = Nothing
        _cpNumber = ""
        _voter = ""
        _currService = ""
        _isActive = "Y"
        _createUser = ""
        ' _createDate = ""
        _lastUser = ""
        ' _lastDate = ""
    End Sub


    Public Function browseClientInfo(ByVal _criteria As String) As DataTable
        Dim sql As String = ""

        Dim sqlWhere As String = ""

        Dim filter() As String = Split(_criteria, "|")

        If filter.Length > 0 Then
            If filter(0) <> "" Then
                sqlWhere += "AND (last_name LIKE '" & filter(0).Trim & "%' OR first_name LIKE '" & filter(0).Trim & "%' OR middle_name LIKE '" & filter(0).Trim & "%') "
            End If

            'If filter(0) <> "" Then
            '    sqlWhere += "AND first_name LIKE '" & filter(1).Trim & "%' "
            'End If
        End If

        sql = "SELECT tbl_client_info.trans_id, last_name, first_name, middle_name, ext_name, tbl_ref_barangay.barangay, " & _
              "DATE_FORMAT(birth_date,'%m/%d/%Y') AS birth_date, cp_number, voter, " & _
              "COALESCE(CASE WHEN tbl_ref_services.service_desc = 'OTHERS' THEN CONCAT(service_desc,' (', service_remarks,')') ELSE service_desc END) AS curr_service, " & _
              "COALESCE(service_date,'') AS service_date, DATEDIFF(NOW(),COALESCE(service_date,NOW())) AS serviceDays FROM tbl_client_info " & _
              "INNER JOIN tbl_ref_barangay ON tbl_client_info.barangay = tbl_ref_barangay.barangay_code " & _
              "LEFT JOIN tbl_client_services ON tbl_client_info.curr_service = tbl_client_services.trans_id " & _
              "LEFT JOIN tbl_ref_services ON tbl_client_services.service = tbl_ref_services.trans_id " & _
              "WHERE tbl_client_info.is_active <> '' " & sqlWhere & _
              "ORDER BY last_name,first_name ASC "

        Return _clsDB.Fill_DataTable(sql, "tbl_client_info")
    End Function

    Public Function browseClientInfoForLog(ByVal _thisId As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT last_name, first_name, middle_name, ext_name, barangay, birth_date, " & _
              "cp_number, voter,curr_service,is_active FROM tbl_client_info " & _
              "WHERE trans_id = '" & _thisId & "' LIMIT 1"

        Return _clsDB.Fill_DataTable(sql, "tbl_client_info")
    End Function


    Public Sub saveClientInfo()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,last_name,first_name,middle_name,ext_name,barangay,birth_date,cp_number,voter,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_client_info", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("last_name", _lastName)
                .ADDPARAM_CMD_String("first_name", _firstName)
                .ADDPARAM_CMD_String("middle_name", _middleName)
                .ADDPARAM_CMD_String("ext_name", _extName)
                .ADDPARAM_CMD_String("barangay", _barangay)
                .ADDPARAM_CMD_String("birth_date", _birthDate)
                .ADDPARAM_CMD_String("cp_number", _cpNumber)
                .ADDPARAM_CMD_String("voter", _voter)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "last_name,first_name,middle_name,ext_name,barangay,birth_date,cp_number,voter,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_client_info", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("last_name", _lastName)
                .ADDPARAM_CMD_String("first_name", _firstName)
                .ADDPARAM_CMD_String("middle_name", _middleName)
                .ADDPARAM_CMD_String("ext_name", _extName)
                .ADDPARAM_CMD_String("barangay", _barangay)
                .ADDPARAM_CMD_String("birth_date", _birthDate)
                .ADDPARAM_CMD_String("cp_number", _cpNumber)
                .ADDPARAM_CMD_String("voter", _voter)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub updateCurrentService()

       

        With _clsDB.dbUtility
            .fieldItems = "curr_service,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_client_info", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("curr_service", _currService)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub getClientInfo(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_client_info WHERE trans_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _lastName = dt.Rows(0)("last_name").ToString
            _firstName = dt.Rows(0)("first_name").ToString
            _middleName = dt.Rows(0)("middle_name").ToString
            _extName = dt.Rows(0)("ext_name").ToString
            _barangay = dt.Rows(0)("barangay").ToString
            _birthDate = dt.Rows(0)("birth_date").ToString
            _cpNumber = dt.Rows(0)("cp_number").ToString
            _voter = dt.Rows(0)("voter").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _createUser = dt.Rows(0)("create_user").ToString
            _createDate = dt.Rows(0)("create_date").ToString
        Else
            initialize()
        End If
    End Sub



End Class
