namespace GalsPassHolder
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOptionsToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChangeMainPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.tblLyoMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnKeyDelete = new System.Windows.Forms.Button();
            this.btnKeyEdit = new System.Windows.Forms.Button();
            this.btnKeyAdd = new System.Windows.Forms.Button();
            this.tblLyoNoteData = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeleteVersion = new System.Windows.Forms.Button();
            this.cmbVersionSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPasteTo = new System.Windows.Forms.Button();
            this.btnCopyFrom = new System.Windows.Forms.Button();
            this.lblClipBoard = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoteData = new System.Windows.Forms.TextBox();
            this.lblNoteData = new System.Windows.Forms.Label();
            this.btnSaveNoteData = new System.Windows.Forms.Button();
            this.btnClearNoteData = new System.Windows.Forms.Button();
            this.btnClearClipboard = new System.Windows.Forms.Button();
            this.btnMakePassword = new System.Windows.Forms.Button();
            this.dataGridViewKeys = new System.Windows.Forms.DataGridView();
            this.dataGridViewNotes = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNoteDelete = new System.Windows.Forms.Button();
            this.btnNoteEdit = new System.Windows.Forms.Button();
            this.btnNoteAdd = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripMain.SuspendLayout();
            this.tblLyoMain.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblLyoNoteData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotes)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.Color.LightGray;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuItemOptionsToolStrip});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(584, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.menuItemSaveExit,
            this.menuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNew.Size = new System.Drawing.Size(183, 22);
            this.menuItemNew.Text = "&New";
            this.menuItemNew.Click += new System.EventHandler(this.Menu_NewToolStripMenuItem_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.menuItemOpen.Size = new System.Drawing.Size(183, 22);
            this.menuItemOpen.Text = "&Open";
            this.menuItemOpen.Click += new System.EventHandler(this.Menu_OpenToolStripMenuItem_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(183, 22);
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.Menu_SaveToolStripMenuItem_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            this.menuItemSaveAs.Size = new System.Drawing.Size(183, 22);
            this.menuItemSaveAs.Text = "Save As";
            this.menuItemSaveAs.Click += new System.EventHandler(this.Menu_SaveAsToolStripMenuItem_Click);
            // 
            // menuItemSaveExit
            // 
            this.menuItemSaveExit.Name = "menuItemSaveExit";
            this.menuItemSaveExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.menuItemSaveExit.Size = new System.Drawing.Size(183, 22);
            this.menuItemSaveExit.Text = "Save and &Exit";
            this.menuItemSaveExit.Click += new System.EventHandler(this.Menu_CloseToolStripMenuItem_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(183, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.Menu_CloseWithoutSavingToolStripMenuItem_Click);
            // 
            // menuItemOptionsToolStrip
            // 
            this.menuItemOptionsToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChangeMainPassword});
            this.menuItemOptionsToolStrip.Name = "menuItemOptionsToolStrip";
            this.menuItemOptionsToolStrip.Size = new System.Drawing.Size(61, 20);
            this.menuItemOptionsToolStrip.Text = "Options";
            // 
            // menuItemChangeMainPassword
            // 
            this.menuItemChangeMainPassword.Name = "menuItemChangeMainPassword";
            this.menuItemChangeMainPassword.Size = new System.Drawing.Size(198, 22);
            this.menuItemChangeMainPassword.Text = "Change main password";
            this.menuItemChangeMainPassword.Click += new System.EventHandler(this.Menu_changeMainPasswordToolStripMenuItem_Click);
            // 
            // tblLyoMain
            // 
            this.tblLyoMain.ColumnCount = 3;
            this.tblLyoMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLyoMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLyoMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLyoMain.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tblLyoMain.Controls.Add(this.tblLyoNoteData, 2, 0);
            this.tblLyoMain.Controls.Add(this.dataGridViewKeys, 1, 0);
            this.tblLyoMain.Controls.Add(this.dataGridViewNotes, 0, 0);
            this.tblLyoMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblLyoMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLyoMain.Location = new System.Drawing.Point(0, 24);
            this.tblLyoMain.Name = "tblLyoMain";
            this.tblLyoMain.RowCount = 2;
            this.tblLyoMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblLyoMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLyoMain.Size = new System.Drawing.Size(584, 315);
            this.tblLyoMain.TabIndex = 100;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btnKeyDelete, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnKeyEdit, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnKeyAdd, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(146, 283);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(146, 32);
            this.tableLayoutPanel2.TabIndex = 102;
            // 
            // btnKeyDelete
            // 
            this.btnKeyDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKeyDelete.Location = new System.Drawing.Point(96, 0);
            this.btnKeyDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnKeyDelete.Name = "btnKeyDelete";
            this.btnKeyDelete.Size = new System.Drawing.Size(50, 32);
            this.btnKeyDelete.TabIndex = 2;
            this.btnKeyDelete.TabStop = false;
            this.btnKeyDelete.Text = "Delete";
            this.btnKeyDelete.UseVisualStyleBackColor = true;
            this.btnKeyDelete.Click += new System.EventHandler(this.NoteKey_btnKeyDelete_Click);
            // 
            // btnKeyEdit
            // 
            this.btnKeyEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKeyEdit.Location = new System.Drawing.Point(48, 0);
            this.btnKeyEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnKeyEdit.Name = "btnKeyEdit";
            this.btnKeyEdit.Size = new System.Drawing.Size(48, 32);
            this.btnKeyEdit.TabIndex = 1;
            this.btnKeyEdit.TabStop = false;
            this.btnKeyEdit.Text = "Edit";
            this.btnKeyEdit.UseVisualStyleBackColor = true;
            this.btnKeyEdit.Click += new System.EventHandler(this.NoteKey_btnKeyEdit_Click);
            // 
            // btnKeyAdd
            // 
            this.btnKeyAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKeyAdd.Location = new System.Drawing.Point(0, 0);
            this.btnKeyAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnKeyAdd.Name = "btnKeyAdd";
            this.btnKeyAdd.Size = new System.Drawing.Size(48, 32);
            this.btnKeyAdd.TabIndex = 0;
            this.btnKeyAdd.TabStop = false;
            this.btnKeyAdd.Text = "A&dd";
            this.btnKeyAdd.UseVisualStyleBackColor = true;
            this.btnKeyAdd.Click += new System.EventHandler(this.NoteKey_btnKeyAdd_Click);
            // 
            // tblLyoNoteData
            // 
            this.tblLyoNoteData.ColumnCount = 5;
            this.tblLyoNoteData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9984F));
            this.tblLyoNoteData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9984F));
            this.tblLyoNoteData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9984F));
            this.tblLyoNoteData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0024F));
            this.tblLyoNoteData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0024F));
            this.tblLyoNoteData.Controls.Add(this.btnDeleteVersion, 4, 0);
            this.tblLyoNoteData.Controls.Add(this.cmbVersionSelect, 2, 0);
            this.tblLyoNoteData.Controls.Add(this.label1, 0, 0);
            this.tblLyoNoteData.Controls.Add(this.btnPasteTo, 0, 6);
            this.tblLyoNoteData.Controls.Add(this.btnCopyFrom, 0, 7);
            this.tblLyoNoteData.Controls.Add(this.lblClipBoard, 1, 6);
            this.tblLyoNoteData.Controls.Add(this.label2, 1, 5);
            this.tblLyoNoteData.Controls.Add(this.txtNoteData, 0, 2);
            this.tblLyoNoteData.Controls.Add(this.lblNoteData, 0, 1);
            this.tblLyoNoteData.Controls.Add(this.btnSaveNoteData, 0, 4);
            this.tblLyoNoteData.Controls.Add(this.btnClearNoteData, 1, 4);
            this.tblLyoNoteData.Controls.Add(this.btnClearClipboard, 4, 6);
            this.tblLyoNoteData.Controls.Add(this.btnMakePassword, 2, 4);
            this.tblLyoNoteData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLyoNoteData.Enabled = false;
            this.tblLyoNoteData.Location = new System.Drawing.Point(295, 0);
            this.tblLyoNoteData.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tblLyoNoteData.Name = "tblLyoNoteData";
            this.tblLyoNoteData.RowCount = 8;
            this.tblLyoMain.SetRowSpan(this.tblLyoNoteData, 2);
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblLyoNoteData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblLyoNoteData.Size = new System.Drawing.Size(289, 315);
            this.tblLyoNoteData.TabIndex = 100;
            this.tblLyoNoteData.Visible = false;
            this.tblLyoNoteData.EnabledChanged += new System.EventHandler(this.NoteData_TblLyoNoteData_EnabledChanged);
            // 
            // btnDeleteVersion
            // 
            this.btnDeleteVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteVersion.Location = new System.Drawing.Point(228, 0);
            this.btnDeleteVersion.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteVersion.MinimumSize = new System.Drawing.Size(20, 20);
            this.btnDeleteVersion.Name = "btnDeleteVersion";
            this.btnDeleteVersion.Size = new System.Drawing.Size(61, 20);
            this.btnDeleteVersion.TabIndex = 10;
            this.btnDeleteVersion.TabStop = false;
            this.btnDeleteVersion.Text = "Delete";
            this.btnDeleteVersion.UseVisualStyleBackColor = true;
            this.btnDeleteVersion.Click += new System.EventHandler(this.NoteData_btnDeleteVersion_Click);
            // 
            // cmbVersionSelect
            // 
            this.tblLyoNoteData.SetColumnSpan(this.cmbVersionSelect, 2);
            this.cmbVersionSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVersionSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersionSelect.FormattingEnabled = true;
            this.cmbVersionSelect.Location = new System.Drawing.Point(114, 1);
            this.cmbVersionSelect.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.cmbVersionSelect.Name = "cmbVersionSelect";
            this.cmbVersionSelect.Size = new System.Drawing.Size(114, 21);
            this.cmbVersionSelect.TabIndex = 9;
            this.cmbVersionSelect.TabStop = false;
            this.cmbVersionSelect.SelectedIndexChanged += new System.EventHandler(this.NoteData_cmbVersionSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tblLyoNoteData.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "version:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPasteTo
            // 
            this.btnPasteTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPasteTo.Location = new System.Drawing.Point(0, 235);
            this.btnPasteTo.Margin = new System.Windows.Forms.Padding(0);
            this.btnPasteTo.Name = "btnPasteTo";
            this.btnPasteTo.Size = new System.Drawing.Size(57, 37);
            this.btnPasteTo.TabIndex = 4;
            this.btnPasteTo.Text = "&Paste To:";
            this.btnPasteTo.UseVisualStyleBackColor = true;
            this.btnPasteTo.Click += new System.EventHandler(this.NoteData_Clipboard_BtnPasteTo_Click);
            // 
            // btnCopyFrom
            // 
            this.btnCopyFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyFrom.Location = new System.Drawing.Point(0, 272);
            this.btnCopyFrom.Margin = new System.Windows.Forms.Padding(0);
            this.btnCopyFrom.Name = "btnCopyFrom";
            this.btnCopyFrom.Size = new System.Drawing.Size(57, 43);
            this.btnCopyFrom.TabIndex = 5;
            this.btnCopyFrom.TabStop = false;
            this.btnCopyFrom.Text = "Copy From:";
            this.btnCopyFrom.UseVisualStyleBackColor = true;
            this.btnCopyFrom.Click += new System.EventHandler(this.NoteData_Clipboard_BtnCopyFrom_Click);
            // 
            // lblClipBoard
            // 
            this.lblClipBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tblLyoNoteData.SetColumnSpan(this.lblClipBoard, 3);
            this.lblClipBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClipBoard.Location = new System.Drawing.Point(59, 236);
            this.lblClipBoard.Margin = new System.Windows.Forms.Padding(2, 1, 2, 0);
            this.lblClipBoard.Name = "lblClipBoard";
            this.tblLyoNoteData.SetRowSpan(this.lblClipBoard, 2);
            this.lblClipBoard.Size = new System.Drawing.Size(167, 79);
            this.lblClipBoard.TabIndex = 12;
            this.lblClipBoard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tblLyoNoteData.SetColumnSpan(this.label2, 3);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(60, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "System clip board";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtNoteData
            // 
            this.tblLyoNoteData.SetColumnSpan(this.txtNoteData, 5);
            this.txtNoteData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNoteData.Location = new System.Drawing.Point(1, 36);
            this.txtNoteData.Margin = new System.Windows.Forms.Padding(1, 0, 1, 2);
            this.txtNoteData.Multiline = true;
            this.txtNoteData.Name = "txtNoteData";
            this.tblLyoNoteData.SetRowSpan(this.txtNoteData, 2);
            this.txtNoteData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNoteData.Size = new System.Drawing.Size(287, 148);
            this.txtNoteData.TabIndex = 3;
            this.txtNoteData.TextChanged += new System.EventHandler(this.NotesData_txtNoteData_TextChanged);
            this.txtNoteData.Enter += new System.EventHandler(this.NoteData_txtNoteData_Enter);
            this.txtNoteData.Leave += new System.EventHandler(this.NoteData_txtNoteData_Leave);
            // 
            // lblNoteData
            // 
            this.lblNoteData.AutoSize = true;
            this.tblLyoNoteData.SetColumnSpan(this.lblNoteData, 5);
            this.lblNoteData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoteData.Location = new System.Drawing.Point(3, 18);
            this.lblNoteData.Name = "lblNoteData";
            this.lblNoteData.Size = new System.Drawing.Size(283, 18);
            this.lblNoteData.TabIndex = 13;
            this.lblNoteData.Text = "Note : Item : Data";
            this.lblNoteData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnSaveNoteData
            // 
            this.btnSaveNoteData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveNoteData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveNoteData.Location = new System.Drawing.Point(0, 186);
            this.btnSaveNoteData.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveNoteData.MinimumSize = new System.Drawing.Size(35, 20);
            this.btnSaveNoteData.Name = "btnSaveNoteData";
            this.btnSaveNoteData.Size = new System.Drawing.Size(57, 31);
            this.btnSaveNoteData.TabIndex = 6;
            this.btnSaveNoteData.Text = "&Save";
            this.btnSaveNoteData.UseVisualStyleBackColor = true;
            this.btnSaveNoteData.Click += new System.EventHandler(this.NoteData_btnSaveNoteData_Click);
            // 
            // btnClearNoteData
            // 
            this.btnClearNoteData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearNoteData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearNoteData.Location = new System.Drawing.Point(57, 186);
            this.btnClearNoteData.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearNoteData.MinimumSize = new System.Drawing.Size(35, 20);
            this.btnClearNoteData.Name = "btnClearNoteData";
            this.btnClearNoteData.Size = new System.Drawing.Size(57, 31);
            this.btnClearNoteData.TabIndex = 8;
            this.btnClearNoteData.TabStop = false;
            this.btnClearNoteData.Text = "Clear";
            this.btnClearNoteData.UseVisualStyleBackColor = true;
            this.btnClearNoteData.Click += new System.EventHandler(this.NoteData_btnClearNoteData_Click);
            // 
            // btnClearClipboard
            // 
            this.btnClearClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearClipboard.Location = new System.Drawing.Point(228, 235);
            this.btnClearClipboard.Margin = new System.Windows.Forms.Padding(0);
            this.btnClearClipboard.Name = "btnClearClipboard";
            this.tblLyoNoteData.SetRowSpan(this.btnClearClipboard, 2);
            this.btnClearClipboard.Size = new System.Drawing.Size(61, 80);
            this.btnClearClipboard.TabIndex = 11;
            this.btnClearClipboard.TabStop = false;
            this.btnClearClipboard.Text = "Clear";
            this.btnClearClipboard.UseVisualStyleBackColor = true;
            this.btnClearClipboard.Click += new System.EventHandler(this.NoteData_Clipboard_btnClearClipboard_Click);
            // 
            // btnMakePassword
            // 
            this.tblLyoNoteData.SetColumnSpan(this.btnMakePassword, 2);
            this.btnMakePassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMakePassword.Location = new System.Drawing.Point(114, 186);
            this.btnMakePassword.Margin = new System.Windows.Forms.Padding(0);
            this.btnMakePassword.Name = "btnMakePassword";
            this.btnMakePassword.Size = new System.Drawing.Size(114, 31);
            this.btnMakePassword.TabIndex = 7;
            this.btnMakePassword.Text = "Generate Password";
            this.btnMakePassword.UseVisualStyleBackColor = true;
            this.btnMakePassword.Click += new System.EventHandler(this.NoteData_BtnMakePassword_Click);
            // 
            // dataGridViewKeys
            // 
            this.dataGridViewKeys.AllowUserToResizeColumns = false;
            this.dataGridViewKeys.AllowUserToResizeRows = false;
            this.dataGridViewKeys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewKeys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewKeys.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewKeys.Enabled = false;
            this.dataGridViewKeys.Location = new System.Drawing.Point(149, 3);
            this.dataGridViewKeys.MultiSelect = false;
            this.dataGridViewKeys.Name = "dataGridViewKeys";
            this.dataGridViewKeys.Size = new System.Drawing.Size(140, 277);
            this.dataGridViewKeys.TabIndex = 2;
            this.dataGridViewKeys.Visible = false;
            this.dataGridViewKeys.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridViewKeys_DataBindingComplete);
            this.dataGridViewKeys.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewKeys_RowLeave);
            this.dataGridViewKeys.SelectionChanged += new System.EventHandler(this.DataGridViewKeys_SelectionChanged);
            this.dataGridViewKeys.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewKeys_UserAddedRow);
            this.dataGridViewKeys.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewKeys_UserDeletedRow);
            this.dataGridViewKeys.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridViewKeys_UserDeletingRow);
            this.dataGridViewKeys.EnabledChanged += new System.EventHandler(this.DataGridViewKeys_EnabledChanged);
            this.dataGridViewKeys.VisibleChanged += new System.EventHandler(this.FrmMain_child_VisibleChanged);
            this.dataGridViewKeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridViewKeys_KeyDown);
            this.dataGridViewKeys.Leave += new System.EventHandler(this.DataGridViewKeys_Leave);
            // 
            // dataGridViewNotes
            // 
            this.dataGridViewNotes.AllowUserToResizeColumns = false;
            this.dataGridViewNotes.AllowUserToResizeRows = false;
            this.dataGridViewNotes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewNotes.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewNotes.Enabled = false;
            this.dataGridViewNotes.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewNotes.MultiSelect = false;
            this.dataGridViewNotes.Name = "dataGridViewNotes";
            this.dataGridViewNotes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridViewNotes.Size = new System.Drawing.Size(140, 277);
            this.dataGridViewNotes.TabIndex = 1;
            this.dataGridViewNotes.Visible = false;
            this.dataGridViewNotes.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewNotes_RowLeave);
            this.dataGridViewNotes.SelectionChanged += new System.EventHandler(this.DataGridViewNotes_SelectionChanged);
            this.dataGridViewNotes.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewNotes_UserAddedRow);
            this.dataGridViewNotes.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewNotes_UserDeletedRow);
            this.dataGridViewNotes.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridViewNotes_UserDeletingRow);
            this.dataGridViewNotes.EnabledChanged += new System.EventHandler(this.FrmMain_child_VisibleChanged);
            this.dataGridViewNotes.VisibleChanged += new System.EventHandler(this.FrmMain_child_VisibleChanged);
            this.dataGridViewNotes.Leave += new System.EventHandler(this.DataGridViewNotes_Leave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnNoteDelete, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNoteEdit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNoteAdd, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 283);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(146, 32);
            this.tableLayoutPanel1.TabIndex = 101;
            // 
            // btnNoteDelete
            // 
            this.btnNoteDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoteDelete.Location = new System.Drawing.Point(96, 0);
            this.btnNoteDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnNoteDelete.Name = "btnNoteDelete";
            this.btnNoteDelete.Size = new System.Drawing.Size(50, 32);
            this.btnNoteDelete.TabIndex = 2;
            this.btnNoteDelete.TabStop = false;
            this.btnNoteDelete.Text = "Delete";
            this.btnNoteDelete.UseVisualStyleBackColor = true;
            this.btnNoteDelete.Click += new System.EventHandler(this.Notes_btnNoteDelete_Click);
            // 
            // btnNoteEdit
            // 
            this.btnNoteEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoteEdit.Location = new System.Drawing.Point(48, 0);
            this.btnNoteEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnNoteEdit.Name = "btnNoteEdit";
            this.btnNoteEdit.Size = new System.Drawing.Size(48, 32);
            this.btnNoteEdit.TabIndex = 1;
            this.btnNoteEdit.TabStop = false;
            this.btnNoteEdit.Text = "Edit";
            this.btnNoteEdit.UseVisualStyleBackColor = true;
            this.btnNoteEdit.Click += new System.EventHandler(this.Notes_btnNoteEdit_Click);
            // 
            // btnNoteAdd
            // 
            this.btnNoteAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoteAdd.Location = new System.Drawing.Point(0, 0);
            this.btnNoteAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnNoteAdd.Name = "btnNoteAdd";
            this.btnNoteAdd.Size = new System.Drawing.Size(48, 32);
            this.btnNoteAdd.TabIndex = 0;
            this.btnNoteAdd.TabStop = false;
            this.btnNoteAdd.Text = "&Add";
            this.btnNoteAdd.UseVisualStyleBackColor = true;
            this.btnNoteAdd.Click += new System.EventHandler(this.Notes_btnNoteAdd_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 100;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.tblLyoMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximumSize = new System.Drawing.Size(60000, 60000);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FrmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Galantha\'s Encrypted Notes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ResizeBegin += new System.EventHandler(this.FrmMain_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.FrmMain_ResizeEnd);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tblLyoMain.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tblLyoNoteData.ResumeLayout(false);
            this.tblLyoNoteData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeys)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotes)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.TableLayoutPanel tblLyoMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveExit;
        private System.Windows.Forms.DataGridView dataGridViewNotes;
        private System.Windows.Forms.DataGridView dataGridViewKeys;
        private System.Windows.Forms.TableLayoutPanel tblLyoNoteData;
        private System.Windows.Forms.Button btnClearNoteData;
        private System.Windows.Forms.Button btnSaveNoteData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteVersion;
        private System.Windows.Forms.Button btnPasteTo;
        private System.Windows.Forms.Button btnCopyFrom;
        private System.Windows.Forms.Label lblClipBoard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNoteData;
        private System.Windows.Forms.Label lblNoteData;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.Button btnClearClipboard;
        private System.Windows.Forms.Button btnMakePassword;
        private System.Windows.Forms.ToolStripMenuItem menuItemOptionsToolStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemChangeMainPassword;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnKeyDelete;
        private System.Windows.Forms.Button btnKeyEdit;
        private System.Windows.Forms.Button btnKeyAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnNoteDelete;
        private System.Windows.Forms.Button btnNoteEdit;
        private System.Windows.Forms.Button btnNoteAdd;
        public System.Windows.Forms.ComboBox cmbVersionSelect;
    }
}

