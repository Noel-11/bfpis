Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsRefsMaterialRoof

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property refDesc As String

    Public Property refPoints As String

    Public Property sortOrder As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _refDesc = ""
        _refPoints = ""
        _sortOrder = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
    End Sub


    Public Function browseRefsMaterialRoof(ByVal _criteria As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT trans_id, ref_desc, ref_points, sort_order, is_active, create_user, create_date, last_user, last_date FROM tbl_refs_material_roof " & _
              "WHERE is_active <> '' ORDER BY ref_desc "

        Return _clsDB.Fill_DataTable(sql, "tbl_refs_material_roof")

    End Function


    Public Sub saveRefsMaterialRoof()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,ref_desc,ref_points,sort_order,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_refs_material_roof", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("ref_desc", _refDesc)
                .ADDPARAM_CMD_String("ref_points", _refPoints)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "ref_desc,ref_points,sort_order,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_refs_material_roof", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("ref_desc", _refDesc)
                .ADDPARAM_CMD_String("ref_points", _refPoints)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getRefsMaterialRoof(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_refs_material_roof WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _refDesc = dt.Rows(0)("ref_desc").ToString
            _refPoints = dt.Rows(0)("ref_points").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
