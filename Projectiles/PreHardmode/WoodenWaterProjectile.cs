using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;

namespace WaterGuns.Projectiles.PreHardmode
{
    public class AcornProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            AIType = ProjectileID.WoodenArrowFriendly;

            Projectile.damage = 1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;

            Projectile.width = 28;
            Projectile.height = 28;

            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            var dust = Dust.NewDust(Projectile.Center, 16, 16, 7, 0, 0, 0, default, 1);
            Main.dust[dust].noGravity = true;
        }
    }

    public class TreeSlam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 33;

            Projectile.width = 76;
            Projectile.height = 146;

            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        int direction;
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);

            Projectile.ai[0] = 5f;
            Projectile.alpha = 255;

            direction = Main.rand.NextFromList(new int[] { -1, 1 });

            Projectile.position.X += 80 * direction;
            Projectile.position.Y -= 100;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14);

            for (int i = 0; i < 12; i++)
            {
                var rotation = Main.rand.Next(-90, 90);
                var velocity = new Vector2(0, -1.4f).RotatedBy(MathHelper.ToRadians(rotation));
                velocity *= 8f;
                var dust = Dust.NewDustDirect(Projectile.Center + new Vector2(0, Projectile.Size.Y / 3), 8, 8, DustID.Cloud, velocity.X, velocity.Y, 75, default);
                dust.scale = 4;
                dust.noGravity = true;
            }
            base.OnKill(timeLeft);
        }

        int delay = 0;
        int delay2 = 0;
        public override void AI()
        {
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1f;
                Projectile.alpha -= 255 / 5;
            }

            if (delay < 4)
                delay += 1;
            else
            {
                if (delay2 < 12)
                {
                    delay2 += 1;
                    Projectile.rotation += 0.14f * direction;
                }
                else
                {
                    Projectile.rotation -= 0.26f * direction;
                    Projectile.velocity = new Vector2(-4f * direction, 5f);
                }
            }

            base.AI();
        }
    }

    public class WoodenWaterProjectile : BaseProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            AIType = ProjectileID.WaterGun;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.player[Main.myPlayer].IsTileTypeInInteractionRange(TileID.Trees, TileReachCheckSettings.Simple) || Main.player[Main.myPlayer].IsTileTypeInInteractionRange(TileID.PalmTree, TileReachCheckSettings.Simple))
            {
                hit.Damage += hit.Damage / 2;
            }

            Vector2 position = target.Center;

            if (!data.fullCharge)
            {
                position.X += Main.rand.Next(-10, 10);
                position.Y -= 320 + Main.rand.Next(-20, 20);
            }

            var attackType = data.fullCharge ? ModContent.ProjectileType<TreeSlam>() : ModContent.ProjectileType<AcornProjectile>();
            var velocity = data.fullCharge ? Vector2.Zero : new Vector2(0, 10);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, velocity, attackType, hit.Damage, Projectile.knockBack, Projectile.owner);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
