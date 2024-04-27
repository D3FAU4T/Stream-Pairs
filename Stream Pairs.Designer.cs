namespace Stream_Pairs
{
    partial class StreamPairs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamPairs));
            this.label1 = new System.Windows.Forms.Label();
            this.MirrorLinkTextBox = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ChannelName = new System.Windows.Forms.Label();
            this.TwitchChannelBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LevelNumber = new System.Windows.Forms.Label();
            this.MatchesBox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DeckName = new System.Windows.Forms.Label();
            this.GoalLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.RecordLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mirror Link:";
            // 
            // MirrorLinkTextBox
            // 
            this.MirrorLinkTextBox.Location = new System.Drawing.Point(75, 14);
            this.MirrorLinkTextBox.Name = "MirrorLinkTextBox";
            this.MirrorLinkTextBox.Size = new System.Drawing.Size(226, 20);
            this.MirrorLinkTextBox.TabIndex = 1;
            this.MirrorLinkTextBox.TextChanged += new System.EventHandler(this.MirrorLinkTextBox_TextChanged);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Enabled = false;
            this.ConnectBtn.Location = new System.Drawing.Point(307, 12);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 2;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Channel    : ";
            // 
            // ChannelName
            // 
            this.ChannelName.AutoSize = true;
            this.ChannelName.Location = new System.Drawing.Point(83, 49);
            this.ChannelName.Name = "ChannelName";
            this.ChannelName.Size = new System.Drawing.Size(33, 13);
            this.ChannelName.TabIndex = 4;
            this.ChannelName.Text = "None";
            // 
            // TwitchChannelBtn
            // 
            this.TwitchChannelBtn.Enabled = false;
            this.TwitchChannelBtn.Location = new System.Drawing.Point(267, 46);
            this.TwitchChannelBtn.Name = "TwitchChannelBtn";
            this.TwitchChannelBtn.Size = new System.Drawing.Size(115, 23);
            this.TwitchChannelBtn.TabIndex = 5;
            this.TwitchChannelBtn.Text = "Goto Twitch channel";
            this.TwitchChannelBtn.UseVisualStyleBackColor = true;
            this.TwitchChannelBtn.Click += new System.EventHandler(this.TwitchChannelBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Level        :";
            // 
            // LevelNumber
            // 
            this.LevelNumber.AutoSize = true;
            this.LevelNumber.Location = new System.Drawing.Point(83, 74);
            this.LevelNumber.Name = "LevelNumber";
            this.LevelNumber.Size = new System.Drawing.Size(53, 13);
            this.LevelNumber.TabIndex = 7;
            this.LevelNumber.Text = "Unknown";
            // 
            // MatchesBox
            // 
            this.MatchesBox.Enabled = false;
            this.MatchesBox.Location = new System.Drawing.Point(21, 192);
            this.MatchesBox.Name = "MatchesBox";
            this.MatchesBox.Size = new System.Drawing.Size(361, 228);
            this.MatchesBox.TabIndex = 8;
            this.MatchesBox.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Matches:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Deck        :";
            // 
            // DeckName
            // 
            this.DeckName.AutoSize = true;
            this.DeckName.Location = new System.Drawing.Point(83, 98);
            this.DeckName.Name = "DeckName";
            this.DeckName.Size = new System.Drawing.Size(53, 13);
            this.DeckName.TabIndex = 11;
            this.DeckName.Text = "Unknown";
            // 
            // GoalLabel
            // 
            this.GoalLabel.AutoSize = true;
            this.GoalLabel.Location = new System.Drawing.Point(83, 121);
            this.GoalLabel.Name = "GoalLabel";
            this.GoalLabel.Size = new System.Drawing.Size(53, 13);
            this.GoalLabel.TabIndex = 13;
            this.GoalLabel.Text = "Unknown";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Goal         :";
            // 
            // RecordLabel
            // 
            this.RecordLabel.AutoSize = true;
            this.RecordLabel.Location = new System.Drawing.Point(82, 144);
            this.RecordLabel.Name = "RecordLabel";
            this.RecordLabel.Size = new System.Drawing.Size(53, 13);
            this.RecordLabel.TabIndex = 15;
            this.RecordLabel.Text = "Unknown";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Record     :";
            // 
            // StreamPairs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 444);
            this.Controls.Add(this.RecordLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.GoalLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DeckName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MatchesBox);
            this.Controls.Add(this.LevelNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TwitchChannelBtn);
            this.Controls.Add(this.ChannelName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.MirrorLinkTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StreamPairs";
            this.Text = "Stream Pairs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MirrorLinkTextBox;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ChannelName;
        private System.Windows.Forms.Button TwitchChannelBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LevelNumber;
        private System.Windows.Forms.RichTextBox MatchesBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DeckName;
        private System.Windows.Forms.Label GoalLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label RecordLabel;
        private System.Windows.Forms.Label label9;
    }
}

