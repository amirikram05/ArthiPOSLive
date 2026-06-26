using Bunifu.Framework.UI;
using System;
using System.Windows.Forms;

namespace ArthiPOS.Utill
{
    public static class Prompt
    {
        private static BunifuMaterialTextbox textBox;
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            textBox = new BunifuMaterialTextbox() { Left = 50, Top = 20, Width = 400 };
            textBox.isPassword = true;
            textBox.OnValueChanged += password_OnValueChanged;
            Button confirmation = new Button() { Text = "Verify", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static string ShowDialogDate()
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };
            DateTimePicker picker = new DateTimePicker()
            { Top = 20 };
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "yyyy-mm-dd";
            picker.ShowUpDown = true;
            prompt.Controls.Add(picker);
            Button confirmation = new Button() { Text = "Verify", Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? picker.Text : "";
        }
        private static void password_OnValueChanged(object sender, EventArgs e)
        {
            textBox.isPassword = true;
        }
    }
}
