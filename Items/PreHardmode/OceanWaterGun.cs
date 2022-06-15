using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace WaterGuns.Items.PreHardmode
{
    public class OceanWaterGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("BasicSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Spawns additional bubbles");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WaterGun);

            Item.damage = 10;
            Item.knockBack = 2;
            Item.shoot = ModContent.ProjectileType<Projectiles.PreHardmode.OceanWaterProjectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Make it a little inaccurate
            Vector2 modifiedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
            Projectile.NewProjectile(source, position, modifiedVelocity, type, damage, knockback, player.whoAmI);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 4);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Seashell, 12);
            recipe.AddIngredient(ItemID.Starfish, 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
