using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace StarboundMusic
{
    public class StarboundMusicSystem : ModSystem
	{
        public override void SetupContent()
        {
            StarboundMusic.canUseMusic = true;
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(FontAssets.MouseText.Value, (Main.curMusic, Main.newMusic).ToString(), new Vector2(256, 256), Main.DiscoColor);
        }
        //public override void PostSetupContent()
        //{
        //}
    }
}
