namespace MartonioJunior.Trinkets.Editor
{
    public interface IMediaInspector
    {
        bool MediaIsLoaded {get; set;}
        void LoadMedia();
    }

    public static partial class IMediaInspectorExtensions
    {
        public static void LazyLoadMedia(this IMediaInspector self)
        {
            if (self.MediaIsLoaded) return;
            
            self.LoadMedia();
            self.MediaIsLoaded = true;
        }
    }
}