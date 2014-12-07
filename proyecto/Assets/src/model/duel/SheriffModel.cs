using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class SheriffModel
    {
        private Role role;
        private Role antiRole;
        private int bullets;
        private bool hiddenBehindBarrel;

        public SheriffModel(Role role, Role antiRole)
        {
            this.role = role;
            this.antiRole = antiRole;
            this.hiddenBehindBarrel = false;
        }

        public Role Role
        {
            get { return role; }
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

        public bool HiddenBehindBarrel
        {
            get { return hiddenBehindBarrel; }
            set { hiddenBehindBarrel = value; }
        }
    }

