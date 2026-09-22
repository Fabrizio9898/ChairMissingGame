using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Menu,
        Playing,
        MusicStopped,
        RoundResolution,
        GameOver
    }

    public enum Winner
    {
        None,
        Player,
        IA
    }
    private const int MinAICount = 1;
    private const int MaxAICount = 7;
    private const int DefaultAICount = 1;


    public GameState CurrentState { get; private set; }

    public Winner CurrentWinner { get; private set; } = Winner.None;

    public int CurrentRound { get; private set; }

    public int AICount { get; private set; } = DefaultAICount;

    private readonly List<Participant> activeParticipants = new();

    public IReadOnlyList<Participant> ActiveParticipants => activeParticipants;

    public int ChairsNeeded => Mathf.Max(0, activeParticipants.Count - 1);

    [SerializeField] private RoundManager roundManager;

    [SerializeField] private HumanPlayer humanPlayer;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.Menu);
    }



    public void StartGame(int aiCount)
    {

        AICount = Mathf.Clamp(aiCount, MinAICount, MaxAICount);

        CurrentRound = 0;
        CurrentWinner = Winner.None;

        activeParticipants.Clear();

        Debug.Log("Comenzando partida contra " + AICount + " IA.");


        RegisterParticipant(humanPlayer);

        for (int i = 0; i < AICount; i++)
        {
            IAPlayer ai = roundManager.CreateAI();
            RegisterParticipant(ai);
        }

        StartNextRound();
    }




    private void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        Debug.Log("Estado actual: " + CurrentState);
    }



    public void RegisterParticipant(Participant participant)
    {
        if (participant == null)
            return;

        if (activeParticipants.Contains(participant))
            return;

        activeParticipants.Add(participant);

        Debug.Log(
            "Participante registrado: " +
            participant.name +
            " | Total: " +
            activeParticipants.Count
        );
    }


    public void EliminateParticipant(Participant participant)
    {
        if (participant == null)
            return;

        if (!activeParticipants.Remove(participant))
            return;

        Debug.Log(
            "Participante eliminado: " +
            participant.name +
            " | Quedan: " +
            activeParticipants.Count
        );
    }



    public void StartNextRound()
    {
        CurrentRound++;

        Debug.Log(
            "Ronda " + CurrentRound +
            " | Participantes: " + activeParticipants.Count +
            " | Sillas necesarias: " + ChairsNeeded
        );

        roundManager.SetupRound(ChairsNeeded);

        ChangeState(GameState.Playing);

    }



    public void EndGame(Winner winner)
    {
        if (winner == Winner.None)
        {
            Debug.LogWarning("No se puede terminar la partida sin ganador.");
            return;
        }

        CurrentWinner = winner;

        ChangeState(GameState.GameOver);

        Debug.Log("Fin de partida. Ganador: " + CurrentWinner);
    }
}