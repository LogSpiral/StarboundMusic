using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarboundMusic
{
    // Token: 0x02000006 RID: 6
    public class StarboundMusicModMenu : ModMenu
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public override Asset<Texture2D> Logo
		{
			get
			{
				return ModContent.Request<Texture2D>("StarboundMusic/StarboundMusicLogo");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public override int Music
		{
			get
			{
				return MusicLoader.GetMusicSlot(Mod, "Assets/Music/StarBound/Atlas");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002CE7 File Offset: 0x00000EE7
		public override ModSurfaceBackgroundStyle MenuBackgroundStyle
		{
			get
			{
				return base.MenuBackgroundStyle;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002CEF File Offset: 0x00000EEF
		public override string DisplayName
		{
			get
			{
				return Language.GetTextValue("Mods.StarboundMusic.BackgroundName");
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002CFC File Offset: 0x00000EFC
		public override void OnSelected()
		{
			SoundEngine.PlaySound(SoundID.Thunder, null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D1D File Offset: 0x00000F1D
		public override void OnDeselected()
		{
			if (SkyEffectActive)
			{
				SkyManager.Instance.Deactivate("StarboundMusic:NoteSky", Array.Empty<object>());
                SkyEffectActive = false;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002CB3 File Offset: 0x00000EB3
		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			return true;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002D40 File Offset: 0x00000F40
		public static float GlowEffectFactor
		{
			get
			{
				if (StarboundMusicConfig.instance.musicNoteInDay)
				{
					return 1f;
				}
				if (!Main.gameMenu)
				{
					return 1f;
				}
				if (Main.dayTime || Main.time > 32400.0)
				{
					return 0f;
				}
				return MathHelper.Clamp(5f - Math.Abs((float)Main.time - 16000f) / 3200f, 0f, 1f);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public override void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
		{
            var logo = ModContent.Request<Texture2D>("StarboundMusic/StarboundMusicLogo_Glow");

            if (StarboundMusicConfig.instance == null || logo.IsDisposed || logo.Value.IsDisposed)
			{
				return;
			}
            //spriteBatch.DrawString(FontAssets.MouseText.Value, (Main.curMusic, Main.newMusic).ToString(), new Vector2(256, 256), Main.DiscoColor);

            spriteBatch.Draw(logo.Value, logoDrawCenter, null, Color.White, logoRotation, logo.Size() * 0.5f, logoScale, 0, 0f);
			if ((!Main.dayTime && Main.time <= 32400.0) || StarboundMusicConfig.instance.musicNoteInDay)
			{
				float fac = GlowEffectFactor;
				for (int i = 0; i < 4; i++)
				{
					Texture2D value = logo.Value;
					Vector2 vector = logoDrawCenter + (((float)Main.time / 3600f + 0.7853982f * (float)i).ToRotationVector2() * 2f + Main.rand.NextVector2Unit(0f, 6.2831855f) * Main.rand.NextFloat(0f, 1f)) * 4f * fac;
					Rectangle? rectangle = null;
					Color white = Color.White;
					white.A = 0;
					spriteBatch.Draw(value, vector, rectangle, white * 0.5f * fac, logoRotation, logo.Size() * 0.5f, logoScale, 0, 0f);
				}
			}
			if (Main.dayTime && !StarboundMusicConfig.instance.musicNoteInDay && SkyEffectActive)
			{
				SkyManager.Instance.Deactivate("StarboundMusic:NoteSky", Array.Empty<object>());
                SkyEffectActive = false;
				return;
			}
			if (!Main.dayTime || (StarboundMusicConfig.instance.musicNoteInDay && !SkyEffectActive))
			{
				SkyManager.Instance.Activate("StarboundMusic:NoteSky", default(Vector2), Array.Empty<object>());
                SkyEffectActive = true;
			}
		}

		// Token: 0x04000022 RID: 34
		private const string menuAssetPath = "StarboundMusic/";

		// Token: 0x04000023 RID: 35
		public static bool SkyEffectActive;
	}
}
