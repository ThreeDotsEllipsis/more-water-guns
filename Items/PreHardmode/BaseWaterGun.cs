using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Items.PreHardmode
{
    public abstract class BaseWaterGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WaterGun);
        }

        protected bool isOffset = false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // All of them use custom projectiles that shoot straight 
            // Make them a little inaccurate like in-game water gun
            Vector2 modifiedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3.3f));
            // Offset if need be
            var offset = isOffset ? new Vector2(position.X + velocity.X * 4, position.Y + velocity.Y * 4) : position;
            Projectile.NewProjectile(source, offset, modifiedVelocity, type, damage, knockback, player.whoAmI);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            // So holding guns look like in-game water gun
            return new Vector2(0, 4);
        }
    }
}
