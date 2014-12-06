using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class RoleUnbalance : IComparable<RoleUnbalance>
{


    private Role role;

    private float unbalance ;


    public RoleUnbalance(Role role, float unbalance)
    {
        this.role = role;
        this.unbalance = unbalance;
    }

    public Role Role
    {
        get { return role; }
    }

    public float Unbalance
    {
        get { return unbalance; }
    }

    public int CompareTo(RoleUnbalance other)
    {
        if (this.unbalance > other.Unbalance) return 1;
        if (this.unbalance < other.Unbalance) return -1;
        return 0;

    }
}

