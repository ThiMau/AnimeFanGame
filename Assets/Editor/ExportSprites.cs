using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportSprites
{
    [MenuItem("Tools/Export Selected Sprites")]
    static void Export()
    {
        foreach (Object obj in Selection.objects)
        {
            Sprite sprite = obj as Sprite;
            if (sprite == null) continue;

            Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] pixels = sprite.texture.GetPixels(
                (int)sprite.rect.x,
                (int)sprite.rect.y,
                (int)sprite.rect.width,
                (int)sprite.rect.height);

            tex.SetPixels(pixels);
            tex.Apply();

            byte[] bytes = tex.EncodeToPNG();
            File.WriteAllBytes("D:/backup/ghepanh/Export" + sprite.name + ".png", bytes);
        }
    }
}
