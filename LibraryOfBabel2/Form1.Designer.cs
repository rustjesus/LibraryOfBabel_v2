namespace LibraryOfBabel2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flpResults = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoadPage = new System.Windows.Forms.Button();
            this.pathTb = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtStartHex = new System.Windows.Forms.TextBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.loadingPercentLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndHex = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(93, 14);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(368, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // flpResults
            // 
            this.flpResults.Location = new System.Drawing.Point(12, 55);
            this.flpResults.Name = "flpResults";
            this.flpResults.Size = new System.Drawing.Size(449, 367);
            this.flpResults.TabIndex = 4;
            // 
            // btnLoadPage
            // 
            this.btnLoadPage.Location = new System.Drawing.Point(467, 425);
            this.btnLoadPage.Name = "btnLoadPage";
            this.btnLoadPage.Size = new System.Drawing.Size(75, 23);
            this.btnLoadPage.TabIndex = 5;
            this.btnLoadPage.Text = "Load Page";
            this.btnLoadPage.UseVisualStyleBackColor = true;
            this.btnLoadPage.Click += new System.EventHandler(this.btnLoadPage_Click);
            // 
            // pathTb
            // 
            this.pathTb.Location = new System.Drawing.Point(12, 428);
            this.pathTb.Name = "pathTb";
            this.pathTb.Size = new System.Drawing.Size(449, 20);
            this.pathTb.TabIndex = 6;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(467, 14);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(321, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // txtStartHex
            // 
            this.txtStartHex.Location = new System.Drawing.Point(598, 55);
            this.txtStartHex.Name = "txtStartHex";
            this.txtStartHex.Size = new System.Drawing.Size(190, 20);
            this.txtStartHex.TabIndex = 8;
            this.txtStartHex.TextChanged += new System.EventHandler(this.txtStartHex_TextChanged_1);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(693, 140);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(95, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel Search";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Start Hexagons (Rooms):";
            // 
            // loadingPercentLabel
            // 
            this.loadingPercentLabel.AutoSize = true;
            this.loadingPercentLabel.Location = new System.Drawing.Point(467, 40);
            this.loadingPercentLabel.Name = "loadingPercentLabel";
            this.loadingPercentLabel.Size = new System.Drawing.Size(33, 13);
            this.loadingPercentLabel.TabIndex = 11;
            this.loadingPercentLabel.Text = "100%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Example: \"test\" is found in hexagon 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(492, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "but \"testing\" isn\'t found until 621+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(464, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "End Hexagons (Rooms):";
            // 
            // txtEndHex
            // 
            this.txtEndHex.Location = new System.Drawing.Point(598, 91);
            this.txtEndHex.Name = "txtEndHex";
            this.txtEndHex.Size = new System.Drawing.Size(190, 20);
            this.txtEndHex.TabIndex = 15;
            this.txtEndHex.TextChanged += new System.EventHandler(this.txtEndHex_TextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(548, 426);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 16;
            this.saveButton.Text = "Save Page";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 466);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.txtEndHex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadingPercentLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.txtStartHex);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pathTb);
            this.Controls.Add(this.btnLoadPage);
            this.Controls.Add(this.flpResults);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.FlowLayoutPanel flpResults;
        private System.Windows.Forms.Button btnLoadPage;
        private System.Windows.Forms.TextBox pathTb;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtStartHex;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label loadingPercentLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndHex;
        private System.Windows.Forms.Button saveButton;
    }
}

