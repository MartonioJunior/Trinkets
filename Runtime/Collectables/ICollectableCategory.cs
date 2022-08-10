namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Interface used to describe a category of collectables.</summary>
  */
  public interface ICollectableCategory : IResource,
                                          IResourceCategory,
                                          IResourceManager<ICollectable> {}
}