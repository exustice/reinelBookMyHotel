namespace BookMyCourt
{
    partial class ForgotPasswordForm
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
            this.txtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.secretAnswerTextBox = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.resetPasswordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(138, 128);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(193, 20);
            this.txtBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Email";
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(195, 257);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(75, 23);
            this.submitBtn.TabIndex = 2;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click_1);
            // 
            // secretAnswerTextBox
            // 
            this.secretAnswerTextBox.Location = new System.Drawing.Point(138, 173);
            this.secretAnswerTextBox.Name = "secretAnswerTextBox";
            this.secretAnswerTextBox.Size = new System.Drawing.Size(193, 20);
            this.secretAnswerTextBox.TabIndex = 3;
            this.secretAnswerTextBox.Visible = false;
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.Location = new System.Drawing.Point(135, 64);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(106, 13);
            this.newPasswordLabel.TabIndex = 4;
            this.newPasswordLabel.Text = "Enter New Password";
            this.newPasswordLabel.Visible = false;
            // 
            // confirmPasswordLabel
            // 
            this.confirmPasswordLabel.AutoSize = true;
            this.confirmPasswordLabel.Location = new System.Drawing.Point(135, 128);
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(116, 13);
            this.confirmPasswordLabel.TabIndex = 5;
            this.confirmPasswordLabel.Text = "Confirm New Password";
            this.confirmPasswordLabel.Visible = false;
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.Location = new System.Drawing.Point(138, 80);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.PasswordChar = '*';
            this.newPasswordTextBox.Size = new System.Drawing.Size(193, 20);
            this.newPasswordTextBox.TabIndex = 6;
            this.newPasswordTextBox.Visible = false;
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(138, 147);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.PasswordChar = '*';
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(193, 20);
            this.confirmPasswordTextBox.TabIndex = 7;
            this.confirmPasswordTextBox.Visible = false;
            // 
            // resetPasswordButton
            // 
            this.resetPasswordButton.Location = new System.Drawing.Point(195, 199);
            this.resetPasswordButton.Name = "resetPasswordButton";
            this.resetPasswordButton.Size = new System.Drawing.Size(75, 23);
            this.resetPasswordButton.TabIndex = 8;
            this.resetPasswordButton.Text = "Reset";
            this.resetPasswordButton.UseVisualStyleBackColor = true;
            this.resetPasswordButton.Visible = false;
            this.resetPasswordButton.Click += new System.EventHandler(this.resetPasswordButton_Click);
            // 
            // ForgotPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 307);
            this.Controls.Add(this.resetPasswordButton);
            this.Controls.Add(this.confirmPasswordTextBox);
            this.Controls.Add(this.newPasswordTextBox);
            this.Controls.Add(this.confirmPasswordLabel);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.secretAnswerTextBox);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBox);
            this.Name = "ForgotPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForgotPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.TextBox secretAnswerTextBox;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.Label confirmPasswordLabel;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Button resetPasswordButton;
    }
}