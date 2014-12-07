using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class InmigrantModel
{
        private Role role;
        private bool hiddenBehindBarrel;

    public InmigrantModel(Role role)
    {
        this.role = role;
        this.hiddenBehindBarrel = false;
    }

    public Role Role
        {
            get { return role; }
        }



        public bool HiddenBehindBarrel
        {
            get { return hiddenBehindBarrel; }
            set { hiddenBehindBarrel = value; }
        }

}
