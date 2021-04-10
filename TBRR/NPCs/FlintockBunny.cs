using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TBRR.NPCs
{
    public class FlintockBunny  : ModNPC
    {
        int regenCounter = 0;
        int reload_gun = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 2;
            npc.knockBackResist = 0.8f;
            npc.width = 38;
            npc.height = 28;
            animationType = 46;
            npc.aiStyle = 3;
            aiType = 73;
            npc.npcSlots = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 0, 10);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flintock Bunny");
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
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GunA"), 1f);
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
                    reload_gun = 0;
                }

            }
        }
        public override void NPCLoot()
        {
            int SetItem = 0;
            SetItem = Main.rand.Next(0, 11);
            if (SetItem == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FlintlockPistol);
            }

        }
        public override void AI()
        {
            reload_gun += 1;
            Player player = Main.player[npc.target];
            if (reload_gun == 64)
            {
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Gore.NewGore(npc.position, (npc.velocity*3)*-1, mod.GetGoreSlot("Gores/Shell"), 0.77f);
                int pro = Projectile.NewProjectile(vector8.X, vector8.Y, 5*npc.spriteDirection, 0, ProjectileID.Bullet, 11, 0f, 0);
                Main.projectile[pro].friendly = false;
                Main.projectile[pro].hostile = true;
                reload_gun = 0;
                Main.PlaySound(SoundID.Item11, (int)npc.position.X, (int)npc.position.Y);
                npc.velocity.X = ((npc.velocity.X/2)*-1);
            }
            if (player.dead)
            {
                reload_gun = 0;
            }


        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            var x = spawnInfo.spawnTileX;
            var y = spawnInfo.spawnTileY;
            var tile = (int)Main.tile[x, y].type;
            return Main.dayTime && player.statLifeMax <= 140 && NPC.downedBoss1 && y < Main.worldSurface ? 0.03f : 0f;
        }
    }
}