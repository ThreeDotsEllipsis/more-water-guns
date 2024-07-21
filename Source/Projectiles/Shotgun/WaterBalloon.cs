using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using WaterGuns.Projectiles.Modules;
using WaterGuns.Utils;

namespace WaterGuns.Projectiles.Shotgun;

public class WaterBalloon : BaseProjectile
{
    public override string Texture => TexturesPath.Projectiles + "WaterBalloon";

    public PropertyModule Property { get; private set; }
    public WaterModule Water { get; private set; }

    public WaterBalloon() : base()
    {
        Property = new PropertyModule(this);
        Water = new WaterModule(this);
    }

    public override void SetDefaults()
    {
        base.SetDefaults();

        Property.SetDefaults(this);
        Property.SetDefaultGravity();
        Property.SetTimeLeft(this, 25);

        Projectile.CritChance = 100;
        Projectile.damage = 1;
        Projectile.penetrate = 1;

        Projectile.width = 22;
        Projectile.height = 22;
    }

    public override void OnKill(int timeLeft)
    {
        base.OnKill(timeLeft);

        Water.KillEffect(Projectile.Center, Vector2.Zero);
    }

    public override void AI()
    {
        base.AI();

        Projectile.velocity = Property.ApplyGravity(Projectile.velocity);
    }
}
