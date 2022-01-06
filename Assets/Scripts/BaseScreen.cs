using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    [SerializeField] private bool isPoolable;
    [SerializeField] private int id;
    public int GetId ()
    {
        return id; 
    }

    public bool IsPoolable()
    {
        return isPoolable;
    }
}
