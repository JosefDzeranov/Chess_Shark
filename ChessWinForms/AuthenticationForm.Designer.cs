namespace Sachy_Obrazky
{
    partial class AuthenticationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthenticationForm));
            this.FirstNameMessage = new System.Windows.Forms.Label();
            this.LastNameMessage = new System.Windows.Forms.Label();
            this.textBox1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.textBox2 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.button1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // FirstNameMessage
            // 
            this.FirstNameMessage.AutoSize = true;
            this.FirstNameMessage.Location = new System.Drawing.Point(211, 18);
            this.FirstNameMessage.Name = "FirstNameMessage";
            this.FirstNameMessage.Size = new System.Drawing.Size(0, 13);
            this.FirstNameMessage.TabIndex = 5;
            this.FirstNameMessage.Click += new System.EventHandler(this.label3_Click);
            // 
            // LastNameMessage
            // 
            this.LastNameMessage.AutoSize = true;
            this.LastNameMessage.Location = new System.Drawing.Point(211, 57);
            this.LastNameMessage.Name = "LastNameMessage";
            this.LastNameMessage.Size = new System.Drawing.Size(0, 13);
            this.LastNameMessage.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Depth = 0;
            this.textBox1.Hint = "";
            this.textBox1.Location = new System.Drawing.Point(131, 98);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.Size = new System.Drawing.Size(147, 23);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Имя";
            this.textBox1.UseSystemPasswordChar = false;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Depth = 0;
            this.textBox2.Hint = "";
            this.textBox2.Location = new System.Drawing.Point(131, 139);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '\0';
            this.textBox2.SelectedText = "";
            this.textBox2.SelectionLength = 0;
            this.textBox2.SelectionStart = 0;
            this.textBox2.Size = new System.Drawing.Size(147, 23);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "Фамилия";
            this.textBox2.UseSystemPasswordChar = false;
            this.textBox2.Click += new System.EventHandler(this.textBox2_Click);
            // 
            // button1
            // 
            this.button1.Depth = 0;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Location = new System.Drawing.Point(63, 181);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.MouseState = MaterialSkin.MouseState.HOVER;
            this.button1.Name = "button1";
            this.button1.Primary = true;
            this.button1.Size = new System.Drawing.Size(215, 19);
            this.button1.TabIndex = 9;
            this.button1.Text = "Вход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Sachy_Obrazky.Properties.Resources.chess_pawn_icon_137768;
            this.pictureBox1.Location = new System.Drawing.Point(64, 98);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // AuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 249);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LastNameMessage);
            this.Controls.Add(this.FirstNameMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthenticationForm";
            this.Text = "0";
            this.Load += new System.EventHandler(this.AuthenticationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label FirstNameMessage;
        public System.Windows.Forms.Label LastNameMessage;
        private MaterialSkin.Controls.MaterialSingleLineTextField textBox1;
        private MaterialSkin.Controls.MaterialSingleLineTextField textBox2;
        private MaterialSkin.Controls.MaterialRaisedButton button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}