using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Items.Hardmode
{
    public class SpectralWaterGun : BaseWaterGun
    {
        int normalDamage = 0;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hitting enemies increases damage. Right click to release wisps");
        }

        public override bool AltFunctionUse(Player player)
        {
            int soulsDamage = Item.damage - normalDamage;
            Item.damage = normalDamage;

            int soulsNumber = soulsDamage / 4;

            soulsNumber = soulsNumber > 10 ? 10 : soulsNumber;

            soulsDamage *= 2;

            var angle = player.position.AngleTo(Main.MouseWorld);
            for (int i = 0; i < soulsNumber; i++)
            {
                Projectile.NewProjectileDirect(Projectile.GetSource_NaturalSpawn(), player.position, new Vector2(10, 0).RotatedBy(angle).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.LostSoulFriendly, soulsDamage, 5, player.whoAmI);
            }

            return base.AltFunctionUse(player);
        }

        public override void HoldItem(Player player)
        {
            if (Main.mouseRight)
            {
                this.AltFunctionUse(player);
            }
            base.HoldItem(player);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 38;
            Item.knockBack = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hardmode.SpectralWaterProjectile>();
            Item.useTime -= 2;
            Item.useAnimation -= 2;

            normalDamage = Item.damage;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpectreBar, 18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}