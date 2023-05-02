using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Projectiles.PreHardmode
{
    public class SwordSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.timeLeft = 55;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.extraUpdates = 0;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.alpha = 75;
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = MathHelper.PiOver4;
            direction = Main.rand.NextBool() ? -1 : 1;
            rotateAmount = Main.rand.NextFloat(-0.03f, 0.03f);
            delayRandomness = Main.rand.Next(-2, 2);
            Projectile.ai[0] = 17f;
            Projectile.alpha = 255;

            base.OnSpawn(source);
        }

        int delay = 10;
        int delayRandomness = 0;
        bool pastTarget = false;
        int direction = 0;
        float rotateAmount = 0;

        public override void AI()
        {
            delay += 1;
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1;
                Projectile.alpha -= 255 / 10;
            }

            if (pastTarget)
            {
                Projectile.rotation += (0.26f + rotateAmount) * direction;
                Projectile.alpha += 255 / 15;
            }
            else if (delay > 46 + delayRandomness)
            {
                pastTarget = true;
                Projectile.velocity = -Projectile.velocity / 2f;
            }
        }

    }

    public class DemonWaterProjectile : BaseProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            AIType = ProjectileID.WaterGun;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (data.fullCharge)
            {
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item8);

                int rotation = Main.rand.Next(0, 360);
                var randomPosition = target.Center + new Vector2(256, 0).RotatedBy(MathHelper.ToRadians(-45)).RotatedBy(MathHelper.ToRadians(rotation));
                var modifiedVelocity = new Vector2(10, 0).RotatedBy(MathHelper.ToRadians(rotation - 180 - 45));

                var proj = Projectile.NewProjectileDirect(base.data, randomPosition, modifiedVelocity, ModContent.ProjectileType<Projectiles.PreHardmode.SwordSlash>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                proj.rotation = MathHelper.ToRadians(rotation - 180);
                proj.scale = 2;

                rotation = rotation - 180 + Main.rand.Next(-45, 45);
                randomPosition = target.Center + new Vector2(256, 0).RotatedBy(MathHelper.ToRadians(-45)).RotatedBy(MathHelper.ToRadians(rotation));
                modifiedVelocity = new Vector2(10, 0).RotatedBy(MathHelper.ToRadians(rotation - 180 - 45));

                var proj2 = Projectile.NewProjectileDirect(base.data, randomPosition, modifiedVelocity, ModContent.ProjectileType<Projectiles.PreHardmode.SwordSlash>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
                proj2.rotation = MathHelper.ToRadians(rotation - 180);
                proj2.scale = 2;
            }
            else
            {

                int rotation = Main.rand.Next(-90, 0);
                var randomPosition = target.Center + new Vector2(256, 0).RotatedBy(MathHelper.ToRadians(-45)).RotatedBy(MathHelper.ToRadians(rotation));
                var modifiedVelocity = new Vector2(10, 0).RotatedBy(MathHelper.ToRadians(rotation - 180 - 45));

                // Spawn default water projectile
                Projectile.NewProjectile(base.data, randomPosition, modifiedVelocity, ModContent.ProjectileType<Projectiles.PreHardmode.SimpleWaterProjectile>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
