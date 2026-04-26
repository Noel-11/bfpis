Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsProfileHead


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

    Public Property addrBarangay As String

    Public Property addrOther As String

    Public Property birthDate As String

    Public Property sex As String

    Public Property civilStatus As String

    Public Property religion As String

    Public Property occupation As String

    Public Property monthlyIncome As String

    Public Property educLevel As String

    Public Property celNo As String

    Public Property is4ps As String

    Public Property householdNo As String

    Public Property economicStatus As String

    Public Property isActive As String

    Public Property remarks As String

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
        _addrBarangay = ""
        _addrOther = ""
        _birthDate = ""
        _sex = ""
        _civilStatus = ""
        _religion = ""
        _occupation = ""
        _monthlyIncome = ""
        _educLevel = ""
        _celNo = ""
        _is4ps = ""
        _householdNo = ""
        _economicStatus = ""
        _isActive = "Y"
        _remarks = ""
        _createUser = HttpContext.Current.Session("UserName")
        _createDate = ""
        _lastUser = HttpContext.Current.Session("UserName")
        _lastDate = ""
    End Sub


    Public Function browseProfileHead(ByVal _criteria As String) As DataTable
        Dim sql As String = ""

        Dim sqlWhere As String = ""

        Dim filter() As String = Split(_criteria, "|")

        If filter.Length > 0 Then
            If filter(0) <> "" Then
                sqlWhere += "AND last_name LIKE '" & filter(0).Trim & "%' "
            End If

            If filter(0) <> "" Then
                sqlWhere += "AND first_name LIKE '" & filter(1).Trim & "%' "
            End If
        End If

        sql = "SELECT tbl_profile_head.trans_id, last_name, first_name, middle_name, ext_name, tbl_ref_barangay.barangay AS addr_barangay, addr_other, DATE_FORMAT(birth_date,'%m/%d/%Y') AS birth_date, sex, civil_status, " & _
              "religion, occupation, monthly_income, educ_level, cel_no, is_4ps, household_no, tbl_ref_economic_status.economic_desc AS economic_status FROM tbl_profile_head " & _
              "INNER JOIN tbl_ref_barangay ON tbl_profile_head.addr_barangay = tbl_ref_barangay.barangay_code " & _
              "INNER JOIN tbl_ref_economic_status ON tbl_profile_head.economic_status = tbl_ref_economic_status.trans_id " & _
              "WHERE tbl_profile_head.is_active <> ''  " & sqlWhere & _
              "ORDER BY last_name,first_name "

        Return _clsDB.Fill_DataTable(sql, "tbl_profile_head")
    End Function


    Public Sub saveProfileHead()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,last_name,first_name,middle_name,ext_name,addr_barangay,addr_other,birth_date,sex,civil_status,religion,occupation,monthly_income,educ_level,cel_no,is_4ps,household_no,economic_status,is_active,entry_date,remarks,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_profile_head", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("last_name", _lastName)
                .ADDPARAM_CMD_String("first_name", _firstName)
                .ADDPARAM_CMD_String("middle_name", _middleName)
                .ADDPARAM_CMD_String("ext_name", _extName)
                .ADDPARAM_CMD_String("addr_barangay", _addrBarangay)
                .ADDPARAM_CMD_String("addr_other", _addrOther)
                .ADDPARAM_CMD_String("birth_date", _birthDate)
                .ADDPARAM_CMD_String("sex", _sex)
                .ADDPARAM_CMD_String("civil_status", _civilStatus)
                .ADDPARAM_CMD_String("religion", _religion)
                .ADDPARAM_CMD_String("occupation", _occupation)
                .ADDPARAM_CMD_String("monthly_income", _monthlyIncome)
                .ADDPARAM_CMD_String("educ_level", _educLevel)
                .ADDPARAM_CMD_String("cel_no", _celNo)
                .ADDPARAM_CMD_String("is_4ps", _is4ps)
                .ADDPARAM_CMD_String("household_no", _householdNo)
                .ADDPARAM_CMD_String("economic_status", _economicStatus)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("remarks", _remarks)
                .ADDPARAM_CMD_String("entry_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("create_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "last_name,first_name,middle_name,ext_name,addr_barangay,addr_other,birth_date,sex,civil_status,religion,occupation,monthly_income,educ_level,cel_no,is_4ps,household_no,economic_status,is_active,remarks,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_profile_head", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("last_name", _lastName)
                .ADDPARAM_CMD_String("first_name", _firstName)
                .ADDPARAM_CMD_String("middle_name", _middleName)
                .ADDPARAM_CMD_String("ext_name", _extName)
                .ADDPARAM_CMD_String("addr_barangay", _addrBarangay)
                .ADDPARAM_CMD_String("addr_other", _addrOther)
                .ADDPARAM_CMD_String("birth_date", _birthDate)
                .ADDPARAM_CMD_String("sex", _sex)
                .ADDPARAM_CMD_String("civil_status", _civilStatus)
                .ADDPARAM_CMD_String("religion", _religion)
                .ADDPARAM_CMD_String("occupation", _occupation)
                .ADDPARAM_CMD_String("monthly_income", _monthlyIncome)
                .ADDPARAM_CMD_String("educ_level", _educLevel)
                .ADDPARAM_CMD_String("cel_no", _celNo)
                .ADDPARAM_CMD_String("is_4ps", _is4ps)
                .ADDPARAM_CMD_String("household_no", _householdNo)
                .ADDPARAM_CMD_String("economic_status", _economicStatus)
                ' .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("remarks", _remarks)
                .ADDPARAM_CMD_String("last_user", HttpContext.Current.Session("UserName"))
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getProfileHead(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_profile_head WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _lastName = dt.Rows(0)("last_name").ToString
            _firstName = dt.Rows(0)("first_name").ToString
            _middleName = dt.Rows(0)("middle_name").ToString
            _extName = dt.Rows(0)("ext_name").ToString
            _addrBarangay = dt.Rows(0)("addr_barangay").ToString
            _addrOther = dt.Rows(0)("addr_other").ToString
            _birthDate = dt.Rows(0)("birth_date").ToString
            _sex = dt.Rows(0)("sex").ToString
            _civilStatus = dt.Rows(0)("civil_status").ToString
            _religion = dt.Rows(0)("religion").ToString
            _occupation = dt.Rows(0)("occupation").ToString
            _monthlyIncome = dt.Rows(0)("monthly_income").ToString
            _educLevel = dt.Rows(0)("educ_level").ToString
            _celNo = dt.Rows(0)("cel_no").ToString
            _is4ps = dt.Rows(0)("is_4ps").ToString
            _householdNo = dt.Rows(0)("household_no").ToString
            _economicStatus = dt.Rows(0)("economic_status").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _remarks = dt.Rows(0)("remarks").ToString
            _createUser = dt.Rows(0)("create_user").ToString
            _createDate = dt.Rows(0)("create_date").ToString
        Else
            initialize()
        End If
    End Sub


End Class
