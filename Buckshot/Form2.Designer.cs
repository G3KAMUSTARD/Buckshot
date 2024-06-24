namespace Buckshot
{
    partial class Form2
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
            this.nick = new System.Windows.Forms.TextBox();
            this.pswrd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Log_in = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.Pasword_Show = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // nick
            // 
            this.nick.Location = new System.Drawing.Point(142, 85);
            this.nick.Name = "nick";
            this.nick.Size = new System.Drawing.Size(136, 20);
            this.nick.TabIndex = 0;
            this.nick.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nick_KeyPress);
            // 
            // pswrd
            // 
            this.pswrd.Location = new System.Drawing.Point(142, 111);
            this.pswrd.Name = "pswrd";
            this.pswrd.Size = new System.Drawing.Size(136, 20);
            this.pswrd.TabIndex = 1;
            this.pswrd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pswrd_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(121, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Вход в программу.";
            // 
            // Log_in
            // 
            this.Log_in.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Log_in.Location = new System.Drawing.Point(209, 137);
            this.Log_in.Name = "Log_in";
            this.Log_in.Size = new System.Drawing.Size(69, 21);
            this.Log_in.TabIndex = 3;
            this.Log_in.Text = "Вход";
            this.Log_in.UseVisualStyleBackColor = false;
            this.Log_in.Click += new System.EventHandler(this.Log_in_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Логин:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пароль:";
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Salmon;
            this.exit.Location = new System.Drawing.Point(94, 137);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(71, 21);
            this.exit.TabIndex = 6;
            this.exit.Text = "Выход";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // Pasword_Show
            // 
            this.Pasword_Show.AutoSize = true;
            this.Pasword_Show.Checked = true;
            this.Pasword_Show.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Pasword_Show.Location = new System.Drawing.Point(284, 114);
            this.Pasword_Show.Name = "Pasword_Show";
            this.Pasword_Show.Size = new System.Drawing.Size(64, 17);
            this.Pasword_Show.TabIndex = 7;
            this.Pasword_Show.Text = "Скрыть";
            this.Pasword_Show.UseVisualStyleBackColor = true;
            this.Pasword_Show.CheckedChanged += new System.EventHandler(this.Show_Password_CheckedChanged);
            this.Pasword_Show.Click += new System.EventHandler(this.Show_Password_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(379, 231);
            this.Controls.Add(this.Pasword_Show);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Log_in);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pswrd);
            this.Controls.Add(this.nick);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nick;
        private System.Windows.Forms.TextBox pswrd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Log_in;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.CheckBox Pasword_Show;
    }
}