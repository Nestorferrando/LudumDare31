using System.Runtime.InteropServices;
using Assets.scripts;
using Assets.scripts.input;
using Assets.src.visualcomponents;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

    private City city;
    private DuelStreet duelStreet;
    private PeriodModel periodModel;
    private GameState _gameState;

    private float speechTime;
    private ManageUIDialogs dialogManager;

    private int waveCounter = 0;



	// Use this for initialization
	void Start () {

      city =  GetComponentsInChildren<City>()[0];
      duelStreet = GetComponentsInChildren<DuelStreet>()[0];
      dialogManager = GetComponentsInChildren<ManageUIDialogs>()[0];
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
	            performInmigrantsEntering();
                break;

        case GameState.duel:
	            performDuel();
                break;

        case GameState.preDuelSpeech:
	            performPreDuelSpeech();
	            break;

        case GameState.allInmigrantDead:
	            performAllInmigrantsDead();
	            break;


        case  GameState.inmigrantsLeave:
	            performInmigrantsLeave();
	            break;

        case GameState.FadeOutWavesCompleted:
	            break;
        case GameState.fadeInWavesCompleted:
                break;

	    }

	}

    private void performInmigrantsLeave()
    {
        if (duelStreet.inmigrantsAlreadyFaded())
        {
            duelStreet.resetWave(InmigrationWaveGenerator.getCriminals(duelStreet.getSheriffModel(),
                city.CityModel.getCityUnbalance()));
            duelStreet.stopInmigrantsFade();
            duelStreet.moveWave();
            nextWaveOrNextPeriod();

        }
    }



    private void performAllInmigrantsDead()
    {
        if (duelStreet.inmigrantsAlreadyFaded())
        {
            duelStreet.resetWave(InmigrationWaveGenerator.getCriminals(duelStreet.getSheriffModel(),
                city.CityModel.getCityUnbalance()));
            duelStreet.stopInmigrantsFade();
            duelStreet.moveWave();
            nextWaveOrNextPeriod();
        }
    }

    private void performPreDuelSpeech()
    {


        if ((duelStreet.getSheriffModel().Bullets <= 0 && Time.realtimeSinceStartup > speechTime + 1))
        {
            _gameState = GameState.inmigrantsLeave;
            duelStreet.moveWave();
            duelStreet.startInmigrantsFade();
            dialogManager.setSheriffSurrenderDialog();
            return;
        }

        if (duelStreet.getSheriffModel().Bullets > 0 && Time.realtimeSinceStartup > speechTime + 3f)
        {
            _gameState = GameState.inmigrantsLeave;
            duelStreet.moveWave();
            duelStreet.startInmigrantsFade();
            return;
        }

        if (duelStreet.getSheriffModel().Bullets > 0)
        {
            InputResult result = InputUtils.readInput();

            if (result.get(InputValues.COCK))
            {
                _gameState = GameState.duel;
                duelStreet.startDuelMode();
                dialogManager.startDuelDialog();
            }
        }
    }

    private void performDuel()
    {
        if (!duelStreet.isDuelModeActive())
        {
            waveCounter++;
            
            switch (duelStreet.getDuelResult())
            {
                case DuelResult.won:
                    _gameState = GameState.allInmigrantDead;
                    break;
                case DuelResult.surrender:
                    dialogManager.setSheriffSurrenderDialog();
                    _gameState = GameState.inmigrantsLeave;
                    duelStreet.moveWave();
                    break;
                case DuelResult.dead:
                    _gameState = GameState.inmigrantsLeave;
                    duelStreet.moveWave();

                    break;
            }
        duelStreet.startInmigrantsFade();
        }
    }

    private void performInmigrantsEntering()
    {
        if (duelStreet.enemiesCloseFromFrontBarrels())
        {
            duelStreet.stopWave();
            _gameState = GameState.preDuelSpeech;
            speechTime = Time.realtimeSinceStartup;
            dialogManager.setWaveDialog(duelStreet.GetInmigrantModel(0).Role);
        }
    }


    private void nextWaveOrNextPeriod()
    {
        if (waveCounter == GameRules.wavesPerPeriod)
        {
            _gameState = GameState.FadeOutWavesCompleted;
        }
        else
        {
            _gameState = GameState.inmigrantsEntering;
        }
    }
}
