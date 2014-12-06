using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Building : MonoBehaviour
    {
        public int buildingType;


        public void setAppeareance(Role role)
        {
            GetComponent<Animator>().Play("building" + buildingType+role.ToString());
        }


    }

