using System.Runtime.InteropServices;
using Assets.scripts;
using Assets.scripts.input;
using Assets.src.visualcomponents;
using Assets.src.visualcomponents.ui_dialog;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

    private City city;
    private DuelStreet duelStreet;
    private PeriodModel periodModel;
    private SplashScreen splashScreen;
    private GameState _gameState;

    private BlackCurtain curtain;

    private float speechTime;
    private ManageUIDialogs dialogManager;

    private int waveCounter = 0;
    private int deadSheriffs = 0;



	// Use this for initialization
	void Start () {

      city =  GetComponentsInChildren<City>()[0];
      duelStreet = GetComponentsInChildren<DuelStreet>()[0];
      dialogManager = GetComponentsInChildren<ManageUIDialogs>()[0];
      curtain = GetComponentsInChildren<BlackCurtain>()[0];
      splashScreen = GetComponentsInChildren<SplashScreen>()[0];
      periodModel=new PeriodModel();
	    _gameState = GameState.splashScreen;
	}


    private void initialize()
    {
      duelStreet.updateSheriffModel(new SheriffModel(Role.Criminal,Role.Criminal));
      duelStreet.resetWave(InmigrationWaveGenerator.getCriminals(duelStreet.getSheriffModel(),city.CityModel.getCityUnbalance()));

      duelStreet.moveWave();
      _gameState = GameState.inmigrantsEntering;
      periodModel.reset();
        city.CityModel.resetToInitialPopulation();
      city.Regenerate();
      deadSheriffs = 0;
      waveCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
        
    switch(_gameState) 
	    {
            case GameState.splashScreen:
	                performSplashScreen();
	            break;
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
	            performFadeOutWavesCompleted();
	            break;
        case GameState.fadeInWavesCompleted:
                performFadeInWavesCompleted();
	            break;
        case GameState.fadeOutGameFinished:
	            performFadeOutGameFinished();
	            break;


	    }

	}

    private void performFadeOutGameFinished()
    {
        if (curtain.fadeFinished())
        {
            InputResult result = InputUtils.readInput();
            if (result.get(InputValues.SHOOT))
            {
                initialize();
                curtain.fadeToAlphaStatistics();
            }
        }
    }

    private void performSplashScreen()
    {
        InputResult result = InputUtils.readInput();
        if (result.get(InputValues.SHOOT))
        {
            _gameState = GameState.inmigrantsEntering;
            splashScreen.remove();
            initialize();
        }
    }

    private void performFadeInWavesCompleted()
    {
        if (curtain.fadeFinished())
        {
            duelStreet.moveWave();
            _gameState = GameState.inmigrantsEntering;
        }
    }

    private void performFadeOutWavesCompleted()
    {
        if (curtain.fadeFinished())
        {
            periodModel.increasePeriod();
            waveCounter = 0;
            city.Regenerate();
            duelStreet.updateSheriffModel(new SheriffModel(Role.Criminal, Role.Criminal));
            duelStreet.resetWave(InmigrationWaveGenerator.getCriminals(duelStreet.getSheriffModel(),
                city.CityModel.getCityUnbalance()));
            _gameState = GameState.fadeInWavesCompleted;
            curtain.fadeToAlphaPeriod();
        }
    }

    private void performInmigrantsLeave()
    {
        if (duelStreet.inmigrantsAlreadyFaded())
        {
            for (int i = 0; i < duelStreet.ActiveInmigrants; i++)
            {
                if (duelStreet.GetInmigrantModel(i).Alive)
                city.CityModel.AddIndividual(duelStreet.GetInmigrantModel(i).Role);
            }

            duelStreet.resetWave(InmigrationWaveGenerator.getCriminals(duelStreet.getSheriffModel(),
                city.CityModel.getCityUnbalance()));
            duelStreet.stopInmigrantsFade();
            duelStreet.moveWave();
            nextWaveOrNextPeriodOrGameFinish();

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
            nextWaveOrNextPeriodOrGameFinish();
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
                    dialogManager.setNewSheriffDialog();
                    deadSheriffs++;
                    break;
            }
        duelStreet.startInmigrantsFade();
        }
    }

    private void performInmigrantsEntering()
    {
        if (duelStreet.enemiesCloseFromFrontBarrels())
        {
            waveCounter++;

            if (duelStreet.getSheriffModel().Alive)
            {
                duelStreet.stopWave();
                _gameState = GameState.preDuelSpeech;
                speechTime = Time.realtimeSinceStartup;
                dialogManager.setWaveDialog(duelStreet.GetInmigrantModel(0).Role);
            }
            else
            {
                duelStreet.startInmigrantsFade();
                _gameState = GameState.inmigrantsLeave;
            }

        }
    }


    private void nextWaveOrNextPeriodOrGameFinish()
    {
        if (waveCounter >= GameRules.wavesPerPeriod)
        {

            if (periodModel.isGameFinished() ||
                city.CityModel.getCityUnbalance().worstUnbalance() >= GameRules.totalInfestation)
            {
                curtain.fadeToBlackStatistics(periodModel.currentPeriod(), deadSheriffs,
                    city.CityModel.getCityUnbalance());
                _gameState = GameState.fadeOutGameFinished;
            }
            else
            {
                curtain.fadeToBlackPeriod(periodModel.currentYear() + "-" +
                                          (periodModel.currentYear() + GameRules.yearsPerPeriod));
                _gameState = GameState.FadeOutWavesCompleted;
            }


        }
        else
        {
            _gameState = GameState.inmigrantsEntering;
        }
    }
}
