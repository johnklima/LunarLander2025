using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    #region Static Values
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            // If the instance is null, an attempt is made to find an existing one.
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();

                // If not found, an exception is thrown
                if (_instance == null)
                {
                    throw new System.Exception("No instances of LevelManager were found in the scene.");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Actions
    public Action OnWin;
    public Action OnLose;
    #endregion

    #region Internal
    public void SetGameAsWin() => OnWin?.Invoke();
    public void SetGameAsLose() => OnLose?.Invoke();
    #endregion
}
