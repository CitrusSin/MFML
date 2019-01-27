namespace MFML.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.optifineButton = new System.Windows.Forms.Button();
            this.downloadGame = new System.Windows.Forms.Button();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.versionsBox = new MetroFramework.Controls.MetroComboBox();
            this.playerNameBox = new MetroFramework.Controls.MetroTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.startMCButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(1, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 420);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 420);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.34188F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.65812F));
            this.tableLayoutPanel2.Controls.Add(this.optifineButton, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.downloadGame, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.versionsBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.playerNameBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(234, 414);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // optifineButton
            // 
            this.optifineButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.tableLayoutPanel2.SetColumnSpan(this.optifineButton, 2);
            this.optifineButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optifineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optifineButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.optifineButton.Location = new System.Drawing.Point(3, 377);
            this.optifineButton.Name = "optifineButton";
            this.optifineButton.Size = new System.Drawing.Size(228, 34);
            this.optifineButton.TabIndex = 7;
            this.optifineButton.Text = "为当前版本安装Optifine";
            this.optifineButton.UseVisualStyleBackColor = false;
            this.optifineButton.Click += new System.EventHandler(this.optifineButton_Click);
            // 
            // downloadGame
            // 
            this.downloadGame.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.tableLayoutPanel2.SetColumnSpan(this.downloadGame, 2);
            this.downloadGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadGame.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downloadGame.Location = new System.Drawing.Point(3, 337);
            this.downloadGame.Name = "downloadGame";
            this.downloadGame.Size = new System.Drawing.Size(228, 34);
            this.downloadGame.TabIndex = 4;
            this.downloadGame.Text = "添加版本";
            this.downloadGame.UseVisualStyleBackColor = false;
            this.downloadGame.Click += new System.EventHandler(this.downloadGame_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "版本：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "游戏名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionsBox
            // 
            this.versionsBox.FormattingEnabled = true;
            this.versionsBox.ItemHeight = 23;
            this.versionsBox.Location = new System.Drawing.Point(74, 31);
            this.versionsBox.Name = "versionsBox";
            this.versionsBox.Size = new System.Drawing.Size(157, 29);
            this.versionsBox.TabIndex = 5;
            this.versionsBox.UseSelectable = true;
            // 
            // playerNameBox
            // 
            // 
            // 
            // 
            this.playerNameBox.CustomButton.Image = null;
            this.playerNameBox.CustomButton.Location = new System.Drawing.Point(137, 2);
            this.playerNameBox.CustomButton.Name = "";
            this.playerNameBox.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.playerNameBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.playerNameBox.CustomButton.TabIndex = 1;
            this.playerNameBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.playerNameBox.CustomButton.UseSelectable = true;
            this.playerNameBox.CustomButton.Visible = false;
            this.playerNameBox.Lines = new string[0];
            this.playerNameBox.Location = new System.Drawing.Point(74, 3);
            this.playerNameBox.MaxLength = 32767;
            this.playerNameBox.Name = "playerNameBox";
            this.playerNameBox.PasswordChar = '\0';
            this.playerNameBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.playerNameBox.SelectedText = "";
            this.playerNameBox.SelectionLength = 0;
            this.playerNameBox.SelectionStart = 0;
            this.playerNameBox.ShortcutsEnabled = true;
            this.playerNameBox.ShowClearButton = true;
            this.playerNameBox.Size = new System.Drawing.Size(157, 22);
            this.playerNameBox.TabIndex = 6;
            this.playerNameBox.UseSelectable = true;
            this.playerNameBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.playerNameBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.playerNameBox.Leave += new System.EventHandler(this.playerNameBox_Leave);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.startMCButton, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.settingsButton, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(683, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(114, 414);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // startMCButton
            // 
            this.startMCButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.startMCButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startMCButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMCButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startMCButton.Location = new System.Drawing.Point(3, 367);
            this.startMCButton.Name = "startMCButton";
            this.startMCButton.Size = new System.Drawing.Size(108, 44);
            this.startMCButton.TabIndex = 1;
            this.startMCButton.Text = "启动Minecraft";
            this.startMCButton.UseVisualStyleBackColor = false;
            this.startMCButton.Click += new System.EventHandler(this.startMCButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.settingsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.settingsButton.Location = new System.Drawing.Point(3, 317);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(108, 44);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.Text = "设置";
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(771, 1);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 29);
            this.CloseButton.TabIndex = 1;
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
            this.MinimizeButton.Location = new System.Drawing.Point(741, 1);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(30, 29);
            this.MinimizeButton.TabIndex = 2;
            this.MinimizeButton.Text = "-";
            this.MinimizeButton.UseVisualStyleBackColor = true;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(802, 451);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Micrafast\'s Minecraft Launcher";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button downloadGame;
        private System.Windows.Forms.Button startMCButton;
        private MetroFramework.Controls.MetroComboBox versionsBox;
        private MetroFramework.Controls.MetroTextBox playerNameBox;
        private System.Windows.Forms.Button optifineButton;
    }
}

