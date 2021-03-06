using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace WaterGuns.Items.PreHardmode
{
    public class GoldWaterGun : BaseWaterGun
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Water Splitter");
            Tooltip.SetDefault("Shoots two streams of water");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 11;
            Item.knockBack = 3;

            base.defaultInaccuracy = 2;
            base.isOffset = true;
            base.offsetAmount = new Vector2(5f, 5f);
            base.offsetIndependent = new Vector2(0, -6);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 2; i++)
            {
                int distanceBetween = 4;
                Vector2 modifiedVelocity = velocity.RotatedBy(MathHelper.ToRadians(distanceBetween * i * player.direction));
                base.SpawnProjectile(player, source, position, modifiedVelocity, type, damage, knockback);
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("MoreWaterGuns:GoldBars", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
