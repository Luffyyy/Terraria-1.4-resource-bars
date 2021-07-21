using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace CoolHealthManaBars.UI
{
    public enum ResourceBarType
    {
        HP,
        MP
    }

    class UIBarPanel : UIElement
    {
        private Texture2D SpriteLeft;
        private Texture2D SpriteMiddle;
        private Texture2D SpriteRight;
        private Texture2D SpriteFill;
        private Texture2D SpriteFill2;
        private UIText text;

        public ResourceBarType Type;

        public float Fill = 1f;
        private int _leftSpriteOffset = 0;

        public UIBarPanel(ResourceBarType type)
        {
            if (SpriteLeft == null)
            {
                Type = type;

                string typeStr = type == ResourceBarType.HP ? "HP" : "MP";
                SpriteLeft = ModContent.GetTexture($"CoolHealthManaBars/Textures/UI/Panel_Left");
                SpriteMiddle = ModContent.GetTexture($"CoolHealthManaBars/Textures/UI/{typeStr}_Panel_Middle");
                SpriteRight = ModContent.GetTexture($"CoolHealthManaBars/Textures/UI/{typeStr}_Panel_Right");
                SpriteFill = ModContent.GetTexture($"CoolHealthManaBars/Textures/UI/{typeStr}_Fill");
                if (type == ResourceBarType.HP)
                {
                    SpriteFill2 = ModContent.GetTexture("CoolHealthManaBars/Textures/UI/HP_Fill_Honey");
                }
            }

            text = new UIText("0 / 0", 0.8f)
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            }; //text to show current hp or mana

            _leftSpriteOffset = Type == ResourceBarType.HP ? -12 : -6;

            Append(text);

            int width = Type == ResourceBarType.HP ? 300 : 180;
            int height = 38;

            Width.Set(width, 0f);
            Height.Set(height, 0f);

            text.Width.Set(width - 52, 0f);
            text.Height.Set(24, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            CoolHealthManaBars.instance.Logger.Debug("Before: " + dimensions.X);
            //dimensions.X /= Main.UIScale;
            CoolHealthManaBars.instance.Logger.Debug("After: " + dimensions.X);

            Player player = Main.player[Main.myPlayer];

            int width = (int)dimensions.Width;

            Rectangle rectMiddle = new Rectangle((int)dimensions.X + 6, (int)dimensions.Y, width - 52, 24);
            Rectangle rectFill = new Rectangle(rectMiddle.X, (int)dimensions.Y + 6, (int)(rectMiddle.Width * Fill), 12);
            Rectangle rectLeft = new Rectangle(rectMiddle.X - 6, (int)dimensions.Y, 6, 24);
            Rectangle rectRight = new Rectangle(rectMiddle.Right - 16, (int)dimensions.Y + _leftSpriteOffset, 46, 38);

            // Center the text based on the center positio nof the rect middle
            text.Width.Set(rectMiddle.Width, 0f);
            text.Height.Set(rectFill.Height, 0f);
            text.Left.Set(-20f, 0f); // Dunno why 0 isn't actually in 0 (dimensions.X I assume)
            text.Top.Set(-6f, 0f);
            text.Recalculate();

            spriteBatch.Draw(SpriteLeft, rectLeft, Color.White);
            spriteBatch.Draw(SpriteRight, rectRight, Color.White);
            spriteBatch.Draw(SpriteMiddle, rectMiddle, Color.White);

            if (SpriteFill2 != null && player.statLifeMax >= 500)
            {
                spriteBatch.Draw(SpriteFill2, rectFill, Color.White);
            }
            else
            {
                spriteBatch.Draw(SpriteFill, rectFill, Color.White);
            }
        }

        public override void Update(GameTime t)
        {
            Player player = Main.player[Main.myPlayer];

            if (Type == ResourceBarType.HP) 
            { 
                text.SetText(player.statLife + " / " + player.statLifeMax2); //Set Life
                Fill = player.statLife / (float)player.statLifeMax2;
            } else
            {
                text.SetText(player.statMana + " / " + player.statManaMax2); //Set Mana
                Fill = player.statMana / (float)player.statManaMax2;
            }

            base.Update(t);
        }

    }
}
