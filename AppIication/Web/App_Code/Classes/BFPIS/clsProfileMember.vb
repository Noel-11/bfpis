Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsProfileMember

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property headId As String

    Public Property memberLname As String

    Public Property memberFname As String

    Public Property memberMname As String

    Public Property memberEname As String

    Public Property memberRelation As String

    Public Property memberBdate As String

    Public Property memberSex As String

    Public Property isActive As String

    Public Property memberRemarks As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region

    Public Sub initialize()
        _transId = ""
        _headId = ""
        _memberLname = ""
        _memberFname = ""
        _memberMname = ""
        _memberEname = ""
        _memberRelation = ""
        _memberBdate = ""
        _memberSex = ""
        _isActive = "Y"
        _memberRemarks = ""
        _createUser = HttpContext.Current.Session("UserName")
        _createDate = ""
        _lastUser = HttpContext.Current.Session("UserName")
        _lastDate = ""
    End Sub


    Public Function browseProfileMember(ByVal _thisId As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT tbl_profile_member.trans_id, head_id, CONCAT(member_lname,', ',member_fname,' ',member_ename,' ',member_mname) AS memberName," & _
              "member_lname, member_fname, member_mname, member_ename, " & _
              "tbl_ref_relation.relation_desc AS member_relation, DATE_FORMAT(member_bdate,'%m/%d/%Y') AS member_bdate, " & _
              "member_sex, member_remarks FROM tbl_profile_member " & _
              "INNER JOIN tbl_ref_relation ON tbl_profile_member.member_relation = tbl_ref_relation.trans_id " & _
              "WHERE tbl_profile_member.is_active = 'Y' AND head_id = '" & _thisId & "' " & _
              "ORDER BY tbl_ref_relation.sort_order,member_lname, member_fname "

        Return _clsDB.Fill_DataTable(sql, "tbl_profile_member")
    End Function


    Public Sub saveProfileMember()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,head_id,member_lname,member_fname,member_mname,member_ename,member_relation,member_bdate,member_sex,is_active,member_remarks,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_profile_member", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("head_id", _headId)
                .ADDPARAM_CMD_String("member_lname", _memberLname)
                .ADDPARAM_CMD_String("member_fname", _memberFname)
                .ADDPARAM_CMD_String("member_mname", _memberMname)
                .ADDPARAM_CMD_String("member_ename", _memberEname)
                .ADDPARAM_CMD_String("member_relation", _memberRelation)
                .ADDPARAM_CMD_String("member_bdate", _memberBdate)
                .ADDPARAM_CMD_String("member_sex", _memberSex)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("member_remarks", _memberRemarks)
                .ADDPARAM_CMD_String("create_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "head_id,member_lname,member_fname,member_mname,member_ename,member_relation,member_bdate,member_sex,is_active,member_remarks,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_profile_member", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("head_id", _headId)
                .ADDPARAM_CMD_String("member_lname", _memberLname)
                .ADDPARAM_CMD_String("member_fname", _memberFname)
                .ADDPARAM_CMD_String("member_mname", _memberMname)
                .ADDPARAM_CMD_String("member_ename", _memberEname)
                .ADDPARAM_CMD_String("member_relation", _memberRelation)
                .ADDPARAM_CMD_String("member_bdate", _memberBdate)
                .ADDPARAM_CMD_String("member_sex", _memberSex)
                '.ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("member_remarks", _memberRemarks)
                .ADDPARAM_CMD_String("last_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub updateIsActive()

        With _clsDB.dbUtility
            .fieldItems = "is_active,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_profile_member", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("last_user", HttpContext.Current.Session("UserName"))
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub getProfileMember(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_profile_member WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _headId = dt.Rows(0)("head_id").ToString
            _memberLname = dt.Rows(0)("member_lname").ToString
            _memberFname = dt.Rows(0)("member_fname").ToString
            _memberMname = dt.Rows(0)("member_mname").ToString
            _memberEname = dt.Rows(0)("member_ename").ToString
            _memberRelation = dt.Rows(0)("member_relation").ToString
            _memberBdate = dt.Rows(0)("member_bdate").ToString
            _memberSex = dt.Rows(0)("member_sex").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _memberRemarks = dt.Rows(0)("member_remarks").ToString
        Else
            initialize()
        End If
    End Sub


End Class
