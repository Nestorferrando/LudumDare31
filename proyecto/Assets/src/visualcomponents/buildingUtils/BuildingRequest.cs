using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class BuildingRequest : IComparable<BuildingRequest>
{


    private Role role;

    private int desiredAmount;


    public BuildingRequest(Role role, int desiredAmount)
    {
        this.role = role;
        this.desiredAmount = desiredAmount;
    }

    public Role Role
    {
        get { return role; }
    }

    public int DesiredAmount
    {
        get { return desiredAmount; }
    }

    public int CompareTo(BuildingRequest other)
    {
        if (this.desiredAmount > other.desiredAmount) return 1;
        if (this.desiredAmount < other.desiredAmount) return -1;
        return 0;

    }
}

