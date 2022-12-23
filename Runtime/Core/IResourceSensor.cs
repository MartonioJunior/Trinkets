namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used to allow checking operations inside a group.</summary>
    */
    public interface IResourceSensor
    {
        /**
        <summary>Evaluates a group's contents.</summary>
        <param name="group">The group to be analyzed.</param>
        <returns><c>true</c> when the group is approved.<br/>
        <c>false</c> when the group is rejected.</returns>
        */
        bool Check(IResourceGroup group);
    }
}