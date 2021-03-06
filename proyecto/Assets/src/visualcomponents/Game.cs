﻿using System.Runtime.InteropServices;
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
    private Tutorial tutorial;
    private GameState _gameState;

    private BlackCurtain curtain;

    private float speechTime;
    private ManageUIDialogs dialogManager;

    private int waveCounter = 0;
    private int deadSheriffs = 0;

    private bool tutorialDone = false;



	// Use this for initialization
	void Start () {

      city =  GetComponentsInChildren<City>()[0];
      duelStreet = GetComponentsInChildren<DuelStreet>()[0];
      dialogManager = GetComponentsInChildren<ManageUIDialogs>()[0];
      curtain = GetComponentsInChildren<BlackCurtain>()[0];
      splashScreen = GetComponentsInChildren<SplashScreen>()[0];
      GameObject tutorialObj = GameObject.Find("Tutorial");
      tutorial = tutorialObj.GetComponent<Tutorial>();
      periodModel=new PeriodModel();
	    _gameState = GameState.splashScreen;
	}


    private void initialize()
    {
      duelStreet.updateSheriffModel(new SheriffModel(Role.Neutral,InmigrationUtils.getRandomRole()));
      duelStreet.resetWave(InmigrationUtils.getInmigrants(duelStreet.getSheriffModel(), city.CityModel.getCityUnbalance()));

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
        case GameState.tutorialState:

	            if (tutorial.TutorialFinished)
	            {
                    tutorial.destroy();
	                duelStreet.startDuelMode(true);
	                dialogManager.startDuelDialog();
	                _gameState = GameState.duel;
	            }
	            break;
	    }

	}

    private void performFadeOutGameFinished()
    {
        if (curtain.fadeFinished())
        {
            InputResult result = InputUtils.readInput();
            if (result.get(InputValues.CROUCH))
            {
                initialize();
                curtain.fadeToAlphaStatistics();
            }
        }
    }

    private void performSplashScreen()
    {
        InputResult result = InputUtils.readInput();
        if (result.get(InputValues.CROUCH))
        {
            _gameState = GameState.inmigrantsEntering;
            splashScreen.remove();
            initialize();
            duelStreet.stopWave();

            curtain.fadeToBlackPeriod(periodModel.currentYear() + ""); //+ "-"); // + (periodModel.currentYear() + GameRules.yearsPerPeriod));
            _gameState = GameState.FadeOutWavesCompleted;

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
            duelStreet.updateSheriffModel(new SheriffModel(Role.Neutral, duelStreet.getSheriffModel().AntiRole));
            duelStreet.resetWave(InmigrationUtils.getInmigrants(duelStreet.getSheriffModel(),
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

            duelStreet.resetWave(InmigrationUtils.getInmigrants(duelStreet.getSheriffModel(),
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
            duelStreet.resetWave(InmigrationUtils.getInmigrants(duelStreet.getSheriffModel(),
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

        if (duelStreet.getSheriffModel().Bullets > 0)
        {
            InputResult result = InputUtils.readInput();

            if (result.get(InputValues.COCK))
            {
                if (!this.tutorialDone)
                {
                    this.tutorialDone = true;
                    _gameState = GameState.tutorialState;
                    tutorial.runTutorial();
                }
                else
                {
                    _gameState = GameState.duel;
                    duelStreet.startDuelMode(false);
                    dialogManager.startDuelDialog();
                }

            }
            if (result.get(InputValues.SHOOT))
            {
                _gameState = GameState.inmigrantsLeave;
                duelStreet.moveWave();
                duelStreet.startInmigrantsFade();
                dialogManager.avoidDuelDialog();
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
                if (duelStreet.getSheriffModel().Bullets > 0)
                {
                    duelStreet.enableGetInIcon(true);
                    duelStreet.enableGunIcon(true);
                }
                speechTime = Time.realtimeSinceStartup;
                dialogManager.setWaveDialog(duelStreet.GetInmigrantModel(0).Role);
            }
            else
            {
                dialogManager.setSheriffIsDeadDialog();
                duelStreet.startInmigrantsFade();
                _gameState = GameState.inmigrantsLeave;
            }

        }
    }


    private void nextWaveOrNextPeriodOrGameFinish()
    {
        if (waveCounter >= GameRules.wavesPerPeriod)
        {
            duelStreet.stopWave();
            Debug.Log("====> current infestation " + city.CityModel.getCityUnbalance().worstUnbalance());

            if (periodModel.isGameFinished() ||
                city.CityModel.getCityUnbalance().worstUnbalance() >= GameRules.totalInfestation)
            {
                curtain.fadeToBlackStatistics(periodModel.currentPeriod(), deadSheriffs,
                city.CityModel.getCityUnbalance());
                _gameState = GameState.fadeOutGameFinished;
            }
            else
            {
                curtain.fadeToBlackPeriod(periodModel.currentYear() + "");
                _gameState = GameState.FadeOutWavesCompleted;
            }
        }
        else
        {
            _gameState = GameState.inmigrantsEntering;
        }
    }
}
