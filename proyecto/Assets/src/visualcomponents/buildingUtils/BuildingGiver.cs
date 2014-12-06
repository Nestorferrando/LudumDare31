using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



    class BuildingGiver
    {

        private int availableBuildings;

        public BuildingGiver(int availableBuildings)
        {
            this.availableBuildings = availableBuildings;
        }

        public int tryToGetBuilding(int amount)
        {

            int oldAvailableBuildings = availableBuildings;

            availableBuildings = Math.Max(0, availableBuildings - amount);

            return oldAvailableBuildings - availableBuildings;

        }
    }

