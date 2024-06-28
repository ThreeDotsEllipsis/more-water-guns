using Microsoft.Xna.Framework;
using Terraria;

namespace WaterGuns.Projectiles.Modules;

public abstract class BaseProjectileModule
{
    protected BaseProjectileModule(BaseProjectile baseProjectile)
    {
        baseProjectile.AddModule(this);
    }

    public virtual bool RuntimeTileCollide(BaseProjectile baseProjectile, Vector2 oldVelocity)
    {
        return true;
    }

    public virtual void RuntimeHitNPC(BaseProjectile baseProjectile, NPC target, NPC.HitInfo hit)
    {
    }

    public virtual void RuntimeAI(BaseProjectile baseProjectile)
    {
    }
}
