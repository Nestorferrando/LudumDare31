using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    

    class PeriodModel
    {



        private int _currentPeriod = 0;


        public int currentYear()
        {
            return GameRules.startingYear + _currentPeriod * GameRules.yearsPerPeriod;
        }

        public int currentPeriod()
        {
            return _currentPeriod;
        }

        public void increasePeriod()
        {
            _currentPeriod++;
        }

        public bool isGameFinished()
        {
            return _currentPeriod >= GameRules.totalPeriods;
        }
    }

