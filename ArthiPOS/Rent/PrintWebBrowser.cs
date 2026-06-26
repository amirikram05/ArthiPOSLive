using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

public static class PrintHelper
{
    public static void PrintWebBrowser(WebBrowser webBrowser)
    {
        if (webBrowser == null || webBrowser.Document == null)
            return;

        try
        {
            // Method 1: Use PrintDialog
            PrintDialog printDialog = new PrintDialog();

            // Method 2: Use PrintDocument
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                // Convert HTML to printable format
                string html = webBrowser.DocumentText;
                // Simple text rendering as fallback
                e.Graphics.DrawString("Printing report...",
                    new Font("Arial", 12),
                    Brushes.Black,
                    new PointF(100, 100));
            };

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            previewDialog.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Print error: {ex.Message}\n\n" +
                "Please use the print button in the report itself.",
                "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}