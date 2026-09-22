using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform seatPoint;
    [SerializeField] private Transform interactionPoint;

    public Transform SeatPoint => seatPoint;
    public Transform InteractionPoint => interactionPoint;

    public Participant Occupant { get; private set; }

    public bool IsOccupied => Occupant != null;

    public bool TryOccupy(Participant participant)
    {
        if (participant == null)
            return false;

        if (IsOccupied)
            return false;

        Occupant = participant;

        Debug.Log(participant.name + " ocupó " + name);

        return true;
    }

    public void ResetChair()
    {
        Occupant = null;
    }
}