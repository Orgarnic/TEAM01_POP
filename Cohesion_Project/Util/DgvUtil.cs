using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Cohesion_Project
{
   class DgvUtil
   {
      public static void DgvInit(DataGridView dgv)
      {
         dgv.BackgroundColor = Color.White;
         dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(188,220,244);
         dgv.Dock = DockStyle.Fill;
         dgv.AllowUserToAddRows = false;
         dgv.AllowUserToDeleteRows = false;
         dgv.AllowUserToResizeRows = false;
         dgv.AllowUserToResizeColumns = false;
         dgv.AutoGenerateColumns = false;
         dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         dgv.MultiSelect = false;
         dgv.Font = new Font("맑은 고딕", 9, FontStyle.Bold);
      }
      /// <summary>
      /// DataGridView TextColumn 추가
      /// </summary>
      /// <param name="dgv">데이터 그리드 뷰</param>
      /// <param name="text">헤더 컬럼</param>
      /// <param name="property">프로퍼티 명</param>
      /// <param name="width">셀 컬럼 넓이</param>
      /// <param name="readOnly">읽기 전용</param>
      /// <param name="align">셀 정렬 (0 왼쪽, 1 중간, 2오른)</param>
      /// <param name="visible"></param>
      /// <param name="frozen">고정</param>
      public static void AddTextCol(DataGridView dgv, string text, string property, int width = 100, bool readOnly = false, int align = 0, bool visible = true, bool frozen = false)
      {
         DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
         col.Name = property;
         col.DataPropertyName = property;
         col.HeaderText = text;
         col.Width = width;
         col.Visible = visible;
         col.ReadOnly = readOnly;
         col.Frozen = frozen;
         col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
         col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
         switch (align)
         {
            case 0:
               col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
               break;
            case 1:
               col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
               break;
            case 2:
               col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
               break;
         }
         dgv.Columns.Add(col);
      }
      public static void AddComboxCol(DataGridView dgv, List<Object> list, string text, string property, int width = 100, bool readOnly = false, bool frozen = false)
      {
         DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
         col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
         col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
         col.DataSource = list;
         col.HeaderText = text;
         col.DataPropertyName = property;
         col.Width = width;
         col.ReadOnly = readOnly;
         col.Frozen = frozen;
         col.DisplayMember = "Code_Name";
         col.ValueMember = "Code";
         dgv.Columns.Add(col);
      }
      public static void AddButtonCol(DataGridView dgv, string text, string property, int width = 100, string cellText = "", bool frozen = false, bool visible = true)
      {
         DataGridViewButtonColumn col = new DataGridViewButtonColumn();
         col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
         col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
         col.UseColumnTextForButtonValue = true;
         col.DefaultCellStyle.BackColor = Color.White;
         col.HeaderText = text;
         col.DataPropertyName = property;
         col.Width = width;
         col.Text = cellText;
         col.Frozen = frozen;
         col.Visible = visible;
         dgv.Columns.Add(col);
      }
      public static void AddCheckBoxCol(DataGridView dgv, string text, string property, int width = 100, bool frozen = false, bool visible = true)
      {
         DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
         col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
         col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
         col.HeaderText = text;
         col.DataPropertyName = property;
         col.Width = width;
         col.Frozen = frozen;
         col.Visible = visible;
         dgv.Columns.Add(col);
      }
      public static T DgvToDto<T>(DataGridView dgv)
      {
         return (T)dgv.Rows[dgv.CurrentRow.Index].DataBoundItem;
      }
   }
}
