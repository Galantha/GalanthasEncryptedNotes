/*Created by Layla aka Galantha
 * MIT license
 * no warranty
 * 
 * Copyright 2020-12-27
 * 
 * email: gal_0xff@outlook.com
 */

using System;
using System.Windows.Forms;

namespace GalsPassHolder
{
    static class GalInputDialog
    {
        public static DialogResult Show(out String answer, Form parent, string question, string title = "?", string fontName = "Microsoft Sans Serif", bool isPasswordInput = false)
        {
            var dialog = new Dialog(question, title, parent, fontName, isPasswordInput);
            DialogResult result = dialog.ShowDialog(parent);
            answer = dialog.Value;
            dialog.Dispose();
            return result;
        }

        private class Dialog : Form
        {
            private readonly TextBox txtInput = new TextBox();
            private readonly Label lblQuestionMark = new Label() { Text = "?" };
            private readonly Button btnOk = new Button() { Text = "Ok", DialogResult = DialogResult.OK };
            private readonly Button btnCancel = new Button() { Text = "Cancel", DialogResult = DialogResult.Cancel };
            private readonly Label lblQuestion = new Label();
            private readonly TableLayoutPanel tbllyoPanel = new TableLayoutPanel();
            public string Value
            {
                get { return txtInput.Text; }
                set { txtInput.Text = value; }
            }
            private const double defaultWidth = 300;
            private const double defaultHeight = (defaultWidth / 16) * 9;
            private const double defaultFontSize = 10;
            private readonly string defaultFontName;

            public Dialog(String question, String title, Form parentForm, String defaultFontName, bool isPasswordInput) : base()
            {
                //this.Parent = parentForm;
                this.Text = title;
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                this.Width = Convert.ToInt32(parentForm.Width / 2);
                if (this.Width < defaultWidth) this.Width = Convert.ToInt32(defaultWidth);
                this.Height = Width / 16 * 9;
                this.ControlBox = false;
                this.StartPosition = FormStartPosition.CenterParent;
                this.Resize += (object a, EventArgs b) => FormResized();
                this.Controls.Add(tbllyoPanel);
                this.defaultFontName = defaultFontName;
                this.DialogResult = DialogResult.Cancel;

                lblQuestion.Text = question;
                txtInput.UseSystemPasswordChar = isPasswordInput;

                //no with statement in C# :(
                { //"with"
                    var w = tbllyoPanel;

                    w.ColumnCount = 3;
                    w.RowCount = 3;
                    for (int i = 0; i < w.RowCount; i++)
                        w.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / w.RowCount));

                    w.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                    w.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
                    w.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    w.Controls.Add(lblQuestion);
                    w.Controls.Add(lblQuestionMark);
                    w.Controls.Add(txtInput);
                    w.Controls.Add(btnCancel);
                    w.Controls.Add(btnOk);

                    w.SetColumn(lblQuestionMark, 0);
                    w.SetRow(lblQuestionMark, 0);
                    w.SetRowSpan(lblQuestionMark, 2);

                    w.SetColumn(lblQuestion, 1);
                    w.SetRow(lblQuestion, 0);
                    w.SetColumnSpan(lblQuestion, 2);

                    w.SetColumn(txtInput, 1);
                    w.SetRow(txtInput, 1);
                    w.SetColumnSpan(txtInput, 2);

                    w.SetColumn(btnOk, 0);
                    w.SetRow(btnOk, 2);
                    w.SetColumnSpan(btnOk, 2);

                    w.SetColumn(btnCancel, 2);
                    w.SetRow(btnCancel, 2);
                } //w goes out of scope here

                btnOk.Click += (object a, EventArgs b) => OkClick();
                btnCancel.Click += (object a, EventArgs b) => CancelClick();
                txtInput.TextChanged += (object a, EventArgs b) => TxtInputChanged();

                GalFormFunctions.RecursiveSetProperty<DockStyle>(this, "Dock", DockStyle.Fill);
                GalFormFunctions.RecursiveSetProperty<System.Drawing.ContentAlignment>(this, "TextAlign", System.Drawing.ContentAlignment.MiddleCenter);
                lblQuestion.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                //lblQuestionMark.TextAlign = System.Drawing.ContentAlignment.BottomCenter;

                TxtInputChanged();
                FormResized();
            }

            private void OkClick()
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            private void CancelClick()
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            private void FormResized()
            {
                var xMultiplier = this.Width / defaultWidth;
                var yMultiplier = this.Height / defaultHeight;
                double multiplier;
                if (xMultiplier < yMultiplier)
                    multiplier = xMultiplier;
                else
                    multiplier = yMultiplier;

                float fontSize = Convert.ToSingle(defaultFontSize * multiplier);
                var font = new System.Drawing.Font(defaultFontName, fontSize);

                GalFormFunctions.RecursiveSetProperty<System.Drawing.Font>(this, "Font", font);
                lblQuestionMark.Font = new System.Drawing.Font(defaultFontName, fontSize * 3);
            }


            private void TxtInputChanged()
            {
                if (txtInput.Text.Length > 0)
                {
                    btnOk.Enabled = true;
                    this.AcceptButton = btnOk;
                }
                else
                {
                    btnOk.Enabled = false;
                    this.AcceptButton = btnCancel;
                }
            }
        }
    }

    static class GalFormFunctions
    {
        public static readonly IFormatProvider inv = System.Globalization.CultureInfo.InvariantCulture;
        public static void RecursiveSetProperty<T>(Control ctr, String property, T propertyValue, System.Collections.Generic.List<Type> includeOnlyTypes = null, System.Collections.Generic.List<Type> excludeTypes = null)
        {
            System.Windows.Forms.Control.ControlCollection controls = null;
            controls = ctr.Controls;

            foreach (Control childControl in controls)
                RecursiveSetProperty<T>(childControl, property, propertyValue, includeOnlyTypes, excludeTypes);

            Type type = ctr.GetType();
            if ((includeOnlyTypes == null || includeOnlyTypes.Contains(type)) && (excludeTypes == null || !excludeTypes.Contains(type)))
                SetPropertyIfExists<T>(ctr, property, propertyValue);
        }
        public static void SetPropertyIfExists<T>(Control ctr, String property, T propertyValue)
        {
            var prop = ctr.GetType().GetProperty(property);
            if (propertyValue.GetType().Name == "Font")
            {
                var oldFont = (System.Drawing.Font)prop.GetValue(ctr);
                var newFont = (System.Drawing.Font)(object)propertyValue;
                if (newFont.Height != oldFont.Height || newFont.Unit != oldFont.Unit || newFont.Name != oldFont.Name || newFont.Size != oldFont.Size || newFont.Style != oldFont.Style)
                    prop.SetValue(ctr, propertyValue);
            }
            else
            {
                if (prop != null && prop.PropertyType == propertyValue.GetType())
                    prop.SetValue(ctr, propertyValue); //this frequently triggers events
            }
        }

        private static readonly System.Collections.Generic.List<string> ControlValuesTypeNamesSupported = new System.Collections.Generic.List<string>(new string[] { "TextBox", "NumericUpDown", "CheckBox" });
        public enum Direction
        {
            save,
            load
        }
        public static void ControlValuesToFromDT(Direction direction, string prefix, Control ctr, System.Data.DataTable dt, bool recursive = true, System.Collections.Generic.List<string> typeNames = null, string dbFieldName = "name", string dbFieldValue = "value")
        {
            if (typeNames is null)
                typeNames = ControlValuesTypeNamesSupported;

            var myPrefix = prefix + "-" + ctr.Name + "As" + ctr.GetType().Name;

            string typeName = ctr.GetType().Name;
            if (ControlValuesTypeNamesSupported.Contains(typeName))
                switch (typeName)
                {
                    case "TextBox":
                        var txt = (TextBox)ctr;
                        if (direction == Direction.save)
                            SaveToDT(dt, myPrefix, txt.Text, dbFieldName, dbFieldValue);
                        else
                            txt.Text = GetFromDT(dt, myPrefix, txt.Text, dbFieldName, dbFieldValue);
                        break;
                    case "NumericUpDown":
                        var num = (NumericUpDown)ctr;
                        if (direction == Direction.save)
                            SaveToDT(dt, myPrefix, num.Value.ToString(inv), dbFieldName, dbFieldValue);
                        else
                            num.Value = Convert.ToDecimal(GetFromDT(dt, myPrefix, num.Value.ToString(inv), dbFieldName, dbFieldValue), inv);
                        break;
                    case "CheckBox":
                        var chk = (CheckBox)ctr;
                        if (direction == Direction.save)
                            SaveToDT(dt, myPrefix, chk.Checked.ToString(inv), dbFieldName, dbFieldValue);
                        else
                            chk.Checked = Convert.ToBoolean(GetFromDT(dt, prefix, chk.Checked.ToString(inv), dbFieldName, dbFieldValue), inv);
                        break;
                    default:
                        throw new Exception("unhandled type" + typeName);
                }

            if (recursive)
                foreach (Control child in ctr.Controls)
                    ControlValuesToFromDT(direction, myPrefix, child, dt, recursive, typeNames, dbFieldName, dbFieldValue);
        }

        private static void SaveToDT(System.Data.DataTable dt, string name, string value, string dbFieldName = "name", string dbFieldValue = "value")
        {
            lock (dt)
            {
                var rows = dt.Select(dbFieldName + "='" + name + "'");
                if (rows.Length > 0)
                {
                    //found existing row
                    var row = rows[0];
                    row[dbFieldValue] = value;
                    dt.AcceptChanges();
                }
                else
                {
                    //make new row
                    var row = dt.NewRow();
                    row[dbFieldName] = name;
                    row[dbFieldValue] = value;
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                }
            }
        }
        private static string GetFromDT(System.Data.DataTable dt, string name, string defaultValue = "", string dbFieldName = "name", string dbFieldValue = "value")
        {
            lock (dt)
            {
                var rows = dt.Select(dbFieldName + "='" + name + "'");
                if (rows.Length > 0)
                {
                    //found a record
                    var row = rows[0];
                    return (string)row[dbFieldValue];
                }
                else
                {
                    //return default value
                    return defaultValue;
                }
            }
        }

    }
}
