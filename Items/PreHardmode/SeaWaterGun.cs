using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Items.PreHardmode
{
    public class SeaWaterGun : BaseWaterGun
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 8;
            Item.knockBack = 2;
            Item.shoot = ModContent.ProjectileType<Projectiles.PreHardmode.SeaWaterProjectile>();

            base.maxPumpLevel = 11;
            base.offsetIndependent = new Vector2(0, -2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var _pumpLevel = pumpLevel;
            if (pumpLevel >= maxPumpLevel)
            {
                for (int i = -1; i < 2; i++)
                {
                    var modifiedVelocity = velocity.RotatedBy(MathHelper.ToRadians(Main.rand.Next(5, 10) * i));
                    // Slow down the starfish
                    modifiedVelocity /= Main.rand.NextFloat(1f, 1.5f);
                    base.SpawnProjectile(player, source, position, modifiedVelocity, ModContent.ProjectileType<Projectiles.PreHardmode.StarfishProjectile>(), damage / 5, knockback);
                    pumpLevel = _pumpLevel;
                }
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Seashell, 12);
            recipe.AddIngredient(ItemID.Starfish, 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
