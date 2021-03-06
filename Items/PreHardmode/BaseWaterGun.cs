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
            Item.shoot = ModContent.ProjectileType<Projectiles.PreHardmode.SimpleWaterProjectile>();
            Item.useAmmo = ItemID.BottledWater;
        }

        public float CalculateAccuracy(float inaccuracy = 3.3f)
        {
            return Main.player[Main.myPlayer].GetModPlayer<GlobalPlayer>().CalculateAccuracy(inaccuracy);
        }

        public float CalculateSpeed()
        {
            return Main.player[Main.myPlayer].GetModPlayer<GlobalPlayer>().CalculateSpeed();
        }

        protected bool isOffset = true;
        protected float defaultInaccuracy = 3.3f;
        protected Vector2 offsetAmount = new Vector2(4, 4);
        protected Vector2 offsetIndependent = new Vector2(0, 0);
        public Projectile SpawnProjectile(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            WaterGuns.ProjectileData data = new WaterGuns.ProjectileData(source);

            // Ammo Inflicts Statuses ------------------------------------------------------------
            if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledBathWater>())
            {
                data.hasBuff = true;
                data.buffType = BuffID.Confused;
                data.buffTime = 240;

                data.color = new Color(247, 2, 248);
            }
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledIchor>())
            {
                data.hasBuff = true;
                data.buffType = BuffID.Ichor;
                data.buffTime = 240;

                data.color = new Color(255, 250, 41);
            }
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledVenom>())
            {
                data.hasBuff = true;
                data.buffType = BuffID.Venom;
                data.buffTime = 240;

                data.color = new Color(173, 103, 230);
            }
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledPoison>())
            {
                data.hasBuff = true;
                data.buffType = BuffID.Poisoned;
                data.buffTime = 240;

                data.color = new Color(0, 194, 129);
            }
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledCursedFire>())
            {
                data.hasBuff = true;
                data.buffType = BuffID.CursedInferno;
                data.buffTime = 240;

                data.color = new Color(96, 248, 2);
            }
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledCryogel>())
            {
                data.hasBuff = true;
                data.buffType = BuffID.Frostburn;
                data.buffTime = 240;

                data.color = new Color(67, 100, 176);
            }
            // -----------------------------------------------------------------------------------------

            // Ammo Special Effects
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledChlorophyte>())
            {
                data.homesIn = true;

                data.color = new Color(17, 143, 36);
            }
            else if (source.AmmoItemIdUsed == ModContent.ItemType<Ammo.BottledWater.BottledMeteorite>())
            {
                data.bounces = true;

                data.color = new Color(150, 56, 147);
            }

            float inaccuracy = CalculateAccuracy(defaultInaccuracy);
            // All of them use custom projectiles that shoot straight 
            // Make them a little inaccurate like in-game water gun
            Vector2 modifiedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(inaccuracy)) * CalculateSpeed();
            // Offset if need be
            var offset = isOffset ? new Vector2(position.X + velocity.X * offsetAmount.X, position.Y + velocity.Y * offsetAmount.Y) : position;
            var proj = Projectile.NewProjectileDirect(data, offset + offsetIndependent, modifiedVelocity, type, damage, knockback, player.whoAmI);
            return proj;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SpawnProjectile(player, source, position, velocity, type, damage, knockback);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            // So holding guns look like in-game water gun
            return new Vector2(0, 4);
        }
    }
}
