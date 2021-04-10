using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TBRR.NPCs
{
    public class GiantBunny : ModNPC
    {
        int regenCounter = 0;
        int SetItem = 0;
        int imp = 0;
        int high_jump_or_long = 0;
        int charge = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 250;
            npc.damage = 36;
            npc.defense = 20;
            npc.knockBackResist = 0.0f;
            npc.width = 40;
            npc.height = 40;
            animationType = 46;
            npc.aiStyle = 3;
            aiType = 460;
            npc.npcSlots = 0.8f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 1, 50, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gargantuan Bunny");
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
					Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 2f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 2f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 2f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, 2.5f * (float)hitDirection, -2.5f, 0, default(Color), 2f);
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantFoot"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantHead"), 1f);
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
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (charge < 60)
            {
                target.AddBuff(BuffID.Dazed, 120);
                crit = true;
            }
            else
            {

            }
        }
        public override void NPCLoot()
        {
            SetItem = Main.rand.Next(0, 4);
            if (SetItem == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FuzzyCarrot);
            }
            if (SetItem == 2)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BunnyHood);
            }
        }
        public override void AI()
        {
            charge += 1;
            Player player = Main.player[npc.target];
            if (player.dead)
            {
                npc.aiStyle = 0;
                aiType = 460;
                regenCounter += 1;
                if (regenCounter == imp && npc.life < npc.lifeMax)
                {
                    npc.life += 1;
                    regenCounter = 0;
                    imp -= 2;
                }
                if (npc.life > npc.lifeMax)
                {
                    npc.life = npc.lifeMax;
                }
                if (imp < 2)
                {
                    imp = 2;
                }
            }
            if (!player.dead)
            {
                npc.aiStyle = 3;
                npc.friendly = false;
                aiType = 47;
                regenCounter = 0;
                imp = 20;
            }
            if (charge > 160 && !player.dead)
            {
                high_jump_or_long = Main.rand.Next(-6, 6);
                if (high_jump_or_long >= 0)
                {
                npc.velocity.Y = -8;
                npc.damage = player.statLifeMax / 2;
                npc.velocity.X = 12 * npc.spriteDirection;
                charge = 0;
                }
                if (high_jump_or_long < 0)
                {
                    npc.velocity.X = 4*npc.spriteDirection;
                    npc.damage = player.statLifeMax / 2;
                    npc.velocity.Y = -14;
                    charge = 0;
                }
            }
            if (charge < 60)
            {
                npc.damage = 20;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            var x = spawnInfo.spawnTileX;
            var y = spawnInfo.spawnTileY;
            var tile = (int)Main.tile[x, y].type;
            return Main.dayTime && Main.hardMode && y < Main.worldSurface ? 0.03f : 0f;
        }
    }
}