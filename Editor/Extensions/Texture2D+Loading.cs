using System.IO;
using UnityEngine;

namespace MartonioJunior.Trinkets.Editor {
  public static partial class Texture2DExtensions {
    public static Texture2D LoadImage(this Texture2D self, string filePath) {
      byte[] fileData;

      if (File.Exists(filePath)) {
        fileData = File.ReadAllBytes(filePath);
        self.LoadImage(fileData);
      }
      return self;
    }
  }
}