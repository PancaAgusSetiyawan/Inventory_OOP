﻿Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class FormPengeluaran
    Private objPengeluaran As Pengeluaran
    Private objBarang As Barang
    Private objReader As OleDbDataReader
    Private objStock As StockBarang
    Private dblNewAkhir As Double
    Private dblNewIn As Double
    Private dblNewOut As Double
    Private blnStock As Boolean
    Private tmpQty As Double

    Private Sub LockText()
        txtNomor.ReadOnly = True
        cboBarang.Enabled = False
        txtQty.ReadOnly = True
    End Sub

    Private Sub UnLockText()
        txtNomor.ReadOnly = False
        txtQty.ReadOnly = False
        cboBarang.Enabled = True
    End Sub

    Private Sub TextKosong()
        txtHargaSatuan.Text = ""
        cboBarang.Text = ""
        txtNamaBarang.Text = ""
        txtSatuanBarang.Text = ""
        txtSpecBarang.Text = ""
        txtQty.Text = ""
        txtTgl.Text = ""
        txtNomor.Text = ""
    End Sub

    Private Sub PopulateBarang()
        objBarang = New Barang
        objReader = objBarang.PopulateCode
        If objReader.HasRows Then
            While objReader.Read
                cboBarang.Items.Add(objReader(0))
            End While
        End If
        objReader.Close()
    End Sub

    Private Sub ListGrid()
        objPengeluaran = New Pengeluaran
        grdPengeluaran.DataSource = objPengeluaran.GetDataPengeluaran
    End Sub

    Private Sub FormPengeluaran_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TextKosong()
        Call LockText()
        Call ListGrid()
        Call PopulateBarang()
        ButtonSave.Enabled = False
        ButtonCancel.Enabled = False
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Call UnLockText()
        Call TextKosong()
        txtTgl.Text = Now.Date
        txtNomor.Focus()
        ButtonAdd.Enabled = False
        ButtonEdit.Enabled = False
        ButtonDelete.Enabled = False
        ButtonExit.Enabled = False
        ButtonSave.Enabled = True
        ButtonCancel.Enabled = True
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        If txtNomor.Text = "" Then
            MsgBox("Nomor penerimaan tidak boleh kosong")
            txtNomor.Focus()
        ElseIf cboBarang.Text = "" Then
            MsgBox("Kode barang tidak boleh kosong")
            cboBarang.Focus()
        ElseIf txtQty.Text = "" Then
            MsgBox("Quantity tidak boleh kosong")
            txtQty.Focus()
        Else
            objPengeluaran = New Pengeluaran
            With objPengeluaran
                .KodeBarang = cboBarang.Text
                .NomorKeluar = txtNomor.Text
                .QtyKeluar = txtQty.Text
                .TanggalTrans = txtTgl.Text
                If .IsExist Then
                    MsgBox("DUPLICATE DATA")
                Else
                    If .IsSave Then
                        MsgBox("Data sudah di simpan")
                    Else
                        MsgBox("Data tidak dapat disimpan")
                    End If
                End If
            End With

            dblNewAkhir = CDbl(txtAkhir.Text) - CDbl(txtQty.Text)
            dblNewOut = CDbl(txtOut.Text) + CDbl(txtQty.Text)

            objStock = New StockBarang
            objStock.KodeBarang = cboBarang.Text
            objReader = objStock.GetStock
            If objReader.HasRows Then
                objReader.Read()
                dblNewIn = CDbl(objReader(0))
                dblNewOut = CDbl(objReader(1)) + CDbl(txtQty.Text)
                dblNewAkhir = CDbl(objReader(2)) - CDbl(txtQty.Text)
            Else
                MsgBox("Get Stock Error")
                Exit Sub
            End If
            objReader.Close()
            objStock.QuantityIn = dblNewIn
            objStock.QuantityOut = dblNewOut
            objStock.QuantityAkhir = dblNewAkhir
            If Not objStock.IsUpdate Then
                MsgBox("Error Update Stock")
            End If            

            Call ListGrid()
            Call LockText()
            Call TextKosong()
            ButtonAdd.Enabled = True
            ButtonEdit.Enabled = True
            ButtonDelete.Enabled = True
            ButtonExit.Enabled = True
            ButtonSave.Enabled = False
            ButtonCancel.Enabled = False
        End If
    End Sub

    Private Sub ButtonEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEdit.Click
        If txtNomor.Text = "" Or txtQty.Text = "" Or txtTgl.Text = "" Or cboBarang.Text = "" Then
            MsgBox("Data kosong")
        Else
            If ButtonEdit.Text = "Edit" Then
                ButtonEdit.Text = "Update"
                ButtonAdd.Enabled = False
                ButtonEdit.Enabled = True
                ButtonSave.Enabled = False
                ButtonDelete.Enabled = False
                ButtonCancel.Enabled = True
                ButtonExit.Enabled = False
                tmpQty = CDbl(txtQty.Text)
                Call UnLockText()
                txtNomor.ReadOnly = True
                txtQty.Focus()
            Else
                objPengeluaran = New Pengeluaran
                With objPengeluaran
                    .KodeBarang = cboBarang.Text
                    .NomorKeluar = txtNomor.Text
                    .QtyKeluar = txtQty.Text
                    .TanggalTrans = txtTgl.Text
                    If .IsUpdate Then
                        MsgBox("Data sudah diupdate")
                    Else
                        MsgBox("Data tidak dapat diupdate")
                    End If
                End With

                dblNewAkhir = (CDbl(txtAkhir.Text) + tmpQty) - CDbl(txtQty.Text)
                dblNewIn = CDbl(txtIn.Text)
                dblNewOut = (CDbl(txtOut.Text) + CDbl(txtQty.Text)) - tmpQty

                objStock.QuantityIn = dblNewIn
                objStock.QuantityOut = dblNewOut
                objStock.QuantityAkhir = dblNewAkhir
                objStock.KodeBarang = cboBarang.Text
                If Not objStock.IsUpdate Then
                    MsgBox("Error Update Stock")
                End If

                Call ListGrid()
                Call LockText()
                Call TextKosong()
                ButtonEdit.Text = "Edit"
                ButtonAdd.Enabled = True
                ButtonEdit.Enabled = True
                ButtonDelete.Enabled = True
                ButtonExit.Enabled = True
                ButtonSave.Enabled = False
                ButtonCancel.Enabled = False
            End If
        End If
    End Sub

    Private Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        If txtNomor.Text = "" Or txtQty.Text = "" Or txtTgl.Text = "" Or cboBarang.Text = "" Then
            MsgBox("Data kosong")
        Else
            objPengeluaran = New Pengeluaran
            With objPengeluaran
                .KodeBarang = cboBarang.Text
                .NomorKeluar = txtNomor.Text
                .QtyKeluar = txtQty.Text
                .TanggalTrans = txtTgl.Text
                If .IsDelete Then
                    MsgBox("Data sudah dihapus")
                Else
                    MsgBox("Data tidak dapat dihapus")
                End If
            End With

            dblNewAkhir = CDbl(txtAkhir.Text) + CDbl(txtQty.Text)
            dblNewOut = CDbl(txtOut.Text) - CDbl(txtQty.Text)

            dblNewAkhir = CDbl(txtAkhir.Text) + CDbl(txtQty.Text)
            dblNewOut = CDbl(txtOut.Text) - CDbl(txtQty.Text)
            dblNewIn = CDbl(txtIn.Text)

            objStock.QuantityIn = dblNewIn
            objStock.QuantityOut = dblNewOut
            objStock.QuantityAkhir = dblNewAkhir
            objStock.KodeBarang = cboBarang.Text
            If Not objStock.IsUpdate Then
                MsgBox("Error Update Stock")
            End If

            Call ListGrid()
            Call LockText()
            Call TextKosong()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Call TextKosong()
        Call LockText()
        ButtonAdd.Enabled = True
        ButtonEdit.Enabled = True
        ButtonDelete.Enabled = True
        ButtonExit.Enabled = True
        ButtonSave.Enabled = False
        ButtonCancel.Enabled = False
        ButtonEdit.Text = "Edit"
    End Sub

    Private Sub ButtonExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub

    Private Sub grdPengeluaran_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPengeluaran.Click
        Try
            txtNomor.Text = grdPengeluaran.SelectedCells(0).Value
            txtTgl.Text = grdPengeluaran.SelectedCells(1).Value
            cboBarang.Text = grdPengeluaran.SelectedCells(2).Value
            txtQty.Text = Format(grdPengeluaran.SelectedCells(3).Value, "#,##0.00")
            Call GetStock(cboBarang.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) And e.KeyChar <> Chr(Asc(".")) Then
            e.Handled = True
        End If
    End Sub

    Private Sub cboBarang_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBarang.TextChanged
        Try
            objBarang = New Barang
            objBarang.KodeBarang = cboBarang.Text
            objReader = objBarang.GetName
            If objReader.HasRows Then
                objReader.Read()
                txtNamaBarang.Text = objReader("NM_BRG")
                txtHargaSatuan.Text = objReader("HRG_SAT")
                txtSatuanBarang.Text = objReader("SAT_BRG")
                txtSpecBarang.Text = objReader("SPEC_BRG")
            Else
                objReader.Read()
                txtNamaBarang.Text = ""
                txtHargaSatuan.Text = ""
                txtSatuanBarang.Text = ""
                txtSpecBarang.Text = ""
            End If
            Call GetStock(cboBarang.Text)
        Catch ex As Exception
            grdPengeluaran = Nothing
        End Try
    End Sub

    Private Sub GetStock(ByVal strKode As String)
        Try
            objStock = New StockBarang
            objStock.KodeBarang = strKode
            objReader = objStock.GetStock
            If objReader.HasRows Then
                objReader.Read()
                txtIn.Text = Format(objReader(0), "#,##0.00")
                txtOut.Text = Format(objReader(1), "#,##0.00")
                txtAkhir.Text = Format(objReader(2), "#,##0.00")
            Else
                txtIn.Text = 0
                txtOut.Text = 0
                txtAkhir.Text = 0
            End If
            objReader.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged

    End Sub
End Class