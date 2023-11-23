namespace Sachy_Obrazky.Windows
{
    partial class GraphForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphForm));
            this.zedGraphStatisticControl = new ZedGraph.ZedGraphControl();
            this.namelabel = new System.Windows.Forms.Label();
            this.familyLabel = new System.Windows.Forms.Label();
            this.countAttempts_Label = new System.Windows.Forms.Label();
            this.averageAccuracy_Label = new System.Windows.Forms.Label();
            this.bestAccuracy_Label = new System.Windows.Forms.Label();
            this.worstAccuracy_Label = new System.Windows.Forms.Label();
            this.overallAverageAccuracy_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // zedGraphStatisticControl
            // 
            this.zedGraphStatisticControl.Location = new System.Drawing.Point(12, 80);
            this.zedGraphStatisticControl.Name = "zedGraphStatisticControl";
            this.zedGraphStatisticControl.ScrollGrace = 0D;
            this.zedGraphStatisticControl.ScrollMaxX = 0D;
            this.zedGraphStatisticControl.ScrollMaxY = 0D;
            this.zedGraphStatisticControl.ScrollMaxY2 = 0D;
            this.zedGraphStatisticControl.ScrollMinX = 0D;
            this.zedGraphStatisticControl.ScrollMinY = 0D;
            this.zedGraphStatisticControl.ScrollMinY2 = 0D;
            this.zedGraphStatisticControl.Size = new System.Drawing.Size(843, 452);
            this.zedGraphStatisticControl.TabIndex = 0;
            this.zedGraphStatisticControl.UseExtendedPrintDialog = true;
            this.zedGraphStatisticControl.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // namelabel
            // 
            this.namelabel.AutoSize = true;
            this.namelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.namelabel.Location = new System.Drawing.Point(13, 13);
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(36, 16);
            this.namelabel.TabIndex = 1;
            this.namelabel.Text = "Имя:";
            // 
            // familyLabel
            // 
            this.familyLabel.AutoSize = true;
            this.familyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.familyLabel.Location = new System.Drawing.Point(13, 42);
            this.familyLabel.Name = "familyLabel";
            this.familyLabel.Size = new System.Drawing.Size(69, 16);
            this.familyLabel.TabIndex = 1;
            this.familyLabel.Text = "Фамилия:";
            // 
            // countAttempts_Label
            // 
            this.countAttempts_Label.AutoSize = true;
            this.countAttempts_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countAttempts_Label.Location = new System.Drawing.Point(873, 80);
            this.countAttempts_Label.Name = "countAttempts_Label";
            this.countAttempts_Label.Size = new System.Drawing.Size(146, 16);
            this.countAttempts_Label.TabIndex = 2;
            this.countAttempts_Label.Text = "Количество попыток:";
            // 
            // averageAccuracy_Label
            // 
            this.averageAccuracy_Label.AutoSize = true;
            this.averageAccuracy_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.averageAccuracy_Label.Location = new System.Drawing.Point(873, 111);
            this.averageAccuracy_Label.Name = "averageAccuracy_Label";
            this.averageAccuracy_Label.Size = new System.Drawing.Size(128, 16);
            this.averageAccuracy_Label.TabIndex = 2;
            this.averageAccuracy_Label.Text = "Средняя точность:";
            // 
            // bestAccuracy_Label
            // 
            this.bestAccuracy_Label.AutoSize = true;
            this.bestAccuracy_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bestAccuracy_Label.Location = new System.Drawing.Point(873, 182);
            this.bestAccuracy_Label.Name = "bestAccuracy_Label";
            this.bestAccuracy_Label.Size = new System.Drawing.Size(122, 16);
            this.bestAccuracy_Label.TabIndex = 2;
            this.bestAccuracy_Label.Text = "Лучшая точность:";
            // 
            // worstAccuracy_Label
            // 
            this.worstAccuracy_Label.AutoSize = true;
            this.worstAccuracy_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.worstAccuracy_Label.Location = new System.Drawing.Point(873, 214);
            this.worstAccuracy_Label.Name = "worstAccuracy_Label";
            this.worstAccuracy_Label.Size = new System.Drawing.Size(124, 16);
            this.worstAccuracy_Label.TabIndex = 2;
            this.worstAccuracy_Label.Text = "Худшая точность :";
            // 
            // overallAverageAccuracy_Label
            // 
            this.overallAverageAccuracy_Label.AutoSize = true;
            this.overallAverageAccuracy_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.overallAverageAccuracy_Label.Location = new System.Drawing.Point(873, 148);
            this.overallAverageAccuracy_Label.Name = "overallAverageAccuracy_Label";
            this.overallAverageAccuracy_Label.Size = new System.Drawing.Size(179, 16);
            this.overallAverageAccuracy_Label.TabIndex = 2;
            this.overallAverageAccuracy_Label.Text = "Средняя точность (общая):";
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 557);
            this.Controls.Add(this.worstAccuracy_Label);
            this.Controls.Add(this.bestAccuracy_Label);
            this.Controls.Add(this.overallAverageAccuracy_Label);
            this.Controls.Add(this.averageAccuracy_Label);
            this.Controls.Add(this.countAttempts_Label);
            this.Controls.Add(this.familyLabel);
            this.Controls.Add(this.namelabel);
            this.Controls.Add(this.zedGraphStatisticControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphStatisticControl;
        private System.Windows.Forms.Label namelabel;
        private System.Windows.Forms.Label familyLabel;
        private System.Windows.Forms.Label countAttempts_Label;
        private System.Windows.Forms.Label averageAccuracy_Label;
        private System.Windows.Forms.Label bestAccuracy_Label;
        private System.Windows.Forms.Label worstAccuracy_Label;
        private System.Windows.Forms.Label overallAverageAccuracy_Label;
    }
}