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
            this.txtMaxHexagons = new System.Windows.Forms.TextBox();
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
            this.pathTb.TextChanged += new System.EventHandler(this.pathTb_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(467, 14);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(269, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // txtMaxHexagons
            // 
            this.txtMaxHexagons.Location = new System.Drawing.Point(591, 55);
            this.txtMaxHexagons.Name = "txtMaxHexagons";
            this.txtMaxHexagons.Size = new System.Drawing.Size(145, 20);
            this.txtMaxHexagons.TabIndex = 8;
            this.txtMaxHexagons.TextChanged += new System.EventHandler(this.txtMaxHexagons_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 466);
            this.Controls.Add(this.txtMaxHexagons);
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
        private System.Windows.Forms.TextBox txtMaxHexagons;
    }
}

