using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArthiPOS.newmenu
{
    public class NotificationPanel : Form
    {
        private FlowLayoutPanel notificationList;
        private Timer autoCloseTimer;

        public NotificationPanel()
        {
            InitializeComponent();
            //SetupNotifications();
            StartAutoCloseTimer();
        }

        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.White;
            this.Size = new Size(300, 400);
            this.ShowInTaskbar = false;
            this.TopMost = true;

            // Header
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 40;
            header.BackColor = Color.FromArgb(41, 128, 185);
            header.Padding = new Padding(10);

            Label lblTitle = new Label();
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Text = "Notifications";
            lblTitle.Font = new Font("Segoe UI Semibold", 12F);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            Button btnClose = new Button();
            btnClose.Dock = DockStyle.Right;
            btnClose.Size = new Size(30, 30);
            btnClose.Text = "×";
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.BackColor = Color.Transparent;
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 14F);
            btnClose.Click += (s, e) => this.Close();

            header.Controls.Add(lblTitle);
            header.Controls.Add(btnClose);

            // Notification List
            notificationList = new FlowLayoutPanel();
            notificationList.Dock = DockStyle.Fill;
            notificationList.BackColor = Color.White;
            notificationList.FlowDirection = FlowDirection.TopDown;
            notificationList.WrapContents = false;
            notificationList.AutoScroll = true;
            notificationList.Padding = new Padding(10);

            // Clear All Button
            Button btnClearAll = new Button();
            btnClearAll.Dock = DockStyle.Bottom;
            btnClearAll.Height = 40;
            btnClearAll.Text = "Clear All Notifications";
            btnClearAll.FlatStyle = FlatStyle.Flat;
            btnClearAll.BackColor = Color.FromArgb(245, 245, 245);
            btnClearAll.ForeColor = Color.FromArgb(100, 100, 100);
            btnClearAll.Click += (s, e) => ClearAllNotifications();

            this.Controls.Add(notificationList);
            this.Controls.Add(btnClearAll);
            this.Controls.Add(header);

            // Border
            this.Paint += (s, e) => {
                using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
                }
            };
        }

        private void SetupNotifications(string title,string message,NotificationType type)
        {
            // Add sample notifications (replace with real notifications)
            //AddNotification("System", "Database connection established successfully.", NotificationType.Success);
            //AddNotification("Sales", "New sale completed - Order #12345", NotificationType.Info);
            //AddNotification("Inventory", "Low stock alert: Product XYZ is running low.", NotificationType.Warning);
            //AddNotification("System", "Daily backup completed successfully.", NotificationType.Success);
            //AddNotification("Reports", "Monthly report is ready for review.", NotificationType.Info);            //AddNotification("System", "Database connection established successfully.", NotificationType.Success);
            AddNotification(title, message, type);

            if (notificationList.Controls.Count == 0)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "No new notifications";
                lblEmpty.Font = new Font("Segoe UI", 11F);
                lblEmpty.ForeColor = Color.FromArgb(150, 150, 150);
                lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
                lblEmpty.Dock = DockStyle.Fill;
                notificationList.Controls.Add(lblEmpty);
            }
        }

        private void AddNotification(string title, string message, NotificationType type)
        {
            NotificationItem item = new NotificationItem(title, message, type);
            notificationList.Controls.Add(item);
        }

        private void ClearAllNotifications()
        {
            notificationList.Controls.Clear();

            Label lblEmpty = new Label();
            lblEmpty.Text = "All notifications cleared";
            lblEmpty.Font = new Font("Segoe UI", 11F);
            lblEmpty.ForeColor = Color.FromArgb(150, 150, 150);
            lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            lblEmpty.Dock = DockStyle.Fill;
            notificationList.Controls.Add(lblEmpty);
        }

        private void StartAutoCloseTimer()
        {
            autoCloseTimer = new Timer();
            autoCloseTimer.Interval = 10000; // 10 seconds
            autoCloseTimer.Tick += (s, e) => {
                autoCloseTimer.Stop();
                this.Close();
            };
            autoCloseTimer.Start();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Close();
        }
    }

    public class NotificationItem : Panel
    {
        public NotificationItem(string title, string message, NotificationType type)
        {
            this.Size = new Size(280, 80);
            this.BackColor = GetBackgroundColor(type);
            this.BorderStyle = BorderStyle.FixedSingle;
            //this.BorderColor = Color.FromArgb(220, 220, 220);
            this.Margin = new Padding(0, 0, 0, 10);
            this.Padding = new Padding(10);

            // Icon
            Label lblIcon = new Label();
            lblIcon.Size = new Size(30, 30);
            lblIcon.Location = new Point(10, 10);
            lblIcon.Text = GetIcon(type);
            lblIcon.Font = new Font("Segoe UI", 14F);
            lblIcon.ForeColor = GetIconColor(type);
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;

            // Title
            Label lblTitle = new Label();
            lblTitle.Location = new Point(50, 10);
            lblTitle.Size = new Size(210, 20);
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI Semibold", 10F);
            lblTitle.ForeColor = Color.FromArgb(60, 60, 60);

            // Message
            Label lblMessage = new Label();
            lblMessage.Location = new Point(50, 30);
            lblMessage.Size = new Size(210, 40);
            lblMessage.Text = message;
            lblMessage.Font = new Font("Segoe UI", 9F);
            lblMessage.ForeColor = Color.FromArgb(100, 100, 100);

            // Time
            Label lblTime = new Label();
            lblTime.Dock = DockStyle.Bottom;
            lblTime.Height = 15;
            lblTime.Text = DateTime.Now.ToString("hh:mm tt");
            lblTime.Font = new Font("Segoe UI", 8F);
            lblTime.ForeColor = Color.FromArgb(150, 150, 150);
            lblTime.TextAlign = ContentAlignment.MiddleRight;
            lblTime.Padding = new Padding(0, 0, 5, 0);

            this.Controls.Add(lblIcon);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblMessage);
            this.Controls.Add(lblTime);

            // Hover effect
            this.MouseEnter += (s, e) => {
                this.BackColor = Color.FromArgb(250, 250, 250);
            };
            this.MouseLeave += (s, e) => {
                this.BackColor = GetBackgroundColor(type);
            };
            this.Click += (s, e) => {
                // Handle notification click
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private Color GetBackgroundColor(NotificationType type)
        {
            return type switch
            {
                NotificationType.Success => Color.FromArgb(240, 255, 240),
                NotificationType.Warning => Color.FromArgb(255, 250, 240),
                NotificationType.Error => Color.FromArgb(255, 240, 240),
                _ => Color.FromArgb(240, 245, 255)
            };
        }

        private Color GetIconColor(NotificationType type)
        {
            return type switch
            {
                NotificationType.Success => Color.FromArgb(46, 204, 113),
                NotificationType.Warning => Color.FromArgb(230, 126, 34),
                NotificationType.Error => Color.FromArgb(231, 76, 60),
                _ => Color.FromArgb(41, 128, 185)
            };
        }

        private string GetIcon(NotificationType type)
        {
            return type switch
            {
                NotificationType.Success => "✓",
                NotificationType.Warning => "⚠",
                NotificationType.Error => "✗",
                _ => "ℹ"
            };
        }
    }

    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error
    }
}