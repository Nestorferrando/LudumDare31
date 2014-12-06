using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class RoleBuilding : MonoBehaviour
    {


        public void setAppeareance(Role role)
        {
            GetComponent<Animator>().Play("factionBuilding"+role.ToString());
        }


    }

