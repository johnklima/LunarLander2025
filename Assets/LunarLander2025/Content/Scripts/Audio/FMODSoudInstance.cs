public class FMODSoudInstance
{
    private readonly string fond;
    private FMOD.Studio.EventInstance instance;

    public string Fond { get => fond; }
    public FMOD.Studio.EventInstance Instance { get => instance; }

    public FMODSoudInstance(string fond, FMOD.Studio.EventInstance instance)
    {
        this.fond = fond;
        this.instance = instance;
    }
}