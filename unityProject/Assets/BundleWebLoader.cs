using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BundleWebLoader : MonoBehaviour
{
    private static IEnumerator Load(string url, string assetName, System.Action<GameObject> callback)
    {
        Debug.Log($"Start load asset {assetName} from {url}");
        
        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download AssetBundle: " + webRequest.error);
                callback(null);
                yield break;
            }

            AssetBundle remoteAssetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
            if (remoteAssetBundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                callback(null);
                yield break;
            }

            var asset = remoteAssetBundle.LoadAsset<GameObject>(assetName);
            callback(asset);

            remoteAssetBundle.Unload(false);
        }
    }

    public void LoadAssetFromUrl(string url, string assetName, System.Action<GameObject> callback)
    {
        StartCoroutine(Load(url, assetName, callback));
    }
}