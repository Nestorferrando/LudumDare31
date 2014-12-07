using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class PersonModel
{

    protected static Random random = new Random();

        protected Role role;
        protected bool _hiddenBehindBarrel;
        protected bool _Alive;

        protected float shootTime;
        protected float standUpTime;

    public PersonModel(Role role)
    {
        this.role = role;
        this._hiddenBehindBarrel = false;
    }

    public Role Role
        {
            get { return role; }
        }


    public bool HiddenBehindBarrel
    {
        get { return _hiddenBehindBarrel; }
    }

    public void setHiddenBehindBarrel(Boolean hidden)
    {
        if (!this._hiddenBehindBarrel && hidden) standUpTime = Time.realtimeSinceStartup;
        this._hiddenBehindBarrel = hidden;

    }

    public bool Alive
    {
        get { return _Alive; }
        set { _Alive = value; }
    }

    public bool canMove()
    {
        return (Time.realtimeSinceStartup - standUpTime > GameRules.delayBetWeenStandAndShoot &&
                Time.realtimeSinceStartup - shootTime > GameRules.ShootSpeed);
    }
}
