Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class MenuUtama
    Private Sub MenuUtama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim myCon As New OleDbConnection(strCon)
            myCon.Open()
            myCon.Close()
        Catch ex As Exception
            MsgBox("Error connection")
            End
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub BarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarangToolStripMenuItem.Click
        FormBarang.MdiParent = Me
        FormBarang.Show()
    End Sub

    Private Sub PenerimaanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenerimaanToolStripMenuItem.Click
        FormPenerimaan.MdiParent = Me
        FormPenerimaan.Show()
    End Sub

    Private Sub BarangToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarangToolStripMenuItem1.Click
        FormLapBrg.MdiParent = Me
        FormLapBrg.Show()
    End Sub

    Private Sub PenerimaanToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenerimaanToolStripMenuItem1.Click
        FormLapPenerimaan.MdiParent = Me
        FormLapPenerimaan.Show()
    End Sub

    Private Sub PengeluaranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PengeluaranToolStripMenuItem.Click
        FormPengeluaran.MdiParent = Me
        FormPengeluaran.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'Form1.MdiParent = Me
        'Form1.Show()

    End Sub

    Private Sub PengeluaranToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PengeluaranToolStripMenuItem1.Click
        FormLapPengeluaran.MdiParent = Me
        FormLapPengeluaran.Show()
    End Sub
End Class