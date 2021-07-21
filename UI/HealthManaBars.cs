using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace CoolHealthManaBars.UI 
{
    public class HealthManaBars : UIState
    {
        public static bool visible = false;
        private float oldScale = 1f;

        UIBarPanel HPBar;
        UIBarPanel MPBar;

        public override void OnInitialize()
        {
            HPBar = new UIBarPanel(ResourceBarType.HP);
            Append(HPBar);
            MPBar = new UIBarPanel(ResourceBarType.MP);
            Append(MPBar);
        }

        public override void Recalculate()
        {
            if (HPBar != null)
            {
                HPBar.Left.Set(Main.screenWidth - 10f - HPBar.Width.Pixels, 0f);
                HPBar.Top.Set(24f, 0f);

                MPBar.Left.Set(HPBar.Left.Pixels + HPBar.Width.Pixels - MPBar.Width.Pixels - 10f, 0f);
                MPBar.Top.Set(HPBar.Top.Pixels + HPBar.Height.Pixels - 16f, 0f);
            }

            base.Recalculate();
        }

        public override void Update(GameTime t)
        {
            base.Update(t);
            if (oldScale != Main.UIScale)
            {
                oldScale = Main.UIScale;
                CoolHealthManaBars.instance.Logger.Debug("Doing recalculation!");
                Recalculate();
            }
        }
    }
}
