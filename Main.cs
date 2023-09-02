using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Chew7.Properties;
using Microsoft.Win32;

namespace Chew7
{
	// Token: 0x02000002 RID: 2
	public partial class Main : Form
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private void Main_Load(object sender, EventArgs e)
		{
			this.Installed = this.RegRead("CWInstalled");
			if (this.Installed == "TRUE")
			{
				this.thisjob = "uninstall";
			}
			else
			{
				this.thisjob = "install";
			}
			this.CreateCreditsPNG();
			this.CreateFailurePNG();
			this.CreateAlreadyInstalledPNG();
			this.CreateRebootingPNG();
			this.CreatePleaseWaitPNG();
			if (this.thisjob == "uninstall")
			{
				this.BackgroundImage = Resources.BlurBG;
				this.ApplyButton.Image = Resources.Uninstall_Off;
				this.AlreadyInstalledPNG.Visible = true;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 132)
			{
				int num = m.LParam.ToInt32();
				short y = (short)(num >> 16);
				short x = (short)num;
				if (base.PointToClient(new Point((int)x, (int)y)).Y < 500)
				{
					m.Result = new IntPtr(2);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002152 File Offset: 0x00000352
		public void Assassinate(string targetfile)
		{
			if (File.Exists(targetfile))
			{
				File.SetAttributes(targetfile, FileAttributes.Normal);
				File.Delete(targetfile);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000216D File Offset: 0x0000036D
		public void KillTasks()
		{
			this.Run("taskkill", "/f /im cmd.exe", true);
			this.Run("taskkill", "/f /im hale.exe", true);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002194 File Offset: 0x00000394
		public bool RegDelete(string subkey, string valuename)
		{
			bool result;
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				if (valuename != null)
				{
					RegistryKey registryKey = localMachine.CreateSubKey(subkey);
					if (registryKey != null)
					{
						registryKey.DeleteValue(valuename);
					}
				}
				else
				{
					RegistryKey registryKey2 = localMachine.OpenSubKey(subkey);
					if (registryKey2 != null)
					{
						localMachine.DeleteSubKeyTree(subkey);
					}
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021EC File Offset: 0x000003EC
		public string RegRead(string KeyName)
		{
			RegistryKey localMachine = Registry.LocalMachine;
			RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\Chew7");
			if (registryKey == null)
			{
				return null;
			}
			string result;
			try
			{
				string text = (string)registryKey.GetValue(KeyName.ToUpper());
				result = text.ToUpper();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002244 File Offset: 0x00000444
		public string Chew()
		{
			this.Cursor = Cursors.WaitCursor;
			this.KillTasks();
			File.Delete(this.detlog);
			this.Assassinate(this.hale);
			this.ExtractChewPatch();
			string result = "failure";
			if (this.thisjob == "uninstall")
			{
				this.Run(this.hale, "/U", true);
				this.is_installed = this.RegRead("CWInstalled");
				if (this.is_installed == "FALSE")
				{
					this.Assassinate(this.hale);
					this.RegDelete("SOFTWARE\\Chew7", null);
					this.RegDelete("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "Chew7Hale");
					File.Delete(this.detlog);
					result = "success";
				}
			}
			else
			{
				this.Run(this.hale, "", true);
				this.is_installed = this.RegRead("CWInstalled");
				if (this.is_installed == "TRUE")
				{
					File.SetAttributes(this.hale, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive);
					result = "success";
				}
			}
			this.Cursor = Cursors.Default;
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002360 File Offset: 0x00000560
		public void ExtractChewPatch()
		{
			Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("Chew7.Resources.hale.exe");
			this.Assassinate(this.hale);
			string path = this.hale;
			FileStream writeStream = new FileStream(path, FileMode.Create, FileAccess.Write);
			if (manifestResourceStream == null)
			{
				MessageBox.Show("Resources not found...");
				Environment.Exit(0);
			}
			this.ReadWriteStream(manifestResourceStream, writeStream);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023BC File Offset: 0x000005BC
		public void Run(string filename, string sw, bool wait)
		{
			Process process = new Process();
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.EnableRaisingEvents = false;
			process.StartInfo.FileName = filename;
			process.StartInfo.Arguments = sw;
			process.Start();
			process.WaitForExit();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002408 File Offset: 0x00000608
		private void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int num = 256;
			byte[] buffer = new byte[num];
			for (int i = readStream.Read(buffer, 0, num); i > 0; i = readStream.Read(buffer, 0, num))
			{
				writeStream.Write(buffer, 0, i);
			}
			readStream.Close();
			writeStream.Close();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002454 File Offset: 0x00000654
		private void PleaseWait()
		{
			this.BackgroundImage = Resources.BlurBG;
			this.ApplyButton.Visible = false;
			this.ExitButton.Visible = false;
			this.CreditsButton.Visible = false;
			this.AlreadyInstalledPNG.Visible = false;
			this.PleaseWaitPNG.Visible = true;
			this.Refresh();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024B0 File Offset: 0x000006B0
		public bool IsUserAdministrator()
		{
			bool result;
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
				result = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch (UnauthorizedAccessException)
			{
				result = false;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002500 File Offset: 0x00000700
		private void FadeIn()
		{
			base.Opacity = 0.0;
			base.Show();
			this.Refresh();
			for (float num = 0f; num < 1f; num += 0.02f)
			{
				base.Opacity = (double)num;
				Thread.Sleep(5);
			}
			base.Opacity = 1.0;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002560 File Offset: 0x00000760
		private void FadeOut()
		{
			base.Opacity = 1.0;
			for (float num = 1f; num > 0f; num -= 0.05f)
			{
				base.Opacity = (double)num;
				Thread.Sleep(20);
			}
			base.Opacity = 0.0;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025B4 File Offset: 0x000007B4
		public Main()
		{
			if (!this.IsUserAdministrator())
			{
				MessageBox.Show("You need administrator privileges to run this application.", "Privileges Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Process.GetCurrentProcess().Kill();
			}
			if (Main.OSInfo.WinBuild != "6.1")
			{
				MessageBox.Show("This application cannot run on your current operating system.", "Invalid operating system", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Process.GetCurrentProcess().Kill();
			}
			this.InitializeComponent();
			base.ControlBox = false;
			this.Text = string.Empty;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002693 File Offset: 0x00000893
		private void FadeIn(object sender, EventArgs e)
		{
			this.FadeInTimer.Enabled = false;
			this.FadeIn();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026A8 File Offset: 0x000008A8
		private void ApplyButton_Click(object sender, EventArgs e)
		{
			if (this.thisjob == "install")
			{
				DialogResult dialogResult = MessageBox.Show("Your computer will automatically restart after the installation is complete. Please save all your work and close any open programs before continuing.\n\nAre you ready to install?", "Ready to continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
			}
			this.PleaseWait();
			if (this.Chew() == "success")
			{
				this.RebootingPNG.Visible = true;
				this.Refresh();
				Thread.Sleep(1200);
				this.Run("shutdown", "/r /f /t 0 /d p:2:18", false);
			}
			else
			{
				this.RegDelete("SOFTWARE\\Chew7", null);
				this.PleaseWaitPNG.Visible = false;
				this.FailurePNG.Visible = true;
				this.Refresh();
			}
			Thread.Sleep(1200);
			this.FadeOut();
			Application.Exit();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002766 File Offset: 0x00000966
		private void ExitButton_Click(object sender, EventArgs e)
		{
			this.FadeOut();
			Application.Exit();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002774 File Offset: 0x00000974
		private void CreditsButton_Click(object sender, EventArgs e)
		{
			if (this.thisjob == "uninstall")
			{
				this.AlreadyInstalledPNG.Visible = false;
			}
			this.BackgroundImage = Resources.BlurBG;
			this.CreditsButton.Visible = false;
			this.ApplyButton.Visible = false;
			this.ExitButton.Visible = false;
			this.BackButton.Visible = true;
			this.CreditsPNG.Visible = true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000027E8 File Offset: 0x000009E8
		private void BackButton_Click(object sender, EventArgs e)
		{
			this.BackButton.Visible = false;
			this.CreditsPNG.Visible = false;
			this.CreditsButton.Visible = true;
			this.ApplyButton.Visible = true;
			this.ExitButton.Visible = true;
			if (this.thisjob == "uninstall")
			{
				this.AlreadyInstalledPNG.Visible = true;
				this.BackgroundImage = Resources.BlurBG;
				return;
			}
			this.BackgroundImage = Resources.MainBG;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002866 File Offset: 0x00000A66
		private void ApplyButton_MouseEnter(object sender, EventArgs e)
		{
			if (this.thisjob == "install")
			{
				this.ApplyButton.Image = Resources.Install_On;
				return;
			}
			this.ApplyButton.Image = Resources.Uninstall_On;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000289B File Offset: 0x00000A9B
		private void ExitButton_MouseEnter(object sender, EventArgs e)
		{
			this.ExitButton.Image = Resources.Exit_On;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028AD File Offset: 0x00000AAD
		private void CreditsButton_MouseEnter(object sender, EventArgs e)
		{
			this.CreditsButton.Image = Resources.Credits_On;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028BF File Offset: 0x00000ABF
		private void BackButton_MouseEnter(object sender, EventArgs e)
		{
			this.BackButton.Image = Resources.Back_On;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028D1 File Offset: 0x00000AD1
		private void ApplyButton_MouseLeave(object sender, EventArgs e)
		{
			if (this.thisjob == "install")
			{
				this.ApplyButton.Image = Resources.Install_Off;
				return;
			}
			this.ApplyButton.Image = Resources.Uninstall_Off;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002906 File Offset: 0x00000B06
		private void ExitButton_MouseLeave(object sender, EventArgs e)
		{
			this.ExitButton.Image = Resources.Exit_Off;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002918 File Offset: 0x00000B18
		private void CreditsButton_MouseLeave(object sender, EventArgs e)
		{
			this.CreditsButton.Image = Resources.Credits_Off;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000292A File Offset: 0x00000B2A
		private void BackButton_MouseLeave(object sender, EventArgs e)
		{
			this.BackButton.Image = Resources.Back_Off;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000293C File Offset: 0x00000B3C
		public void CreateCreditsPNG()
		{
			this.CreditsPNG.BackColor = Color.Transparent;
			this.CreditsPNG.Location = new Point(109, 60);
			this.CreditsPNG.Size = new Size(398, 270);
			this.CreditsPNG.Image = Resources.credits_text;
			this.CreditsPNG.Visible = false;
			base.Controls.Add(this.CreditsPNG);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void CreateFailurePNG()
		{
			this.FailurePNG.BackColor = Color.Transparent;
			this.FailurePNG.Location = new Point(236, 157);
			this.FailurePNG.Size = new Size(345, 270);
			this.FailurePNG.Image = Resources.Failure;
			this.FailurePNG.Visible = false;
			base.Controls.Add(this.FailurePNG);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A34 File Offset: 0x00000C34
		public void CreateAlreadyInstalledPNG()
		{
			this.AlreadyInstalledPNG.BackColor = Color.Transparent;
			this.AlreadyInstalledPNG.Location = new Point(117, 45);
			this.AlreadyInstalledPNG.Size = new Size(350, 73);
			this.AlreadyInstalledPNG.Image = Resources.AlreadyInstalled;
			this.AlreadyInstalledPNG.Visible = false;
			base.Controls.Add(this.AlreadyInstalledPNG);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void CreateRebootingPNG()
		{
			this.RebootingPNG.BackColor = Color.Transparent;
			this.RebootingPNG.Location = new Point(140, 150);
			this.RebootingPNG.Size = new Size(350, 73);
			this.RebootingPNG.Image = Resources.Rebooting1;
			this.RebootingPNG.Visible = false;
			base.Controls.Add(this.RebootingPNG);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B28 File Offset: 0x00000D28
		public void CreatePleaseWaitPNG()
		{
			this.PleaseWaitPNG.BackColor = Color.Transparent;
			this.PleaseWaitPNG.Location = new Point(120, 150);
			this.PleaseWaitPNG.Size = new Size(400, 300);
			this.PleaseWaitPNG.Image = Resources.PleaseWait1;
			this.PleaseWaitPNG.Visible = false;
			base.Controls.Add(this.PleaseWaitPNG);
		}

		// Token: 0x04000001 RID: 1
		private PictureBox CreditsPNG = new PictureBox();

		// Token: 0x04000002 RID: 2
		private PictureBox PleaseWaitPNG = new PictureBox();

		// Token: 0x04000003 RID: 3
		private PictureBox RebootingPNG = new PictureBox();

		// Token: 0x04000004 RID: 4
		private PictureBox FailurePNG = new PictureBox();

		// Token: 0x04000005 RID: 5
		private PictureBox AlreadyInstalledPNG = new PictureBox();

		// Token: 0x04000006 RID: 6
		private string hale = Path.Combine(Environment.SystemDirectory, "hale.exe");

		// Token: 0x04000007 RID: 7
		private string detlog = Path.Combine(Environment.SystemDirectory, "cwlog.dtl");

		// Token: 0x04000008 RID: 8
		private string Installed;

		// Token: 0x04000009 RID: 9
		private string thisjob;

		// Token: 0x0400000A RID: 10
		private string is_installed;

		// Token: 0x02000003 RID: 3
		public class OSInfo
		{
			// Token: 0x06000024 RID: 36
			[DllImport("Kernel32.dll")]
			internal static extern bool GetProductInfo(int osMajorVersion, int osMinorVersion, int spMajorVersion, int spMinorVersion, out int edition);

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000025 RID: 37 RVA: 0x000030B4 File Offset: 0x000012B4
			public static string WinBuild
			{
				get
				{
					return Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString();
				}
			}
		}
	}
}
