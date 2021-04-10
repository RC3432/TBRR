using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TBRR.NPCs
{
    public class TungstenWarriorBunny : ModNPC
    {
        int regenCounter = 0;
        int SetItem = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 50;
            npc.damage = 18;
            npc.defense = 15;
            npc.knockBackResist = 0.5f;
            npc.width = 38;
            npc.height = 28;
            animationType = 46;
            npc.aiStyle = 3;
            aiType = 47;
            npc.npcSlots = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 6, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Warrior Bunny");
            Main.npcFrameCount[npc.type] = 7;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.2f);
            npc.damage = (int)(npc.damage * 1.2f);
        }

   

        public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 0.7f);
                }
				Gore.NewGore(npc.position, npc.velocity, 77, 1f);
                Gore.NewGore(npc.position, npc.velocity, 77, 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TungSword"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TungHelmet"), 1f);
            }
            if (npc.life >= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 1f);
                }

            }
        }
        public override void NPCLoot()
        {
            SetItem = Main.rand.Next(0, 28);
            if (SetItem == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TungstenShortsword);
            }
            if (SetItem == 2)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TungstenHelmet);
            }
            if (SetItem == 3)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TungstenChainmail);
            }
            if (SetItem == 4)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TungstenGreaves);
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (player.dead)
            {
                npc.aiStyle = 0;
                aiType = 47;
                regenCounter += 1;
                if (regenCounter == 30 && npc.life < npc.lifeMax)
                {
                    npc.life += 1;
                    regenCounter = 0;
                }    
                if (npc.life > npc.lifeMax)
                {
                    npc.life = npc.lifeMax;
                }
            }
            if (!player.dead)
            {
                npc.aiStyle = 3;
                npc.friendly = false;
                aiType = 47;
                regenCounter = 0;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            var x = spawnInfo.spawnTileX;
            var y = spawnInfo.spawnTileY;
            var tile = (int)Main.tile[x, y].type;
            return Main.dayTime && player.statLifeMax <= 220 && player.statLifeMax >= 200 && y < Main.worldSurface ? 0.03f : 0f;
        }
    }
}