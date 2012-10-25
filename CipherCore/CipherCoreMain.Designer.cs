namespace CipherCore
{
    partial class CipherCoreMain
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
            this.keywordBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saltedCheckBox = new System.Windows.Forms.CheckBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.encryptButton = new System.Windows.Forms.Button();
            this.decryptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // keywordBox
            // 
            this.keywordBox.Location = new System.Drawing.Point(81, 16);
            this.keywordBox.Name = "keywordBox";
            this.keywordBox.Size = new System.Drawing.Size(92, 20);
            this.keywordBox.TabIndex = 0;
            this.keywordBox.Tag = "keyword";
            this.keywordBox.Text = "CipherCore";
            this.keywordBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Tag = "Keyword";
            this.label1.Text = "Keyword:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // saltedCheckBox
            // 
            this.saltedCheckBox.AutoSize = true;
            this.saltedCheckBox.Location = new System.Drawing.Point(189, 10);
            this.saltedCheckBox.Name = "saltedCheckBox";
            this.saltedCheckBox.Size = new System.Drawing.Size(56, 17);
            this.saltedCheckBox.TabIndex = 2;
            this.saltedCheckBox.Tag = "salted";
            this.saltedCheckBox.Text = "Salted";
            this.saltedCheckBox.UseVisualStyleBackColor = true;
            this.saltedCheckBox.CheckedChanged += new System.EventHandler(this.saltedCheckBox_CheckedChanged);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(30, 53);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(215, 110);
            this.messageBox.TabIndex = 3;
            this.messageBox.Tag = "text";
            this.messageBox.Text = "<INSERT TEXT>";
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(38, 180);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(85, 30);
            this.encryptButton.TabIndex = 4;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(152, 180);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(85, 30);
            this.decryptButton.TabIndex = 5;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // CipherCoreMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 220);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.saltedCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keywordBox);
            this.Name = "CipherCoreMain";
            this.Text = "CipherCore";
            this.Load += new System.EventHandler(this.CipherCoreMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keywordBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox saltedCheckBox;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button decryptButton;
    }
}