using UnityEngine;

public class Participant : MonoBehaviour
{
    public bool IsEliminated { get; private set; }
    public bool IsSeated { get; private set; }

    public virtual void PrepareForRound()
    {
        IsSeated = false;
    }

    public void Sit()
    {
        IsSeated = true;
    }

    public void Eliminate()
    {
        IsEliminated = true;
        IsSeated = false;
    }
}