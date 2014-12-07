using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PersonModel
{
        protected Role role;
        protected bool hiddenBehindBarrel;

    public PersonModel(Role role)
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
