using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace StarboundMusic
{
	// Token: 0x02000004 RID: 4
	public class StarboundMusicConfig : ModConfig
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002CAC File Offset: 0x00000EAC
		public static StarboundMusicConfig instance
		{
			get
			{
				return ModContent.GetInstance<StarboundMusicConfig>();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002CB3 File Offset: 0x00000EB3
		public override ConfigScope Mode
		{
			get
			{
				return ConfigScope.ClientSide;
			}
		}

		// Token: 0x0400001D RID: 29
		[BackgroundColor(0, 0, 255, 127)]
		[Label("$Mods.StarboundMusic.Config0")]
		[Tooltip("$Mods.StarboundMusic.Config1")]
		[DefaultValue(true)]
		public bool starboundMusicActive;

		// Token: 0x0400001E RID: 30
		[BackgroundColor(127, 0, 255, 127)]
		[Label("$Mods.StarboundMusic.Config2")]
		[Tooltip("$Mods.StarboundMusic.Config3")]
		[DefaultValue(4)]
		[Range(1, 8)]
		public int howLong;

		// Token: 0x0400001F RID: 31
		[BackgroundColor(255, 0, 255, 127)]
		[Label("$Mods.StarboundMusic.Config4")]
		[Tooltip("$Mods.StarboundMusic.Config5")]
		[DefaultValue(true)]
		public bool musicNoteInWorld;

		// Token: 0x04000020 RID: 32
		[BackgroundColor(255, 127, 255, 127)]
		[Label("$Mods.StarboundMusic.Config8")]
		[Tooltip("$Mods.StarboundMusic.Config9")]
		[Range(1, 20)]
		[DefaultValue(1)]
		public int howManyNotes;

		// Token: 0x04000021 RID: 33
		[BackgroundColor(255, 255, 255, 127)]
		[Label("$Mods.StarboundMusic.Config6")]
		[Tooltip("$Mods.StarboundMusic.Config7")]
		[DefaultValue(false)]
		public bool musicNoteInDay;
	}
}
