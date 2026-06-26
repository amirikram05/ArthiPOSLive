using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ArthiPOS.Controls.test
{
    public partial class LogExecMangeForm : Form
    {

        private string logDirectory = "c:\\arthilog"; // Directory where the logs are stored

        public LogExecMangeForm()
        {
            InitializeComponent();
            textBoxLogContent.ScrollBars = ScrollBars.Vertical;
            this.btnLoadLogs_Click(this,new EventArgs());
        }

        // Button click event to load the log dates (log files)
        private void btnLoadLogs_Click(object sender, EventArgs e)
        {
            // Clear the listbox before adding new items
            listBoxLogs.Items.Clear();

            // Check if log directory exists
            if (Directory.Exists(logDirectory))
            {
                // Get all log files in the directory
                var logFiles = Directory.GetFiles(logDirectory, "*.log");

                // Sort log files by creation date and display only the file names (dates)
                foreach (var logFile in logFiles.OrderByDescending(f => f))
                {
                    string fileName = Path.GetFileNameWithoutExtension(logFile);
                    listBoxLogs.Items.Add(fileName); // Add file names (which are dates) to the ListBox
                }
            }
            else
            {
                MessageBox.Show("Log directory not found!");
            }
        }

        // Event handler when a log date is selected from the list
        private void listBoxLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected log date (file name without extension)
            string selectedLogDate = listBoxLogs.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedLogDate))
            {
                // Construct the full log file path
                string logFilePath = Path.Combine(logDirectory, selectedLogDate + ".log");

                // Check if the log file exists
                if (File.Exists(logFilePath))
                {
                    // Read the log file contents and display them in the TextBox
                    string logContent = File.ReadAllText(logFilePath);
                    textBoxLogContent.Text = logContent;
                }
                else
                {
                    MessageBox.Show("Selected log file not found!");
                }
            }
        }
    }
}
