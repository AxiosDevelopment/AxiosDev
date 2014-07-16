Imports System.Configuration  'Contains function to read connection string from web.config
Imports System.Data.SqlClient 'SQL Client Library


Public Class dbUtil

    '
    ' Module-level variable to hold connection string for subsequent processing operations:
    '
    Private m_ConStr As String = ""
    Private m_ProfileImageDir As String = ""
    Private m_WebLogDetailID As Long = 0

    '
    ' Connection String is automatically read from Web.Config during 'New' constructor operation
    '
    Public Sub New()
        m_ConStr = ConfigurationManager.ConnectionStrings("ConnectionString").ToString()
        'm_ProfileImageDir = ConfigurationManager.AppSettings("profileImageDir").ToString()
        'Me.Write() 'Log("New DB: " & m_ConStr)
    End Sub

    Public Sub WriteLog(ByRef sMsg As String)
        System.Diagnostics.Debug.WriteLine(sMsg)
    End Sub

    '
    ' Property ConStr returns connection string as retrieved from Web.Config. May be used externally to open a connection and process data
    '
    Public ReadOnly Property ConStr() As String
        Get
            Return m_ConStr
        End Get
    End Property

    '
    ' Property ProfileImageDir returns url segment as retrieved from Web.Config.
    '
    Public ReadOnly Property ProfileImageDir() As String
        Get
            Return m_ProfileImageDir
        End Get
    End Property

    Public Shared Function ConvertDate(ByVal sTheDate As String) As String
        Return Mid(sTheDate, 6, 2) & "-" & Right(sTheDate, 2) & "-" & Left(sTheDate, 4)
    End Function

    Public Shared Function ConvertToInternalDate(ByVal dtTheDate As DateTime) As String
        Return DatePart(DateInterval.Year, dtTheDate).ToString("0000-") & DatePart(DateInterval.Month, dtTheDate).ToString("00-") & DatePart(DateInterval.Day, dtTheDate).ToString("00")
    End Function

    'Public Shared Function ConvertToDisplaydate(ByVal dtTheDate As DateTime) As String
    '    Return DatePart(DateInterval.Month, dtTheDate).ToString("00/") & DatePart(DateInterval.Day, dtTheDate).ToString("00/") & DatePart(DateInterval.Year, dtTheDate).ToString("0000")
    'End Function
    Public Shared Function LastDayOfMonth(ByVal theDate)
        Dim lstDateCurMonth, fstDateCurMonth, fstDateNxtMonth
        Dim YYYY, MM
        YYYY = DatePart("yyyy", theDate)
        MM = DatePart("m", theDate)
        fstDateCurMonth = MM & "/1/" & YYYY
        fstDateNxtMonth = DateAdd("m", 1, fstDateCurMonth)
        lstDateCurMonth = DateAdd("d", -1, fstDateNxtMonth)
        Return lstDateCurMonth
    End Function

    'Public Shared Function EventFulDate(ByVal theDate)
    '    Dim YYYY, MM, DD
    '    YYYY = DatePart("yyyy", theDate)
    '    MM = DatePart("m", theDate)
    '    If MM < 10 Then
    '        MM = "0" & MM
    '    End If
    '    DD = DatePart("d", theDate)
    '    If DD < 10 Then
    '        DD = "0" & DD
    '    End If
    '    Return YYYY & MM & DD & "00"
    'End Function

    Public Shared Function ConvertStartDate(ByVal theDate As DateTime, ByVal bStart As Boolean)
        If bStart Then
            Return dbUtil.ConvertToInternalDate(theDate) & " 00:00:00"
        Else
            Return dbUtil.ConvertToInternalDate(theDate) & " 23:59:59"
        End If
    End Function

    'Public Shared Function ConvertEventfulDateTime(ByVal theDateTime)
    '    Dim theDate
    '    Dim theTime
    '    If Len(theDateTime) > 0 Then
    '        theDate = Left(theDateTime, 10)
    '        theTime = Right(theDateTime, 8)
    '        If CInt(Left(theTime, 2)) > 12 Then
    '            theTime = CStr(CInt(Left(theTime, 2)) - 12) & ":" & Mid(theTime, 4, 2) & " PM"
    '        Else
    '            theTime = Left(theTime, 2) & ":" & Mid(theTime, 4, 2) & " AM"
    '        End If
    '        Return Mid(theDate, 6, 2) & "-" & Right(theDate, 2) & "-" & Left(theDate, 4) & " " & theTime
    '    Else
    '        Return ""
    '    End If
    'End Function

    'Public Shared Function ConvertGSDate(ByVal theDate)
    '    Dim YYYY, MM, DD
    '    YYYY = DatePart("yyyy", theDate)
    '    MM = DatePart("m", theDate)
    '    If MM < 10 Then
    '        MM = "0" & MM
    '    End If
    '    DD = DatePart("d", theDate)
    '    If DD < 10 Then
    '        DD = "0" & DD
    '    End If
    '    Return YYYY & "-" & MM & "-" & DD

    'End Function

    'Public Shared Function ConvertTo12HrTime(ByVal theTime)
    '    If CInt(Left(theTime, 2)) > 12 Then
    '        Return CStr(CInt(Left(theTime, 2)) - 12) & ":" & Mid(theTime, 4, 2) & " PM"
    '    Else
    '        Return Left(theTime, 2) & ":" & Mid(theTime, 4, 2) & " AM"
    '    End If

    'End Function

    Public Shared Function FormatPhone(ByVal p As String) As String

        If Len(p) = 10 Then
            'valid number
            Return "(" & Left(p, 3) & ")" & Mid(p, 4, 3) & "-" & Right(p, 4)
        Else
            Return "N/A"
        End If
    End Function


    '
    ' GetDataReader returns an open data reader which can be processed in the caller. Once processed, you must close reader to close the
    ' connection. For example:
    '
    '  dim db as dbUtil = New dbUtil() 'Get a new instance of this class. The connection string is automatically read from web.config
    '                                  'during the construction of the new object (see 'New' sub above)
    '  dim dr as SqlDataReader = db.GetDataReader("select ID, ImageURL, Description from MyTable")
    '  dim sb as StringBuilder = New StringBuilder() 'Use StringBuilder instead of String to improve performance
    '  If dr.HasRows Then 'Can skip this step if you are processing more than one row with the 'While' loop
    '    While dr.Read '(loads the row for processing)
    '      sb.Append("ID: " & dr(0) & " ImageURL: " & dr(1) & " Desc: " & dr(2) & "<br>")
    '    End While
    '    dr.Close() ' must close the DataReader to close the connection
    '    Response.Write(sb.ToString())
    '  End If
    '
    Public Function GetDataReader(ByVal sSQL As String) As SqlDataReader
        WriteLog("db.GetDataReader:" & sSQL)
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection) ' connection will remain open until caller closes the reader
        Return DR
    End Function

    ''
    '' Inserts record in same manner as asp code ... Should be eliminated in favor of specific, parameterized queries.
    ''
    'Public Function InsertRecord(ByVal sSQL As String, ByVal sInsertTable As String, ByVal sNewIDColName As String) As String
    '    Dim sRet As String = ""
    '    Using dbConn As New SqlConnection(Me.ConStr)
    '        dbConn.Open()
    '        Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
    '        Dim dbTran As SqlTransaction = dbConn.BeginTransaction("InsertRecord")
    '        dbCmd.Transaction = dbTran

    '        Try
    '            dbCmd.ExecuteNonQuery()
    '            dbCmd.CommandText = "select max(" & sNewIDColName & ") as NewID from " & sInsertTable
    '            Dim dr As SqlDataReader = dbCmd.ExecuteReader(CommandBehavior.SingleRow)
    '            If dr.HasRows Then
    '                dr.Read()
    '                sRet = dr(0).ToString()
    '            End If
    '            dr.Close()
    '            dbTran.Commit()

    '        Catch ex As Exception
    '            sRet = ""
    '            Try ' attempt a rollback
    '                dbTran.Rollback()

    '            Catch ex2 As Exception
    '                ' Log this in event log and error above as well

    '            End Try

    '        End Try
    '        dbConn.Close()
    '    End Using
    '    Return sRet
    'End Function

    '
    ' Exists(sSQL) returns a boolean True if sSQL produces a result row, otherwise false. Connection is closed prior to return.
    '
    Public Function Exists(ByVal sSQL As String) As Boolean
        WriteLog("db.Exists:" & sSQL)
        Dim bRet As Boolean = False

        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            Dim dbDR As SqlDataReader = dbCmd.ExecuteReader(CommandBehavior.SingleRow)
            bRet = dbDR.HasRows
            dbDR.Close()
            dbConn.Close()
        End Using
        Return bRet
    End Function

    '
    ' GetStrID(sSQL) returns an ID (int) converted to a string from a the first column of a single row. Connection is closed prior to return.
    '
    Public Function GetStrID(ByVal sSQL As String) As String
        WriteLog("db.GetStrID:" & sSQL)
        Dim sRet As String = ""
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        If DR.HasRows Then
            If DR.Read Then
                sRet = DR(0).ToString()
            End If
        End If
        DR.Close()
        conn.Close()
        Return sRet
    End Function
    '
    ' GetID(sSQL) returns an ID (int) from a the first column of a single row. Connection is closed prior to return.
    '
    Public Function GetID(ByVal sSQL As String) As Integer
        WriteLog("db.GetID:" & sSQL)
        Dim iRet As Integer = 0
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        If DR.HasRows Then
            If DR.Read Then
                iRet = DR(0)
            End If
        End If
        DR.Close()
        conn.Close()
        Return iRet
    End Function

    '
    ' GetStr(sSQL) is used to return a string value from the first column selected from a single row. Connection is closed prior to return.
    '
    Public Function GetStr(ByVal sSQL As String) As String
        WriteLog("db.GetStr:" & sSQL)
        Dim sRet As String = ""
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        If DR.HasRows Then
            If DR.Read Then
                sRet = DR(0).ToString()
            End If
        End If
        DR.Close()
        conn.Close()
        Return sRet
    End Function

    '
    ' GetInt(sSQL) returns a single integer from the first column of a single row. Connection is closed prior to return.
    ' Can be used to retrieve ID's (as int), Count(*), etc.
    '
    Public Function GetInt(ByVal sSQL As String) As Integer
        WriteLog("db.GetInt:" & sSQL)
        Dim iRet As Integer = 0
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        If DR.HasRows Then
            If DR.Read Then
                iRet = DR(0)
            End If
        End If
        DR.Close()
        conn.Close()
        Return iRet
    End Function

    '
    ' Saves Event to Calendar. Used for a specific process. Please do not use for any other purpose.
    '
    Public Sub SaveToCalendar(ByVal iPersonID As Integer, ByVal iCalType As Integer, ByVal iCalSourceID As Integer, ByVal sShortDesc As String, ByVal sComments As String)
        WriteLog("SaveToCalendar")
        Dim sSQL As String = "INSERT INTO person_calendar (PersonID, CalType, CalSourceID, CalShortDescription, Comments) VALUES "
        sSQL &= "(@PersonID, @CalType, @CalSourceID, @CalShortDescription, @Comments)"

        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            dbCmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = iPersonID
            dbCmd.Parameters.Add("@CalType", SqlDbType.TinyInt).Value = iCalType
            dbCmd.Parameters.Add("@CalSourceID", SqlDbType.Int, 4).Value = iCalSourceID
            dbCmd.Parameters.Add("@CalShortDescription", SqlDbType.VarChar, 10).Value = sShortDesc
            dbCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 200).Value = sComments
            dbCmd.ExecuteNonQuery()
            dbConn.Close()
        End Using
    End Sub




    '
    ' Saves WebLogRecord. Used for a specific process. Please do not use for any other purpose.
    '
    Public Sub SaveWebLog(ByVal iTzOffset As Integer, ByVal iHours As Integer, ByVal sDocTitle As String, ByVal sDocURL As String, ByVal sDocReferrer As String, ByVal sLanguage As String, ByVal sScreenRes As String, ByVal iColorDepth As Integer)
        WriteLog("SaveWebLog")
        Dim sSQL As String = "INSERT INTO BH_Log (PersonID, UserIP, tzOffset, Hours, docTitle, docURL, docReferrer, Language, ScreenRes, ColorDepth, SessionID) VALUES "
        sSQL &= "(@PersonID, @UserIP, @tzOffset, @Hours, @docTitle, @docURL, @docReferrer, @Language, @ScreenRes, @ColorDepth, @SessionID)"

        Dim sSessionID As String = System.Web.HttpContext.Current.Session.SessionID.ToString()
        Dim sUserIP As String
        sUserIP = Trim(System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR"))
        If sUserIP = "" Then
            sUserIP = Trim(System.Web.HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"))
        End If

        If sUserIP = "::1" Then sUserIP = "127.0.0.1" 'Translate IP6 local host to IP4

        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            dbCmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = Me.ConvertID(System.Web.HttpContext.Current.Session("PersonID"))
            dbCmd.Parameters.Add("@UserIP", SqlDbType.VarChar, 50).Value = sUserIP
            dbCmd.Parameters.Add("@tzOffset", SqlDbType.Int, 4).Value = iTzOffset
            dbCmd.Parameters.Add("@Hours", SqlDbType.Int, 4).Value = iHours
            dbCmd.Parameters.Add("@docTitle", SqlDbType.VarChar, 500).Value = sDocTitle
            dbCmd.Parameters.Add("@docURL", SqlDbType.VarChar, 2048).Value = sDocURL
            dbCmd.Parameters.Add("@docReferrer", SqlDbType.VarChar, 2048).Value = sDocReferrer
            dbCmd.Parameters.Add("@Language", SqlDbType.VarChar, 50).Value = sLanguage
            dbCmd.Parameters.Add("@ScreenRes", SqlDbType.VarChar, 50).Value = sScreenRes
            dbCmd.Parameters.Add("@ColorDepth", SqlDbType.Int, 4).Value = iColorDepth
            dbCmd.Parameters.Add("@SessionID", SqlDbType.VarChar, 250).Value = sSessionID
            dbCmd.ExecuteNonQuery()
            dbConn.Close()
        End Using
    End Sub
    '
    ' General routine to validate zip code
    '
    Public Function IsValidZipcode(ByVal sZipCode As String) As Boolean
        WriteLog("db.IsValidZipcode:" & sZipCode)
        Dim bRet As Boolean = False
        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand("select ZipCode from ZIP_CODE where ZipCode=@ZipCode", dbConn)
            dbCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 10).Value = sZipCode.Trim()
            Dim dbDR As SqlDataReader = dbCmd.ExecuteReader(CommandBehavior.SingleRow)
            bRet = dbDR.HasRows
            dbDR.Close()
            dbConn.Close()
        End Using
        Return bRet
    End Function

    '
    ' Executes SQL. Used for generic processes to insert, update, delete. *LY* No longer used ... using parameterized SQL now.
    '
    'Public Sub ExecSQL(ByVal sSQL As String)
    '    WriteLog("ExecSQL: " & sSQL)
    '    Using dbConn As New SqlConnection(Me.ConStr)
    '        dbConn.Open()
    '        Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
    '        dbCmd.ExecuteNonQuery()
    '        dbConn.Close()
    '    End Using
    'End Sub

    '
    ' Convert string date to datetime
    '
    Public Function DateStrToDT(ByVal sIn As String) As DateTime
        Dim dtRet As DateTime = DateTime.Parse(sIn & " 12:00:00 AM")
        Return dtRet
    End Function

    '
    ' TestBoolean(sID) is a helper function that tests a string = true and converts to boolean
    '
    Public Function TestBoolean(ByVal sIN As String) As Boolean
        Dim bRet As Boolean = False
        If Not String.IsNullOrEmpty(sIN) Then
            Try
                bRet = (sIN.Trim().ToLower() = "true")

            Catch ex As Exception
                'do nothing, just leave as false for now
            End Try
        End If
        Return bRet
    End Function

    '
    ' Save user review.
    '
    'Public Function SaveReview(ByVal PersonID As Integer, ByVal CatID As Integer, ByVal ItemID As Integer, ByVal review As String, ByVal arrayReview As Integer()) As String
    '    Dim ret As String = "Failed"
    '    Dim sSQL As String = "DELETE FROM BH_BEE_RATING where PersonID=@PersonID and CatID=@CatID and ItemID=@ItemID"
    '    Using dbConn As New SqlConnection(Me.ConStr)
    '        dbConn.Open()
    '        Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
    '        Dim dbTran As SqlTransaction = dbConn.BeginTransaction("SaveRating")
    '        dbCmd.Transaction = dbTran

    '        Try
    '            Dim dtDateOfRating As DateTime = DateTime.Now
    '            dbCmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID
    '            dbCmd.Parameters.Add("@CatID", SqlDbType.Int).Value = CatID
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            dbCmd.ExecuteNonQuery()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "INSERT INTO BH_BEE_RATING (PersonID,CatID,ItemID,BeeRating,FoodRating,ServiceRating,AtmosphereRating,CleanlinessRating,ValueRating,Review) OUTPUT Inserted.DateOfRating VALUES(@PersonID,@CatID,@ItemID,@BeeRating,@FoodRating,@ServiceRating,@AtmosphereRating,@Cleanlinessrating,@ValueRating,@Review)"
    '            dbCmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID
    '            dbCmd.Parameters.Add("@CatID", SqlDbType.Int).Value = CatID
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            dbCmd.Parameters.Add("@BeeRating", SqlDbType.TinyInt).Value = arrayReview(0)
    '            dbCmd.Parameters.Add("@FoodRating", SqlDbType.TinyInt).Value = arrayReview(1)
    '            dbCmd.Parameters.Add("@ServiceRating", SqlDbType.TinyInt).Value = arrayReview(2)
    '            dbCmd.Parameters.Add("@AtmosphereRating", SqlDbType.TinyInt).Value = arrayReview(3)
    '            dbCmd.Parameters.Add("@CleanlinessRating", SqlDbType.TinyInt).Value = arrayReview(4)
    '            dbCmd.Parameters.Add("@ValueRating", SqlDbType.TinyInt).Value = arrayReview(5)
    '            dbCmd.Parameters.Add("@Review", SqlDbType.VarChar).Value = review
    '            dtDateOfRating = dbCmd.ExecuteScalar() ' returns assigned date of rating for use later

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "DELETE FROM BH_SUM_RATING where ItemID=@ItemID"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            dbCmd.ExecuteNonQuery()

    '            Dim decBeeRating As Decimal = 0
    '            Dim decFoodRating As Decimal = 0
    '            Dim decServiceRating As Decimal = 0
    '            Dim decAtmosphereRating As Decimal = 0
    '            Dim decCleanlinessRating As Decimal = 0
    '            Dim decValueRating As Decimal = 0

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "select avg(convert(decimal (4,1),BeeRating)) from BH_BEE_RATING where ItemID=@ItemID and BeeRating > 0"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            decBeeRating = dbCmd.ExecuteScalar()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "select avg(convert(decimal (4,1),FoodRating)) from BH_BEE_RATING where ItemID=@ItemID and FoodRating > 0"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            decFoodRating = dbCmd.ExecuteScalar()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "select avg(convert(decimal (4,1),ServiceRating)) from BH_BEE_RATING where ItemID=@ItemID and ServiceRating > 0"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            decServiceRating = dbCmd.ExecuteScalar()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "select avg(convert(decimal (4,1),AtmosphereRating)) from BH_BEE_RATING where ItemID=@ItemID and AtmosphereRating > 0"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            decAtmosphereRating = dbCmd.ExecuteScalar()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "select avg(convert(decimal (4,1),CleanlinessRating)) from BH_BEE_RATING where ItemID=@ItemID and CleanlinessRating > 0"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            decCleanlinessRating = dbCmd.ExecuteScalar()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "select avg(convert(decimal (4,1),ValueRating)) from BH_BEE_RATING where ItemID=@ItemID and ValueRating > 0"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            decValueRating = dbCmd.ExecuteScalar()

    '            dbCmd.Parameters.Clear()
    '            dbCmd.CommandText = "INSERT INTO BH_SUM_RATING (ItemID,BeeRating,FoodRating,ServiceRating,AtmosphereRating,Cleanlinessrating,ValueRating) values (@ItemID,@BeeRating,@FoodRating,@ServiceRating,@AtmosphereRating,@Cleanlinessrating,@ValueRating)"
    '            dbCmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID
    '            dbCmd.Parameters.Add("@BeeRating", SqlDbType.Decimal).Value = decBeeRating
    '            dbCmd.Parameters.Add("@FoodRating", SqlDbType.Decimal).Value = decFoodRating
    '            dbCmd.Parameters.Add("@ServiceRating", SqlDbType.Decimal).Value = decServiceRating
    '            dbCmd.Parameters.Add("@AtmosphereRating", SqlDbType.Decimal).Value = decAtmosphereRating
    '            dbCmd.Parameters.Add("@CleanlinessRating", SqlDbType.Decimal).Value = decCleanlinessRating
    '            dbCmd.Parameters.Add("@ValueRating", SqlDbType.Decimal).Value = decValueRating
    '            dbCmd.ExecuteNonQuery()

    '            ' =========================================
    '            '  Save Summary rows for Stinger Reporting *LY* Move this to SPROC
    '            ' =========================================
    '            Dim arrColNames() As String = {"B", "F", "S", "A", "C", "V"}
    '            Dim sColList As String = String.Empty
    '            '
    '            ' Build update column list ...
    '            '
    '            For iSumTypeID As Integer = 0 To 5
    '                Dim sCol As String = arrColNames(iSumTypeID) & arrayReview(iSumTypeID).ToString("#")
    '                AddColToIncList(sColList, sCol)
    '            Next
    '            sColList &= ",Bsum=Bsum+1,Fsum=Fsum+1,Ssum=Ssum+1,Asum=Asum+1,Csum=Csum+1,Vsum=Vsum+1"

    '            Dim iUnitIDList() As Integer = GetUnitIDSet(dtDateOfRating)

    '            dbCmd.CommandText = "UPDATE BH_BEE_RATING_SUMMARY " & sColList & " WHERE ItemID=@ItemID AND SumUnitID=@SumUnitID and UnitID=@UnitID"

    '            For iUnitType As Integer = UnitType.Day To UnitType.Month
    '                dbCmd.Parameters.Clear()
    '                dbCmd.Parameters.Add("@ItemID", SqlDbType.Int, 4).Value = ItemID
    '                dbCmd.Parameters.Add("@SumUnitID", SqlDbType.TinyInt).Value = iUnitType
    '                dbCmd.Parameters.Add("@UnitID", SqlDbType.Int, 4).Value = iUnitIDList(iUnitType)
    '                dbCmd.ExecuteNonQuery()
    '            Next

    '            dbTran.Commit()
    '            ret = "OK"

    '        Catch ex As Exception

    '            Try ' attempt a rollback
    '                dbTran.Rollback()

    '            Catch ex2 As Exception
    '                ' *LY* Log this and error above in event log

    '            End Try

    '        End Try
    '        dbConn.Close()
    '    End Using
    '    Return ret
    'End Function

    '
    ' StrToInt(sID) is a helper function that converts a string  to a 32-Bit Integer
    '
    Public Function StrToInt(ByVal sID As String) As Integer
        Dim iRet As Integer = 0
        If Not String.IsNullOrEmpty(sID) Then
            Try
                iRet = CInt(sID)

            Catch ex As Exception
                'do nothing, just leave as zero for now
            End Try
        End If
        Return iRet
    End Function

    Public Function StrWithMaxLen(ByVal oIN As Object, ByVal iLen As Integer) As String
        Dim sRet As String = ""
        Try
            sRet = oIN.ToString
            If sRet.Length > iLen Then
                sRet = sRet.Substring(0, iLen)
            End If
            sRet = sRet.Trim
        Catch
        End Try
        Return sRet
    End Function

    '
    ' StrToLong(sID) is a helper function that converts a string to a 64-Bit Integer
    '
    Public Function StrToLong(ByVal sID As String) As Long
        Dim loRet As Long = 0
        If Not String.IsNullOrEmpty(sID) Then
            Try
                loRet = CLng(sID)

            Catch ex As Exception
                'do nothing, just leave as zero for now
            End Try
        End If
        Return loRet
    End Function

    '
    ' ConvertID(sID) is a helper function that converts a string ID to a 32-Bit Integer
    '
    Public Function ConvertID(ByVal sID As String) As Integer
        Dim iRet As Integer = 0
        If Not String.IsNullOrEmpty(sID) Then
            Try
                iRet = CInt(sID)

            Catch ex As Exception
                'do nothing, just leave as zero for now
            End Try
        End If
        Return iRet
    End Function

    '
    ' ConvertTinyInt(sIN) is a helper function that converts a string to a SQL Tiny Integer between 0 and 127
    '
    Public Function ConvertTinyInt(ByVal sIN As String) As Integer
        Dim iRet As Integer = 0
        If Not String.IsNullOrEmpty(sIN) Then
            Try
                iRet = CInt(sIN)

            Catch ex As Exception
                'do nothing, just leave as zero for now
            End Try
        End If
        If iRet < 0 Or iRet > 127 Then iRet = 0
        Return iRet
    End Function

    '
    ' ConvertToRating(sIN) is a helper function that converts a string to an Integer between 1 and 5
    '
    Public Function ConvertToRating(ByVal sIN As String) As Integer
        Dim iRet As Integer = 0
        If Not String.IsNullOrEmpty(sIN) Then
            Try
                iRet = CInt(sIN)

            Catch ex As Exception
                'do nothing, just leave as zero for now
            End Try
        End If
        If iRet < 1 Then iRet = 1
        If iRet > 5 Then iRet = 5
        Return iRet
    End Function


    Public Function ClearNull(ByVal sIN As Object) As String
        Dim ret As String = ""
        If Not sIN Is Nothing Then
            If Not IsDBNull(sIN) Then
                ret = sIN.ToString()
            End If
        End If
        Return ret
    End Function
    Public Function StrToDec(ByVal sIn As String) As Decimal
        Dim dRet As Decimal
        If Decimal.TryParse(sIn, dRet) Then
            Return dRet
        Else
            Return 0.0
        End If
    End Function
    Public Function Dec(ByVal dIN As Object) As Decimal
        Dim ret As Decimal = 0
        If Not Convert.IsDBNull(dIN) Then ret = dIN
        Return ret
    End Function

    Public Function Int(ByRef iIn As Object) As Integer
        Dim ret As Integer = 0
        If Not Convert.IsDBNull(iIn) Then ret = iIn
        Return ret
    End Function

    Public Function Str(ByRef iIn As Object) As String
        Dim ret As String = ""
        If Not Convert.IsDBNull(iIn) Then ret = iIn.ToString()
        Return ret
    End Function

    Public Function OptionList(ByRef list() As String, ByVal iSelection As Integer) As String
        Dim sRet As String = ""
        Dim i As Integer = 0
        For i = 1 To UBound(list)
            sRet &= "<option value='" & i.ToString() & "'"
            If iSelection = i Then sRet &= " selected"
            sRet &= ">" & list(i) & "</option>"
        Next i
        Return sRet
    End Function

    Public Function OptionListInt(ByRef list() As Integer, ByVal iSelection As Integer) As String
        Dim sRet As String = ""
        Dim i As Integer = 0
        For i = 1 To UBound(list)
            sRet &= "<option value='" & list(i).ToString() & "'"
            If iSelection = list(i) Then sRet &= " selected"
            sRet &= ">" & list(i).ToString() & "</option>"
        Next i
        Return sRet
    End Function
    '
    ' GetDec(sSQL) returns a single integer from the first column of a single row. Connection is closed prior to return.
    ' Can be used to retrieve ID's (as int), Count(*), etc.
    '
    Public Function GetDec(ByVal sSQL As String) As Decimal
        WriteLog("db.GetDec:" & sSQL)
        Dim iRet As Decimal = 0
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        If DR.HasRows Then
            If DR.Read Then
                iRet = DR(0)
            End If
        End If
        DR.Close()
        conn.Close()
        Return iRet
    End Function

    Public Function BHDateTimeDisplay(ByVal theDateTime As String)
        Dim theDate As String
        Dim theTime As String
        Dim ret As String
        If Len(theDateTime) > 0 Then
            theDate = Left(theDateTime, 10)
            theTime = Right(theDateTime, 8)
            If CInt(Left(theTime, 2)) > 12 Then
                theTime = CStr(CInt(Left(theTime, 2)) - 12) & ":" & Mid(theTime, 4, 2) & " PM"
            Else
                theTime = Left(theTime, 2) & ":" & Mid(theTime, 4, 2) & " AM"
            End If
            ret = Mid(theDate, 6, 2) & "-" & Right(theDate, 2) & "-" & Left(theDate, 4) & " " & theTime
        Else
            ret = ""
        End If
        Return ret
    End Function
    Public Function BHTimeDisplay(ByVal theTime As String)
        Dim ret As String
        If Len(theTime) > 0 Then
            If CInt(Left(theTime, 2)) > 12 Then
                ret = CStr(CInt(Left(theTime, 2)) - 12) & ":" & Mid(theTime, 4, 2) & " PM"
            Else
                ret = CStr(CInt(Left(theTime, 2))) & ":" & Mid(theTime, 4, 2) & " AM"
            End If
        Else
            ret = ""
        End If
        Return ret
    End Function
    Public Function GuidToString(ByVal gGuid As Guid) As String
        Dim sGuid As String = gGuid.ToString()
        Return sGuid.Substring(0, 8) & sGuid.Substring(9, 4) & sGuid.Substring(14, 4) & sGuid.Substring(19, 4) & sGuid.Substring(24, 12)
    End Function
    Public Function StringToGuid(ByVal sGuid As String) As Guid
        Return New Guid(sGuid.Substring(0, 8) & "-" & sGuid.Substring(8, 4) & "-" & sGuid.Substring(12, 4) & "-" & sGuid.Substring(16, 4) & "-" & sGuid.Substring(20, 12))
    End Function

    Public Function CreateObjectSession(ByVal sData As String, ByVal iType As Integer) As String
        Dim gObjectSessionID As Guid = System.Guid.NewGuid
        WriteLog("CreateObjectSession: " & gObjectSessionID.ToString())
        Dim sSQL As String = "insert into BH_OBJECT_SESSION (ID,ObjectType,ObjectData,Accessed) values(@ID,@ObjectType,@ObjectData,getdate())"
        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            dbCmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = gObjectSessionID
            dbCmd.Parameters.Add("@ObjectType", SqlDbType.Int).Value = iType
            dbCmd.Parameters.Add("@ObjectData", SqlDbType.VarChar).Value = sData
            dbCmd.ExecuteNonQuery()
            dbConn.Close()
        End Using
        Return GuidToString(gObjectSessionID)
    End Function

    Public Sub SaveObjectSession(ByVal sID As String, ByVal sData As String)
        Dim gObjectSessionID As Guid = StringToGuid(sID)
        WriteLog("CreateObjectSession: " & sID)
        Dim sSQL As String = "Update BH_OBJECT_SESSION set ObjectData=@ObjectData,Accessed=getdate() where ID=@ID"
        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            dbCmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = gObjectSessionID
            dbCmd.Parameters.Add("@ObjectData", SqlDbType.VarChar).Value = sData
            dbCmd.ExecuteNonQuery()
            dbConn.Close()
        End Using
    End Sub

    Public Sub DeleteObjectSession(ByVal sID As String)
        Dim gObjectSessionID As Guid = StringToGuid(sID)
        WriteLog("DeleteObjectSession: " & sID)
        Dim sSQL As String = "Delete BH_OBJECT_SESSION where ID=@ID"
        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            dbCmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = gObjectSessionID
            dbCmd.ExecuteNonQuery()
            dbConn.Close()
        End Using
    End Sub

    Public Function GetObjectSession(ByVal sID As String) As String
        Dim gObjectSessionID As Guid = StringToGuid(sID)
        WriteLog("GetObjectSession: " & sID)
        Dim sRet As String = ""
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand("Update BH_OBJECT_SESSION set Accessed=getdate() where ID=@ID", conn)
        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = gObjectSessionID
        cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
        cmd.CommandText = "select ObjectData from BH_OBJECT_SESSION where ID=@ID"
        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = gObjectSessionID
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        If DR.HasRows Then
            If DR.Read Then
                sRet = DR(0).ToString()
            End If
        End If
        DR.Close()
        conn.Close()
        Return sRet
    End Function

    '
    ' InsertAndReturnID expects an insert sql statement with an OUTPUT inserted.[ColumnName] clause that returns the 
    ' new ID of the entity, and returns that ID as the value of the function ([ColumnName] is the name of the column
    ' that is an IDENTITY in the table). For example:

    ' Dim iNewKey As Integer = db.InsertAndReturnID("INSERT INTO LY_TEST (Stuff) OUTPUT inserted.TestID VALUES ('Howdy')")
    '
    ' iNewKey will contain the new IDENTITY value of the column that was inserted.
    '
    ' *LY* Note: Should no longer be used. Bundle into data context object using parameterized SQL.
    '
    'Public Function InsertAndReturnID(ByVal sSQL) As Integer
    '    Dim iRet As Integer = 0
    '    Using dbConn As New SqlConnection(Me.ConStr)
    '        dbConn.Open()
    '        Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
    '        iRet = dbCmd.ExecuteScalar()
    '        dbConn.Close()
    '    End Using
    '    Return iRet
    'End Function

    '
    ' Search
    ' Expects a query string (@strQUERY) and optional lat/long (@thisLAT, @thisLONG) parameters and returns 
    ' the sSQL string select columns in an open SQLDataReader
    ' Example:
    '
    ' sSQL=
    'select A.VenueID,VenueName,VenueCity,VenueAddress,VenueState,VenueZipCode
    ' ,dbo.fnGetMiles(@thisLAT,@thisLONG,A.VenueLat,A.VenueLong) as D, COALESCE(B.VenueImage,'') -- Note that COALESCE function eliminates NULL responses
    '  from BH_VENUES as A
    '  left outer join BH_VENUE_IMAGE B on A.VenueID = B.VenueID
    '  where A.VenueName like @strQUERY
    '  and dbo.fnGetMiles(@thisLAT,@thisLONG,A.VenueLat,A.VenueLong) < 50
    '  and dbo.HasCurrentEvents(A.VenueID)=1
    '  Order By D
    '
    '  @strQUERY=
    '  '%Pacific%'
    '
    '  @thisLAT = 33.71100
    '  @thisLONG = -117.80853
    '
    Public Function Search(ByVal sSQL As String, ByVal strQuery As String, ByVal dThisLat As Decimal, ByVal dThisLong As Decimal) As SqlDataReader
        WriteLog("db.SearchDistance:" & sSQL)
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        cmd.Parameters.Add("@strQUERY", SqlDbType.VarChar).Value = strQuery
        If dThisLat <> 0.0 And dThisLong <> 0.0 Then
            cmd.Parameters.Add("@thisLAT", SqlDbType.Decimal).Value = dThisLat
            cmd.Parameters.Add("@thisLONG", SqlDbType.Decimal).Value = dThisLong
        End If
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection) ' connection will remain open until caller closes the reader
        Return DR
    End Function

    '
    ' Overloaded Search function without the GPS coordinates...
    '
    Public Function Search(ByVal sSQL As String, ByVal strQuery As String) As SqlDataReader
        WriteLog("db.SearchDistance:" & sSQL)
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand(sSQL, conn)
        cmd.Parameters.Add("@strQUERY", SqlDbType.VarChar).Value = strQuery
        Dim DR As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection) ' connection will remain open until caller closes the reader
        Return DR
    End Function

    '
    ' ExecuteStingerProcedure
    '
    ' Executes a stored procedure with ItemID (as Integer) parameter. Returns a SqlDataReader with results
    '
    ' Calling example:
    ' 
    ' Dim dr As SqlDataReader = db.ExecuteStingerProcedure("dbo.ReportGraph1", 2100)
    ' If Not dr Is Nothing Then
    '    While dr.Read
    '       sOut &= dr(0).ToString & "--" & dr(1).ToString & vbCRLF
    '    End While
    '    dr.Close()
    ' End If
    ' 
    ' PLEASE NOTE: the sproc should have a SET NOTCOUNT ON; statement immediately preceding the select statement that generates
    ' the return rowset in order to prevent odd return results.
    '
    Public Function ExecuteStingerProcedure(ByVal sProcName As String, ByVal iItemID As Integer) As SqlDataReader
        Dim drRet As SqlDataReader = Nothing
        Try
            Dim conn As SqlConnection = New SqlConnection(m_ConStr)
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandText = sProcName
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = iItemID
            drRet = cmd.ExecuteReader(CommandBehavior.CloseConnection) ' connection will remain open until caller closes the reader

        Catch ex As System.Exception
            Dim sErrLogName As String = ConfigurationManager.AppSettings("errLog").ToString()
            Dim sMsg As String = "Unable to execute stinger procedure: " & ex.Message.ToString() & " :: Stack Trace=" & ex.StackTrace
            System.Diagnostics.EventLog.WriteEntry(sErrLogName, sMsg, System.Diagnostics.EventLogEntryType.Error)
        End Try
        Return drRet
    End Function

    Public Function SaveBvite(ByVal iPersonID As Integer, ByVal sTitle As String, ByVal sHostPhone As String, ByVal sHostName As String, ByVal sDetail As String, ByVal dEventDate As Date, ByVal sTime As String, ByVal sTimeTo As String, ByVal sLocation As String, ByVal sAddress As String, ByVal sCity As String, ByVal sState As String, ByVal sZipCode As String) As Integer
        Dim iRet As Integer = 0
        Dim sSQL = "INSERT INTO BH_BVITE (personID,InvitationTitle, hostPhone,hostname,EventDetail,EventDate,EventTime,EventTimeTo,EventLocation,EventAddress,City,State,Zip)"
        sSQL &= " OUTPUT INSERTED.BviteID VALUES(@PersonID,@Title,@HostPhone,@HostName,@Detail,@EventDate,@EventTime,@TimeTo,@Location,@Address,@City,@State,@ZipCode)"

        Using dbConn As New SqlConnection(Me.ConStr)
            dbConn.Open()
            Dim dbCmd As SqlCommand = New SqlCommand(sSQL, dbConn)
            dbCmd.Parameters.Add("@PersonID", SqlDbType.Int, 4).Value = iPersonID
            dbCmd.Parameters.Add("@Title", SqlDbType.VarChar, 100).Value = Me.XlatHtmlForDisplay(sTitle)
            dbCmd.Parameters.Add("@HostPhone", SqlDbType.VarChar, 100).Value = Me.XlatHtmlForDisplay(sHostPhone)
            dbCmd.Parameters.Add("@HostName", SqlDbType.VarChar, 50).Value = Me.XlatHtmlForDisplay(sHostName)
            dbCmd.Parameters.Add("@Detail", SqlDbType.NVarChar, 300).Value = Me.XlatHtmlForDisplay(sDetail)
            dbCmd.Parameters.Add("@EventDate", SqlDbType.Date).Value = dEventDate
            dbCmd.Parameters.Add("@EventTime", SqlDbType.VarChar, 100).Value = Me.XlatHtmlForDisplay(sTime)
            dbCmd.Parameters.Add("@TimeTo", SqlDbType.VarChar, 50).Value = Me.XlatHtmlForDisplay(sTimeTo)
            dbCmd.Parameters.Add("@Location", SqlDbType.VarChar, 100).Value = Me.XlatHtmlForDisplay(sLocation)
            dbCmd.Parameters.Add("@Address", SqlDbType.VarChar, 100).Value = Me.XlatHtmlForDisplay(sAddress)
            dbCmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = Me.XlatHtmlForDisplay(sCity)
            dbCmd.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = Me.XlatHtmlForDisplay(sState)
            dbCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 50).Value = Me.XlatHtmlForDisplay(sZipCode)
            iRet = dbCmd.ExecuteScalar()
            dbConn.Close()
        End Using
        Return iRet
    End Function

    Private Function XlatToken(ByVal sIn As String, ByVal sToken As String) As String
        sIn = sIn.Replace("&lt;" & sToken & "&gt;", "<" & sToken & ">")
        sIn = sIn.Replace("<" & sToken & "&gt;", "<" & sToken & ">")
        sIn = sIn.Replace("&lt;" & sToken & ">", "<" & sToken & ">")
        sIn = sIn.Replace("&lt;/" & sToken & "&gt;", "<" & sToken & ">")
        sIn = sIn.Replace("</" & sToken & "&gt;", "<" & sToken & ">")
        sIn = sIn.Replace("&lt;/" & sToken & ">", "<" & sToken & ">")
        Return sIn
    End Function

    Public Function XlatHtmlForDisplay(ByVal sIN As String) As String
        Dim sbOut As StringBuilder = New StringBuilder("")
        Dim i As Integer
        Dim sChar As String
        Dim sLookAhead As String = ""
        Dim sLookBack As String = ""
        Dim iHTML As Integer = 0
        Dim bSkip As Boolean = False
        Dim bAnchor As Boolean = False
        Dim iAnchorStart As Integer = 0

        'Remove starting and ending quotes, if present:
        If ((sIN.Length > 2) And (sIN.StartsWith(Chr(34))) And (sIN.EndsWith(Chr(34)))) Then
            sIN = sIN.Substring(1, sIN.Length - 2)
        End If

        sIN = XlatToken(sIN, "blockquote")
        sIN = XlatToken(sIN, "dl")
        sIN = XlatToken(sIN, "dt")
        sIN = XlatToken(sIN, "dd")

        Dim iLen As Integer = sIN.Length - 1

        For i = 0 To iLen
            sChar = sIN.Substring(i, 1)
            Dim iChar As Integer = Asc(sChar)
            Select Case iChar
                Case 60 '<
                    sLookAhead = sIN.Substring(i + 1).ToLower()
                    If sLookAhead.StartsWith("font") _
                       Or sLookAhead.StartsWith("img") _
                       Or sLookAhead.StartsWith("li>") _
                       Or sLookAhead.StartsWith("blockquote>") _
                       Or sLookAhead.StartsWith("dl>") _
                       Or sLookAhead.StartsWith("dt>") _
                       Or sLookAhead.StartsWith("dd>") _
                       Or sLookAhead.StartsWith("sup>") _
                       Or sLookAhead.StartsWith("ul>") _
                       Or sLookAhead.StartsWith("u>") _
                       Or sLookAhead.StartsWith("p>") _
                       Or sLookAhead.StartsWith("b>") _
                       Or sLookAhead.StartsWith("i>") _
                       Or sLookAhead.StartsWith("/") _
                       Or sLookAhead.StartsWith("br") _
                       Or sLookAhead.StartsWith("em") _
                       Or sLookAhead.StartsWith("/em") _
                       Or sLookAhead.StartsWith("strong>") _
                       Or sLookAhead.StartsWith("a") Then
                        iHTML = iHTML + 1
                        bSkip = True
                        If sLookAhead.StartsWith("a") Then
                            bAnchor = True
                            iAnchorStart = sbOut.Length
                        Else
                            sbOut.Append(sChar)
                        End If
                        If sLookAhead.StartsWith("Escrit") Then
                            Dim a As String = "help"
                        End If
                    Else
                        sbOut.Append("&lt;")
                    End If
                Case 62 '>
                    sLookBack = sbOut.ToString().ToLower()
                    If sLookBack.EndsWith("/") _
                       Or sLookBack.EndsWith("a") _
                       Or sLookBack.EndsWith("/a") _
                       Or sLookBack.EndsWith("<li") _
                       Or sLookBack.EndsWith("</li") _
                       Or sLookBack.EndsWith("<ul") _
                       Or sLookBack.EndsWith("</ul") _
                       Or sLookBack.EndsWith("</u") _
                       Or sLookBack.EndsWith("<u") _
                       Or sLookBack.EndsWith("<i") _
                       Or sLookBack.EndsWith("</i") _
                       Or sLookBack.EndsWith("<p") _
                       Or sLookBack.EndsWith("</p") _
                       Or sLookBack.EndsWith("<b") _
                       Or sLookBack.EndsWith("</b") _
                       Or sLookBack.EndsWith("<br") _
                       Or sLookBack.EndsWith("</br") _
                       Or sLookBack.EndsWith(Chr(34)) _
                       Or sLookBack.EndsWith("strong") _
                       Or sLookBack.EndsWith("<sup") _
                       Or sLookBack.EndsWith("</sup") _
                       Or sLookBack.EndsWith("<em") _
                       Or sLookBack.EndsWith("</em") _
                       Or sLookBack.EndsWith("/strong") _
                       Or sLookBack.EndsWith("blockquote") _
                       Or sLookBack.EndsWith("/blockquote") _
                       Or sLookBack.EndsWith("dl") _
                       Or sLookBack.EndsWith("/dl") _
                       Or sLookBack.EndsWith("dt") _
                       Or sLookBack.EndsWith("/dt") _
                       Or sLookBack.EndsWith("dd") _
                       Or sLookBack.EndsWith("/dd") _
                       Or sLookBack.EndsWith("'") Then
                        iHTML = iHTML - 1
                        If iHTML < 1 Then bSkip = False
                        If iHTML < 0 Then iHTML = 0
                        If (sLookBack.EndsWith("a") Or sLookBack.EndsWith(Chr(34))) And iAnchorStart > 0 Then
                            Dim tmpOut As String = sbOut.ToString()
                            sbOut = New StringBuilder(tmpOut.Substring(0, iAnchorStart))
                        Else
                            sbOut.Append(sChar)
                        End If
                    Else
                        sbOut.Append("&gt;")
                    End If

                Case 0 To 31, 127 To 128
                    'Skip
                    sbOut.Append(" ")

                Case 39, 145, 146
                    If Not bSkip Then
                        sbOut.Append("&#39;")
                    Else
                        sbOut.Append(sChar)
                    End If

                Case 34, 147, 148
                    If Not bSkip Then
                        sbOut.Append("&quot;")
                    Else
                        sbOut.Append(sChar)
                    End If

                Case 129 To 255
                    sbOut.Append("&#" & Asc(sChar) & ";")

                Case Else
                    sbOut.Append(sChar)
            End Select
        Next
        sbOut.Replace("|", "!")
        Return sbOut.ToString.Trim
    End Function

    Public Function ServerGetDate() As DateTime
        Dim dtRet As DateTime
        Dim conn As SqlConnection = New SqlConnection(m_ConStr)
        conn.Open()
        Dim cmd As SqlCommand = New SqlCommand("Select getdate()", conn)
        dtRet = cmd.ExecuteScalar()
        conn.Close()
        Return dtRet
    End Function
End Class
