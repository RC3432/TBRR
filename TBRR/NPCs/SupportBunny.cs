using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TBRR.NPCs
{
    public class SupportBunny  : ModNPC
    {
        int heal = 0;
        int spawn = 1;
        int attack = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 21;
            npc.damage = 3;
            npc.defense = 4;
            npc.knockBackResist = 0.0f;
            npc.width = 38;
            npc.height = 28;
            animationType = 46;
            npc.aiStyle = 3;
            aiType = 73;
            npc.npcSlots = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 1, 6);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Support Bunny");
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
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, 0, mod.ProjectileType("SupportRing"), 0, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, 0, mod.ProjectileType("SupportRing"), 0, 0f, 0);
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, 0, mod.ProjectileType("SupportRing"), 0, 0f, 0);
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
        public override void AI()
        {
            attack += 1;
            Lighting.AddLight(npc.position, 0.8f, 0.0f, 0.8f);
            Player player = Main.player[npc.target];
            heal += 1;
            if (heal > 59)
            {
                 Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                Projectile.NewProjectile(vector8.X, vector8.Y, 0, 0, mod.ProjectileType("SupportRing"), 0, 0f, 0);
                heal = 0;
            }
            if (player.dead)
            {
                heal += 1;
                npc.aiStyle = 0;
            }
            if (!player.dead)
            {
                npc.aiStyle = 3;
            }

  

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            var x = spawnInfo.spawnTileX;
            var y = spawnInfo.spawnTileY;
            var tile = (int)Main.tile[x, y].type;
            return Main.dayTime && player.statLifeMax <= 140 && NPC.downedBoss2 && y < Main.worldSurface ? 0.03f : 0f;
        }
    }
}