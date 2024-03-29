using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace WaterGuns.Items.PreHardmode
{
    public class BeeWaterGun : BaseWaterGun
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 20;
            Item.knockBack = 2;
            Item.shoot = ModContent.ProjectileType<Projectiles.PreHardmode.BeeWaterProjectile>();

            // base.offsetAmount = new Vector2(0, 0);
            base.offsetIndependent = new Vector2(0, -4);
            base.maxPumpLevel = 12;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, -2);
        }

        int delay = 2;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            base.SpawnProjectile(player, source, position, velocity, type, damage, knockback);

            delay += 1;
            if (delay >= 2)
            {
                delay = 0;
                int locDamage = 8;
                if (player.strongBees)
                {
                    locDamage = 12;
                }
                base.SpawnProjectile(player, source, position, velocity, ProjectileID.Bee, locDamage, knockback);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BeeWax, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
