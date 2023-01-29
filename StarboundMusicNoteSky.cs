using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace StarboundMusic
{
	// Token: 0x02000007 RID: 7
	public class StarboundMusicNoteSky : CustomSky
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002F9C File Offset: 0x0000119C
		public override void OnLoad()
		{
			_texture = ModContent.Request<Texture2D>("StarboundMusic/Note", AssetRequestMode.AsyncLoad);
			GenerateLanterns(false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002FB8 File Offset: 0x000011B8
		private void GenerateLanterns(bool onlyMissing)
		{
			if (!onlyMissing)
			{
				_notes = new MusicNote[2000];
			}
			for (int i = 0; i < _notes.Length; i++)
			{
				if (!onlyMissing || !_notes[i].Active)
				{
					_notes[i].Position = new Vector2((float)(_random.Next(0, 1920) * 16), (float)_random.Next(-560, 1680));
					ResetLantern(i);
					_notes[i].Active = true;
				}
			}
			_lanternsDrawing = _notes.Length;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000306C File Offset: 0x0000126C
		public void ResetLantern(int i)
		{
			_notes[i].Depth = (1f - (float)Main.rand.Next(0, 2000) / (float)_notes.Length) * 4.4f + 1.6f;
			_notes[i].Speed = -1.5f - 2.5f * (float)_random.NextDouble();
			_notes[i].Texture = _texture.Value;
			_notes[i].Variant = _random.Next(3);
			_notes[i].TimeUntilFloat = (int)((float)(2000 + _random.Next(1200)) * 2f);
			_notes[i].TimeUntilFloatMax = _notes[i].TimeUntilFloat;
			_notes[i].startFade = false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000317C File Offset: 0x0000137C
		public override void Update(GameTime gameTime)
		{
			if (StarboundMusicConfig.instance == null)
			{
				return;
			}
			float velo = 0f;
			if (!Main.dayTime)
			{
				if (Main.gameMenu)
				{
					if (Main.time < 3200.0)
					{
						velo = 1f;
					}
					if (Main.time >= 29200.0)
					{
						velo = -1f;
					}
				}
				else
				{
					if (Main.time < 100.0)
					{
						velo = 1f;
					}
					if (Main.time >= 32300.0)
					{
						velo = -1f;
					}
				}
			}
			if (StarboundMusicConfig.instance.musicNoteInDay)
			{
				velo = 1f;
			}
			_opacity = Utils.Clamp(_opacity + velo * 0.01f, 0f, 1f);
			for (int i = 0; i < _notes.Length; i++)
			{
				if (_notes[i].Active)
				{
					_notes[i].startFade = (i >= 100 * StarboundMusicConfig.instance.howManyNotes);
					if (_notes[i].fade > 0f)
					{
						float num = 0.1f;
						float num2 = (float)Math.Sin((double)(_notes[i].Position.X / 120f));
                        MusicNote[] notes = _notes;
						int num3 = i;
						notes[num3].Position.Y = notes[num3].Position.Y + num2 * 0.5f;
                        MusicNote[] notes2 = _notes;
						int num4 = i;
						notes2[num4].Position.Y = notes2[num4].Position.Y + _notes[i].FloatAdjustedSpeed * 0.5f;
                        MusicNote[] notes3 = _notes;
						int num5 = i;
						notes3[num5].Position.X = notes3[num5].Position.X + (0.1f + num) * (3f - _notes[i].Speed) * 0.5f * ((float)i / (float)_notes.Length + 1.5f) / 2.5f;
						_notes[i].Rotation = num2 * (float)((num >= 0f) ? 1 : -1) * 0.5f;
						_notes[i].TimeUntilFloat = Math.Max(0, _notes[i].TimeUntilFloat - 1);
					}
					if (_notes[i].Position.Y < 0f)
					{
						if (_active)
						{
							_notes[i].startFade = true;
							if (_notes[i].fade == 0f)
							{
								ResetLantern(i);
								_notes[i].Position = new Vector2((float)(_random.Next(0, 1920) * 16), (float)_random.Next(-560, 1680));
							}
						}
						else
						{
							_notes[i].Active = false;
							_lanternsDrawing--;
						}
					}
					_notes[i].fade = Utils.Clamp(_notes[i].fade + (float)((_notes[i].startFade || !StarboundMusicModMenu.SkyEffectActive) ? -1 : 1) * 0.01f, 0f, 1f);
				}
			}
			_active = true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000034E0 File Offset: 0x000016E0
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (StarboundMusicConfig.instance == null || _texture.IsDisposed || _texture.Value.IsDisposed)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < _notes.Length; i++)
			{
				float depth = _notes[i].Depth;
				if (num == -1 && depth < maxDepth)
				{
					num = i;
				}
				if (depth <= minDepth)
				{
					break;
				}
				num2 = i;
			}
			if (num == -1)
			{
				return;
			}
			Vector2 value = new Vector2(960f, 560f);
			Color color = default(Color);
			Vector2 vector = default(Vector2);
			for (int j = num; j < num2; j++)
			{
				if (_notes[j].Active && _notes[j].fade > 0f)
				{
					color = new Color(60, 240, 250, 120);
					float num3 = 1f;
					if (_notes[j].Depth > 5f)
					{
						num3 = 0.3f;
					}
					else if ((double)_notes[j].Depth > 4.5)
					{
						num3 = 0.4f;
					}
					else if (_notes[j].Depth > 4f)
					{
						num3 = 0.5f;
					}
					else if ((double)_notes[j].Depth > 3.5)
					{
						num3 = 0.6f;
					}
					else if (_notes[j].Depth > 3f)
					{
						num3 = 0.7f;
					}
					else if ((double)_notes[j].Depth > 2.5)
					{
						num3 = 0.8f;
					}
					else if (_notes[j].Depth > 2f)
					{
						num3 = 0.9f;
					}
					color *= num3;
					vector = new Vector2(1f / _notes[j].Depth, 0.9f / _notes[j].Depth);
					vector *= 1.2f;
					Vector2 vector2 = _notes[j].Position;
					vector2 = (vector2 - value) * vector + value;
					vector2.X = (vector2.X + 500f) % 4000f;
					if (vector2.X < 0f)
					{
						vector2.X += 4000f;
					}
					vector2.X -= 500f;
                    MusicNote note = _notes[j];
					Color color2 = color;
					color2.A = 0;
					DrawNote(spriteBatch, note, color2 * StarboundMusicModMenu.GlowEffectFactor, vector, vector2, num3);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000037BC File Offset: 0x000019BC
		private void DrawNote(SpriteBatch spriteBatch, MusicNote note, Color opacity, Vector2 depthScale, Vector2 position, float alpha)
		{
			if (note.Texture.IsDisposed) return;
			float y = (Main.GlobalTimeWrappedHourly % 6f / 6f * 6.2831855f).ToRotationVector2().Y;
			float scale = y * 0.2f + 0.8f;
			Color color = new Color(255, 255, 255, 0) * _opacity * alpha * scale * 0.4f;
			for (int num = 0; num < 5; num++)
			{
				Vector2 value = new Vector2(0f, 2f).RotatedBy((double)(6.2831855f * (float)num * 0.2f + note.Rotation), default(Vector2)) * y;
				spriteBatch.Draw(note.Texture, position + value + _random.NextVector2Unit(0f, 6.2831855f) * 4f * _random.NextFloat(0f, 1f) * StarboundMusicModMenu.GlowEffectFactor, new Rectangle?(note.GetSourceRectangle()), color * note.fade, note.Rotation, note.GetSourceRectangle().Size() / 2f, depthScale.X * 2f, 0, 0f);
			}
			spriteBatch.Draw(note.Texture, position, new Rectangle?(note.GetSourceRectangle()), opacity * _opacity * note.fade, note.Rotation, note.GetSourceRectangle().Size() / 2f, depthScale.X * 2f, 0, 0f);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003985 File Offset: 0x00001B85
		public override void Activate(Vector2 position, params object[] args)
		{
			if (_active)
			{
				_leaving = false;
				GenerateLanterns(true);
				return;
			}
			GenerateLanterns(false);
			_active = true;
			_leaving = false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000039B3 File Offset: 0x00001BB3
		public override void Deactivate(params object[] args)
		{
			_leaving = true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000039BC File Offset: 0x00001BBC
		public override bool IsActive()
		{
			return _active;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000039C4 File Offset: 0x00001BC4
		public override void Reset()
		{
			_active = false;
		}

		// Token: 0x04000024 RID: 36
		private bool _active;

		// Token: 0x04000025 RID: 37
		private bool _leaving;

		// Token: 0x04000026 RID: 38
		private float _opacity;

		// Token: 0x04000027 RID: 39
		private Asset<Texture2D> _texture;

		// Token: 0x04000028 RID: 40
		private MusicNote[] _notes;

		// Token: 0x04000029 RID: 41
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x0400002A RID: 42
		private int _lanternsDrawing;

		// Token: 0x0400002B RID: 43
		private const float slowDown = 0.5f;

		// Token: 0x02000009 RID: 9
		private struct MusicNote
		{
			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600002A RID: 42 RVA: 0x000039E0 File Offset: 0x00001BE0
			// (set) Token: 0x0600002B RID: 43 RVA: 0x000039E8 File Offset: 0x00001BE8
			public Texture2D Texture
			{
				get
				{
					return _texture;
				}
				set
				{
					_texture = value;
					FrameWidth = value.Width / 3;
					FrameHeight = value.Height;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00003A0B File Offset: 0x00001C0B
			public float FloatAdjustedSpeed
			{
				get
				{
					return Speed * ((float)TimeUntilFloat / (float)TimeUntilFloatMax);
				}
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00003A23 File Offset: 0x00001C23
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(FrameWidth * Variant, 0, FrameWidth, FrameHeight);
			}

			// Token: 0x0400002E RID: 46
			private const int MAX_FRAMES_X = 3;

			// Token: 0x0400002F RID: 47
			public int Variant;

			// Token: 0x04000030 RID: 48
			public int TimeUntilFloat;

			// Token: 0x04000031 RID: 49
			public int TimeUntilFloatMax;

			// Token: 0x04000032 RID: 50
			private Texture2D _texture;

			// Token: 0x04000033 RID: 51
			public Vector2 Position;

			// Token: 0x04000034 RID: 52
			public float Depth;

			// Token: 0x04000035 RID: 53
			public float Rotation;

			// Token: 0x04000036 RID: 54
			public int FrameHeight;

			// Token: 0x04000037 RID: 55
			public int FrameWidth;

			// Token: 0x04000038 RID: 56
			public float Speed;

			// Token: 0x04000039 RID: 57
			public bool Active;

			// Token: 0x0400003A RID: 58
			public float fade;

			// Token: 0x0400003B RID: 59
			public bool startFade;
		}
	}
}
