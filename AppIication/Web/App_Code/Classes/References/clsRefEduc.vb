Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsRefEduc

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property educDesc As String

    Public Property sortOrder As String

    Public Property refPoints As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region

    Public Sub initialize()
        _transId = ""
        _educDesc = ""
        _sortOrder = getSortOrder()
        _refPoints = "0"
        _isActive = "Y"
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
    End Sub

    Private Function getSortOrder() As Integer

        Dim _cnt As Integer = 0

        _cnt = browseRefEduc("").Rows.Count

        Return _cnt + 1
    End Function

    Public Function browseRefEduc(ByVal _criteria As String) As DataTable
        Dim sql As String = ""

        Dim sqlWhere As String = ""

        If _criteria <> "" Then
            sqlWhere += "AND educ_desc LIKE '" & _criteria & "%' "
        End If

        sql = "SELECT trans_id, educ_desc, sort_order, ref_points, is_active, create_user, create_date FROM tbl_ref_educ " & _
              "WHERE  is_active <> '' " & sqlWhere & _
              "ORDER BY sort_order "

        Return _clsDB.Fill_DataTable(sql, "tbl_ref_educ")
    End Function


    Public Sub saveRefEduc()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,educ_desc,sort_order,ref_points,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_ref_educ", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("educ_desc", _educDesc)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("ref_points", _refPoints)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "educ_desc,sort_order,ref_points,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_ref_educ", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("educ_desc", _educDesc)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("ref_points", _refPoints)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getRefEduc(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_ref_educ WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _educDesc = dt.Rows(0)("educ_desc").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
            _refPoints = dt.Rows(0)("ref_points").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
