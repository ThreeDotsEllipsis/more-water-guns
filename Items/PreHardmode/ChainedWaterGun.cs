using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WaterGuns.Items.PreHardmode
{
    public class ChainedWaterGun : BaseWaterGun
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ChainGuillotines);
            Item.useAmmo = ItemID.BottledWater;

            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.damage = 21;
            Item.knockBack = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.PreHardmode.ChainedWaterProjectile>();
            Item.rare = ItemRarityID.White;

            Item.value = Item.buyPrice(0, 10, 30, 0);
            base.maxPumpLevel = 14;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddCondition(LocalizedText.Empty, () => false);
            recipe.Register();
        }
    }
}
