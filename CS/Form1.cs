using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            tbl.Columns.Add("Check", typeof(bool));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            CheckGridViewClick(e, gridView1);
        }
        private static void CheckGridViewClick(MouseEventArgs e, GridView view)
        {
            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.InRowCell)
            {
                GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
                GridCellInfo cellInfo = vi.GetGridCellInfo(hi);
                if (cellInfo == null)
                    return;
                CheckEditViewInfo cInfo = cellInfo.ViewInfo as CheckEditViewInfo;
                if (cInfo == null)
                    return;
                Rectangle r = cInfo.CheckInfo.GlyphRect;
                r.Offset(cellInfo.Bounds.Location);
                if (r.Contains(e.Location))
                {
                    ProcessCheckClick(e, view, hi);
                }
            }
        }
        private static void ProcessCheckClick(MouseEventArgs e, GridView view, GridHitInfo hi)
        {
            view.FocusedColumn = hi.Column;
            view.FocusedRowHandle = hi.RowHandle;
            view.ShowEditor();
            CheckEdit edit = view.ActiveEditor as CheckEdit;
            if (edit == null) return;
            edit.Toggle();
            DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

    }
}
