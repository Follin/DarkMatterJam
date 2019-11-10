using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODManager : MonoBehaviour
{
    FMOD.Studio.EventInstance PlayerAttackEvent;
    FMOD.Studio.EventInstance PlayerHurtEvent;
    FMOD.Studio.EventInstance PlayerDeathEvent;
    FMOD.Studio.EventInstance EnemyAmbientEvent;
    FMOD.Studio.EventInstance EnemyDeathEvent;
    FMOD.Studio.EventInstance MenuMusicEvent;
    FMOD.Studio.EventInstance GameloopMusicEvent;
    FMOD.Studio.EventInstance TransitionToWhiteEvent;
    FMOD.Studio.EventInstance TransitionToDarkEvent;

    FMOD.Studio.PARAMETER_DESCRIPTION TransitionParam;
    FMOD.Studio.PARAMETER_ID TransitionID;

    FMOD.Studio.PARAMETER_DESCRIPTION MenuParam;
    FMOD.Studio.PARAMETER_ID MenuID;

    FMOD.Studio.PARAMETER_DESCRIPTION GameloopParam;
    FMOD.Studio.PARAMETER_ID GameloopID;

    FMOD.Studio.PLAYBACK_STATE GameloopPlaybackState;
    FMOD.Studio.PLAYBACK_STATE MenuPlaybackState;

    [SerializeField]
    private GameObject ManagerObject;

    private GameManager GameManager;

    [SerializeField]
    [Range(0.0f, 100f)]
    private float NoiseChance;

    private bool WorldstateBuffer;
    private float SceneBuffer;

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("DarkBells_To_LightWithHigh", out TransitionParam);
        TransitionID = TransitionParam.id;

        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("Menu_Out", out MenuParam);
        MenuID = MenuParam.id;

        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("GameloopFader", out GameloopParam);
        GameloopID = GameloopParam.id;

        GameloopMusicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Gameloop");

        MenuMusicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Menu");

        GameManager = ManagerObject.GetComponent<GameManager>();

        WorldstateBuffer = GameManager.InDarkWorld(); //dark world = true, white world = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (WorldstateBuffer != GameManager.InDarkWorld())
        {
            if (GameManager.InDarkWorld())
            {
                TransitionToDarkWorld();
            }
            else
            {
                TransitionToWhiteWorld();
            }
        }

        WorldstateBuffer = GameManager.InDarkWorld();

        if (SceneBuffer != SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                PlayMenuMusic();
            }
            else
            {
                PlayGameloopMusic();
            }
        }

        SceneBuffer = SceneManager.GetActiveScene().buildIndex;
    }

    public void PlayerHurt()
    {
        PlayerHurtEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Damaged");
        PlayerHurtEvent.start();
        Debug.Log("Player Hurt");
    }

    public void PlayerAttack()
    {
        PlayerAttackEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Attack");
        PlayerAttackEvent.start();
        Debug.Log("Player Attacked");

    }
    public void PlayerDeath()
    {
        PlayerDeathEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player_Death");
        PlayerDeathEvent.start();
        Debug.Log("Player Death");

    }
    public void EnemyDeath()
    {
        EnemyDeathEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Enemy_2_Killed");
        EnemyDeathEvent.start();
        Debug.Log("Enemy Death");

    }
    public void EnemyAmbient()
    {
        if (Random.Range(0, NoiseChance) == NoiseChance)
        {
            EnemyAmbientEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Enemy_2_Ambient");
            EnemyAmbientEvent.start();
            Debug.Log("Enemy Ambient");

        }
    }
    public void TransitionToWhiteWorld()
    {
        GameloopMusicEvent.setParameterByName("DarkBells_To_LightWithHigh", (float)1);
        TransitionToWhiteEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Transition_To_White");
        TransitionToWhiteEvent.start();
        Debug.Log("Transition to white");

    }
    public void TransitionToDarkWorld()
    {
        GameloopMusicEvent.setParameterByName("DarkBells_To_LightWithHigh", (float)0);
        TransitionToDarkEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Transition_To_Dark");
        TransitionToDarkEvent.start();
        Debug.Log("Transition to dark");
    }
    public void PlayMenuMusic()
    {
        GameloopMusicEvent.getPlaybackState(out GameloopPlaybackState);
        if (GameloopPlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            GameloopMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        MenuMusicEvent.start();
        Debug.Log("Menu Music start");
    }
    public void PlayGameloopMusic()
    {
        MenuMusicEvent.getPlaybackState(out MenuPlaybackState);
        if (MenuPlaybackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            MenuMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        FadeInGameMusic();
        GameloopMusicEvent.start();
        Debug.Log("Game music start");
    }
    public void FadeInGameMusic()
    {
        Debug.Log((GameloopMusicEvent.setParameterByID(GameloopID, (float)0)).ToString());
        Debug.Log("Fade in game music");
    }
    public void FadeOutGameMusic()
    {
        Debug.Log((GameloopMusicEvent.setParameterByID(GameloopID, (float)1)).ToString());
        Debug.Log("Fade out game music");
    }
    public void FadeOutMenuMusic()
    {
        Debug.Log(MenuMusicEvent.setParameterByID(MenuID, 1).ToString());
        Debug.Log("Fade out menu music");
    }
    public void FadeInMenuMusic()
    {
        Debug.Log(MenuMusicEvent.setParameterByID(MenuID, 0).ToString());
        Debug.Log("Fade in menu music");
    }
}
