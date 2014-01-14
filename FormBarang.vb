Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class FormBarang
    Private objBarang As Barang
    Private objStock As StockBarang
    Private objReader As OleDbDataReader

    Private Sub LockText()
        txtHargaSatuan.ReadOnly = True
        txtKodeBarang.ReadOnly = True
        txtNamaBarang.ReadOnly = True
        txtSatuanBarang.ReadOnly = True
        txtSpecBarang.ReadOnly = True
    End Sub

    Private Sub UnLockText()
        txtHargaSatuan.ReadOnly = False
        txtKodeBarang.ReadOnly = False
        txtNamaBarang.ReadOnly = False
        txtSatuanBarang.ReadOnly = False
        txtSpecBarang.ReadOnly = False
    End Sub

    Private Sub TextKosong()
        txtHargaSatuan.Text = ""
        txtKodeBarang.Text = ""
        txtNamaBarang.Text = ""
        txtSatuanBarang.Text = ""
        txtSpecBarang.Text = ""
        txtIn.Text = ""
        txtOut.Text = ""
        txtAkhir.Text = ""
    End Sub

    Private Sub FormBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TextKosong()
        Call LockText()
        Call ListGrid()        
        ButtonSave.Enabled = False
        ButtonCancel.Enabled = False
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Call UnLockText()
        Call TextKosong()
        txtKodeBarang.Focus()
        ButtonAdd.Enabled = False
        ButtonEdit.Enabled = False
        ButtonDelete.Enabled = False
        ButtonExit.Enabled = False
        ButtonSave.Enabled = True
        ButtonCancel.Enabled = True
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        If txtKodeBarang.Text = "" Then
            MsgBox("Kode barang tidak boleh kosong")
            txtKodeBarang.Focus()
        ElseIf txtNamaBarang.Text = "" Then
            MsgBox("Nama barang tidak boleh kosong")
            txtNamaBarang.Focus()
        ElseIf txtSpecBarang.Text = "" Then
            MsgBox("Spec barang tidak boleh kosong")
            txtSpecBarang.Focus()
        ElseIf txtSatuanBarang.Text = "" Then
            MsgBox("Satuan barang tidak boleh kosong")
            txtSatuanBarang.Focus()
        ElseIf txtHargaSatuan.Text = "" Then
            MsgBox("Harga satuan tidak boleh kosong")
            txtHargaSatuan.Focus()
        Else
            objBarang = New Barang
            With objBarang
                .KodeBarang = txtKodeBarang.Text
                .NamaBarang = txtNamaBarang.Text
                .SpecBarang = txtSpecBarang.Text
                .SatuanBarang = txtSatuanBarang.Text
                .HargaSatuan = txtHargaSatuan.Text
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

    Private Sub ListGrid() ' mengisi DgView ( manampilkan isi database pada Datagridvie )
        objBarang = New Barang
        grdBarang.DataSource = objBarang.GetDataBarang
    End Sub

    Private Sub txtHargaSatuan_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHargaSatuan.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) And e.KeyChar <> Chr(Asc(".")) Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdBarang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBarang.Click
        Try
            txtKodeBarang.Text = grdBarang.SelectedCells(0).Value
            txtNamaBarang.Text = grdBarang.SelectedCells(1).Value
            txtSatuanBarang.Text = grdBarang.SelectedCells(2).Value
            txtSpecBarang.Text = grdBarang.SelectedCells(3).Value
            txtHargaSatuan.Text = Format(grdBarang.SelectedCells(4).Value, "#,##0.00")
            Call GetStock(txtKodeBarang.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
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

    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        If txtKodeBarang.Text = "" Or txtNamaBarang.Text = "" Or txtHargaSatuan.Text = "" Or txtSpecBarang.Text = "" Or txtSatuanBarang.Text = "" Then
            MsgBox("Data kosong")
        Else
            objBarang = New Barang
            With objBarang
                .KodeBarang = txtKodeBarang.Text
                .NamaBarang = txtNamaBarang.Text
                .SpecBarang = txtSpecBarang.Text
                .SatuanBarang = txtSatuanBarang.Text
                .HargaSatuan = txtHargaSatuan.Text
                If .IsDelete Then
                    MsgBox("Data sudah dihapus")
                Else
                    MsgBox("Data tidak dapat dihapus")
                End If
            End With
            Call ListGrid()
            Call TextKosong()
            Call LockText()
        End If
    End Sub

    Private Sub ButtonEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit.Click
        If txtKodeBarang.Text = "" Or txtNamaBarang.Text = "" Or txtHargaSatuan.Text = "" Or txtSpecBarang.Text = "" Or txtSatuanBarang.Text = "" Then
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
                Call UnLockText()
                txtKodeBarang.ReadOnly = True
                txtNamaBarang.Focus()
            Else
                objBarang = New Barang
                With objBarang
                    .KodeBarang = txtKodeBarang.Text
                    .NamaBarang = txtNamaBarang.Text
                    .SpecBarang = txtSpecBarang.Text
                    .SatuanBarang = txtSatuanBarang.Text
                    .HargaSatuan = txtHargaSatuan.Text
                    If .IsUpdate Then
                        MsgBox("Data sudah diupdate")
                    Else
                        MsgBox("Data tidak dapat diupdate")
                    End If
                End With
                Call ListGrid()
                Call TextKosong()
                Call LockText()
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

    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
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
End Class