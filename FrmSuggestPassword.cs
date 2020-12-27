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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GalsPassHolder
{
    public partial class FrmSuggestPassword : Form
    {
        public static DialogResult ShowDialog(out string password, Form owner, System.Data.DataTable fieldValuesDataTable = null, string fontName = "Microsoft Sans Serif")
        {
            var frm = new FrmSuggestPassword(fieldValuesDataTable, fontName);
            frm.ShowDialog(owner);
            password = frm.Password;
            return frm.result;
        }

        readonly string fontName;
        bool shownFirstTime = true;
        const float defaultWidth = 356f;
        const float defaultHeight = 310f;
        bool updateAppearance = false;
        readonly System.Data.DataTable settingsDataTable;
        const string formSettingsPrefix = "SuggestPassword";
        public DialogResult result = DialogResult.Cancel;
        readonly Random rand = new Random();

        public string Password
        {
            get { return lblPassword.Text; }
        }

        private FrmSuggestPassword(System.Data.DataTable fieldValuesDataTable = null, string fontName = "Microsoft Sans Serif") : base()
        {
            InitializeComponent();

            this.fontName = fontName;
            this.settingsDataTable = fieldValuesDataTable;
        }

        private void BtnReGenerate_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void FrmSuggestPassword_Load(object sender, EventArgs e)
        {
            if (settingsDataTable != null)
                GalFormFunctions.ControlValuesToFromDT(GalFormFunctions.Direction.load, formSettingsPrefix, this, settingsDataTable);
            GeneratePassword();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            this.Close();
        }

        void UpdateAppearance()
        {
            if (updateAppearance)
            {
                const float defaultFontSize = 8.25f;
                float width = this.Width;
                float height = this.Height;
                float widthRatio = width / defaultWidth;
                float heightRatio = height / defaultHeight;
                float ratio;
                if (widthRatio < heightRatio)
                    ratio = widthRatio;
                else
                    ratio = heightRatio;

                var font = new Font(fontName, defaultFontSize * ratio);
                GalFormFunctions.RecursiveSetProperty<Font>(this, "Font", font, excludeTypes: new List<Type>(new Type[] { txtCustom1.GetType(), numLength.GetType() }));
                font = new Font(fontName, defaultFontSize * ratio * 1.3f);
                GalFormFunctions.RecursiveSetProperty<Font>(this, "Font", font, includeOnlyTypes: new List<Type>(new Type[] { txtCustom1.GetType(), numLength.GetType() }));

                if (chkCustom1.Checked)
                    txtCustom1.Enabled = true;
                else
                    txtCustom1.Enabled = false;

                if (chkCustom2.Checked)
                    txtCustom2.Enabled = true;
                else
                    txtCustom2.Enabled = false;
            }
        }

        private void TableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            UpdateAppearance();
        }

        private void ChkCustom1_CheckedChanged(object sender, EventArgs e) //also handled Custom2
        {
            UpdateAppearance();
            GeneratePassword();
        }

        private void GeneratePassword()
        {
            StringBuilder source = new StringBuilder();
            var inserters = new SortedList<int, char>(); //I like it to include at least one character randomly placed from each category, hence this extra complexity which reduces randomness
            if (chkCustom1.Checked)
                Appender(source, inserters, txtCustom1.Text);
            if (chkCustom2.Checked)
                Appender(source, inserters, txtCustom2.Text);

            AppendCheckBoxIfChecked(source, inserters, new CheckBox[] { chkLowerCase, chkNumbers, chkSymbols1, chkSymbols2, chkUpperCase });
            string sampler = source.ToString();
            StringBuilder password = new StringBuilder();

            if (sampler.Length > 0)
                for (int i = 0; i < numLength.Value; i++)
                {
                    if (inserters.ContainsKey(i))
                        password.Append(inserters[i]);
                    else
                        password.Append(sampler[rand.Next(0, sampler.Length - 1)]);
                }

            else
                password.Append("Error: No checkboxes checked.");
            lblPassword.Text = password.ToString();
        }

        private void AppendCheckBoxIfChecked(StringBuilder text, SortedList<int, char> inserters, CheckBox[] checkBoxes)
        {
            foreach (var chk in checkBoxes)
                if (chk.Checked) Appender(text, inserters, chk.Text);
        }

        private void Appender(StringBuilder source, SortedList<int, char> inserters, string text)
        {
            source.Append(text);
            var lastIndex = Convert.ToInt32(numLength.Value) - 1;
            if (text.Length > 0 && inserters.Count < lastIndex)
            {
                var key = rand.Next(0, lastIndex);
                while (inserters.ContainsKey(key))
                    key = rand.Next(0, lastIndex); //eventually we will find a working key //stops sorted list collisions
                inserters.Add(key, text[rand.Next(0, text.Length - 1)]);
            }
        }

        private void ChkSymbols2_CheckedChanged(object sender, EventArgs e) //handles chkLowerCase, chkNumbers, chkSymbols1, chkSymbols2, chkUpperCase
        {
            GeneratePassword();
        }

        private void NumLength_ValueChanged(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void FrmSuggestPassword_Shown(object sender, EventArgs e)
        {
            if (shownFirstTime)
            {
                shownFirstTime = false;
                float toHeight = this.Owner.Height * 0.9f;
                float toWidth = (toHeight / defaultHeight) * defaultWidth;
                if (toHeight > defaultHeight)
                    this.Height = Convert.ToInt32(toHeight);
                if (toWidth > defaultWidth)
                    this.Width = Convert.ToInt32(toWidth);

                this.Location = new System.Drawing.Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2, Owner.Location.Y + Owner.Height / 2 - this.Height / 2);

                updateAppearance = true;
                UpdateAppearance();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            this.Close();
        }

        private void BtnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Password);
        }

        private void FrmSuggestPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settingsDataTable != null)
                GalFormFunctions.ControlValuesToFromDT(GalFormFunctions.Direction.save, formSettingsPrefix, this, settingsDataTable);
        }
    }
}
