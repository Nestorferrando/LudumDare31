using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SheriffModel : PersonModel
{

    private Role antiRole;
    private int bullets;

    private Boolean cocked;
    private float cockTime;


    public SheriffModel(Role role, Role antiRole)
        : base(role)
    {
        Debug.Log(antiRole);
        this.antiRole = antiRole;
        this.bullets = GameRules.sheriffBullets;
    }

    public new void setHiddenBehindBarrel(Boolean hidden)
    {
        if (this.HiddenBehindBarrel == hidden) return;
        if (hidden) this.cocked = false;
        base.setHiddenBehindBarrel(hidden);

    }

    public Role AntiRole
    {
        get { return antiRole; }
    }

    public Boolean tryToShoot()
    {
        if (bullets <= 0 || !cocked) return false;
        cocked = false;
        shootTime = Time.realtimeSinceStartup;
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

    public bool cock()
    {
        if (cocked)
            return false;

        cocked = true;
        cockTime = Time.realtimeSinceStartup;
        return true;
    }


    public new bool canMove()
    {
        return (
            (Time.realtimeSinceStartup - shootTime > GameRules.SheriffShootSpeed) && 
            
            base.canMove() && Time.realtimeSinceStartup - cockTime > GameRules.sheriffCockSpeed);
    }


}

