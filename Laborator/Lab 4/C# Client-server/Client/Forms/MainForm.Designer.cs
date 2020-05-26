namespace Client
{
    partial class MainForm
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
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.childrenDataGridView = new System.Windows.Forms.DataGridView();
            this.eventsDataGridView = new System.Windows.Forms.DataGridView();
            this.controlPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.eventsComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logoutButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.ageTextBox = new System.Windows.Forms.TextBox();
            this.probe1ComboBox = new System.Windows.Forms.ComboBox();
            this.probe2ComboBox = new System.Windows.Forms.ComboBox();
            this.saveChildButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.childrenDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsDataGridView)).BeginInit();
            this.controlPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 3;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.mainPanel.Controls.Add(this.childrenDataGridView, 2, 0);
            this.mainPanel.Controls.Add(this.eventsDataGridView, 0, 0);
            this.mainPanel.Controls.Add(this.controlPanel, 1, 0);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 1;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Size = new System.Drawing.Size(800, 450);
            this.mainPanel.TabIndex = 0;
            // 
            // childrenDataGridView
            // 
            this.childrenDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.childrenDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childrenDataGridView.Location = new System.Drawing.Point(547, 3);
            this.childrenDataGridView.Name = "childrenDataGridView";
            this.childrenDataGridView.RowTemplate.Height = 24;
            this.childrenDataGridView.Size = new System.Drawing.Size(250, 444);
            this.childrenDataGridView.TabIndex = 0;
            // 
            // eventsDataGridView
            // 
            this.eventsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.eventsDataGridView.Name = "eventsDataGridView";
            this.eventsDataGridView.RowTemplate.Height = 24;
            this.eventsDataGridView.Size = new System.Drawing.Size(250, 444);
            this.eventsDataGridView.TabIndex = 1;
            // 
            // controlPanel
            // 
            this.controlPanel.ColumnCount = 1;
            this.controlPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlPanel.Controls.Add(this.panel1, 0, 0);
            this.controlPanel.Controls.Add(this.panel2, 0, 2);
            this.controlPanel.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(259, 3);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.RowCount = 3;
            this.controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.controlPanel.Size = new System.Drawing.Size(282, 444);
            this.controlPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchButton);
            this.panel1.Controls.Add(this.eventsComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 42);
            this.panel1.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.searchButton.Location = new System.Drawing.Point(184, 9);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(82, 24);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Căutare";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // eventsComboBox
            // 
            this.eventsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.eventsComboBox.FormattingEnabled = true;
            this.eventsComboBox.Location = new System.Drawing.Point(9, 9);
            this.eventsComboBox.Name = "eventsComboBox";
            this.eventsComboBox.Size = new System.Drawing.Size(168, 24);
            this.eventsComboBox.TabIndex = 0;
            this.eventsComboBox.SelectedValueChanged += new System.EventHandler(this.eventsComboBox_SelectedValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.logoutButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 399);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 42);
            this.panel2.TabIndex = 1;
            // 
            // logoutButton
            // 
            this.logoutButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.logoutButton.Location = new System.Drawing.Point(101, 9);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 24);
            this.logoutButton.TabIndex = 0;
            this.logoutButton.Text = "Log out";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ageTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.probe1ComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.probe2ComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.saveChildButton, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 121);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 201);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nume";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Vârsta";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Proba 1";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Proba 2";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(132, 7);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(123, 22);
            this.nameTextBox.TabIndex = 1;
            // 
            // ageTextBox
            // 
            this.ageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ageTextBox.Location = new System.Drawing.Point(132, 43);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.Size = new System.Drawing.Size(123, 22);
            this.ageTextBox.TabIndex = 1;
            // 
            // probe1ComboBox
            // 
            this.probe1ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.probe1ComboBox.FormattingEnabled = true;
            this.probe1ComboBox.Location = new System.Drawing.Point(132, 78);
            this.probe1ComboBox.Name = "probe1ComboBox";
            this.probe1ComboBox.Size = new System.Drawing.Size(123, 24);
            this.probe1ComboBox.TabIndex = 2;
            // 
            // probe2ComboBox
            // 
            this.probe2ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.probe2ComboBox.FormattingEnabled = true;
            this.probe2ComboBox.Location = new System.Drawing.Point(132, 113);
            this.probe2ComboBox.Name = "probe2ComboBox";
            this.probe2ComboBox.Size = new System.Drawing.Size(123, 24);
            this.probe2ComboBox.TabIndex = 2;
            // 
            // saveChildButton
            // 
            this.saveChildButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveChildButton.Location = new System.Drawing.Point(132, 160);
            this.saveChildButton.Name = "saveChildButton";
            this.saveChildButton.Size = new System.Drawing.Size(75, 24);
            this.saveChildButton.TabIndex = 3;
            this.saveChildButton.Text = "Înscriere";
            this.saveChildButton.UseVisualStyleBackColor = true;
            this.saveChildButton.Click += new System.EventHandler(this.saveChildButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainPanel);
            this.Name = "MainForm";
            this.Text = "Atletism";
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.childrenDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventsDataGridView)).EndInit();
            this.controlPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.DataGridView childrenDataGridView;
        private System.Windows.Forms.DataGridView eventsDataGridView;
        private System.Windows.Forms.TableLayoutPanel controlPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox eventsComboBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox ageTextBox;
        private System.Windows.Forms.ComboBox probe1ComboBox;
        private System.Windows.Forms.ComboBox probe2ComboBox;
        private System.Windows.Forms.Button saveChildButton;
    }
}

