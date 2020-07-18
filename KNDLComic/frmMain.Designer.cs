namespace KNDLComic
{
    partial class frmMain
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
            this.lvwDownloadLinks = new System.Windows.Forms.ListView();
            this.colLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewLink = new System.Windows.Forms.TextBox();
            this.cmdAddLink = new System.Windows.Forms.Button();
            this.cmdSend = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.chkOneDoc = new System.Windows.Forms.CheckBox();
            this.txtDocName = new System.Windows.Forms.TextBox();
            this.lvwFeeds = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdAddFeed = new System.Windows.Forms.Button();
            this.cmdChangeState = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.lswReplaces = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdAddReplace = new System.Windows.Forms.Button();
            this.cmdDelReplace = new System.Windows.Forms.Button();
            this.txtReplaces = new System.Windows.Forms.TextBox();
            this.cmbSend = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lvwDownloadLinks
            // 
            this.lvwDownloadLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDownloadLinks.CheckBoxes = true;
            this.lvwDownloadLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLink,
            this.colStatus});
            this.lvwDownloadLinks.FullRowSelect = true;
            this.lvwDownloadLinks.HideSelection = false;
            this.lvwDownloadLinks.Location = new System.Drawing.Point(12, 41);
            this.lvwDownloadLinks.Name = "lvwDownloadLinks";
            this.lvwDownloadLinks.Size = new System.Drawing.Size(548, 345);
            this.lvwDownloadLinks.TabIndex = 0;
            this.lvwDownloadLinks.UseCompatibleStateImageBehavior = false;
            this.lvwDownloadLinks.View = System.Windows.Forms.View.Details;
            // 
            // colLink
            // 
            this.colLink.Text = "Link";
            this.colLink.Width = 444;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 89;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link:";
            // 
            // txtNewLink
            // 
            this.txtNewLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewLink.Location = new System.Drawing.Point(48, 12);
            this.txtNewLink.Name = "txtNewLink";
            this.txtNewLink.Size = new System.Drawing.Size(630, 20);
            this.txtNewLink.TabIndex = 2;
            // 
            // cmdAddLink
            // 
            this.cmdAddLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddLink.Location = new System.Drawing.Point(684, 10);
            this.cmdAddLink.Name = "cmdAddLink";
            this.cmdAddLink.Size = new System.Drawing.Size(75, 23);
            this.cmdAddLink.TabIndex = 3;
            this.cmdAddLink.Text = "Add Link";
            this.cmdAddLink.UseVisualStyleBackColor = true;
            this.cmdAddLink.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdSend
            // 
            this.cmdSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSend.Location = new System.Drawing.Point(12, 421);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(41, 23);
            this.cmdSend.TabIndex = 5;
            this.cmdSend.Text = "Send Selected to:";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(418, 423);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(422, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // chkOneDoc
            // 
            this.chkOneDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOneDoc.AutoSize = true;
            this.chkOneDoc.Location = new System.Drawing.Point(131, 425);
            this.chkOneDoc.Name = "chkOneDoc";
            this.chkOneDoc.Size = new System.Drawing.Size(84, 17);
            this.chkOneDoc.TabIndex = 6;
            this.chkOneDoc.Text = "As One Doc";
            this.chkOneDoc.UseVisualStyleBackColor = true;
            this.chkOneDoc.CheckedChanged += new System.EventHandler(this.chkOneDoc_CheckedChanged);
            // 
            // txtDocName
            // 
            this.txtDocName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDocName.Enabled = false;
            this.txtDocName.Location = new System.Drawing.Point(221, 423);
            this.txtDocName.Name = "txtDocName";
            this.txtDocName.Size = new System.Drawing.Size(191, 20);
            this.txtDocName.TabIndex = 7;
            // 
            // lvwFeeds
            // 
            this.lvwFeeds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwFeeds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwFeeds.FullRowSelect = true;
            this.lvwFeeds.HideSelection = false;
            this.lvwFeeds.Location = new System.Drawing.Point(566, 57);
            this.lvwFeeds.Name = "lvwFeeds";
            this.lvwFeeds.Size = new System.Drawing.Size(274, 165);
            this.lvwFeeds.TabIndex = 8;
            this.lvwFeeds.UseCompatibleStateImageBehavior = false;
            this.lvwFeeds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Link";
            this.columnHeader1.Width = 508;
            // 
            // cmdAddFeed
            // 
            this.cmdAddFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddFeed.Location = new System.Drawing.Point(765, 10);
            this.cmdAddFeed.Name = "cmdAddFeed";
            this.cmdAddFeed.Size = new System.Drawing.Size(75, 23);
            this.cmdAddFeed.TabIndex = 9;
            this.cmdAddFeed.Text = "Add Feed";
            this.cmdAddFeed.UseVisualStyleBackColor = true;
            this.cmdAddFeed.Click += new System.EventHandler(this.cmdAddFeed_Click);
            // 
            // cmdChangeState
            // 
            this.cmdChangeState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdChangeState.Location = new System.Drawing.Point(15, 392);
            this.cmdChangeState.Name = "cmdChangeState";
            this.cmdChangeState.Size = new System.Drawing.Size(110, 23);
            this.cmdChangeState.TabIndex = 10;
            this.cmdChangeState.Text = "Toggle State";
            this.cmdChangeState.UseVisualStyleBackColor = true;
            this.cmdChangeState.Click += new System.EventHandler(this.cmdChangeState_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(747, 228);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(94, 23);
            this.cmdRefresh.TabIndex = 11;
            this.cmdRefresh.Text = "Refresh Feeds";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lswReplaces
            // 
            this.lswReplaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lswReplaces.CheckBoxes = true;
            this.lswReplaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lswReplaces.FullRowSelect = true;
            this.lswReplaces.HideSelection = false;
            this.lswReplaces.Location = new System.Drawing.Point(566, 257);
            this.lswReplaces.Name = "lswReplaces";
            this.lswReplaces.Size = new System.Drawing.Size(274, 129);
            this.lswReplaces.TabIndex = 12;
            this.lswReplaces.UseCompatibleStateImageBehavior = false;
            this.lswReplaces.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "This Value";
            this.columnHeader2.Width = 114;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "With This";
            this.columnHeader3.Width = 118;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Replace Strings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "RSS Feeds";
            // 
            // cmdAddReplace
            // 
            this.cmdAddReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddReplace.Location = new System.Drawing.Point(701, 392);
            this.cmdAddReplace.Name = "cmdAddReplace";
            this.cmdAddReplace.Size = new System.Drawing.Size(41, 23);
            this.cmdAddReplace.TabIndex = 15;
            this.cmdAddReplace.Text = "Add";
            this.cmdAddReplace.UseVisualStyleBackColor = true;
            this.cmdAddReplace.Click += new System.EventHandler(this.cmdAddReplace_Click);
            // 
            // cmdDelReplace
            // 
            this.cmdDelReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelReplace.Location = new System.Drawing.Point(748, 392);
            this.cmdDelReplace.Name = "cmdDelReplace";
            this.cmdDelReplace.Size = new System.Drawing.Size(93, 23);
            this.cmdDelReplace.TabIndex = 16;
            this.cmdDelReplace.Text = "Delete Replace";
            this.cmdDelReplace.UseVisualStyleBackColor = true;
            this.cmdDelReplace.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtReplaces
            // 
            this.txtReplaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplaces.Location = new System.Drawing.Point(567, 394);
            this.txtReplaces.Name = "txtReplaces";
            this.txtReplaces.Size = new System.Drawing.Size(128, 20);
            this.txtReplaces.TabIndex = 17;
            // 
            // cmbSend
            // 
            this.cmbSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSend.FormattingEnabled = true;
            this.cmbSend.Items.AddRange(new object[] {
            "Checked",
            "Selected",
            "New"});
            this.cmbSend.Location = new System.Drawing.Point(60, 423);
            this.cmbSend.Name = "cmbSend";
            this.cmbSend.Size = new System.Drawing.Size(65, 21);
            this.cmbSend.TabIndex = 18;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 456);
            this.Controls.Add(this.cmbSend);
            this.Controls.Add(this.txtReplaces);
            this.Controls.Add(this.cmdDelReplace);
            this.Controls.Add(this.cmdAddReplace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lswReplaces);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdChangeState);
            this.Controls.Add(this.cmdAddFeed);
            this.Controls.Add(this.lvwFeeds);
            this.Controls.Add(this.txtDocName);
            this.Controls.Add(this.chkOneDoc);
            this.Controls.Add(this.cmdSend);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.cmdAddLink);
            this.Controls.Add(this.txtNewLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvwDownloadLinks);
            this.Name = "frmMain";
            this.Text = "KNDLComic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwDownloadLinks;
        private System.Windows.Forms.ColumnHeader colLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewLink;
        private System.Windows.Forms.Button cmdAddLink;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.CheckBox chkOneDoc;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.TextBox txtDocName;
        private System.Windows.Forms.ListView lvwFeeds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button cmdAddFeed;
        private System.Windows.Forms.Button cmdChangeState;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.ListView lswReplaces;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdAddReplace;
        private System.Windows.Forms.Button cmdDelReplace;
        private System.Windows.Forms.TextBox txtReplaces;
        private System.Windows.Forms.ComboBox cmbSend;
    }
}

