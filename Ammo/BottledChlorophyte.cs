using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Ammo
{
    public class BottledChlorophyte : BaseAmmo
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            base.damage = 9;
            base.homesIn = true;
            base.color = new Color(17, 143, 36);

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 50);
            recipe.AddIngredient(ItemID.ChlorophyteOre, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.ReplaceResult(Item.type, 50);
            recipe.Register();
        }
    }
}
