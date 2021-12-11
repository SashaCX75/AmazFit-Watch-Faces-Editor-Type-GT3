
namespace ControlLibrary
{
    partial class UCtrl_Segments_Opt
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox_image = new System.Windows.Forms.ComboBox();
            this.label01 = new System.Windows.Forms.Label();
            this.dataGridView_coordinates_set = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label02 = new System.Windows.Forms.Label();
            this.contextMenuStrip_XY_InTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьСтрокуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_coordinates_set)).BeginInit();
            this.contextMenuStrip_XY_InTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_image
            // 
            this.comboBox_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_image.DropDownWidth = 135;
            this.comboBox_image.FormattingEnabled = true;
            this.comboBox_image.Location = new System.Drawing.Point(3, 34);
            this.comboBox_image.MaxDropDownItems = 25;
            this.comboBox_image.Name = "comboBox_image";
            this.comboBox_image.Size = new System.Drawing.Size(76, 21);
            this.comboBox_image.TabIndex = 162;
            this.comboBox_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label01
            // 
            this.label01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label01.Location = new System.Drawing.Point(3, 0);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(80, 30);
            this.label01.TabIndex = 161;
            this.label01.Text = "Стартовое изображение";
            this.label01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dataGridView_coordinates_set
            // 
            this.dataGridView_coordinates_set.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_coordinates_set.ColumnHeadersHeight = 21;
            this.dataGridView_coordinates_set.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView_coordinates_set.ContextMenuStrip = this.contextMenuStrip_XY_InTable;
            this.dataGridView_coordinates_set.EnableHeadersVisualStyles = false;
            this.dataGridView_coordinates_set.Location = new System.Drawing.Point(3, 86);
            this.dataGridView_coordinates_set.Name = "dataGridView_coordinates_set";
            this.dataGridView_coordinates_set.RowHeadersWidth = 45;
            this.dataGridView_coordinates_set.RowTemplate.Height = 18;
            this.dataGridView_coordinates_set.Size = new System.Drawing.Size(165, 213);
            this.dataGridView_coordinates_set.TabIndex = 163;
            this.dataGridView_coordinates_set.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_coordinates_set_CellClick);
            this.dataGridView_coordinates_set.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_coordinates_set_CellEndEdit);
            this.dataGridView_coordinates_set.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_coordinates_set_CellMouseDoubleClick);
            this.dataGridView_coordinates_set.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_coordinates_set_CellMouseDown);
            this.dataGridView_coordinates_set.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView_coordinates_set_RowPrePaint);
            this.dataGridView_coordinates_set.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_coordinates_set_RowsRemoved);
            this.dataGridView_coordinates_set.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_coordinates_set_UserAddedRow);
            this.dataGridView_coordinates_set.EnabledChanged += new System.EventHandler(this.dataGridView_coordinates_set_EnabledChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "X";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Y";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label02
            // 
            this.label02.AutoSize = true;
            this.label02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label02.Location = new System.Drawing.Point(3, 68);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(95, 13);
            this.label02.TabIndex = 164;
            this.label02.Text = "Набор координат";
            this.label02.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // contextMenuStrip_XY_InTable
            // 
            this.contextMenuStrip_XY_InTable.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_XY_InTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатыToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.вставитьToolStripMenuItem,
            this.удалитьСтрокуToolStripMenuItem});
            this.contextMenuStrip_XY_InTable.Name = "contextMenuStrip_XY_InTable";
            this.contextMenuStrip_XY_InTable.Size = new System.Drawing.Size(197, 108);
            this.contextMenuStrip_XY_InTable.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_XY_InTable_Opening);
            // 
            // вставитьКоординатыToolStripMenuItem
            // 
            this.вставитьКоординатыToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.Actions_insert_text_icon;
            this.вставитьКоординатыToolStripMenuItem.Name = "вставитьКоординатыToolStripMenuItem";
            this.вставитьКоординатыToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.вставитьКоординатыToolStripMenuItem.Text = "Вставить координаты";
            this.вставитьКоординатыToolStripMenuItem.Click += new System.EventHandler(this.вставитьКоординатыToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.Files_Copy_File_icon;
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            this.копироватьToolStripMenuItem.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.Files_Clipboard_icon;
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // удалитьСтрокуToolStripMenuItem
            // 
            this.удалитьСтрокуToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.table_row_delete_icon;
            this.удалитьСтрокуToolStripMenuItem.Name = "удалитьСтрокуToolStripMenuItem";
            this.удалитьСтрокуToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.удалитьСтрокуToolStripMenuItem.Text = "Удалить строку";
            this.удалитьСтрокуToolStripMenuItem.Click += new System.EventHandler(this.удалитьСтрокуToolStripMenuItem_Click);
            // 
            // UCtrl_Segments_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView_coordinates_set);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.comboBox_image);
            this.Controls.Add(this.label01);
            this.Name = "UCtrl_Segments_Opt";
            this.Size = new System.Drawing.Size(250, 316);
            this.Load += new System.EventHandler(this.UCtrl_Segments_Opt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_coordinates_set)).EndInit();
            this.contextMenuStrip_XY_InTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_image;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.DataGridView dataGridView_coordinates_set;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_XY_InTable;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьСтрокуToolStripMenuItem;
    }
}
