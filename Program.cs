using System;
using System.Windows.Forms;

namespace Chew7
{
	// Token: 0x02000004 RID: 4
	internal static class Program
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003101 File Offset: 0x00001301
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}
	}
}
