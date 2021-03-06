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
            DisplayName.SetDefault("Bee Liquifier");
            Tooltip.SetDefault("Spawns bees and slows down enemies");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 20;
            Item.knockBack = 2;
            Item.shoot = ModContent.ProjectileType<Projectiles.PreHardmode.BeeWaterProjectile>();
        }

        int delay = 2;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            delay += 1;
            if (delay >= 2)
            {
                delay = 0;
                base.SpawnProjectile(player, source, position, velocity, ProjectileID.Bee, 8, knockback);
            }
            base.SpawnProjectile(player, source, position, velocity, type, damage, knockback);
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
