namespace States
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
            this.statesControl = new States.StatesProject.StatesControl();
            this.SuspendLayout();
            // 
            // statesControl
            // 
            this.statesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statesControl.Location = new System.Drawing.Point(0, 0);
            this.statesControl.Name = "statesControl";
            this.statesControl.Size = new System.Drawing.Size(500, 500);
            this.statesControl.TabIndex = 0;
            this.statesControl.Text = "statesControl1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.statesControl);
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "Form1";
            this.Text = "Состояния";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private StatesProject.StatesControl statesControl;
    }
}

