using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class SheriffModel : PersonModel 
    {
        
        private Role antiRole;
        private int bullets;


        public SheriffModel(Role role, Role antiRole) :base(role)
        {
            this.antiRole = antiRole;
            this.bullets = GameRules.sheriffBullets;
            
        }

        public Role AntiRole
        {
            get { return antiRole; }
        }

        public Boolean consumeBullet()
        {
            if (bullets <= 0) return false;
            bullets--;
            return true;
        }

        public int Bullets
        {
            get { return bullets; }
        }

        public void refillBullets()
        {
            this.bullets = GameRules.sheriffBullets;
        }

    }

