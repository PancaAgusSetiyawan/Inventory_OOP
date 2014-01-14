Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class StockBarang
    Private strSQL As String
    Private myCon As OleDbConnection
    Private objCommand As OleDbCommand
    Private objReader As OleDbDataReader
    Private blnReturn As Boolean
    Private strKodeBarang As String
    Private dblQtyIn As Double
    Private dblQtyOut As Double
    Private dblQTyAkhir As Double

    Public Property KodeBarang() As String
        Get
            Return strKodeBarang
        End Get
        Set(ByVal value As String)
            strKodeBarang = value
        End Set
    End Property

    Public Property QuantityIn() As Double
        Get
            Return dblQtyIn
        End Get
        Set(ByVal value As Double)
            dblQtyIn = value
        End Set
    End Property

    Public Property QuantityOut() As Double
        Get
            Return dblQtyOut
        End Get
        Set(ByVal value As Double)
            dblQtyOut = value
        End Set
    End Property

    Public Property QuantityAkhir() As Double
        Get
            Return dblQTyAkhir
        End Get
        Set(ByVal value As Double)
            dblQTyAkhir = value
        End Set
    End Property

    Public Function IsSave() As Boolean
        myCon = New OleDbConnection(strCon)
        Try
            myCon.Open()
            strSQL = "INSERT INTO TBL_STOCK (KD_BRG,QTY_IN,QTY_OUT,QTY_AKHIR) VALUES('" & KodeBarang & "'," & QuantityIn & "," & QuantityOut & "," & QuantityAkhir & ")"
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
            strSQL = "UPDATE TBL_STOCK SET QTY_IN = " & QuantityIn & ",QTY_OUT = " & QuantityOut & ",QTY_AKHIR = " & dblQTyAkhir & " WHERE KD_BRG ='" & KodeBarang & "'"
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

    Public Function GetStock() As OleDbDataReader
        'menampilkan stok dr database ke textbox scr auto (boleh d bilang pncarian ) 
        Try
            myCon = New OleDbConnection(strCon)
            Dim tmpReader As OleDbDataReader
            myCon.Open()
            strSQL = "SELECT QTY_IN,QTY_OUT,QTY_AKHIR FROM TBL_STOCK WHERE KD_BRG='" & KodeBarang & "'"
            objCommand = New OleDbCommand(strSQL, myCon)
            tmpReader = objCommand.ExecuteReader(CommandBehavior.Default)
            objCommand.Dispose()
            Return tmpReader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
