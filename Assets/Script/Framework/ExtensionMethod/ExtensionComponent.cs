using UnityEngine;

public static class ExtensionComponent
{
    /// <summary>
    /// 複数のタグの内、どれか１つ存在するかチェック
    /// </summary>
    /// <param name="tagList"></param>
    /// <returns>存在：true、無し : false</returns>
    public static bool CompareTag(this Component component, params string[] tagList)
    {
        bool isFound = false;

        foreach(var tag in tagList)
        {
            if(component.CompareTag(tag))
            {
                isFound = true;
                break;
            }
        }
        return isFound;
    }
}