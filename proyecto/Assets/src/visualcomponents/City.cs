using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class City : MonoBehaviour
{



    private static readonly int ROLE_BUILDINGS_AMOUNT=4;
    private static readonly int NEUTRAL_BUILDINGS_AMOUNT = 3;


    private CityModel cityModel;




	// Use this for initialization
	void Start () {

        cityModel=new CityModel();
	
	}
	
	// Update is called once per frame
	void Update () {
	

        this.Regenerate();

	}


    public CityModel CityModel
    {
        get { return cityModel; }
    }

    public void Regenerate()

    {

        CityStatus cityStatus = cityModel.getCityUnbalance();

        List<Role> roleBuildings = BuildingUtils.getRoleBuildingsType(cityStatus,ROLE_BUILDINGS_AMOUNT);

        List<Role> neutralBuildings = BuildingUtils.getNeutralBuildingsType(cityStatus, NEUTRAL_BUILDINGS_AMOUNT);

        
        
        
        Building[] neutralBuildingComponents = GetComponentsInChildren<Building>();

        for (int i = 0; i < neutralBuildingComponents.Length; i++)
        {
            neutralBuildingComponents[i].setAppeareance(neutralBuildings[i]);
        }

        
        RoleBuilding[] roleBuildingComponents = GetComponentsInChildren<RoleBuilding>();

        for (int i = 0; i < roleBuildingComponents.Length; i++)
        {
            roleBuildingComponents[i].setAppeareance(roleBuildings[i]);
        }
        

    }


}
