Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class Pengeluaran
    Private strSQL As String
    Private myCon As OleDbConnection
    Private objCommand As OleDbCommand
    Private objReader As OleDbDataReader
    Private objDataTable As DataTable
    Private blnReturn As Boolean
    Private strNomor As String
    Private intQty As Integer
    Private dtTglTrans As Date
    Private strKodeBarang As String

    Public Property TanggalTrans() As Date
        Get
            Return dtTglTrans
        End Get
        Set(ByVal value As Date)
            dtTglTrans = value
        End Set
    End Property

    Public Property KodeBarang() As String
        Get
            Return strKodeBarang
        End Get
        Set(ByVal value As String)
            strKodeBarang = value
        End Set
    End Property

    Public Property NomorKeluar() As String
        Get
            Return strNomor
        End Get
        Set(ByVal value As String)
            strNomor = value
        End Set
    End Property

    Public Property QtyKeluar() As Integer
        Get
            Return intQty
        End Get
        Set(ByVal value As Integer)
            intQty = value
        End Set
    End Property

    Public Function GetDataPengeluaran() As DataTable
        Try
            myCon = New OleDbConnection(strCon)
            objDataTable = New DataTable
            myCon.Open()
            strSQL = "SELECT * FROM TBL_PENGELUARAN"
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

    Public Function IsExist() As Boolean
        myCon = New OleDbConnection(strCon)
        Try
            myCon.Open()
            strSQL = "SELECT * FROM TBL_PENGELUARAN WHERE NO_PENGELUARAN = '" & NomorKeluar & "'"
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
            strSQL = "INSERT INTO TBL_PENGELUARAN (NO_PENGELUARAN,TGL_PENGELUARAN,KD_BRG,QTY) VALUES('" & NomorKeluar & "','" & TanggalTrans & "','" & KodeBarang & "'," & QtyKeluar & ")"
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
            strSQL = "UPDATE TBL_PENGELUARAN SET KD_BRG = '" & KodeBarang & "', QTY = " & QtyKeluar & " WHERE NO_PENGELUARAN = '" & NomorKeluar & "'"
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
            strSQL = "DELETE FROM TBL_PENGELUARAN WHERE NO_PENGELUARAN = '" & NomorKeluar & "'"
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
End Class
