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
            this.inputBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.secretQuestionLabel = new System.Windows.Forms.Label();
            this.confirmPasswordBox = new System.Windows.Forms.TextBox();
            this.newPasswordBox = new System.Windows.Forms.TextBox();
            this.resetPasswordButton = new System.Windows.Forms.Button();
            this.newPasswordlbl = new System.Windows.Forms.Label();
            this.confirmPasswordlbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(140, 101);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(183, 20);
            this.inputBox.TabIndex = 0;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(196, 65);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(60, 13);
            this.emailLabel.TabIndex = 1;
            this.emailLabel.Text = "Enter Email";
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(192, 162);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(75, 23);
            this.submitBtn.TabIndex = 2;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // secretQuestionLabel
            // 
            this.secretQuestionLabel.AutoSize = true;
            this.secretQuestionLabel.Location = new System.Drawing.Point(137, 78);
            this.secretQuestionLabel.Name = "secretQuestionLabel";
            this.secretQuestionLabel.Size = new System.Drawing.Size(83, 13);
            this.secretQuestionLabel.TabIndex = 3;
            this.secretQuestionLabel.Text = "Secret Question";
            this.secretQuestionLabel.Visible = false;
            // 
            // confirmPasswordBox
            // 
            this.confirmPasswordBox.Location = new System.Drawing.Point(140, 191);
            this.confirmPasswordBox.Name = "confirmPasswordBox";
            this.confirmPasswordBox.Size = new System.Drawing.Size(183, 20);
            this.confirmPasswordBox.TabIndex = 4;
            this.confirmPasswordBox.Visible = false;
            // 
            // newPasswordBox
            // 
            this.newPasswordBox.Location = new System.Drawing.Point(140, 146);
            this.newPasswordBox.Name = "newPasswordBox";
            this.newPasswordBox.Size = new System.Drawing.Size(183, 20);
            this.newPasswordBox.TabIndex = 5;
            this.newPasswordBox.Visible = false;
            // 
            // resetPasswordButton
            // 
            this.resetPasswordButton.Location = new System.Drawing.Point(192, 247);
            this.resetPasswordButton.Name = "resetPasswordButton";
            this.resetPasswordButton.Size = new System.Drawing.Size(75, 23);
            this.resetPasswordButton.TabIndex = 6;
            this.resetPasswordButton.Text = "Reset";
            this.resetPasswordButton.UseVisualStyleBackColor = true;
            this.resetPasswordButton.Visible = false;
            // 
            // newPasswordlbl
            // 
            this.newPasswordlbl.AutoSize = true;
            this.newPasswordlbl.Location = new System.Drawing.Point(174, 130);
            this.newPasswordlbl.Name = "newPasswordlbl";
            this.newPasswordlbl.Size = new System.Drawing.Size(106, 13);
            this.newPasswordlbl.TabIndex = 7;
            this.newPasswordlbl.Text = "Enter New Password";
            this.newPasswordlbl.Visible = false;
            // 
            // confirmPasswordlbl
            // 
            this.confirmPasswordlbl.AutoSize = true;
            this.confirmPasswordlbl.Location = new System.Drawing.Point(174, 175);
            this.confirmPasswordlbl.Name = "confirmPasswordlbl";
            this.confirmPasswordlbl.Size = new System.Drawing.Size(116, 13);
            this.confirmPasswordlbl.TabIndex = 8;
            this.confirmPasswordlbl.Text = "Confirm New Password";
            this.confirmPasswordlbl.Visible = false;
            // 
            // ForgotPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 307);
            this.Controls.Add(this.confirmPasswordlbl);
            this.Controls.Add(this.newPasswordlbl);
            this.Controls.Add(this.resetPasswordButton);
            this.Controls.Add(this.newPasswordBox);
            this.Controls.Add(this.confirmPasswordBox);
            this.Controls.Add(this.secretQuestionLabel);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.inputBox);
            this.Name = "ForgotPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForgotPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Label secretQuestionLabel;
        private System.Windows.Forms.TextBox confirmPasswordBox;
        private System.Windows.Forms.TextBox newPasswordBox;
        private System.Windows.Forms.Button resetPasswordButton;
        private System.Windows.Forms.Label newPasswordlbl;
        private System.Windows.Forms.Label confirmPasswordlbl;
    }
}