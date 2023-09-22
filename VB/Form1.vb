Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid

Namespace WindowsApplication1

    Public Partial Class Form1
        Inherits Form

        Private Function CreateTable(ByVal RowCount As Integer) As DataTable
            Dim tbl As DataTable = New DataTable()
            tbl.Columns.Add("Name", GetType(String))
            tbl.Columns.Add("ID", GetType(Integer))
            tbl.Columns.Add("Number", GetType(Integer))
            tbl.Columns.Add("Date", GetType(Date))
            tbl.Columns.Add("Check", GetType(Boolean))
            For i As Integer = 0 To RowCount - 1
                tbl.Rows.Add(New Object() {String.Format("Name{0}", i), i, 3 - i, Date.Now.AddDays(i)})
            Next

            Return tbl
        End Function

        Public Sub New()
            InitializeComponent()
            gridControl1.DataSource = CreateTable(20)
        End Sub

        Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            CheckGridViewClick(e, gridView1)
        End Sub

        Private Shared Sub CheckGridViewClick(ByVal e As MouseEventArgs, ByVal view As GridView)
            Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
            If hi.InRowCell Then
                Dim vi As GridViewInfo = TryCast(view.GetViewInfo(), GridViewInfo)
                Dim cellInfo As GridCellInfo = vi.GetGridCellInfo(hi)
                If cellInfo Is Nothing Then Return
                Dim cInfo As CheckEditViewInfo = TryCast(cellInfo.ViewInfo, CheckEditViewInfo)
                If cInfo Is Nothing Then Return
                Dim r As Rectangle = cInfo.CheckInfo.GlyphRect
                r.Offset(cellInfo.Bounds.Location)
                If r.Contains(e.Location) Then
                    ProcessCheckClick(e, view, hi)
                End If
            End If
        End Sub

        Private Shared Sub ProcessCheckClick(ByVal e As MouseEventArgs, ByVal view As GridView, ByVal hi As GridHitInfo)
            view.FocusedColumn = hi.Column
            view.FocusedRowHandle = hi.RowHandle
            view.ShowEditor()
            Dim edit As CheckEdit = TryCast(view.ActiveEditor, CheckEdit)
            If edit Is Nothing Then Return
            edit.Toggle()
            DXMouseEventArgs.GetMouseArgs(e).Handled = True
        End Sub
    End Class
End Namespace
