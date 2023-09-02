using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Chew7.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"), CompilerGenerated]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003580 File Offset: 0x00001780
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000013 RID: 19
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
