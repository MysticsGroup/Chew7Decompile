namespace Chew7
{
	// Token: 0x02000002 RID: 2
	public partial class Main : global::System.Windows.Forms.Form
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002BA3 File Offset: 0x00000DA3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Chew7.Main));
			this.ApplyButton = new global::System.Windows.Forms.PictureBox();
			this.ExitButton = new global::System.Windows.Forms.PictureBox();
			this.CreditsButton = new global::System.Windows.Forms.PictureBox();
			this.BackButton = new global::System.Windows.Forms.PictureBox();
			this.FadeInTimer = new global::System.Windows.Forms.Timer(this.components);
			((global::System.ComponentModel.ISupportInitialize)this.ApplyButton).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.ExitButton).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.CreditsButton).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.BackButton).BeginInit();
			base.SuspendLayout();
			this.ApplyButton.BackColor = global::System.Drawing.Color.Transparent;
			this.ApplyButton.Image = global::Chew7.Properties.Resources.Install_Off;
			this.ApplyButton.Location = new global::System.Drawing.Point(238, 159);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new global::System.Drawing.Size(123, 30);
			this.ApplyButton.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ApplyButton.TabIndex = 2;
			this.ApplyButton.TabStop = false;
			this.ApplyButton.MouseLeave += new global::System.EventHandler(this.ApplyButton_MouseLeave);
			this.ApplyButton.Click += new global::System.EventHandler(this.ApplyButton_Click);
			this.ApplyButton.MouseEnter += new global::System.EventHandler(this.ApplyButton_MouseEnter);
			this.ExitButton.BackColor = global::System.Drawing.Color.Transparent;
			this.ExitButton.Image = global::Chew7.Properties.Resources.Exit_Off;
			this.ExitButton.Location = new global::System.Drawing.Point(238, 210);
			this.ExitButton.Name = "ExitButton";
			this.ExitButton.Size = new global::System.Drawing.Size(123, 30);
			this.ExitButton.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ExitButton.TabIndex = 3;
			this.ExitButton.TabStop = false;
			this.ExitButton.MouseLeave += new global::System.EventHandler(this.ExitButton_MouseLeave);
			this.ExitButton.Click += new global::System.EventHandler(this.ExitButton_Click);
			this.ExitButton.MouseEnter += new global::System.EventHandler(this.ExitButton_MouseEnter);
			this.CreditsButton.BackColor = global::System.Drawing.Color.Transparent;
			this.CreditsButton.Image = global::Chew7.Properties.Resources.Credits_Off;
			this.CreditsButton.Location = new global::System.Drawing.Point(5, 370);
			this.CreditsButton.Name = "CreditsButton";
			this.CreditsButton.Size = new global::System.Drawing.Size(77, 26);
			this.CreditsButton.TabIndex = 4;
			this.CreditsButton.TabStop = false;
			this.CreditsButton.MouseLeave += new global::System.EventHandler(this.CreditsButton_MouseLeave);
			this.CreditsButton.Click += new global::System.EventHandler(this.CreditsButton_Click);
			this.CreditsButton.MouseEnter += new global::System.EventHandler(this.CreditsButton_MouseEnter);
			this.BackButton.BackColor = global::System.Drawing.Color.Transparent;
			this.BackButton.Image = global::Chew7.Properties.Resources.Back_Off;
			this.BackButton.Location = new global::System.Drawing.Point(5, 370);
			this.BackButton.Name = "BackButton";
			this.BackButton.Size = new global::System.Drawing.Size(77, 26);
			this.BackButton.TabIndex = 4;
			this.BackButton.TabStop = false;
			this.BackButton.Visible = false;
			this.BackButton.MouseLeave += new global::System.EventHandler(this.BackButton_MouseLeave);
			this.BackButton.Click += new global::System.EventHandler(this.BackButton_Click);
			this.BackButton.MouseEnter += new global::System.EventHandler(this.BackButton_MouseEnter);
			this.FadeInTimer.Enabled = true;
			this.FadeInTimer.Interval = 1;
			this.FadeInTimer.Tick += new global::System.EventHandler(this.FadeIn);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Chew7.Properties.Resources.MainBG;
			base.ClientSize = new global::System.Drawing.Size(599, 399);
			base.Controls.Add(this.BackButton);
			base.Controls.Add(this.ExitButton);
			base.Controls.Add(this.ApplyButton);
			base.Controls.Add(this.CreditsButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Main";
			base.Opacity = 0.0;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			base.Load += new global::System.EventHandler(this.Main_Load);
			((global::System.ComponentModel.ISupportInitialize)this.ApplyButton).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.ExitButton).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.CreditsButton).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.BackButton).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400000B RID: 11
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.PictureBox ApplyButton;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.PictureBox ExitButton;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.PictureBox CreditsButton;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.PictureBox BackButton;

		// Token: 0x04000010 RID: 16
		public global::System.Windows.Forms.Timer FadeInTimer;
	}
}
