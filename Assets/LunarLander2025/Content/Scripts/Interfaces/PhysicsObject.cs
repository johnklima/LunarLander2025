namespace Interfaces
{
    /// <summary>
    /// Summary of changes made on 2025-02-02:
    /// - Added a new property Planet to the IPhysicsObject interface.
    /// </summary>
    public interface IPhysicsObject
    {
        public float Drag { get; set; }
        public Planet Planet { get; set; }
    }
}