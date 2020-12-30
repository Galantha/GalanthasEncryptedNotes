/*Created by Layla aka Galantha
 * MIT license
 * no warranty
 * 
 * Copyright 2020-12-27
 * 
 * email: gal_0xff@outlook.com
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalsPassHolder
{
    public partial class FrmMain : Form
    {

        private const float defaultWidth = 600f;
        private const float defaultHeight = 400f;
        private const float defaultFontSize = 8.25f;
        private const string notesFileExtension = "gendb"; //seems a somewhat unique extension
        private const string notesFileExtensionFilter = "Gal Encrypted Note DataBase (*." + notesFileExtension + ")|*." + notesFileExtension + "|All Files (*.*)|*.*";
        private const string notesFileDefault = "GalsEncryptedNotesDefaultDatabase." + notesFileExtension;
        private static readonly IFormatProvider inv = GalFormFunctions.inv;

        private bool formClosingFlag = false;
        public bool closeWithoutSaving = false; //gets set by exception handler to prevent writing bad file
        private bool FrmMain_Shown_firstRun = true;
        private bool allowResizeUpdate = false;
        private bool resizingThreadExit = true;

        private string mainHashKey = "";
        private string fileName = "";
        public static string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GalanthasEncryptedNotes";

        private DsGalNoteManager dsGalData;

        private DataGridViewRow dataGridViewNotes_oldCurrentRow = null;
        private int dataGridViewNotes_LastFocusedRowIndex = -1;
        private Boolean DtViewNotesDataChangedHandler_ignoreEvents = false;
        private int dataGridViewNotes_lastIdEdited = -1;
        private DataRowChangeEventHandler dtNotes_drceh = null;

        private DataGridViewRow dataGridViewKeys_oldCurrentRow = null;
        private int dataGridViewKeys_lastRowFocusedIndex = -1;
        private string dataGridViewKeys_salt = "";
        private int dataGridViewKeys_dtNoteKeys_id = -1;
        private const int dtNotesKeys_iterations = 100;
        private bool dataGridViewKeys_ignoreUpdate = true;
        private int dataGridViewKeys_lastIdEdited = -1;

        private bool NoteData_cmbVersionSelect_ignoreEvents = true;
        private bool NoteData_ignoreUpdates = true;

        public FrmMain()
        {
            InitializeComponent();

            try
            {
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);
            }
            catch
            {
                folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            string version;
            try
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (System.Deployment.Application.InvalidDeploymentException)
            {
                version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }

            lblVersion.Text = version; //+ " " + "Copyright " + DateTime.Now.Year.ToString() + " MIT";
        }

        private void FrmMain_ResizeBegin(object sender, EventArgs e)
        {
            if (!allowResizeUpdate)
                return;

            resizingThreadExit = false;
            var endTime = DateTime.Now.AddSeconds(30);
            var t = new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(20);
                while (!resizingThreadExit && !formClosingFlag && allowResizeUpdate && DateTime.Now < endTime)
                {
                    Invoke(new Action(() => FrmMain_UpdateAfterResize())); // update after resize is expensive, so the goal here is to ignore most of the resize events
                    System.Threading.Thread.Sleep(20);
                }
                resizingThreadExit = true;
            });
            t.IsBackground = true;
            t.Start();
        }
        private void FrmMain_ResizeEnd(object sender, EventArgs e)
        {
            resizingThreadExit = true;
            FrmMain_UpdateAfterResize();
        }
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (resizingThreadExit)
                FrmMain_UpdateAfterResize();
        }
        private void FrmMain_UpdateAfterResize()
        {
            if (!allowResizeUpdate)
                return;

            if (btnDeleteVersion.Width < 50)
                btnDeleteVersion.Text = "Del";
            else
                btnDeleteVersion.Text = "Delete";
            if (btnDeleteVersion.Width < 58)
            {
                btnPasteFrom.Text = "Fr" + Environment.NewLine + "om";
                btnCopyTo.Text = "To";
            }
            else
            {
                btnPasteFrom.Text = "Paste" + Environment.NewLine + "from:";
                btnCopyTo.Text = "Copy" + Environment.NewLine + "to:";
            }

            float currentWidth = this.Width;
            float currentHeight = this.Height;

            const float fontMultiplier = 1.0f;
            float newFontSizeW = (currentWidth / defaultWidth) * fontMultiplier * defaultFontSize;
            float newFontSizeH = (currentHeight / defaultHeight) * fontMultiplier * defaultFontSize;
            float newFontSize;
            if (newFontSizeH < newFontSizeW)
                newFontSize = newFontSizeH;
            else
                newFontSize = newFontSizeW;

            var nf = new Font("Microsoft Sans Serif", newFontSize);
            GalFormFunctions.RecursiveSetProperty<Font>(this, "Font", nf, new List<Type>() { btnKeyAdd.GetType(), cmbVersionSelect.GetType(), menuStripMain.GetType(), menuItemOptionsToolStrip.GetType(), menuItemNew.GetType(), lblClipBoard.GetType(), dataGridViewNotes.GetType() }, excludeControls: new List<Control>() { lblVersion }); //Type.GetType("DataGridViewRow")  //Type.GetType("DataGridViewColumn") //dataGridViewNotes.GetType()

            var nf2 = new Font(nf.Name, Convert.ToSingle(newFontSize * 1.1));
            GalFormFunctions.RecursiveSetProperty<Font>(this, "Font", nf2, new List<Type>() { txtNoteData.GetType() });

            lblVersion.Font = new Font(nf.Name, newFontSize * 0.8f);
            lblNoteData.MaximumSize = new Size(txtNoteData.Width, 0);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmMain_FormClosingDetails();
            if (!closeWithoutSaving && !string.IsNullOrWhiteSpace(mainHashKey))
                File_SaveToFile();
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain_FormClosingDetails();
        }
        private void FrmMain_FormClosingDetails()
        {
            formClosingFlag = true;
        }
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            if (FrmMain_Shown_firstRun)
            {
                FrmMain_Shown_firstRun = false;

                var screen = Screen.FromControl(this);

                float divider = 2.5f;
                int setWidth = Convert.ToInt32(screen.Bounds.Width / divider);
                int setHeight = Convert.ToInt32(screen.Bounds.Height / divider);

                if (setWidth < this.MinimumSize.Width)
                    setWidth = this.MinimumSize.Width;
                if (setHeight < this.MinimumSize.Height)
                    setHeight = this.MinimumSize.Height;

                int x = Convert.ToInt32((screen.Bounds.Width / 2) - setWidth / 2);
                int y = Convert.ToInt32((screen.Bounds.Height / 2) - setHeight / 2);

                Point newLocation = new Point(x, y);

                this.Location = newLocation;
                this.Size = new Size(setWidth, setHeight);

                System.Threading.Thread newThread = new System.Threading.Thread(NoteData_Clipboard_ThreadClipboardMonitor);
                newThread.IsBackground = true;
                newThread.Start();

                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 0)
                    foreach (string arg in args)
                        if (arg.ToLower().EndsWith(".gendb", true, System.Globalization.CultureInfo.InvariantCulture))
                        {
                            File_Open(args[0]);
                            break;
                        }
            }
            FrmMain_setEnableAndVisible();
            allowResizeUpdate = true;
            FrmMain_UpdateAfterResize();
        }
        private void FrmMain_setEnableAndVisible()
        {
            var openFileMsg = "Hint: click File then New or File then Open.";
            if (string.IsNullOrWhiteSpace(mainHashKey))
            {
                menuItemNew.Enabled = true;
                menuItemOpen.Enabled = true;
                menuItemSave.Enabled = false;
                menuItemSaveAs.Enabled = false;
                menuItemSaveExit.Enabled = false;
                menuItemExit.Enabled = true;
                dataGridViewNotes.Visible = false;
                lblStatus.Text = openFileMsg;
                menuItemChangeMainPassword.Enabled = false;
                menuItemOptionsToolStrip.Enabled = false;
            }
            else
            {
                const bool t = true;
                menuItemSave.Enabled = t;
                menuItemSaveAs.Enabled = t;
                menuItemSaveExit.Enabled = t;
                if (lblStatus.Text == openFileMsg)
                    lblStatus.Text = "Enter a note, and then enter a note item, and then enter data for that note item.";
                menuItemChangeMainPassword.Enabled = true;
                menuItemOptionsToolStrip.Enabled = true;
            }

            if (dataGridViewNotes.Visible && dataGridViewNotes.Enabled)
            {
                const bool t = true;
                btnNoteAdd.Visible = t;
                btnNoteEdit.Visible = t;
                btnNoteDelete.Visible = t;

                if (dataGridViewNotes.CurrentRow != null)
                {
                    btnNoteEdit.Enabled = t;
                    btnNoteDelete.Enabled = t;
                }
                else
                {
                    btnNoteEdit.Enabled = false;
                    btnNoteDelete.Enabled = false;
                }
            }
            else
            {
                const bool f = false;
                btnNoteAdd.Visible = f;
                btnNoteEdit.Visible = f;
                btnNoteDelete.Visible = f;
                dataGridViewKeys.Visible = f;
            }

            if (dataGridViewKeys.Visible && dataGridViewKeys.Enabled)
            {
                const bool t = true;
                btnKeyAdd.Visible = t;
                btnKeyEdit.Visible = t;
                btnKeyDelete.Visible = t;

                if (dataGridViewKeys.CurrentRow != null)
                {
                    btnKeyDelete.Enabled = t;
                    btnKeyEdit.Enabled = t;
                }
                else
                {
                    const bool f = false;
                    btnKeyDelete.Enabled = f;
                    btnKeyEdit.Enabled = f;
                }
            }
            else
            {
                const bool f = false;
                btnKeyAdd.Visible = f;
                btnKeyEdit.Visible = f;
                btnKeyDelete.Visible = f;
                tblLyoNoteData.Visible = f;
            }
        }
        private void FrmMain_child_VisibleChanged(object sender, EventArgs e)
        {
            FrmMain_setEnableAndVisible();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (this.dataGridViewKeys.ContainsFocus)
                {
                    this.dataGridViewKeys.EndEdit();

                    if (this.dataGridViewKeys.CurrentCell != null && !GalFormFunctions.VerifyDataGridViewRowCell(dataGridViewKeys.CurrentRow.Cells[2]))
                    {
                        if (this.dataGridViewKeys.CurrentRow.IsNewRow && this.dataGridViewKeys.Rows.Count >= 2)
                            this.dataGridViewKeys.CurrentCell = this.dataGridViewKeys.Rows[this.dataGridViewKeys.Rows.Count - 2].Cells[2];

                        NoteData_update();
                        this.txtNoteData.Focus();
                    }
                    else
                    {
                        this.dataGridViewKeys.CurrentCell = null;
                        this.dataGridViewKeys.CurrentCell = this.dataGridViewKeys.Rows[this.dataGridViewKeys.NewRowIndex].Cells[2];
                    }
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void Menu_CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteData_SaveNoteData(skipSaveToFile: true);
            if (!this.File_SaveToFile())
            {
                MessageBox.Show(this, "Save Cancelled, cancelling program close.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                this.closeWithoutSaving = true;
                this.Close();
            }
        }
        private void Menu_SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteData_SaveNoteData(skipSaveToFile: true);
            File_SaveAsToFile();
        }
        private void Menu_OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File_Open();
        }
        private void Menu_CloseWithoutSavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeWithoutSaving = true;
            this.Close();
        }
        private void Menu_SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteData_SaveNoteData(skipSaveToFile: true);
            File_SaveToFile();
        }
        private void Menu_NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File_NewDatabase();
        }
        private void Menu_changeMainPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //one of the more difficult tasks
            lblStatus.Text = "Hint: changing the main password takes time, the encrypted data needs decrypted and then re-encrypted with the new information.";
            if (GalInputDialog.Show(out string currentPass, this, "Confirm current password", "Current password", isPasswordInput: true) == DialogResult.OK)
            {
                currentPass = currentPass.Trim();
                if (mainHashKey != GalLib.BytesToStrStorage(GalLib.GetHash512(GalLib.GetNonRandomSalt(currentPass.Length), currentPass)))
                    MessageBox.Show(this, "Incorrect current password", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    String oldMainHashKey = mainHashKey;
                    DsGalNoteManager oldDs = (DsGalNoteManager)dsGalData.Copy();

                    if (File_NewDatabase())
                    {
                        closeWithoutSaving = true;
                        this.Controls.Remove(tblLyoMain);

                        var lblConversionStatus = new Label()
                        {
                            Enabled = false,
                            BorderStyle = BorderStyle.Fixed3D
                        };

                        this.Controls.Add(lblConversionStatus);
                        lblConversionStatus.BringToFront();
                        lblConversionStatus.Dock = DockStyle.Fill;
                        menuStripMain.Enabled = false;

                        DsGalNoteManager dsTemp = null;

                        var t = new System.Threading.Thread(() => //not sure if this is awful or genius?
                        {
                            dsTemp = (DsGalNoteManager)dsGalData.Copy();

                            DataTable dt = dsTemp.dtSettings;
                            foreach (DataRow row in oldDs.dtSettings.Rows)
                            {
                                if ((string)row["name"] != "verify")
                                {
                                    var newRow = dt.NewRow();
                                    newRow["name"] = row["name"];
                                    newRow["value"] = row["value"];
                                    dt.Rows.Add(newRow);
                                }
                            }
                            dt.AcceptChanges();
                            BeginInvoke(new Action(() => lblConversionStatus.Text = "copied settings" + Environment.NewLine + lblConversionStatus.Text));

                            var dtNotes = dsTemp.dtNotes;
                            foreach (DataRow oldNoteRow in oldDs.dtNotes)
                            {
                                var newNoteRow = dtNotes.NewRow();
                                newNoteRow["name"] = oldNoteRow["name"];
                                dtNotes.Rows.Add(newNoteRow);
                                dtNotes.AcceptChanges();
                                var newNoteRowId = newNoteRow["id"];
                                BeginInvoke(new Action(() => lblConversionStatus.Text = "copied note:" + (string)newNoteRow["name"] + Environment.NewLine + lblConversionStatus.Text));

                                var oldMatchingKeyRows = oldDs.dtNoteKeys.Select("notes_id=" + ((int)oldNoteRow["id"]).ToString(inv));
                                foreach (DataRow oldKeyRow in oldMatchingKeyRows)
                                {
                                    string salt = (string)oldKeyRow["salt"];
                                    string encryptedKey = (string)oldKeyRow["key"];
                                    String plainKey = GalLib.DecryptString(salt, oldMainHashKey, encryptedKey, dtNotesKeys_iterations);
                                    if (formClosingFlag) return;

                                    var newNoteKeyRow = dsTemp.dtNoteKeys.NewRow();
                                    newNoteKeyRow["notes_id"] = newNoteRowId;
                                    newNoteKeyRow["salt"] = salt;
                                    newNoteKeyRow["key"] = GalLib.EncryptString(salt, mainHashKey, plainKey, dtNotesKeys_iterations);
                                    if (formClosingFlag) return;
                                    dsTemp.dtNoteKeys.Rows.Add(newNoteKeyRow);
                                    dsTemp.dtNoteKeys.AcceptChanges();
                                    BeginInvoke(new Action(() => lblConversionStatus.Text = "copied " + (string)newNoteRow["name"] + " item:" + plainKey + Environment.NewLine + lblConversionStatus.Text));

                                    var newNoteKeyRowId = (int)newNoteKeyRow["id"];
                                    var oldKeyRowId = ((int)oldKeyRow["id"]).ToString(inv);

                                    var oldMatchingKeyDataRows = oldDs.dtNoteKeyValues.Select("noteKey_id=" + oldKeyRowId);
                                    foreach (DataRow oldKeyDataRow in oldMatchingKeyDataRows)
                                    {
                                        var newKeyDataRow = dsTemp.dtNoteKeyValues.NewRow();
                                        newKeyDataRow["timestamp"] = oldKeyDataRow["timestamp"];
                                        newKeyDataRow["noteKey_id"] = newNoteKeyRowId;
                                        var encryptedData = (string)oldKeyDataRow["value"];
                                        var plainText = GalLib.DecryptString(salt, oldMainHashKey, encryptedData);
                                        if (formClosingFlag) return;
                                        newKeyDataRow["value"] = GalLib.EncryptString(salt, mainHashKey, plainText);
                                        if (formClosingFlag) return;
                                        dsTemp.dtNoteKeyValues.Rows.Add(newKeyDataRow);
                                        dsTemp.dtNoteKeyValues.AcceptChanges();
                                        BeginInvoke(new Action(() => lblConversionStatus.Text = "copied " + (string)newNoteRow["name"] + " " + plainKey + " " + ((DateTime)newKeyDataRow["timestamp"]).ToString() + " data" + Environment.NewLine + lblConversionStatus.Text));
                                    }
                                }
                            }

                            Invoke(new Action(() =>
                           {
                               dsGalData = dsTemp;
                               this.Controls.Remove(lblConversionStatus);
                               this.Controls.Add(tblLyoMain);
                               menuStripMain.Enabled = true;
                               tblLyoMain.BringToFront();
                               File_BindDataGridViewNotes();
                               lblStatus.Text = "Main password change complete!";
                           }
                            ));
                            closeWithoutSaving = false;

                        });
                        t.IsBackground = true;
                        t.Start();
                    }
                }
            }
        }


        private void File_Open(string openFile = "")
        {
            DialogResult dialogResult;
            if (string.IsNullOrWhiteSpace(openFile))
            {
                var ofDialog = new OpenFileDialog()
                {
                    Filter = notesFileExtensionFilter,
                    AutoUpgradeEnabled = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = notesFileExtension,
                    Multiselect = false,
                    Title = "Open Galantha's encrypted notes database from file"
                };
                ofDialog.InitialDirectory = folder;
                if (string.IsNullOrWhiteSpace(fileName))
                    ofDialog.FileName = notesFileDefault;
                else
                    ofDialog.FileName = this.fileName;

                dialogResult = ofDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    folder = System.IO.Path.GetDirectoryName(ofDialog.FileName) + "\\";
                    fileName = System.IO.Path.GetFileName(ofDialog.FileName);
                }

                ofDialog.Dispose();
            }
            else
            {
                folder = System.IO.Path.GetDirectoryName(openFile) + "\\";
                fileName = System.IO.Path.GetFileName(openFile);
                dialogResult = DialogResult.OK;
            }

            if (dialogResult == DialogResult.OK)
            {
                if (this.File_GetMainHash(confirm: false))
                {
                    bool failed = false;
                    try
                    {
                        lock (dsGalData)
                            dsGalData = GalLib.DecryptDataSetFromFile<DsGalNoteManager>(folder + fileName, mainHashKey);
                    }
                    catch (System.Security.Cryptography.CryptographicException)
                    {
                        failed = true;
                        MessageBox.Show(this, "Password for " + fileName + " incorrect", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (!failed)
                    {
                        var dataRows = dsGalData.dtSettings.Select("name='verify'");
                        if (!(dataRows.Length > 0))
                        {
                            if (MessageBox.Show(this, fileName + " missing verification information.  Attempt to open anyway?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                File_BindDataGridViewNotes();
                                closeWithoutSaving = true;
                            }
                            else
                            {
                                fileName = "";
                                dsGalData = null;
                            }


                        }
                        else
                        {
                            if ("test" != GalLib.DecryptString("verify", mainHashKey, (string)dataRows[0]["value"]))
                                MessageBox.Show(this, fileName + " failed mainHaskKey check.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            else
                            {
                                File_BindDataGridViewNotes();
                            }
                        }
                    }
                }
            }
        }
        private bool File_SaveAsToFile()
        {
            if (string.IsNullOrWhiteSpace(this.mainHashKey))
                if (!File_NewDatabase())
                    return false;

            var sfDialog = new SaveFileDialog()
            {
                Filter = notesFileExtensionFilter,
                AddExtension = true,
                AutoUpgradeEnabled = true,
                Title = "Save Galantha's encrypted notes database to file"
            };
            if (string.IsNullOrWhiteSpace(fileName))
                sfDialog.FileName = notesFileDefault;
            else
                sfDialog.FileName = this.fileName;
            sfDialog.InitialDirectory = folder;


            if (sfDialog.ShowDialog() != DialogResult.OK)
            {
                sfDialog.Dispose();
                return false;
            }
            else
            {
                folder = System.IO.Path.GetDirectoryName(sfDialog.FileName) + "\\"; //the little gottyas
                fileName = System.IO.Path.GetFileName(sfDialog.FileName);
                sfDialog.Dispose();
                NoteData_SaveNoteData(skipSaveToFile: true);
                bool success = File_SaveDatabaseToFile();
                return success;
            }
        }
        private bool File_SaveDatabaseToFile()
        {
            try
            {
                lock (dsGalData)
                    GalLib.EncryptDataSetToFile<DsGalNoteManager>(dsGalData, folder + fileName, mainHashKey, true);

                lblStatus.Text = "Saved at " + DateTime.Now.ToLongTimeString();
                return true;
            }
            catch (Exception ex)
            {
                //not good, not good at all
                var msg = "!! Unable to save file " + fileName + " !!" + Environment.NewLine + Environment.NewLine;
                msg += "Error:" + Environment.NewLine + ex.Message;
                MessageBox.Show(this, msg, "Failed to save!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        private bool File_SaveToFile()
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(mainHashKey))
                return File_SaveAsToFile();
            else
            {
                return File_SaveDatabaseToFile();
            }
        }
        private bool File_NewDatabase()
        {
            if (!File_GetMainHash(true))
                return false;
            else
            {
                fileName = "";
                lock (dsGalData)
                    dsGalData = new DsGalNoteManager();
                var newRow = dsGalData.dtSettings.NewRow();
                newRow["name"] = "verify";
                newRow["value"] = GalLib.EncryptString("verify", mainHashKey, "test");
                lock (dsGalData)
                    dsGalData.dtSettings.Rows.Add(newRow);
                File_BindDataGridViewNotes();
                return true;
            }
        }
        private bool File_GetMainHash(bool newPass = false, bool confirm = true)
        {
            bool result = false;
            {
                string passMessage;
                string confirmMessage;
                if (!newPass)
                {
                    passMessage = "Enter password?";
                    confirmMessage = "Please confirm password?";
                }
                else
                {
                    passMessage = "Enter new password?";
                    confirmMessage = "Please confirm new password?";
                }
                if (GalInputDialog.Show(out string password, this, passMessage, "Password", isPasswordInput: true) == DialogResult.OK)
                {
                    string confirmPassword = "";
                    if (!confirm || GalInputDialog.Show(out confirmPassword, this, confirmMessage, "Confirm password", isPasswordInput: true) == DialogResult.OK)
                    {
                        password = password.Trim();
                        confirmPassword = confirmPassword.Trim();
                        if (confirm && password != confirmPassword)
                            MessageBox.Show(this, "Error: password and confirm password do not match!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            dsGalData = new DsGalNoteManager();
                            mainHashKey = GalLib.BytesToStrStorage(GalLib.GetHash512(GalLib.GetNonRandomSalt(password.Length), password));
                            result = true;
                        }
                        confirmPassword = ""; //this is done to reduce the time sensitive data is in memory
                        password = "";
                    }
                }
            } //forces the above out of scope and collectable
            GC.Collect(); //garbage collection
            return result;
        }
        private void File_BindDataGridViewNotes()
        {
            dataGridViewKeys_ignoreUpdate = true;
            allowResizeUpdate = false;
            lock (dsGalData.dtNotes)
            {
                if (dtNotes_drceh == null)
                    dtNotes_drceh = new DataRowChangeEventHandler(Notes_dtNotes_rowChanged);
                dsGalData.dtNotes.RowChanged -= dtNotes_drceh; // It is a klug and I know it!  It is a klug and I know it!  But it works so well, and I do not know a better way, so I guess it is going to be a klug!
                dsGalData.dtNotes.RowChanged += dtNotes_drceh;

                dataGridViewNotes.DataSource = dsGalData.dtNotes;
            }

            for (int i = 0; i < dataGridViewNotes.Rows.Count; i += 1)
                dataGridViewNotes.Rows[i].HeaderCell.Value = i.ToString(inv);

            dataGridViewNotes.Columns[0].Visible = false;
            //dataGridViewNotes.RowHeadersWidth = 5; //does not work?
            dataGridViewNotes.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewNotes.Columns[1].HeaderText = "Note name:";
            dataGridViewNotes.Visible = dataGridViewNotes.Enabled = true;
            if (dataGridViewNotes.Rows.Count > 1)
                dataGridViewNotes.Rows[0].Selected = true;
            dataGridViewKeys_ignoreUpdate = false;
            DataGridViewKeys_update();
            allowResizeUpdate = true;
            FrmMain_UpdateAfterResize();
        }


        private void DataGridViewNotes_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewKeys_update();
        }
        private void DataGridViewNotes_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewKeys_update();
        }
        private void DataGridViewNotes_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridViewNotes_LastFocusedRowIndex = e.RowIndex;
            DataGridViewKeys_update();
        }
        private void DataGridViewNotes_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewKeys_update();
            FrmMain_setEnableAndVisible();
        }
        private void DataGridViewNotes_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string item = (string)e.Row.Cells[1].Value;
            DialogResult result = MessageBox.Show(this, "Are you sure you want to delete note: " + item, "Delete Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }
        private void DataGridViewNotes_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridViewNotes.SelectionMode = DataGridViewSelectionMode.CellSelect;//shift key bug solution
        }
        private void DataGridViewNotes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewNotes.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;//shift key bug solution
        }
        private void Notes_btnNoteAdd_Click(object sender, EventArgs e)
        {
            var dg = dataGridViewNotes;
            dg.Focus();
            dg.CurrentCell = dg.Rows[dg.Rows.Count - 1].Cells[1];
            dg.BeginEdit(false);
            lblStatus.Text = "Hint: Rows can also be added by typing in the blank one at the bottom of the grid.";
        }
        private void Notes_btnNoteEdit_Click(object sender, EventArgs e)
        {
            var dg = dataGridViewNotes;
            dg.Focus();
            if (dg.CurrentRow != null)
                dg.CurrentCell = dg.CurrentRow.Cells[1];
            dg.BeginEdit(true);
            lblStatus.Text = "Hint: Rows can also be edited by selecting and then just typing.";
        }
        private void Notes_btnNoteDelete_Click(object sender, EventArgs e)
        {
            var dg = dataGridViewNotes;
            dg.Focus();
            if (dg.CurrentRow != null)
            {
                if (MessageBox.Show(this, "Delete " + (string)dg.CurrentRow.Cells[1].Value + "?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    dg.Rows.Remove(dg.CurrentRow);
                lblStatus.Text = "Hint: Rows can also be deleted by clicking the selector on the left of the row, and then pressing the delete key.";
            }
        }
        private void Notes_dtNotes_rowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row != null && e.Row[0] != null && !Convert.IsDBNull(e.Row[0]))
            {
                dataGridViewNotes_lastIdEdited = (int)e.Row[0];
                DataGridViewKeys_update();
            }

        }
        private void dataGridViewNotes_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridViewNotes.RowHeadersWidth = 5;
        }


        private void DataGridViewKeys_update()
        {
            if (dataGridViewKeys_ignoreUpdate)
                return;

            if (!InvokeRequired)
            {
                var t = new System.Threading.Thread(DataGridViewKeys_update);
                t.IsBackground = true;
                t.Start();
                return;
            }

            //c# || = orelse
            int dtNotesKeysId = -1;
            string currentRow2 = "";
            bool updateViewKeys = false;
            Invoke(new Action(() =>
            {
                var currentRow = this.dataGridViewNotes.CurrentRow; // current row
                if (currentRow == null && dataGridViewNotes.SelectedRows.Count > 0)
                    currentRow = dataGridViewNotes.SelectedRows[0];

                //if current row is empty, try to find a good alternate
                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                {
                    foreach (DataGridViewRow dgvRow in dataGridViewNotes.Rows)
                        if (GalFormFunctions.CheckForValidDataGridViewRow(dgvRow) && (int)dgvRow.Cells[0].Value == dataGridViewNotes_lastIdEdited)
                        {
                            currentRow = dgvRow;
                            break;
                        }
                }

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    if ((dataGridViewNotes_LastFocusedRowIndex >= 0) && (dataGridViewNotes_LastFocusedRowIndex < dataGridViewNotes.Rows.Count))
                        currentRow = dataGridViewNotes.Rows[dataGridViewNotes_LastFocusedRowIndex];

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    if ((dataGridViewNotes.Rows.Count > 0))
                        currentRow = dataGridViewNotes.Rows[dataGridViewNotes.Rows.Count - 1];

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    if ((dataGridViewNotes.Rows.Count > 1))
                        currentRow = dataGridViewNotes.Rows[dataGridViewNotes.Rows.Count - 2];

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow) || dataGridViewNotes.Enabled == false)
                    BeginInvoke(new Action(() => dataGridViewKeys.Visible = dataGridViewKeys.Enabled = false));
                else
                {
                    if (dataGridViewNotes_oldCurrentRow is null || dataGridViewNotes_oldCurrentRow != currentRow || dataGridViewNotes_oldCurrentRow.Cells[1].Value != currentRow.Cells[1].Value || dataGridViewKeys.DataSource == null)
                    {
                        dataGridViewNotes_oldCurrentRow = currentRow;
                        dtNotesKeysId = (int)currentRow.Cells[0].Value;
                        currentRow2 = (String)currentRow.Cells[1].Value;
                        updateViewKeys = true;
                    }
                }
            }));

            if (updateViewKeys)
            {
                NoteData_ignoreUpdates = true;
                Invoke(new Action(() => dataGridViewKeys.DataSource = null));
                DataRow[] drs = null;
                DataTable dtKeysForNote = null;

                lock (dsGalData.dtNoteKeys)
                {
                    drs = dsGalData.dtNoteKeys.Select("notes_id = " + dtNotesKeysId.ToString(inv));  //why was I not able to use Cells["id"]?
                    dtKeysForNote = dsGalData.dtNoteKeys.Clone();
                    if (drs.Length > 0) dtKeysForNote = drs.CopyToDataTable<DataRow>(); //this also overwrite copies schema
                }
                lock (dtKeysForNote)
                {
                    dtKeysForNote.Columns["id"].AutoIncrement = false;
                    dtKeysForNote.Columns["id"].DefaultValue = -1;
                    dtKeysForNote.PrimaryKey = null;
                    dtKeysForNote.Columns["id"].Unique = false;
                    dtKeysForNote.Columns["id"].ReadOnly = false;
                    dtKeysForNote.Columns["salt"].Unique = false;
                    dtKeysForNote.Columns["salt"].ReadOnly = false;
                    dtKeysForNote.Columns["notes_id"].DefaultValue = dtNotesKeysId;

                    var assignments = new List<Task>();
                    foreach (DataRow dr in dtKeysForNote.Rows) //this is why we are non-primary thread
                        assignments.Add(Task.Run(() => dr["key"] = GalLib.DecryptString((string)dr["salt"], mainHashKey, (string)dr["key"], dtNotesKeys_iterations)));  //expensive, but fully multithreaded
                    Task runAssignments = Task.WhenAll(assignments);
                    runAssignments.Wait();
                    dtKeysForNote.AcceptChanges();
                }
                BeginInvoke(new Action(() =>
                {
                    lock (dataGridViewKeys) lock (dtKeysForNote)
                        {
                            Boolean exitInvoke = false;
                            try
                            {
                                NoteData_ignoreUpdates = true;
                                dataGridViewKeys.DataSource = dtKeysForNote;
                            }
                            catch (ArgumentException)
                            {
                                exitInvoke = true;
                            }
                            if (!exitInvoke)
                            {
                                dataGridViewKeys.Columns[0].Visible = false; //would prefer using column names, but that throws an error
                                dataGridViewKeys.Columns[1].Visible = false;
                                dataGridViewKeys.Columns[3].Visible = false;
                                dataGridViewKeys.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                dataGridViewKeys.Columns[2].HeaderText = "Items for: " + currentRow2;
                            }

                            dtKeysForNote.RowChanged += new DataRowChangeEventHandler(NoteKey_DtKeysForNote_rowChanged);
                            dtKeysForNote.RowDeleting += new DataRowChangeEventHandler(NoteKey_DtKeysForNote_rowDeleting);

                        }

                    dataGridViewKeys.Enabled = dataGridViewKeys.Visible = true;
                    NoteData_ignoreUpdates = false;
                    NoteData_update();
                }));
            }
        }
        private void DataGridViewKeys_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FrmMain_UpdateAfterResize();
        }
        private void DataGridViewKeys_SelectionChanged(object sender, EventArgs e)
        {
            NoteData_update();
            FrmMain_setEnableAndVisible();
        }
        private void DataGridViewKeys_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            NoteData_update();
        }
        private void DataGridViewKeys_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            NoteData_update();
        }
        private void DataGridViewKeys_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewKeys_lastRowFocusedIndex = e.RowIndex;
            NoteData_update();
        }
        private void DataGridViewKeys_EnabledChanged(object sender, EventArgs e)
        {
            if (dataGridViewKeys.Enabled == false)
            {
                dataGridViewKeys.DataSource = null;
            }
            FrmMain_setEnableAndVisible();
        }
        private void DataGridViewKeys_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string item = (string)e.Row.Cells[2].Value;
            DialogResult result = MessageBox.Show(this, "Are you sure you want to delete note item: " + item, "Delete Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }
        private void NoteKey_DtKeysForNote_rowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (DtViewNotesDataChangedHandler_ignoreEvents != true) //prevent event recursion
            {
                DtViewNotesDataChangedHandler_ignoreEvents = true;

                DataRow changedRow = e.Row;
                DataTable displayedTable = e.Row.Table;
                DataTable sourceTable = dsGalData.dtNoteKeys;
                if ((int)e.Row["id"] == -1)
                {
                    //create new row in source, then updated id
                    DataRow newSourceRow = sourceTable.NewRow();
                    //index 0 is autoincrement id
                    newSourceRow["notes_id"] = changedRow["notes_id"]; //is notes_id int
                    var salt = GalLib.GetRandomSalt(10);
                    newSourceRow["salt"] = salt;
                    newSourceRow["key"] = GalLib.EncryptString(salt, mainHashKey, (string)changedRow["key"], dtNotesKeys_iterations); //is key string
                    lock (sourceTable)
                    {
                        sourceTable.Rows.Add(newSourceRow);
                        sourceTable.AcceptChanges();
                    }
                    e.Row["id"] = newSourceRow["id"];
                    e.Row["salt"] = salt;
                    displayedTable.AcceptChanges();
                }
                else
                {
                    //modify row in source
                    lock (sourceTable)
                    {
                        DataRow editSourceRow = sourceTable.Select("id = " + changedRow["id"].ToString())[0]; //unique id, only 1 will ever be returned
                                                                                                              //editSourceRow[1] = changedRow[1]; //this will never change
                        if (editSourceRow["key"] != changedRow["key"])
                        {
                            editSourceRow["key"] = GalLib.EncryptString((string)editSourceRow["salt"], mainHashKey, (string)changedRow["key"], dtNotesKeys_iterations);
                            sourceTable.AcceptChanges();
                        }
                    }
                }

                DtViewNotesDataChangedHandler_ignoreEvents = false;
            }

            if (e.Row != null && e.Row[0] != null && !Convert.IsDBNull(e.Row[0]))
                dataGridViewKeys_lastIdEdited = (int)e.Row[0];
        }
        private void NoteKey_DtKeysForNote_rowDeleting(object sender, DataRowChangeEventArgs e)
        {
            DataTable sourceTable = dsGalData.dtNoteKeys;
            var id = (int)e.Row["id"];
            if (id >= 0)
            {
                lock (sourceTable)
                {
                    DataRow[] deleteRows = sourceTable.Select("id = " + id.ToString(inv));
                    if (deleteRows.Length > 0)
                    {
                        var deleteRow = deleteRows[0];
                        sourceTable.Rows.Remove(deleteRow);
                        sourceTable.AcceptChanges();
                    }
                }

            }
        }
        private void NoteKey_btnKeyAdd_Click(object sender, EventArgs e)
        {
            var dg = dataGridViewKeys;
            dg.Focus();
            dg.CurrentCell = dg.Rows[dg.Rows.Count - 1].Cells[2];
            dg.BeginEdit(false);
            lblStatus.Text = "Hint: Rows can also be added by typing in the blank one at the bottom of the grid.";
        }
        private void NoteKey_btnKeyEdit_Click(object sender, EventArgs e)
        {
            var dg = dataGridViewKeys;
            dg.Focus();
            if (dg.CurrentRow != null)
                dg.CurrentCell = dg.CurrentRow.Cells[2];
            dg.BeginEdit(true);
            lblStatus.Text = "Hint: Rows can also be edited by selecting and then just typing.";
        }
        private void NoteKey_btnKeyDelete_Click(object sender, EventArgs e)
        {
            var dg = dataGridViewKeys;
            dg.Focus();
            if (dg.CurrentRow != null)
            {
                if (MessageBox.Show(this, "Delete " + (string)dg.CurrentRow.Cells[2].Value + "?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    dg.Rows.Remove(dg.CurrentRow);
                lblStatus.Text = "Hint: Rows can also be deleted by clicking the selector on the left of the row, and then pressing the delete key.";
            }
        }
        private void DataGridViewKeys_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridViewKeys.SelectionMode = DataGridViewSelectionMode.CellSelect; //shift key bug solution
        }
        private void DataGridViewKeys_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewKeys.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;//shift key bug solution
        }



        private void NoteData_BtnMakePassword_Click(object sender, EventArgs e)
        {
            if (FrmSuggestPassword.ShowDialog(out string password, this, dsGalData.dtSettings) == DialogResult.OK)
            {
                NoteData_ignoreUpdates = true;
                if (String.IsNullOrWhiteSpace(txtNoteData.Text))
                    txtNoteData.Text = password;
                else
                    txtNoteData.Text += Environment.NewLine + password;
                NoteData_SaveNoteData(skipSaveToFile: true);
                NoteData_ignoreUpdates = false;
            }
        }
        private void NoteData_update()
        {
            if (NoteData_ignoreUpdates)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(NoteData_update));
                return;
            }

            NoteData_cmbVersionSelect_ignoreEvents = true;
            string key = "";
            string note = "";

            if (dataGridViewKeys.Enabled == false)
                tblLyoNoteData.Visible = tblLyoNoteData.Enabled = false;
            else
            {
                var currentRow = this.dataGridViewKeys.CurrentRow;

                //if current row is empty, try to find a good alternate
                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                {
                    foreach (DataGridViewRow dgvRow in dataGridViewKeys.Rows)
                        if (GalFormFunctions.CheckForValidDataGridViewRow(dgvRow) && (int)dgvRow.Cells[0].Value == dataGridViewKeys_lastIdEdited)
                        {
                            currentRow = dgvRow;
                            break;
                        }
                }

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    if ((dataGridViewKeys_lastRowFocusedIndex >= 0) && (dataGridViewKeys_lastRowFocusedIndex < dataGridViewKeys.Rows.Count))
                        currentRow = dataGridViewKeys.Rows[dataGridViewKeys_lastRowFocusedIndex];

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    if (dataGridViewKeys.Rows.Count > 0)
                        currentRow = dataGridViewKeys.Rows[dataGridViewKeys.Rows.Count - 1];

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    if (dataGridViewKeys.Rows.Count > 1)
                        currentRow = dataGridViewKeys.Rows[dataGridViewKeys.Rows.Count - 2];

                if (!GalFormFunctions.CheckForValidDataGridViewRow(currentRow))
                    tblLyoNoteData.Visible = tblLyoNoteData.Enabled = false;
                else
                {
                    if (dataGridViewKeys_oldCurrentRow == null || dataGridViewKeys_oldCurrentRow != currentRow || dataGridViewKeys_oldCurrentRow.Cells[0].Value != currentRow.Cells[0].Value || cmbVersionSelect.DataSource == null)
                    {
                        dataGridViewKeys_oldCurrentRow = currentRow;
                        dataGridViewKeys_salt = (string)currentRow.Cells[3].Value;
                        dataGridViewKeys_dtNoteKeys_id = (int)currentRow.Cells[0].Value;

                        key = currentRow.Cells[2].Value.ToString();
                        note = (string)dataGridViewNotes_oldCurrentRow.Cells[1].Value;

                        if (GalFormFunctions.CheckForValidDataGridViewRow(dataGridViewNotes.CurrentRow))
                            note = (string)dataGridViewNotes.CurrentRow.Cells[1].Value;

                        string noteLabelString = "Data pad";
                        if (!string.IsNullOrWhiteSpace(key))
                            noteLabelString += " for item: " + key;
                        if (!string.IsNullOrWhiteSpace(note))
                            noteLabelString += ", for note: " + note;
                        BeginInvoke(new Action(() => lblNoteData.Text = noteLabelString));

                        DataRow[] dataRows = null;
                        lock (dsGalData.dtNoteKeyValues)
                        {
                            dataRows = dsGalData.dtNoteKeyValues.Select("noteKey_id = " + dataGridViewKeys_dtNoteKeys_id.ToString(inv), "timestamp DESC");
                        }

                        NoteData_cmbVersionSelect_ignoreEvents = true;
                        cmbVersionSelect.Items.Clear();
                        cmbVersionSelect.DisplayMember = "value";
                        cmbVersionSelect.ValueMember = "key";
                        cmbVersionSelect.Items.Add(new KeyValuePair<int, String>(-1, "New"));
                        foreach (var dr in dataRows)
                            cmbVersionSelect.Items.Add(new KeyValuePair<int, String>((int)dr["id"], ((DateTime)dr["timestamp"]).ToString(inv)));
                        if (cmbVersionSelect.Items.Count == 1)
                            cmbVersionSelect.SelectedIndex = 0;
                        else
                            cmbVersionSelect.SelectedIndex = 1;

                        NoteData_LoadSelectedVersionIntoTxtNotesData();
                        tblLyoNoteData.Visible = tblLyoNoteData.Enabled = true;
                    }
                }

            }
        }
        private void NoteData_LoadSelectedVersionIntoTxtNotesData()
        {
            if (cmbVersionSelect.Items.Count <= 1 || cmbVersionSelect.SelectedIndex == 0 || cmbVersionSelect.SelectedItem == null)
            {
                txtNoteData.Text = "";
                NoteData_cmbVersionSelect_ignoreEvents = false;
            }
            else
            {
                NoteData_cmbVersionSelect_ignoreEvents = true;
                KeyValuePair<int, String> kv = (KeyValuePair<int, String>)cmbVersionSelect.SelectedItem;
                int id = kv.Key;
                
                if (id < 0)
                    throw new Exception("This error should never happen! -> cmbVersionSelect key value less than 0");

                DataRow[] dataRows = null;
                lock (dsGalData.dtNoteKeyValues)
                {
                    dataRows = dsGalData.dtNoteKeyValues.Select("id =" + id.ToString(inv));
                }

                if (dataRows.Length < 1)
                    //throw new Exception("These error should never happen either, non-existant version displayed in cmbVersionSelect.");
                    return; //it is possible to trigger this error with a race condition, soundless exit is fine //since I converted this to be singled threaded again, I wonder if this is still an issue?
                if (dataRows.Length > 1)
                    throw new Exception("This error should never happen, 2x \"unique\" ids found for cmbVersionSelect");

                var encryptedText = (string)dataRows[0]["value"];
                string plainText;
                try
                {
                    plainText = GalLib.DecryptString(dataGridViewKeys_salt, mainHashKey, encryptedText); //orginally this function ( NoteData_LoadSelectedVersionIntoTxtNotesData ) was called excessively, and I made it a background thread.  Eventually I fixed the excessive call, and converted back to the process thread
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    plainText = "Error: " + ex.GetType().Name + Environment.NewLine + ex.Message;
                }

                NoteData_cmbVersionSelect_ignoreEvents = true; //this used to be needed when this was multi-threaded
                txtNoteData.Text = plainText;
                NoteData_cmbVersionSelect_ignoreEvents = false;

            }
        }
        private void NoteData_TblLyoNoteData_EnabledChanged(object sender, EventArgs e)
        {
            if (tblLyoNoteData.Enabled == false)
            {
                txtNoteData.Text = "";
                lblNoteData.Text = "";
                cmbVersionSelect.DataSource = null;
                cmbVersionSelect.Text = null;
            }

        }
        private void NoteData_btnSaveNoteData_Click(object sender, EventArgs e)
        {
            NoteData_SaveNoteData(true);
        }
        private void NoteData_SaveNoteData(bool alwaysSave = false, bool skipSaveToFile = false)
        {
            if (alwaysSave || (!string.IsNullOrWhiteSpace(txtNoteData.Text) && tblLyoNoteData.Enabled))
            {
                if (dataGridViewKeys_dtNoteKeys_id < 0)
                {
                    MessageBox.Show(this, "Note data failed to save, manually save!", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool save = true;
                if (!alwaysSave && cmbVersionSelect.Items.Count > 1 && cmbVersionSelect.SelectedItem != null && cmbVersionSelect.SelectedIndex > 0)
                {
                    save = false;
                }

                if (save)
                {
                    var dr = dsGalData.dtNoteKeyValues.NewRow();
                    dr["noteKey_id"] = dataGridViewKeys_dtNoteKeys_id;
                    dr["value"] = GalLib.EncryptString(dataGridViewKeys_salt, mainHashKey, txtNoteData.Text);
                    dr["timestamp"] = DateTime.Now;
                    lock (dsGalData.dtNoteKeyValues)
                        dsGalData.dtNoteKeyValues.Rows.Add(dr);
                    NoteData_update();
                    if (!skipSaveToFile)
                        File_SaveToFile(); //can call back to this function sometimes, hence NoteData_SaveNoteData_saving 
                }
            }
        }
        private void NoteData_btnClearNoteData_Click(object sender, EventArgs e)
        {
            var t = new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(100);  //if it is a hack and it works, it is not awful, right?
                BeginInvoke(new Action(() =>
                {

                    NoteData_cmbVersionSelect_ignoreEvents = true;
                    NoteData_ignoreUpdates = true;
                    if (cmbVersionSelect.Items.Count > 0)
                        cmbVersionSelect.SelectedIndex = 0;
                    txtNoteData.Text = "";
                    NoteData_ignoreUpdates = false;
                    NoteData_cmbVersionSelect_ignoreEvents = false;
                }));
            });
            t.IsBackground = true;
            t.Start(); //why you ask, why?   we are basically letting the events from leaving the txtbox go by before we clear it
        }
        private void NoteData_btnDeleteVersion_Click(object sender, EventArgs e)
        {
            if (cmbVersionSelect.Items.Count > 0)
            {
                if (cmbVersionSelect.SelectedIndex == 0 || cmbVersionSelect.SelectedItem is null)
                {
                    NoteData_ignoreUpdates = true;
                    txtNoteData.Text = "";
                    NoteData_ignoreUpdates = false;
                }
                else
                {
                    KeyValuePair<int, String> kv = (KeyValuePair<int, String>)cmbVersionSelect.SelectedItem;
                    int id = kv.Key;
                    lock (dsGalData.dtNoteKeyValues)
                    {
                        var dataRows = dsGalData.dtNoteKeyValues.Select("id =" + id.ToString(inv));
                        if (dataRows.Length > 0)
                            dsGalData.dtNoteKeyValues.Rows.Remove(dataRows[0]);
                    }
                    NoteData_update();
                }
            }
        }
        private void NoteData_cmbVersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!NoteData_cmbVersionSelect_ignoreEvents)
                NoteData_LoadSelectedVersionIntoTxtNotesData();
        }
        private void NotesData_txtNoteData_TextChanged(object sender, EventArgs e)
        {
            if (!NoteData_cmbVersionSelect_ignoreEvents)
            {
                NoteData_cmbVersionSelect_ignoreEvents = true;
                cmbVersionSelect.SelectedIndex = 0;
                NoteData_cmbVersionSelect_ignoreEvents = false;
            }
        }
        private void NoteData_txtNoteData_Leave(object sender, EventArgs e)
        {
            if (!NoteData_cmbVersionSelect_ignoreEvents)
                NoteData_SaveNoteData(skipSaveToFile: true);
        }
        private void NoteData_txtNoteData_Enter(object sender, EventArgs e)
        {
            lblStatus.Text = "Hint: The note data pad stores versions of the pad from the past.  Use the versions drop down list to select pass versions.";
        }
        private void NoteData_Clipboard_ThreadClipboardMonitor()
        {
            String clipBoardtext = "";
            while (!formClosingFlag)
            {
                try
                {
                    this.Invoke(new Action(() => clipBoardtext = Clipboard.GetText()));
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    clipBoardtext = "";
                }
                catch (System.AccessViolationException)
                {
                    clipBoardtext = "";
                }
                catch (System.ObjectDisposedException)
                {
                    clipBoardtext = "";
                    formClosingFlag = true;
                }

                if (!formClosingFlag && clipBoardtext != this.lblClipBoard.Text)
                {
                    try
                    {
                        lblClipBoard.Invoke(new Action(() => { lblClipBoard.Text = clipBoardtext; }));
                    }
                    catch (System.ObjectDisposedException)
                    {
                        formClosingFlag = true;
                    }
                }


                if (!formClosingFlag)
                    System.Threading.Thread.Sleep(100);
            }
        }
        private void NoteData_Clipboard_BtnCopyFrom_Click(object sender, EventArgs e)
        {
            var t = new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(100);  //if it is a hack and it works, it is not awful, right?
                BeginInvoke(new Action(() =>
       {
           var text = Clipboard.GetText();
           if (text != txtNoteData.Text)
           {
               NoteData_cmbVersionSelect_ignoreEvents = true;
               NoteData_ignoreUpdates = true;
               var oldText = txtNoteData.Text;
               cmbVersionSelect.SelectedIndex = 0;
               if (string.IsNullOrWhiteSpace(oldText))
                   txtNoteData.Text = Clipboard.GetText();
               else
                   txtNoteData.Text = oldText + Environment.NewLine + Clipboard.GetText();
               NoteData_SaveNoteData(false, false);
               NoteData_ignoreUpdates = false;
               NoteData_cmbVersionSelect_ignoreEvents = false;
           }
       }));
            });
            t.IsBackground = true;
            t.Start(); //why you ask, why?  well when this button is clicked, the leave event for txtItemData is triggered, which then triggers the save event, which is on its own thread, and then triggers the load event about 2 ms later (which overwrites what we put in here if we do not wait).  We are just giving time for all those events happen before putting in our data.
        }
        private void NoteData_Clipboard_BtnPasteTo_Click(object sender, EventArgs e)
        {
            String text = txtNoteData.SelectedText;
            if (string.IsNullOrWhiteSpace(text))
                text = txtNoteData.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                if (Clipboard.ContainsText())
                    Clipboard.Clear();
            }
            else
                Clipboard.SetText(text);
        }
        private void NoteData_Clipboard_btnClearClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }
    }
}
