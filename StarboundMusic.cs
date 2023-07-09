using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarboundMusic
{
    public class StarboundMusic : Mod
    {
        public override void Load()
        {
            canUseMusic = false;
            Terraria.On_Main.UpdateAudio_DecideOnNewMusic += StarboundMusicHook;
            SkyManager.Instance["StarboundMusic:NoteSky"] = new StarboundMusicNoteSky();
        }
        public override void Unload()
        {
            Terraria.On_Main.UpdateAudio_DecideOnNewMusic -= StarboundMusicHook;

        }
        public static bool canUseMusic = false;
        private void StarboundMusicHook(Terraria.On_Main.orig_UpdateAudio_DecideOnNewMusic orig, Main self)
        {
            bool notUseStbMusic = StarboundMusicConfig.instance == null || !StarboundMusicConfig.instance.starboundMusicActive || !canUseMusic;
            var fieldInfo = typeof(Main).GetField("_isAsyncLoadComplete", BindingFlags.NonPublic | BindingFlags.Static);
            bool loadComplete = (bool)fieldInfo.GetValue(null);
            if (!notUseStbMusic) fieldInfo.SetValue(null, false);
            orig.Invoke(self);
            if (Main.netMode == NetmodeID.Server) return;
            if (notUseStbMusic)
            {
                return;
            }
            bool flag = false;
            foreach (var n in Main.npc)
            {
                if (n.active && n.boss)
                {
                    flag = true;
                    break;
                }
                if (n.active && new int[] { 422, 493, 507, 517 }.Contains(n.type) && Vector2.Distance(Main.LocalPlayer.Center, n.Center) < 5000)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                switch (Main.newMusic)
                {
                    case MusicID.Boss1:
                        GetMusicIndex("StarBound/ArcticBattle1");
                        break;
                    case MusicID.Boss2:
                        GetMusicIndex("StarBound/ArcticBattle3");
                        break;
                    case MusicID.Boss3:
                        GetMusicIndex("StarBound/ArcticBattle2");
                        break;
                    case MusicID.EmpressOfLight:
                    case MusicID.DukeFishron:
                    case MusicID.Plantera:
                    case MusicID.Boss5:
                    case MusicID.QueenSlime:
                        GetMusicIndex("StarBound/DesertBattle2");
                        break;
                    case MusicID.Boss4:
                        GetMusicIndex("StarBound/OceanBattle1");
                        break;
                    case MusicID.LunarBoss:
                        GetMusicIndex("StarBound/ForestBattle2");
                        break;
                    case MusicID.TheTowers:
                        GetMusicIndex("StarBound/CrystalBattle1");
                        break;
                }
            }
            else
            {
                Main.newMusic = starboundNewMusic;
            }
            if (Main.gameMenu)
            {
                GetMusicIndex("StarBound/Atlas");
            }
            fieldInfo.SetValue(null, loadComplete);

        }
        public void GetMusicIndex(ref int orig, string path)
        {
            if (MusicLoader.MusicExists("StarboundMusic/Assets/Music/" + path))
            {
                orig = MusicLoader.GetMusicSlot(this, "Assets/Music/" + path);
            }
        }
        public void GetMusicIndex(string path)
        {
            if (MusicLoader.MusicExists("StarboundMusic/Assets/Music/" + path))
            {
                Main.newMusic = MusicLoader.GetMusicSlot(this, "Assets/Music/" + path);
            }
        }
        public static int starboundNewMusic;
    }
}
