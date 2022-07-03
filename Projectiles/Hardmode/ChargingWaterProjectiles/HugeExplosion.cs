using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Projectiles.Hardmode.ChargingWaterProjectiles
{
    public class HugeExplosion : BaseProjectile
    {
        int width = 256;
        int height = 256;
        public override void SetDefaults()
        {
            base.SetDefaults();
            AIType = ProjectileID.WaterGun;
            Projectile.height = height;
            Projectile.width = width;
            Projectile.timeLeft = 4;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                Main.npc[i].AddBuff(BuffID.Confused, 240);
            }
            for (int i = 0; i < 32; i++)
            {
                var rotation = Main.rand.Next(0, 360);
                var velocity = new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(rotation));
                velocity *= 3.6f;
                var dust = Dust.NewDustDirect(Projectile.Center, 16, 16, DustID.Wet, velocity.X, velocity.Y);
                dust.scale = 3f;
            }
            base.Kill(timeLeft);
        }
    }
}
