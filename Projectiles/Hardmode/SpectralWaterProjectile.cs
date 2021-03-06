using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace WaterGuns.Projectiles.Hardmode
{
    public class SpectralWaterProjectile : BaseProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            AIType = ProjectileID.WaterGun;
            Projectile.timeLeft += 10;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.damage < 88)
            {
                Main.player[Projectile.owner].HeldItem.damage += 2;
                if (Main.player[Projectile.owner].HeldItem.damage >= 88)
                {
                    // Play sound to know it cant become stronger
                    SoundEngine.PlaySound(SoundID.Item4);
                }
            }

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI()
        {
            base.AI();
            base.CreateDust(default, 1f);
        }
    }
}
