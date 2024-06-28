using Microsoft.Xna.Framework;

namespace WaterGuns.Projectiles.Modules;

public class PropertyModule : BaseProjectileModule
{
    public float DefaultGravity { get; set; }
    public int DefaultTime { get; set; }

    public float Gravity { get; set; }
    public float GravityChange { get; set; }

    public PropertyModule(BaseProjectile baseProjectile) : base(baseProjectile)
    {
    }

    public void SetDefaults(BaseProjectile baseProjectile)
    {
        baseProjectile.Projectile.damage = 0;
        baseProjectile.Projectile.penetrate = 0;
        baseProjectile.Projectile.timeLeft = 0;

        baseProjectile.Projectile.width = 0;
        baseProjectile.Projectile.height = 0;

        DefaultTime = baseProjectile.Projectile.timeLeft;
        baseProjectile.Projectile.hostile = false;
        baseProjectile.Projectile.friendly = true;
    }

    public void SetDefaultGravity()
    {
        Gravity = 0.01f;
        DefaultGravity = Gravity;
        GravityChange = 0.02f;
    }

    public Vector2 ApplyGravity(Vector2 velocity)
    {
        Gravity += GravityChange;
        velocity.Y += Gravity;

        return velocity;
    }
}
