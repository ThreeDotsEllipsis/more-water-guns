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
            Tooltip.SetDefault("Inflicts ichor debuff");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 35;
            Item.knockBack = 3;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hardmode.IchorWaterProjectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Put it above the mouse
            // Could create complications if zoomed out too much
            // Projectiles will not reach all the way to the bottom
            position.Y -= Main.ViewSize.Y / 1.5f;
            position.X = Main.MouseWorld.X;

            // Speed them up a bit
            int projectileSpeed = 14;

            // Chage accuracy
            float inaccuracy = player.GetModPlayer<GlobalPlayer>().CalculateAccuracy(4);

            for (int i = 0; i < 4; i++)
            {
                var modifiedVelocity = new Vector2(0, 1).RotatedByRandom(MathHelper.ToRadians(inaccuracy));
                position.X = position.RotatedByRandom(MathHelper.ToRadians(0.4f)).X;
                modifiedVelocity *= projectileSpeed;

                Projectile.NewProjectile(source, position, modifiedVelocity, type, damage, knockback, player.whoAmI);
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
