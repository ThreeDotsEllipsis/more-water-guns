using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Ammo.BottledWater
{
    public class EndlessBottledWater : BaseAmmo
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Endless");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.ammo = ItemID.BottledWater;
            Item.height = 20;
            Item.width = 20;
            Item.consumable = false;
            Item.maxStack = 1;
        }
    }
}
