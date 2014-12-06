using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class BuildingUtils 
    {
        

        public static List<Role> getRoleBuildingsType(CityStatus status, int roleBuildingsSize)
        {

            List<Role> assignedBuildings=new List<Role>();
            if ( (roleBuildingsSize%4)!=0) throw new Exception("role building size should be a multiple of four");


            BuildingGiver giver = new BuildingGiver(roleBuildingsSize);

            int equilibratedBuildingsPerRole = roleBuildingsSize/4;
            float additionalHousesUnbalanceRatio = roleBuildingsSize / GameRules.totalInfestation * 0.75f;



            float totalUnbalance =  status.BusinessUnbalance + status.CriminalUnbalance +  status.IndianUnbalance + status.MinerUnbalance;


                float adJustementFactor = 1.0f;

                if (totalUnbalance > GameRules.totalInfestation)
                    adJustementFactor = GameRules.totalInfestation/totalUnbalance;

                additionalHousesUnbalanceRatio = additionalHousesUnbalanceRatio*adJustementFactor;

               int  desiredIndianHouses = equilibratedBuildingsPerRole+(int)Math.Round(additionalHousesUnbalanceRatio*status.IndianUnbalance);
               int  desiredCriminalHouses = equilibratedBuildingsPerRole+(int)Math.Round(additionalHousesUnbalanceRatio*status.CriminalUnbalance);
               int  desiredMinerHouses = equilibratedBuildingsPerRole+(int)Math.Round(additionalHousesUnbalanceRatio*status.MinerUnbalance);
               int  desiredBusinessHouses =equilibratedBuildingsPerRole+ (int)Math.Round(additionalHousesUnbalanceRatio*status.BusinessUnbalance);

        
                List<BuildingRequest> requests = new List<BuildingRequest>();

                requests.Add(new BuildingRequest(Role.Indian,desiredIndianHouses));
                requests.Add(new BuildingRequest(Role.Criminal,desiredCriminalHouses));
                requests.Add(new BuildingRequest(Role.Miner,desiredMinerHouses));
                requests.Add(new BuildingRequest(Role.BusinessMan,desiredBusinessHouses));
                requests.Sort();

            while (requests.Any())
            {

                BuildingRequest request = requests.Last();
                requests.RemoveAt(requests.Count-1);
                int assigedBuildings = giver.tryToGetBuilding(request.DesiredAmount);
                for (int i = 0; i < assigedBuildings; i++)
                {
                    assignedBuildings.Add(request.Role); 
                }
            }
            return assignedBuildings;
        }


        public static List<Role> getNeutralBuildingsType(CityStatus status, int neutralBuildings)
        {
            List<Role> assignedBuildings = new List<Role>();


            List<RoleUnbalance> unbalances = new List<RoleUnbalance>();
            unbalances.Add(new RoleUnbalance(Role.Indian, status.IndianUnbalance));
            unbalances.Add(new RoleUnbalance(Role.Criminal, status.CriminalUnbalance));
            unbalances.Add(new RoleUnbalance(Role.Miner, status.MinerUnbalance));
            unbalances.Add(new RoleUnbalance(Role.BusinessMan, status.BusinessUnbalance));
            unbalances.Sort();

            RoleUnbalance worstUnbalance = unbalances.Last();

            int fuckedBuildings = (int)Math.Round(worstUnbalance.Unbalance/GameRules.totalInfestation * neutralBuildings);

            if (fuckedBuildings > neutralBuildings) fuckedBuildings = neutralBuildings;


            for (int i = 0; i < fuckedBuildings; i++)
            {
                assignedBuildings.Add(worstUnbalance.Role);
            }

            while (assignedBuildings.Count < neutralBuildings)
            {
               assignedBuildings.Add(Role.Neutral); 
            }

            return assignedBuildings;
        }





    }

