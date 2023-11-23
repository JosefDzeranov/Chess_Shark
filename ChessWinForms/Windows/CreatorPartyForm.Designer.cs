namespace Sachy_Obrazky.Windows
{
    partial class CreatorPartyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatorPartyForm));
            this.namePartyTextBox = new System.Windows.Forms.TextBox();
            this.stepPartyTextBox = new System.Windows.Forms.TextBox();
            this.namePartyLabel = new System.Windows.Forms.Label();
            this.stepPartyLabel = new System.Windows.Forms.Label();
            this.savePartyButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.errorListLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // namePartyTextBox
            // 
            this.namePartyTextBox.Location = new System.Drawing.Point(28, 36);
            this.namePartyTextBox.Name = "namePartyTextBox";
            this.namePartyTextBox.Size = new System.Drawing.Size(718, 20);
            this.namePartyTextBox.TabIndex = 0;
            // 
            // stepPartyTextBox
            // 
            this.stepPartyTextBox.Location = new System.Drawing.Point(28, 88);
            this.stepPartyTextBox.Multiline = true;
            this.stepPartyTextBox.Name = "stepPartyTextBox";
            this.stepPartyTextBox.Size = new System.Drawing.Size(718, 180);
            this.stepPartyTextBox.TabIndex = 0;
            // 
            // namePartyLabel
            // 
            this.namePartyLabel.AutoSize = true;
            this.namePartyLabel.Location = new System.Drawing.Point(28, 13);
            this.namePartyLabel.Name = "namePartyLabel";
            this.namePartyLabel.Size = new System.Drawing.Size(83, 13);
            this.namePartyLabel.TabIndex = 1;
            this.namePartyLabel.Text = "namePartyLabel";
            // 
            // stepPartyLabel
            // 
            this.stepPartyLabel.AutoSize = true;
            this.stepPartyLabel.Location = new System.Drawing.Point(28, 72);
            this.stepPartyLabel.Name = "stepPartyLabel";
            this.stepPartyLabel.Size = new System.Drawing.Size(77, 13);
            this.stepPartyLabel.TabIndex = 1;
            this.stepPartyLabel.Text = "stepPartyLabel";
            // 
            // savePartyButton
            // 
            this.savePartyButton.Depth = 0;
            this.savePartyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.savePartyButton.Location = new System.Drawing.Point(537, 316);
            this.savePartyButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.savePartyButton.Name = "savePartyButton";
            this.savePartyButton.Primary = true;
            this.savePartyButton.Size = new System.Drawing.Size(193, 37);
            this.savePartyButton.TabIndex = 2;
            this.savePartyButton.Text = "Сохранить";
            this.savePartyButton.UseVisualStyleBackColor = true;
            this.savePartyButton.Click += new System.EventHandler(this.savePartyButton_Click);
            // 
            // errorListLabel
            // 
            this.errorListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorListLabel.ForeColor = System.Drawing.Color.Red;
            this.errorListLabel.Location = new System.Drawing.Point(28, 316);
            this.errorListLabel.Name = "errorListLabel";
            this.errorListLabel.Size = new System.Drawing.Size(487, 105);
            this.errorListLabel.TabIndex = 3;
            this.errorListLabel.Text = "errorListLabel";
            // 
            // CreatorPartyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.errorListLabel);
            this.Controls.Add(this.savePartyButton);
            this.Controls.Add(this.stepPartyLabel);
            this.Controls.Add(this.namePartyLabel);
            this.Controls.Add(this.stepPartyTextBox);
            this.Controls.Add(this.namePartyTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreatorPartyForm";
            this.Text = "Добавить новую партию";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox namePartyTextBox;
        private System.Windows.Forms.TextBox stepPartyTextBox;
        private System.Windows.Forms.Label namePartyLabel;
        private System.Windows.Forms.Label stepPartyLabel;
        private MaterialSkin.Controls.MaterialRaisedButton savePartyButton;
        private System.Windows.Forms.Label errorListLabel;
    }
}