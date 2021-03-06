using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Items.Hardmode
{
    public class IchorWaterGun : BaseWaterGun
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Rainer");
            Tooltip.SetDefault("Inflicts the ichor debuff");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 35;
            Item.knockBack = 3;
            Item.scale = 0.8f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hardmode.IchorWaterProjectile>();
            base.defaultInaccuracy = 4;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float offsetInaccuracy = CalculateAccuracy(0.3f);

            // Put it above the mouse
            // Could create complications if zoomed out too much
            // Projectiles will not reach all the way to the bottom
            position.Y -= Main.ViewSize.Y / 1.5f;
            position.X = Main.MouseWorld.X;

            for (int i = 0; i < 4; i++)
            {
                var modifiedVelocity = new Vector2(0, 14);
                position.X = position.RotatedByRandom(MathHelper.ToRadians(offsetInaccuracy)).X;

                base.SpawnProjectile(player, source, position, modifiedVelocity, type, damage, knockback);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ichor, 18);
            recipe.AddIngredient(ModContent.ItemType<PreHardmode.CrimsonWaterGun>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
