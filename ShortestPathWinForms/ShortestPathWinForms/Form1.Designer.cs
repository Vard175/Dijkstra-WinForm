
namespace ShortestPathWinForms
{
    partial class Form1
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
            this.buttonGeneratePath = new System.Windows.Forms.Button();
            this.buttonShowDistance = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGeneratePath
            // 
            this.buttonGeneratePath.Location = new System.Drawing.Point(13, 13);
            this.buttonGeneratePath.Name = "buttonGeneratePath";
            this.buttonGeneratePath.Size = new System.Drawing.Size(160, 40);
            this.buttonGeneratePath.TabIndex = 0;
            this.buttonGeneratePath.Text = "Generate Path";
            this.buttonGeneratePath.UseVisualStyleBackColor = true;
            this.buttonGeneratePath.Click += new System.EventHandler(this.buttonGeneratePath_Click);
            // 
            // buttonShowDistance
            // 
            this.buttonShowDistance.Location = new System.Drawing.Point(13, 60);
            this.buttonShowDistance.Name = "buttonShowDistance";
            this.buttonShowDistance.Size = new System.Drawing.Size(160, 41);
            this.buttonShowDistance.TabIndex = 1;
            this.buttonShowDistance.Text = "Show Distance";
            this.buttonShowDistance.UseVisualStyleBackColor = true;
            this.buttonShowDistance.Click += new System.EventHandler(this.buttonShowDistance_Click);
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(13, 107);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(160, 40);
            this.buttonRestart.TabIndex = 3;
            this.buttonRestart.Text = "Clear Path";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonShowDistance);
            this.Controls.Add(this.buttonGeneratePath);
            this.Name = "Form1";
            this.Text = "Shortest Path Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGeneratePath;
        private System.Windows.Forms.Button buttonShowDistance;
        private System.Windows.Forms.Button buttonRestart;
    }
}

