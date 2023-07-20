using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace StarboundMusic
{
	public class StarboundMusicConfig : ModConfig
	{
		public static StarboundMusicConfig instance
		{
			get
			{
				return ModContent.GetInstance<StarboundMusicConfig>();
			}
		}
		public override ConfigScope Mode
		{
			get
			{
				return ConfigScope.ClientSide;
			}
		}

		[BackgroundColor(0, 255, 255, 127)]
		[DefaultValue(true)]
		public bool starboundMusicActive;

		[BackgroundColor(0, 192, 255, 127)]
		[DefaultValue(4)]
		[Range(1, 8)]
		public int howLong;

		[BackgroundColor(0, 128, 255, 127)]
		[DefaultValue(true)]
		public bool musicNoteInWorld;

		[BackgroundColor(0, 64, 255, 127)]
		[Range(1, 20)]
		[DefaultValue(1)]
		public int howManyNotes;

		[BackgroundColor(0, 0, 255, 127)]
		[DefaultValue(false)]
		public bool musicNoteInDay;
	}
}
