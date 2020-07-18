namespace KNDLComic
{
    partial class frmBrowser
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
            this.brsMain = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // brsMain
            // 
            this.brsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brsMain.Location = new System.Drawing.Point(0, 0);
            this.brsMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.brsMain.Name = "brsMain";
            this.brsMain.Size = new System.Drawing.Size(607, 435);
            this.brsMain.TabIndex = 0;
            this.brsMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.brsMain_DocumentCompleted);
            this.brsMain.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.brsMain_ProgressChanged);
            // 
            // frmBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 435);
            this.Controls.Add(this.brsMain);
            this.Name = "frmBrowser";
            this.Text = "frmBrowser";
            this.Load += new System.EventHandler(this.frmBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser brsMain;
    }
}