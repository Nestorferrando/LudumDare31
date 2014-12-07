using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

    private City city;
    private DuelStreet duelStreet;
    private PeriodModel periodModel;
    private GameState _gameState;


	// Use this for initialization
	void Start () {

      city =  GetComponentsInChildren<City>()[0];
      duelStreet = GetComponentsInChildren<DuelStreet>()[0];
      periodModel=new PeriodModel();
	    initialize();

	}


    private void initialize()
    {
      duelStreet.updateSheriffModel(new SheriffModel(Role.Criminal,Role.Criminal));
      duelStreet.resetWave(InmigrationWaveGenerator.getCriminals(duelStreet.getSheriffModel(),city.CityModel.getCityUnbalance()));

      duelStreet.moveWave();
      _gameState = GameState.inmigrantsEntering;

    }
	
	// Update is called once per frame
	void Update () {
	
        
    switch(_gameState) 
	    {
        case GameState.inmigrantsEntering:

	            if (duelStreet.enemiesCloseFromFrontBarrels())
	            {
	                duelStreet.stopWave();
	                _gameState = GameState.duel;
                    duelStreet.startDuelMode();
	            }
                break;

        case GameState.duel:
	            if (!duelStreet.isDuelModeActive())
	            {
	                Debug.Log(duelStreet.getDuelResult());
                    _gameState= GameState.inmigrantsLeave;
                    duelStreet.moveWave();
	            }


	            break;


	    }






	}

    void newWave()
    {
        
    }
}
