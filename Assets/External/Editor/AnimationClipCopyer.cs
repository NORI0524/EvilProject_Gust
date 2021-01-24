using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
public static class AnimationClipCopyer
{
    private const string ITEM_NAME = "Assets/Copy Animation Clip";

    [MenuItem(ITEM_NAME)]
    private static void CopyAnimationClip()
    {
        if (!CanCopyAnimationClip())
        {
            return;
        }

        var clipList = new List<AnimationClip>();

        foreach (var clip in Selection.objects.OfType<AnimationClip>())
        {
            var clone = new AnimationClip();
            var events = AnimationUtility.GetAnimationEvents(clip);
            var srcClipInfo = AnimationUtility.GetAnimationClipSettings(clip);

            AnimationUtility.SetAnimationEvents(clone, events);
            AnimationUtility.SetAnimationClipSettings(clone, srcClipInfo);

            foreach (var n in AnimationUtility.GetAllCurves(clip, true))
            {
                clone.SetCurve(
                    relativePath: n.path,
                    type: n.type,
                    propertyName: n.propertyName,
                    curve: n.curve
                );
            }

            var path = AssetDatabase.GetAssetPath(clip);
            var directory = Path.GetDirectoryName(path);
            var outputPath = directory + "/" + clip.name + ".anim";

            AssetDatabase.CreateAsset(clone, outputPath);
            clipList.Add(clone);
        }

        AssetDatabase.Refresh();
        Selection.objects = clipList.ToArray();
    }

    [MenuItem(ITEM_NAME, true)]
    private static bool CanCopyAnimationClip()
    {
        return Selection.objects.All(c => c is AnimationClip);
    }
}