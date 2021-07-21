using CoolHealthManaBars.UI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CoolHealthManaBars
{
	public class CoolHealthManaBars : Mod
	{
        public static CoolHealthManaBars instance;
        public UserInterface CustomResources;
        public HealthManaBars HealthManaBars;

        public bool isSetup = false;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int i = layers.FindIndex((Predicate<GameInterfaceLayer>)(layer => ((string)layer.Name).Equals("Vanilla: Resource Bars")));
            if (i == -1)
            {
                return;
            }

            layers.Insert(i, new LegacyGameInterfaceLayer("CustomBars: Custom Resource Bar", delegate {
            if (HealthManaBars.visible)
            {
                //Update CustomBars
                CustomResources.Update(Main._drawInterfaceGameTime);
                HealthManaBars.Draw(Main.spriteBatch);
            }
            return true;
            }, InterfaceScaleType.UI));

            // Removes old resources
            if (!isSetup)
            {
                On.Terraria.Main.DrawInterface_Resources_Life += Main_DrawInterface_Resources_Life1;
                On.Terraria.Main.DrawInterface_Resources_Mana += Main_DrawInterface_Resources_Mana;
                isSetup = true;
            }
        }

        private void Main_DrawInterface_Resources_Mana(On.Terraria.Main.orig_DrawInterface_Resources_Mana orig)
        {
            
        }

        private void Main_DrawInterface_Resources_Life1(On.Terraria.Main.orig_DrawInterface_Resources_Life orig)
        {
            
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                instance = this;
                CustomResources = new UserInterface();
                HealthManaBars = new HealthManaBars();
                HealthManaBars.visible = true;
                CustomResources.SetState(HealthManaBars);
            }
        }
    }
}