using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace WaterGuns
{
    public class WaterGuns : Mod
    {
        public override void AddRecipeGroups()
        {
            Terraria.RecipeGroup goldBars = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            Terraria.RecipeGroup.RegisterGroup("MoreWaterGuns:GoldBars", goldBars);

            Terraria.RecipeGroup titaniumBars = new Terraria.RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Titanium Bar", new int[]
            {
                ItemID.TitaniumBar,
                ItemID.AdamantiteBar
            });
            Terraria.RecipeGroup.RegisterGroup("MoreWaterGuns:TitaniumBars", titaniumBars);
            base.AddRecipeGroups();
        }
    }
}
