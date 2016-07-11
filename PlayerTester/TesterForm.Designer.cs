namespace PlayerTester
{
    partial class TesterForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TesterForm));
            this.LabelChannels = new System.Windows.Forms.Label();
            this.LabelBitsPerSample = new System.Windows.Forms.Label();
            this.LabelSamplesPreSecond = new System.Windows.Forms.Label();
            this.ButtonRecord = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelChannels
            // 
            this.LabelChannels.AutoSize = true;
            this.LabelChannels.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelChannels.Location = new System.Drawing.Point(60, 84);
            this.LabelChannels.Name = "LabelChannels";
            this.LabelChannels.Size = new System.Drawing.Size(51, 13);
            this.LabelChannels.TabIndex = 23;
            this.LabelChannels.Text = "Channels";
            // 
            // LabelBitsPerSample
            // 
            this.LabelBitsPerSample.AutoSize = true;
            this.LabelBitsPerSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBitsPerSample.Location = new System.Drawing.Point(36, 45);
            this.LabelBitsPerSample.Name = "LabelBitsPerSample";
            this.LabelBitsPerSample.Size = new System.Drawing.Size(75, 13);
            this.LabelBitsPerSample.TabIndex = 21;
            this.LabelBitsPerSample.Text = "BitsPerSample";
            // 
            // LabelSamplesPreSecond
            // 
            this.LabelSamplesPreSecond.AutoSize = true;
            this.LabelSamplesPreSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSamplesPreSecond.Location = new System.Drawing.Point(12, 12);
            this.LabelSamplesPreSecond.Name = "LabelSamplesPreSecond";
            this.LabelSamplesPreSecond.Size = new System.Drawing.Size(99, 13);
            this.LabelSamplesPreSecond.TabIndex = 19;
            this.LabelSamplesPreSecond.Text = "SamplesPerSecond";
            // 
            // ButtonRecord
            // 
            this.ButtonRecord.Location = new System.Drawing.Point(47, 126);
            this.ButtonRecord.Name = "ButtonRecord";
            this.ButtonRecord.Size = new System.Drawing.Size(64, 23);
            this.ButtonRecord.TabIndex = 30;
            this.ButtonRecord.Text = "Record";
            this.ButtonRecord.UseVisualStyleBackColor = true;
            this.ButtonRecord.Click += new System.EventHandler(this.ButtonRecord_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 31;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(117, 8);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 31;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(117, 84);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 31;
            // 
            // button1
            // 
            this.ButtonStop.Location = new System.Drawing.Point(117, 126);
            this.ButtonStop.Name = "button1";
            this.ButtonStop.Size = new System.Drawing.Size(75, 23);
            this.ButtonStop.TabIndex = 35;
            this.ButtonStop.Text = "button1";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 164);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ButtonRecord);
            this.Controls.Add(this.LabelChannels);
            this.Controls.Add(this.LabelBitsPerSample);
            this.Controls.Add(this.LabelSamplesPreSecond);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TesterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Player Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelChannels;
        private System.Windows.Forms.Label LabelBitsPerSample;
        private System.Windows.Forms.Label LabelSamplesPreSecond;
        private System.Windows.Forms.Button ButtonRecord;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button ButtonStop;
    }
}