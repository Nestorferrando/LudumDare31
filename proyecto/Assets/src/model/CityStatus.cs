using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class CityStatus
    {

        private int totalPopulation;
        private float neutralBalance;
        private float indianUnbalance;
        private float businessUnbalance;
        private float criminalUnbalance;
        private float minerUnbalance;


        public CityStatus(int totalPopulation, float neutralBalance, float indianUnbalance, float businessUnbalance, float criminalUnbalance, float minerUnbalance)
        {
            this.totalPopulation = totalPopulation;
            this.neutralBalance = neutralBalance;
            this.indianUnbalance = indianUnbalance;
            this.businessUnbalance = businessUnbalance;
            this.criminalUnbalance = criminalUnbalance;
            this.minerUnbalance = minerUnbalance;
        }



        public float NeutralBalance
        {
            get { return neutralBalance; }
        }

        public float IndianUnbalance
        {
            get { return indianUnbalance; }
        }

        public float BusinessUnbalance
        {
            get { return businessUnbalance; }
        }

        public float CriminalUnbalance
        {
            get { return criminalUnbalance; }
        }

        public float MinerUnbalance
        {
            get { return minerUnbalance; }
        }

        public int TotalPopulation
        {
            get { return totalPopulation; }
        }
    }

