using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.Items
{
    public class IronWaterGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("BasicSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WaterGun);

            Item.damage = 10;
            Item.knockBack = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
