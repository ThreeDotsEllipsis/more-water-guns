using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WaterGuns.Projectiles.Sea;

namespace WaterGuns.Players;

public class SeaSource : IEntitySource
{
    public NPC Target;

    public string Context { get; set; }
    public SeaSource(IEntitySource source)
    {
        Context = source.Context;
    }
}

public class SeaPlayer : ModPlayer
{
    public Dictionary<NPC, HugeBubble> _bubbles { get; private set; }
    public int ProjectileDamage { get; set; }

    public SeaPlayer()
    {
        _bubbles = new();
    }

    public bool CanHome(NPC target)
    {
        if (!_bubbles.ContainsKey(target))
            return true;

        return !_bubbles[target].IsMaxStage;
    }

    public void AddBubble(NPC target)
    {
        if (!_bubbles.ContainsKey(target))
        {
            var source = new SeaSource(Projectile.GetSource_NaturalSpawn());
            source.Target = target;

            var projectile = Projectile.NewProjectileDirect(
                source, target.Center, Vector2.Zero, ModContent.ProjectileType<HugeBubble>(), 0, 0, Main.myPlayer);

            _bubbles[target] = projectile.ModProjectile as HugeBubble;
        }

        _bubbles[target].Enlarge();
    }

    public bool BubbleCollide(Rectangle rect)
    {
        foreach (var bubble in _bubbles.Values)
        {
            if (bubble.IsMaxStage && bubble.WorldRectangle.Intersects(rect))
            {
                bubble.Explode();

                return true;
            }
        }

        return false;
    }

    public void RemoveBubble(NPC target)
    {
        _bubbles.Remove(target);
    }
}
