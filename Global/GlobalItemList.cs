using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Global
{
    public class GlobalItemList : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.WaterGun)
            {
                item.DamageType = DamageClass.Ranged;
                item.damage = 1;

                item.noMelee = true;
                item.autoReuse = true;
            }

            // Make bottled water ammo
            else if (item.type == ItemID.BottledWater)
            {
                item.ammo = ItemID.BottledWater;
                item.useAmmo = 0;
                item.value = 6;
            }
        }
    }
}
