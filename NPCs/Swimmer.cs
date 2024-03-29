using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;

namespace WaterGuns.NPCs
{
    [AutoloadHead]
    public class Swimmer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.npcFrameCount[NPC.type] = 23;
            NPCID.Sets.AttackFrameCount[NPC.type] = NPCID.Sets.AttackType[NPCID.Dryad];
            NPCID.Sets.ExtraFramesCount[NPC.type] = 0;
            NPCID.Sets.DangerDetectRange[NPC.type] = 250;
            NPCID.Sets.AttackType[NPC.type] = NPCID.Sets.AttackType[NPCID.ArmsDealer];
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Pirate, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Princess, AffectionLevel.Like)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Dislike)
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 28;
            NPC.height = 52;
            NPC.aiStyle = 7;
            NPC.defense = 5;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.1f;
            AnimationType = NPCID.Dryad;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ModContent.GoreType<Gores.Swimmer_Gore1>());
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ModContent.GoreType<Gores.Swimmer_Gore2>());
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ModContent.GoreType<Gores.Swimmer_Gore3>());
            }
            base.HitEffect(hit);
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Lana",
                "Marina",
                "Anna",
                "Minami",
                "Morgana",
                "Mary",
                "Aqua",
                "Ridley",
                "Sonia",
                "Nami",
                "Joyce"
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            base.OnChatButtonClicked(firstButton, ref shopName);

            if (firstButton)
            {
                shopName = "Shop";
            }
        }

        public override void AddShops()
        {
            new NPCShop(Type)
                .Add(ItemID.BottledWater)
                .Add<Items.PreHardmode.ShotgunWaterGun>(Condition.DownedKingSlime)
                .Add<Items.PreHardmode.ChainedWaterGun>(Condition.DownedQueenBee)
                .Add<Items.Hardmode.WaterBalloonGun>(Condition.Hardmode)
                .Register();
        }


        public override string GetChat()
        {
            NPC.FindFirstNPC(ModContent.NPCType<Swimmer>());
            switch (Main.rand.Next(4))
            {
                case 0: return "Hello! It's lovely weather today, isn't it?";
                case 1: return "I think sunny days are the best.";
                case 2: return "I love going to the beach!";
                case 3: return "Did you know Water Guns use Bottled Water as ammo?";
                default: return "DefaultText";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 6;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 30;
        }

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            Main.GetItemDrawFrame(ModContent.ItemType<Swimmer_Gun>(), out item, out itemFrame);
            scale = 0.9f;
            horizontalHoldoutOffset = 12;
            base.DrawTownAttackGun(ref item, ref itemFrame, ref scale, ref horizontalHoldoutOffset);
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Swimmer_Projectile>();
            attackDelay = 1;
        }


        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 10f;
        }
    }
}
