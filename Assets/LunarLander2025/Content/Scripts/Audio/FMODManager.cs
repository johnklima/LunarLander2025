using System.Collections.Generic;
using UnityEngine;

public class FMODManager : MonoBehaviour
{
    #region Public Fields
    public GameObject Player;
    #endregion

    #region Static Values
    /*
    private static //FMODManager _instance;
    public static //FMODManager Instance
    {
        get
        {
            // If the instance is null, an attempt is made to find an existing one.
            if (_instance == null)
            {
                _instance = FindObjectOfType<//FMODManager>();

                // If not found, an exception is thrown
                if (_instance == null)
                {
                    throw new System.Exception("No instance of //FMODManager was found in the scene.");
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Private Fields
    private string currentFond;
    private FMOD.Studio.EventInstance currentInstance;
    private readonly List<FMODSoudInstance> instances = new();
    #endregion

    #region Mono
    void Update()
    {
        FMODUnity.RuntimeManager.SetListenerLocation(Player);
    }
    #endregion

    #region Internal
    private FMODSoudInstance GetInstance(string path)
    {
        foreach (var instance in instances)
        {
            if (instance.Fond == path)
            {
                return instance;
            }
        }
        return null;
    }
    #endregion

    #region Public Methods
    public void PlayOneShot2DSound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/" + path, Camera.main.transform.position);
    }
    public void PlayConstant2DSound(string path)
    {
        if (path != currentFond)
        {
            var newInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + path);
            instances.Add(new FMODSoudInstance(path, newInstance));

            currentFond = path;
            currentInstance = newInstance;
            currentInstance.start();
        }
    }
    public void StopConstant2DSound(string path)
    {
        var tempInstance = GetInstance(path);
        tempInstance.Instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void StopAllConstant2DSound()
    {
        foreach(var instance in instances)
        {
            var tempInstance = GetInstance(instance.Fond);
            tempInstance.Instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    */
    #endregion
    
}