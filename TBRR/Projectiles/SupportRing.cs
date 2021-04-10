using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TBRR.Projectiles
{
	public class SupportRing : ModProjectile
	{
		int heal_drops = 0;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Support Ring");
		}
		public override void SetDefaults()
		{
			projectile.width = 128;
			projectile.height = 128;
			projectile.aiStyle = 0;
			projectile.friendly = false;
			projectile.scale = 1;
            projectile.hostile = true;
			projectile.alpha = 0;
			projectile.penetrate = -1;
			projectile.timeLeft = 51;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			projectile.alpha += 20;
			projectile.scale *= 1.1f;
			for (int i = 0; i < 1001; i++)
			{
				Projectile target = Main.projectile[i];
				if (target.type != projectile.type && target.hostile != true)
				{
					if (projectile.getRect().Intersects(target.getRect()))
					{
						target.friendly = false;
						target.penetrate = 1;
						target.velocity.Y *= -1;
						target.velocity.X *= -1;
						target.hostile = true;
					}
				}
			}
			if (projectile.timeLeft == 51)
            {
			for (int b = 0; b < Main.maxNPCs; b++)
			{
				if (Main.npc[b].active && !Main.npc[b].dontTakeDamage && Main.npc[b].lifeMax > 1 && Main.npc[b].friendly == false)
				{
					NPC npc = Main.npc[b];
						if (projectile.Hitbox.Intersects(Main.npc[b].Hitbox))
						{

							if (npc.life < npc.lifeMax)
							{
								npc.HealEffect(5, true);
								npc.life += 5;
								if (npc.life > npc.lifeMax)
								{
									npc.life = npc.lifeMax;
								}
							}
							else
                            {


                            }
						}
		
					}
				}
			}
            }

		}
    }


