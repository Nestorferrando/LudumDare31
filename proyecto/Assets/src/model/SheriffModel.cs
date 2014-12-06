using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class SheriffModel
    {
        private Role role;
        private Role antiRole;
        private int bullets;

        public SheriffModel(Role role, Role antiRole)
        {
            this.role = role;
            this.antiRole = antiRole;
        }

        public Role Role
        {
            get { return role; }
        }

        public Role AntiRole
        {
            get { return antiRole; }
        }

        public int Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }


    }

