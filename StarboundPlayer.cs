using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace StarboundMusic
{
	// Token: 0x02000003 RID: 3
	public class StarboundPlayer : ModPlayer
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002238 File Offset: 0x00000438
		private void CurrentMusicChanger(int[] indexs)
		{
			if (StarboundMusicConfig.instance == null)
			{
				return;
			}
			if (dayMusic.Length == 0 || dayMusic[0] == 0)
			{
				LoadMusic();
			}
			if (musicCounter % (StarboundMusicConfig.instance.howLong * 3600) == 0 || !indexs.Contains(currentMusic) || Main.musicError > 0)
			{
				musicCounter = 0;
				int index = currentMusic;
				int counter = 0;
				while (index == currentMusic || (index == 106 && counter < 10))
				{
					currentMusic = Main.rand.Next(indexs);
					counter++;
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022CD File Offset: 0x000004CD
		private int GetMusic(string path)
		{
			return MusicLoader.GetMusicSlot(Mod, "Assets/Music/" + path);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E8 File Offset: 0x000004E8
		private int[] ToMusicIndex(string[] names, string preName = null)
		{
			List<int> result = [];
			foreach (string name in names)
			{
				if (MusicLoader.MusicExists("StarboundMusic/Assets/Music/" + preName + name))
				{
					result.Add(GetMusic(preName + name));
				}
			}
			return result.ToArray();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000233C File Offset: 0x0000053C
		private void LoadMusic()
		{
			LoadStbMusic(ref dayMusic, new string[]
			{
                "CygnusX1",
                "HymntotheStars",
                "StellarFormation",
				"VastImmortalSuns",
				"EpsilonIndi"
			});
			LoadStbMusic(ref nightMusic, new string[]
			{
				"CygnusX1",
				"LargeMagellanicCloud",
				"HymntotheStars",
				"Atlas",
				"Mercury",
				"Planetarium",
				"M54",
				"Jupiter",
				"Starbound"
			});
			LoadStbMusic(ref desertMusic, new string[]
			{
				"EridanusSupervoid",
				"IWastheSun(BeforeitwasCool)",
				"TranquilityBase"
			});
			LoadStbMusic(ref snowMusic, new string[]
			{
				"HorseheadNebula",
				"TranquilityBase",
				"Europa"
			});
			LoadStbMusic(ref hallowMusic, new string[]
			{
				"Error0xBFAF000",
				"AccretionDisc",
				"OntheBeachatNightOriginalVersion"
			});
            //				"ScorianFlow",
            LoadStbMusic(ref evilMusic, new string[]
			{

				"Psyche",
				"Nomads(Passacaglia)",
				"TheApex",
				"Drosera"
			});
			LoadStbMusic(ref jungleMusic, new string[]
			{
				"Haiku",
				"BlueStraggler"
			});
			LoadStbMusic(ref undergroundMusic, new string[]
			{
				"Procyon",
				"ArcticExploration2",
				"OceanExploration2",
				"M54",
				"ForestExploration1",
				"OceanExploration1",
				"ArcticConstellation1",
				"ArcticConstellation2",
				"LavaExploration1"
			});
			LoadStbMusic(ref undergroundDesertMusic, new string[]
			{
				"EridanusSupervoid",
				"ArcticExploration1"
			});
			LoadStbMusic(ref undergroundSnowMusic, new string[]
			{
				"Mira",
				"ArcticExploration1",
				"ForestExploration2",
				"Europa"
			});
			LoadStbMusic(ref undergroundHallowMusic, new string[]
			{
				"Error0xBFAF000",
				"AccretionDisc",
				"CrystalExploration2",
				"GlacialHorizon",
				"ForsakenGrotto"
			});
            //				"ScorianFlow",
            LoadStbMusic(ref undergroundEvilMusic, new string[]
			{

				"Psyche",
				"Nomads(Passacaglia)",
				"CrystalExploration1",
				"DesertExploration1",
				"DesertExploration2",
				"TheApex",
				"Ultramarine"
			});
			LoadStbMusic(ref undergroundJungleMusic, new string[]
			{
				"LavaExploration2",
				"GlacialHorizon",
				"ForsakenGrotto"
			});
			LoadStbMusic(ref bloodMoonMusic, new string[]
			{
				"Nomads(Passacaglia)",
				"TentacleBattle1",
				"TentacleExploration1"
			});
			LoadStbMusic(ref golbinArmyMusic, new string[]
			{
				"GravitationalCollapse"
			});
			LoadStbMusic(ref oldOnesMusic, new string[]
			{
				"EventHorizon",
				"ImpactEvent"
			});
			LoadStbMusic(ref eclipseMusic, new string[]
			{
				"Nomads(Passacaglia)",
				"TentacleBattle1"
			});
			LoadStbMusic(ref frostMoonMusic, new string[]
			{
				"Nomads(Passacaglia)",
				"TentacleBattle1"
			});
			LoadStbMusic(ref pumpkinMoonMusic, new string[]
			{
				"Nomads(Passacaglia)",
				"TentacleBattle1"
			});
			LoadStbMusic(ref martianMusic, new string[]
			{
				"TheApex"
			});
            // "ScorianFlow",
			LoadStbMusic(ref dungeonMusic, new string[]
			{
				"Psyche",
				"TranquilityBase"
			});
			LoadStbMusic(ref hellMusic, new string[]
			{
				"Casiopeia",
				"LavaExploration1"
			});
			LoadStbMusic(ref skyMusic, new string[]
			{
				"HorseheadNebula",
				"Haiku"
			});
			LoadStbMusic(ref rainMusic, new string[]
			{
				"HorseheadNebula",
				"Haiku",
				"BlueStraggler"
			});
			LoadStbMusic(ref oceanMusic, new string[]
			{
				"HorseheadNebula",
				"StellarAcclimation"
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002794 File Offset: 0x00000994
		private void LoadStbMusic(ref int[] index, params string[] names)
		{
			index = ToMusicIndex(names, "StarBound/");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000027A4 File Offset: 0x000009A4
		public override void Load()
		{
			LoadMusic();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000027AC File Offset: 0x000009AC
		private bool ZoneDeepAbyss
		{
			get
			{
				if (!Player.ZoneBeach || !Player.wet)
				{
					return false;
				}
				int x = (int)Player.Center.X / 16;
				int y = (int)Player.Center.Y / 16;
				int count = 0;
				for (int i = 0; i < 7200; i++)
				{
					Tile t = Main.tile[(int)MathHelper.Clamp((float)(x - 60 + i % 120), 0f, (float)Main.maxTilesX), (int)MathHelper.Clamp((float)(y - 30 + i / 120), 0f, (float)Main.maxTilesY)];
					if (t.LiquidType == 0 && t.LiquidAmount > 0)
					{
						count++;
					}
					if (count > 300)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002878 File Offset: 0x00000A78
		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			if (Main.gameMenu)
			{
				return;
			}
			if (StarboundMusicConfig.instance == null)
			{
				return;
			}
			if ((Main.dayTime && !StarboundMusicConfig.instance.musicNoteInDay) || (!StarboundMusicConfig.instance.musicNoteInWorld && StarboundMusicModMenu.SkyEffectActive))
			{
				SkyManager.Instance.Deactivate("StarboundMusic:NoteSky", Array.Empty<object>());
				StarboundMusicModMenu.SkyEffectActive = false;
			}
			if ((!Main.dayTime || StarboundMusicConfig.instance.musicNoteInDay) && StarboundMusicConfig.instance.musicNoteInWorld && !StarboundMusicModMenu.SkyEffectActive)
			{
				SkyManager.Instance.Activate("StarboundMusic:NoteSky", default(Vector2), Array.Empty<object>());
				StarboundMusicModMenu.SkyEffectActive = true;
			}
			base.ModifyDrawInfo(ref drawInfo);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002928 File Offset: 0x00000B28
		public override void ResetEffects()
		{
			musicCounter++;
			if (dayMusic.Length == 0)
			{
				LoadMusic();
			}
			if (Player.ZoneDirtLayerHeight || Player.ZoneRockLayerHeight)
			{
				if (Player.ZoneJungle)
				{
					CurrentMusicChanger(undergroundJungleMusic);
				}
				else if (Player.ZoneHallow)
				{
					CurrentMusicChanger(undergroundHallowMusic);
				}
				else if (Player.ZoneCrimson || Player.ZoneCorrupt)
				{
					CurrentMusicChanger(undergroundEvilMusic);
				}
				else if (Player.ZoneSnow)
				{
					CurrentMusicChanger(undergroundSnowMusic);
				}
				else if (Player.ZoneDesert)
				{
					CurrentMusicChanger(undergroundDesertMusic);
				}
				else
				{
					CurrentMusicChanger(undergroundMusic);
				}
			}
			else if (Main._shouldUseStormMusic)
			{
				currentMusic = GetMusic("StarBound/Inviolate");
			}
			else if (Player.ZoneJungle)
			{
				CurrentMusicChanger(jungleMusic);
			}
			else if (Player.ZoneHallow)
			{
				CurrentMusicChanger(hallowMusic);
			}
			else if (Player.ZoneCrimson || Player.ZoneCorrupt)
			{
				CurrentMusicChanger(evilMusic);
			}
			else if (Player.ZoneSnow)
			{
				CurrentMusicChanger(snowMusic);
			}
			else if (Player.ZoneDesert)
			{
				CurrentMusicChanger(desertMusic);
			}
			else if (Player.ZoneSkyHeight)
			{
				CurrentMusicChanger(skyMusic);
			}
			else if (Player.ZoneRain)
			{
				CurrentMusicChanger(rainMusic);
			}
			else if (Main.dayTime)
			{
				CurrentMusicChanger(dayMusic);
			}
			else
			{
				CurrentMusicChanger(nightMusic);
			}
			if (Player.ZoneBeach)
			{
				if (ZoneDeepAbyss)
				{
					currentMusic = GetMusic("StarBound/TheDeep");
				}
				else
				{
					CurrentMusicChanger(oceanMusic);
				}
			}
			if ((Main.bgStyle == 9 && (double)Player.position.Y < Main.worldSurface * 16.0 + (double)(Main.screenHeight / 2)) || Main.undergroundBackground == 2)
			{
				GetMusic("StarBound/ViaAurora");
			}
			if (Main.tile[(int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f)].WallType == 87)
			{
				currentMusic = GetMusic("StarBound/Casiopeia");
			}
			if (Player.ZoneDungeon)
			{
				CurrentMusicChanger(dungeonMusic);
			}
			if (Player.ZoneUnderworldHeight)
			{
				CurrentMusicChanger(hellMusic);
			}
			if (Main.invasionProgressIcon == 4)
			{
				CurrentMusicChanger(golbinArmyMusic);
			}
			if (Main.bloodMoon)
			{
				CurrentMusicChanger(bloodMoonMusic);
			}
			if (Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
			{
				CurrentMusicChanger(eclipseMusic);
			}
			if (DD2Event.Ongoing || new int[]
			{
				7,
				10,
				11
			}.Contains(Main.invasionProgressIcon))
			{
				CurrentMusicChanger(oldOnesMusic);
			}
			StarboundMusic.starboundNewMusic = currentMusic;
		}

		// Token: 0x04000002 RID: 2
		private int currentMusic;

		// Token: 0x04000003 RID: 3
		private int musicCounter;

		// Token: 0x04000004 RID: 4
		public static int[] dayMusic;

		// Token: 0x04000005 RID: 5
		public static int[] nightMusic;

		// Token: 0x04000006 RID: 6
		public static int[] desertMusic;

		// Token: 0x04000007 RID: 7
		public static int[] snowMusic;

		// Token: 0x04000008 RID: 8
		public static int[] hallowMusic;

		// Token: 0x04000009 RID: 9
		public static int[] evilMusic;

		// Token: 0x0400000A RID: 10
		public static int[] jungleMusic;

		// Token: 0x0400000B RID: 11
		public static int[] undergroundMusic;

		// Token: 0x0400000C RID: 12
		public static int[] undergroundDesertMusic;

		// Token: 0x0400000D RID: 13
		public static int[] undergroundSnowMusic;

		// Token: 0x0400000E RID: 14
		public static int[] undergroundHallowMusic;

		// Token: 0x0400000F RID: 15
		public static int[] undergroundEvilMusic;

		// Token: 0x04000010 RID: 16
		public static int[] undergroundJungleMusic;

		// Token: 0x04000011 RID: 17
		public static int[] bloodMoonMusic;

		// Token: 0x04000012 RID: 18
		public static int[] golbinArmyMusic;

		// Token: 0x04000013 RID: 19
		public static int[] oldOnesMusic;

		// Token: 0x04000014 RID: 20
		public static int[] eclipseMusic;

		// Token: 0x04000015 RID: 21
		public static int[] frostMoonMusic;

		// Token: 0x04000016 RID: 22
		public static int[] pumpkinMoonMusic;

		// Token: 0x04000017 RID: 23
		public static int[] martianMusic;

		// Token: 0x04000018 RID: 24
		public static int[] dungeonMusic;

		// Token: 0x04000019 RID: 25
		public static int[] hellMusic;

		// Token: 0x0400001A RID: 26
		public static int[] rainMusic;

		// Token: 0x0400001B RID: 27
		public static int[] skyMusic;

		// Token: 0x0400001C RID: 28
		public static int[] oceanMusic;
	}
}
