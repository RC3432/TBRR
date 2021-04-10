using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using System;
using Terraria.ModLoader;

namespace TBRR.NPCs
{
    public class NinjaBunny : ModNPC
    {
        int regenCounter = 0;
        int SetItem = 0;
        int toss_se = 0;
        bool said_retreat = false;
        public override void SetDefaults()
        {
            npc.lifeMax = 22;
            npc.damage = 10;
            npc.defense = 9;
            npc.knockBackResist = 0.8f;
            npc.width = 38;
            npc.stepSpeed = 10;
            npc.height = 28;
            animationType = 46;
            npc.aiStyle = 3;
            aiType = 47;
            npc.npcSlots = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 5, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Bunny");
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
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NinjaGoreBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NinjaGoreHelmet"), 1f);
            }
            if (npc.life >= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    npc.alpha = 0;
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
            SetItem = Main.rand.Next(0, 35);
            if (SetItem == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Katana);
            }
            if (SetItem == 2)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Shuriken, Main.rand.Next(1, 55));
            }
            if (SetItem == 3)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.NinjaHood);
            }
            if (SetItem == 4)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.NinjaShirt);
            }
            if (SetItem == 5)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.NinjaPants);
            }
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
          
        }

        public override void AI()
        {
           
            toss_se += 1;
            Player player = Main.player[npc.target];
            if (npc.alpha < 240)
            {
                npc.chaseable = true;            
            }
            if (npc.alpha < 240)
            {
                npc.alpha += 5;
            }
            if (npc.alpha > 240)
            {
                npc.alpha = 240;
                npc.chaseable = false;
            }
            if (toss_se == 330)
            {
                npc.velocity.Y = -14;
                npc.damage = 30;
                npc.velocity.X = 0;
                npc.noTileCollide = true;
            }
            if (npc.noTileCollide == true && npc.velocity.Y > 0)
            {
                npc.noTileCollide = false;
            }
            if (toss_se == 360)
            {
                npc.alpha = 0;
                int numberProjectiles = Main.rand.Next(5, 7);
                for (int z = 0; z < numberProjectiles; z++)
                {
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int pro = Projectile.NewProjectile(vector8.X, vector8.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, -5), ProjectileID.Shuriken, 6, 0f, 0);
                    Main.projectile[pro].friendly = false;
                    Main.projectile[pro].hostile = true;
                    Main.projectile[pro].tileCollide = false;
                    npc.aiStyle = 0;
                
                }
            }    
            if (toss_se > 360)
            {
                npc.alpha = 0;
                npc.defense = 0;
            }
            if (toss_se == 420)
            {
                npc.aiStyle = 3;
                npc.damage = 10;
                toss_se = 0;
                npc.defense = 9;
            }
            
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            var x = spawnInfo.spawnTileX;
            var y = spawnInfo.spawnTileY;
            var tile = (int)Main.tile[x, y].type;
            return Main.dayTime && NPC.downedSlimeKing && y < Main.worldSurface ? 0.02f : 0f;


        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (npc.alpha > 122)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}