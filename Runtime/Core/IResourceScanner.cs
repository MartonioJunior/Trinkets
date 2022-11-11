namespace MartonioJunior.Trinkets
{
    /**
    <summary>Interface used for scanning resources into a group</summary>
    */
    public interface IResourceScanner: IResourceSensor, IResourceTaxer
    {
        /**
        <summary>Checks whether a scanner is allowed to remove resources from the
        group after a scan operation.</summary>
        <returns><c>true</c>: Scan removes the resources from a group.<br/>
        <c>false</c>: Scan maintains the group's resources intact.</returns>
        */
        bool TaxGroupOnScan {get; set;}
    }

    public static partial class IResourceScannerExtensions
    {
        /**
        <summary>Checks whether a group fulfills the specified criteria
        of a <cref>IResourceScanner</cref></summary>
        <param name="group">The group to be scanned.</param>
        <returns><c>true</c> when the group passes a scan.<br/>
        <c>false</c> when the group does not fulfill the criteria.</returns>
        */
        public static bool Scan(this IResourceScanner self, IResourceGroup group)
        {
            bool checkIsValid = self.Check(group);
            if (checkIsValid && self.TaxGroupOnScan) {
                self.Tax(group);
            }
            return checkIsValid;
        }
    }
}