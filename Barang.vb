Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class Barang
    Private strKodeBarang As String
    Private strNamaBarang As String
    Private strSpecBarang As String
    Private strSatBarang As String
    Private dblHargaSat As Double

    Private strSQL As String    
    Private myCon As OleDbConnection
    Private objCommand As OleDbCommand
    Private objReader As OleDbDataReader
    Private objDataTable As DataTable
    Private blnReturn As Boolean

    Public Property KodeBarang() As String
        Get
            Return strKodeBarang
        End Get
        Set(ByVal value As String)
            strKodeBarang = value
        End Set
    End Property

    Public Property NamaBarang() As String
        Get
            Return strNamaBarang
        End Get
        Set(ByVal value As String)
            strNamaBarang = value
        End Set
    End Property

    Public Property SpecBarang() As String
        Get
            Return strSpecBarang
        End Get
        Set(ByVal value As String)
            strSpecBarang = value
        End Set
    End Property

    Public Property SatuanBarang() As String
        Get
            Return strSatBarang
        End Get
        Set(ByVal value As String)
            strSatBarang = value
        End Set
    End Property


    Public Property HargaSatuan() As Double
        Get
            Return dblHargaSat
        End Get
        Set(ByVal value As Double)
            dblHargaSat = value
        End Set
    End Property

    Public Function IsExist() As Boolean
        myCon = New OleDbConnection(strCon)
        Try
            myCon.Open()
            strSQL = "SELECT * FROM TBL_BARANG WHERE KD_BRG = '" & KodeBarang & "'"
            objCommand = New OleDbCommand(strSQL, myCon)
            objReader = objCommand.ExecuteReader(CommandBehavior.Default)
            If objReader.HasRows Then
                blnReturn = True
            Else
                blnReturn = False
            End If
            objCommand.Dispose()
            objReader.Close()
        Catch ex As Exception
            blnReturn = False
        Finally
            myCon.Close()
            objCommand = Nothing
            objReader = Nothing
            myCon = Nothing
        End Try
        Return blnReturn
    End Function

    Public Function IsSave() As Boolean
        myCon = New OleDbConnection(strCon)
        Try
            myCon.Open()
            strSQL = "INSERT INTO TBL_BARANG (KD_BRG,NM_BRG,SAT_BRG,SPEC_BRG,HRG_SAT) VALUES('" & KodeBarang & "','" & NamaBarang & "','" & SatuanBarang & "','" & SpecBarang & "'," & HargaSatuan & ")"
            objCommand = New OleDbCommand(strSQL, myCon)
            If objCommand.ExecuteNonQuery Then
                blnReturn = True
            Else
                blnReturn = False
            End If            
        Catch ex As Exception
            blnReturn = False
        Finally
            myCon.Close()
            objCommand = Nothing
            objReader = Nothing
            myCon = Nothing
        End Try
        Return blnReturn
    End Function

    Public Function IsDelete() As Boolean
        myCon = New OleDbConnection(strCon)
        Try
            myCon.Open()
            strSQL = "DELETE FROM TBL_BARANG WHERE KD_BRG = '" & KodeBarang & "'"
            objCommand = New OleDbCommand(strSQL, myCon)
            If objCommand.ExecuteNonQuery Then
                blnReturn = True
            Else
                blnReturn = False
            End If
        Catch ex As Exception
            blnReturn = False
        Finally
            myCon.Close()
            objCommand = Nothing
            objReader = Nothing
            myCon = Nothing
        End Try
        Return blnReturn
    End Function

    Public Function IsUpdate() As Boolean
        myCon = New OleDbConnection(strCon)
        Try
            myCon.Open()
            strSQL = "UPDATE TBL_BARANG SET NM_BRG = '" & NamaBarang & "', SAT_BRG = '" & SatuanBarang & "', SPEC_BRG = '" & SpecBarang & "', HRG_SAT = " & HargaSatuan & " WHERE KD_BRG = '" & KodeBarang & "'"
            objCommand = New OleDbCommand(strSQL, myCon)
            If objCommand.ExecuteNonQuery Then
                blnReturn = True
            Else
                blnReturn = False
            End If
        Catch ex As Exception
            blnReturn = False
        Finally
            myCon.Close()
            objCommand = Nothing
            objReader = Nothing
            myCon = Nothing
        End Try
        Return blnReturn
    End Function

    Public Function GetDataBarang() As DataTable

        Try
            myCon = New OleDbConnection(strCon)
            objDataTable = New DataTable
            myCon.Open()
            strSQL = "SELECT * FROM TBL_BARANG"
            objCommand = New OleDbCommand(strSQL, myCon)
            objReader = objCommand.ExecuteReader(CommandBehavior.Default)
            objDataTable.Load(objReader)
            objCommand.Dispose()
            objReader.Close()
            myCon.Close()
            Return objDataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function PopulateCode() As OleDbDataReader
        ' megisi cbo yang ada d frm penerimaan
        Try
            myCon = New OleDbConnection(strCon)
            Dim tmpReader As OleDbDataReader
            myCon.Open()
            strSQL = "SELECT KD_BRG FROM TBL_BARANG"
            objCommand = New OleDbCommand(strSQL, myCon)
            tmpReader = objCommand.ExecuteReader(CommandBehavior.Default)
            objCommand.Dispose()
            Return tmpReader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetName() As OleDbDataReader
        'load unt otomats mengisi textbox dr dtabase barang (ada d frm penerimaan )
        Try
            myCon = New OleDbConnection(strCon)
            Dim tmpReader As OleDbDataReader
            myCon.Open()
            strSQL = "SELECT * FROM TBL_BARANG WHERE KD_BRG = '" & KodeBarang & "'"
            objCommand = New OleDbCommand(strSQL, myCon)
            tmpReader = objCommand.ExecuteReader(CommandBehavior.Default)
            objCommand.Dispose()
            Return tmpReader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
