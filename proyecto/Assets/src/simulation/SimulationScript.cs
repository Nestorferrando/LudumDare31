using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SimulationScript : MonoBehaviour
{

  

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {

        SheriffModel sheriff = new SheriffModel(Role.BusinessMan,Role.Indian);
        CityModel cityModel = new CityModel();

	    for (int i = 0; i < 15; i++)
	    {

	        for (int wave = 0; wave < 3; wave++)
	        {
	            Role[] inmigrants = InmigrationUtils.getInmigrants(sheriff, cityModel.getCityUnbalance());

	            for (int j = 0; j < inmigrants.Length; j++)
	            {
                  
	                cityModel.AddIndividual(inmigrants[j]);
	            }
	        }
	    }


      
        /*
        Debug.Log("-----------------------");
        Debug.Log("bus " + cityModel.getCityUnbalance().BusinessUnbalance);
        Debug.Log("cri " + cityModel.getCityUnbalance().CriminalUnbalance);
        Debug.Log("ind " + cityModel.getCityUnbalance().IndianUnbalance);
        Debug.Log("min " + cityModel.getCityUnbalance().MinerUnbalance);
        */
        List<Role> buildings = BuildingUtils.getRoleBuildingsType(new CityStatus(100, 1, 0, 0.2f, 0, 0), 4);
	    Debug.Log(buildings[0]+" "+buildings[1]+" "+buildings[2]+" "+buildings[3]);





	}
}
