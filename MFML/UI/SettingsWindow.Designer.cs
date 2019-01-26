namespace MFML.UI
{
    partial class SettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.CloseButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mcFolderBox = new MetroFramework.Controls.MetroTextBox();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.javaPathBox = new MetroFramework.Controls.MetroTextBox();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.memoryBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.debugBox = new MetroFramework.Controls.MetroToggle();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.BMCLAPIBox = new MetroFramework.Controls.MetroToggle();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(721, 1);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 29);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.MouseEnter += new System.EventHandler(this.TopBarButton_MouseEnter);
            this.CloseButton.MouseLeave += new System.EventHandler(this.TopBarButton_MouseLeave);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimizeButton.ForeColor = System.Drawing.Color.White;
            this.MinimizeButton.Location = new System.Drawing.Point(691, 1);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(30, 29);
            this.MinimizeButton.TabIndex = 3;
            this.MinimizeButton.Text = "-";
            this.MinimizeButton.UseVisualStyleBackColor = true;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.metroTabControl1);
            this.panel1.Location = new System.Drawing.Point(1, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 470);
            this.panel1.TabIndex = 4;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(750, 470);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.flowLayoutPanel1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(742, 428);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Minecraft设置";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(742, 428);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.metroLabel1);
            this.flowLayoutPanel2.Controls.Add(this.mcFolderBox);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(736, 32);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(3, 3);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(209, 25);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Minecraft主文件夹路径：";
            // 
            // mcFolderBox
            // 
            this.mcFolderBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.mcFolderBox.CustomButton.Image = null;
            this.mcFolderBox.CustomButton.Location = new System.Drawing.Point(492, 1);
            this.mcFolderBox.CustomButton.Name = "";
            this.mcFolderBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mcFolderBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mcFolderBox.CustomButton.TabIndex = 1;
            this.mcFolderBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mcFolderBox.CustomButton.UseSelectable = true;
            this.mcFolderBox.CustomButton.Visible = false;
            this.mcFolderBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mcFolderBox.Lines = new string[0];
            this.mcFolderBox.Location = new System.Drawing.Point(218, 4);
            this.mcFolderBox.MaxLength = 32767;
            this.mcFolderBox.Name = "mcFolderBox";
            this.mcFolderBox.PasswordChar = '\0';
            this.mcFolderBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mcFolderBox.SelectedText = "";
            this.mcFolderBox.SelectionLength = 0;
            this.mcFolderBox.SelectionStart = 0;
            this.mcFolderBox.ShortcutsEnabled = true;
            this.mcFolderBox.Size = new System.Drawing.Size(514, 23);
            this.mcFolderBox.TabIndex = 1;
            this.mcFolderBox.UseSelectable = true;
            this.mcFolderBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mcFolderBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mcFolderBox.Leave += new System.EventHandler(this.mcFolderBox_Leave);
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.flowLayoutPanel6);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(742, 428);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Java设置";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel6.Controls.Add(this.flowLayoutPanel7);
            this.flowLayoutPanel6.Controls.Add(this.flowLayoutPanel8);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(742, 428);
            this.flowLayoutPanel6.TabIndex = 3;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.metroLabel4);
            this.flowLayoutPanel7.Controls.Add(this.javaPathBox);
            this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(736, 32);
            this.flowLayoutPanel7.TabIndex = 0;
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.Location = new System.Drawing.Point(3, 3);
            this.metroLabel4.Margin = new System.Windows.Forms.Padding(3);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(98, 25);
            this.metroLabel4.TabIndex = 0;
            this.metroLabel4.Text = "Java路径：";
            // 
            // javaPathBox
            // 
            this.javaPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.javaPathBox.CustomButton.Image = null;
            this.javaPathBox.CustomButton.Location = new System.Drawing.Point(603, 1);
            this.javaPathBox.CustomButton.Name = "";
            this.javaPathBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.javaPathBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.javaPathBox.CustomButton.TabIndex = 1;
            this.javaPathBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.javaPathBox.CustomButton.UseSelectable = true;
            this.javaPathBox.CustomButton.Visible = false;
            this.javaPathBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.javaPathBox.Lines = new string[0];
            this.javaPathBox.Location = new System.Drawing.Point(107, 4);
            this.javaPathBox.MaxLength = 32767;
            this.javaPathBox.Name = "javaPathBox";
            this.javaPathBox.PasswordChar = '\0';
            this.javaPathBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.javaPathBox.SelectedText = "";
            this.javaPathBox.SelectionLength = 0;
            this.javaPathBox.SelectionStart = 0;
            this.javaPathBox.ShortcutsEnabled = true;
            this.javaPathBox.Size = new System.Drawing.Size(625, 23);
            this.javaPathBox.TabIndex = 1;
            this.javaPathBox.UseSelectable = true;
            this.javaPathBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.javaPathBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.javaPathBox.Leave += new System.EventHandler(this.javaPathBox_Leave);
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Controls.Add(this.metroLabel5);
            this.flowLayoutPanel8.Controls.Add(this.memoryBox);
            this.flowLayoutPanel8.Controls.Add(this.metroLabel6);
            this.flowLayoutPanel8.Location = new System.Drawing.Point(3, 41);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(736, 32);
            this.flowLayoutPanel8.TabIndex = 2;
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.Location = new System.Drawing.Point(3, 3);
            this.metroLabel5.Margin = new System.Windows.Forms.Padding(3);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(138, 25);
            this.metroLabel5.TabIndex = 0;
            this.metroLabel5.Text = "最大分配内存：";
            // 
            // memoryBox
            // 
            this.memoryBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.memoryBox.CustomButton.Image = null;
            this.memoryBox.CustomButton.Location = new System.Drawing.Point(100, 1);
            this.memoryBox.CustomButton.Name = "";
            this.memoryBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.memoryBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.memoryBox.CustomButton.TabIndex = 1;
            this.memoryBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.memoryBox.CustomButton.UseSelectable = true;
            this.memoryBox.CustomButton.Visible = false;
            this.memoryBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.memoryBox.Lines = new string[0];
            this.memoryBox.Location = new System.Drawing.Point(147, 4);
            this.memoryBox.MaxLength = 32767;
            this.memoryBox.Name = "memoryBox";
            this.memoryBox.PasswordChar = '\0';
            this.memoryBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoryBox.SelectedText = "";
            this.memoryBox.SelectionLength = 0;
            this.memoryBox.SelectionStart = 0;
            this.memoryBox.ShortcutsEnabled = true;
            this.memoryBox.Size = new System.Drawing.Size(122, 23);
            this.memoryBox.TabIndex = 1;
            this.memoryBox.UseSelectable = true;
            this.memoryBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.memoryBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.memoryBox.Leave += new System.EventHandler(this.memoryBox_Leave);
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel6.Location = new System.Drawing.Point(275, 3);
            this.metroLabel6.Margin = new System.Windows.Forms.Padding(3);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(37, 25);
            this.metroLabel6.TabIndex = 2;
            this.metroLabel6.Text = "MB";
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.flowLayoutPanel3);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(742, 428);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "启动器设置";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel9);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(742, 428);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.metroLabel2);
            this.flowLayoutPanel4.Controls.Add(this.debugBox);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(736, 32);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(3, 3);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(3);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(210, 25);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "启动游戏后打开调试窗口";
            // 
            // debugBox
            // 
            this.debugBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugBox.Location = new System.Drawing.Point(219, 3);
            this.debugBox.Name = "debugBox";
            this.debugBox.Size = new System.Drawing.Size(90, 25);
            this.debugBox.TabIndex = 1;
            this.debugBox.Text = "Off";
            this.debugBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.debugBox.UseSelectable = true;
            this.debugBox.CheckedChanged += new System.EventHandler(this.debugBox_CheckedChanged);
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel9.Controls.Add(this.metroLabel7);
            this.flowLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel9.Location = new System.Drawing.Point(0, 38);
            this.flowLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(742, 161);
            this.flowLayoutPanel9.TabIndex = 2;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.metroLabel3);
            this.flowLayoutPanel5.Controls.Add(this.BMCLAPIBox);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(736, 36);
            this.flowLayoutPanel5.TabIndex = 2;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.Location = new System.Drawing.Point(3, 3);
            this.metroLabel3.Margin = new System.Windows.Forms.Padding(3);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(370, 25);
            this.metroLabel3.TabIndex = 0;
            this.metroLabel3.Text = "使用BMCLAPI加速下载游戏文件（国内推荐）";
            // 
            // BMCLAPIBox
            // 
            this.BMCLAPIBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BMCLAPIBox.Location = new System.Drawing.Point(379, 3);
            this.BMCLAPIBox.Name = "BMCLAPIBox";
            this.BMCLAPIBox.Size = new System.Drawing.Size(90, 25);
            this.BMCLAPIBox.TabIndex = 1;
            this.BMCLAPIBox.Text = "Off";
            this.BMCLAPIBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BMCLAPIBox.UseSelectable = true;
            this.BMCLAPIBox.CheckedChanged += new System.EventHandler(this.BMCLAPIBox_CheckedChanged);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroLabel7.Location = new System.Drawing.Point(3, 42);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(691, 114);
            this.metroLabel7.TabIndex = 3;
            this.metroLabel7.Text = "BMCLAPI协议：\r\n1.BMCLAPI下的所有文件，除BMCLAPI本身的源码之外，归源站点所有\r\n2.BMCLAPI会尽量保证文件的完整性、有效性和实时性，" +
    "对于使用BMCLAPI带来的一切纠纷，与BMCLAPI无关。\r\n3.BMCLAPI和BMCL不同，属于非开源项目\r\n4.所有使用BMCLAPI的程序必需在下载界" +
    "面或其他可视部分标明来源\r\n5.禁止在BMCLAPI上二次封装其他协议";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(752, 501);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel8.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox mcFolderBox;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroToggle debugBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox javaPathBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroTextBox memoryBox;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroToggle BMCLAPIBox;
        private MetroFramework.Controls.MetroLabel metroLabel7;
    }
}