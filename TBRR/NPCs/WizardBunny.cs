using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TBRR.NPCs
{
    public class WizardBunny : ModNPC
    {
        int regenCounter = 0;
        int reload_spell = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 20;
            npc.damage = 3;
            npc.defense = 11;
            npc.knockBackResist = 0.8f;
            npc.width = 38;
            npc.height = 28;
            animationType = 46;
            npc.aiStyle = 3;
            aiType = 73;
            npc.npcSlots = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 8, 5);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Bunny");
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
				Gore.NewGore(npc.position, npc.velocity, 76, 1f);
				Gore.NewGore(npc.position, npc.velocity, 77, 1f);
                Gore.NewGore(npc.position, npc.velocity, 77, 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Wizard_Hat"), 1f);
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
                    reload_spell = 0;
                }

            }
        }
        public override void NPCLoot()
        {
            int SetItem = 0;
            SetItem = Main.rand.Next(0, 11);
            if (SetItem == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.WizardHat);
            }

        }
        public override void AI()
        {
            
            Dust.NewDust(npc.position, npc.width, npc.height, 27, 0 + Main.rand.Next(-1, 1), 0 + Main.rand.Next(-4, -1), 100, default, 0.5f);
            Lighting.AddLight(npc.position, 0.8f, 0.0f, 0.8f);
            reload_spell += 1;
            Player player = Main.player[npc.target];
            if (reload_spell == 100)
            {
                int chaorb = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.ChaosBall);
                int chaorb2 = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y - 50, NPCID.ChaosBall);
                int chaorb3 = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 50, NPCID.ChaosBall);
                Main.npc[chaorb].damage = npc.damage*2;
                Main.npc[chaorb].knockBackResist = 1f;
                Main.npc[chaorb].velocity *= 2;
                Main.npc[chaorb2].damage = npc.damage * 2;
                Main.npc[chaorb2].knockBackResist = 1f;
                Main.npc[chaorb2].velocity *= 2;
                Main.npc[chaorb3].damage = npc.damage * 2;
                Main.npc[chaorb3].knockBackResist = 1f;
                Main.npc[chaorb3].velocity *= 2;
                Main.PlaySound(SoundID.Item8, (int)npc.position.X, (int)npc.position.Y);
            }
            if (reload_spell == 100)
            {
                npc.aiStyle = 0;
            }
            if (reload_spell == 400)
            {
                int numberProjectileseB = Main.rand.Next(16, 23);
                for (int f = 0; f < numberProjectileseB; f++)
                {
                    int num135 = Dust.NewDust(npc.position, npc.width, npc.height, 27, 0 + Main.rand.Next(-5, 5), 0 + Main.rand.Next(-5, 5), 100, default, 2);
                }
                npc.aiStyle = 3;
                npc.position.X = player.position.X+(Main.rand.Next(-70, -56)* player.direction);
                npc.position.Y = player.position.Y;
                int numberProjectilese = Main.rand.Next(16, 23);
                for (int i = 0; i < numberProjectilese; i++)
                {
                    int num138 = Dust.NewDust(npc.position, npc.width, npc.height, 27, 0 + Main.rand.Next(-5, 5), 0 + Main.rand.Next(-5, 5), 100, default, 2);
                }
                reload_spell = 0;
            }
            if (player.dead)
            {
                reload_spell = 0;
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            var x = spawnInfo.spawnTileX;
            var y = spawnInfo.spawnTileY;
            var tile = (int)Main.tile[x, y].type;
            return !Main.dayTime && NPC.downedBoss3 && y < Main.worldSurface ? 0.03f : 0f;
        }
    }
}