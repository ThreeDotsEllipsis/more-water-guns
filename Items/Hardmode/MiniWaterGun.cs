using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WaterGuns.Items.Hardmode
{
    public class MiniWaterGun : BaseWaterGun
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Mini Water Gun");
            Tooltip.SetDefault("Rapid but inaccurate\nDrops from SantaNK1");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 45;
            Item.knockBack = 4;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hardmode.MiniWaterProjectile>();
            base.defaultInaccuracy = 8;

            base.offsetAmount = new Vector2(6, 6);
            base.offsetIndependent = new Vector2(0, 14);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, 14);
        }

        Projectile turret = null;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!player.HasBuff<Buffs.TurretSummonBuff>())
            {
                if (pumpLevel >= maxPumpLevel)
                {
                    player.AddBuff(ModContent.BuffType<Buffs.TurretSummonBuff>(), 60 * 100);
                    turret = Projectile.NewProjectileDirect(Projectile.GetSource_NaturalSpawn(), player.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Hardmode.TurretWaterProjectile>(), 0, 0, player.whoAmI);
                }
            }
            base.SpawnProjectile(player, source, position, velocity, type, damage, knockback);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddCondition(NetworkText.FromLiteral("Mods.WaterGuns.Conditions.Never"), (_) => false);
            recipe.Register();
        }
    }
}
