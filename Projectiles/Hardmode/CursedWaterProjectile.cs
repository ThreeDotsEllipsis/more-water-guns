using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Projectiles.Hardmode
{
    public class CursedWaterProjectile : BaseProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            AIType = ProjectileID.WaterGun;

            base.affectedByAmmoBuff = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.height == 16)
            {
                for (int i = -1; i < 2; i += 2)
                {
                    int rotation = Main.rand.Next(0, 360);
                    var randomPosition = target.Center + new Vector2(256, 0).RotatedBy(MathHelper.ToRadians(rotation));
                    var modifiedVelocity = new Vector2(10, 0).RotatedBy(MathHelper.ToRadians(rotation - 180));

                    // Spawn default water projectile
                    var proj = Projectile.NewProjectileDirect(data, randomPosition, modifiedVelocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    proj.tileCollide = false;
                    proj.height -= 1;
                }
            }

            target.AddBuff(BuffID.CursedInferno, 240);
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);


            data.color = new Color(96, 248, 2);
            data.dustScale = 1.4f;
        }

        int delay = 0;
        int delayMax = 5;
        public override void AI()
        {
            base.AI();


            base.CreateDust();

            // Cursed Flame dust that emits light
            var dust2 = Dust.NewDust(Projectile.position, 5, 5, DustID.CursedTorch, 0, 0, 0, default, 1.6f);
            Main.dust[dust2].fadeIn = 1.2f;
            Main.dust[dust2].noGravity = true;
        }
    }
}
