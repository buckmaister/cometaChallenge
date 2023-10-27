using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenSeaApi : MonoBehaviour
{
    /*  PARAMS for challenge
     * id 0
     * chain goerli
     * addr 0x699311a306b6D50567c1E707Bb14923Bd0BA6D93
     * base https://testnets-api.opensea.io/api/v2/chain/{chain}/contract/{address}/nft/{identifier}
     *
     * FOR Instance
     * pos 0,0,0
     */

    private string openSeaUrl = "https://testnets-api.opensea.io/api/v2/chain/goerli/contract/0x699311a306b6D50567c1E707Bb14923Bd0BA6D93/nfts/0";

    private BundleWebLoader _loader;
    private Vector3 spawnPos = new Vector3(0,0,0);
    
    private void Start()
    {
        BundleWebLoader loader = GetComponent<BundleWebLoader>();
        
        StartCoroutine(GetMetadataAndRemoteAssetUrl((metadataUrl, remoteAssetUrl) =>
        {
            if (!string.IsNullOrEmpty(metadataUrl) && !string.IsNullOrEmpty(remoteAssetUrl))
            {
                Debug.Log("metadata URL: " + metadataUrl);
                Debug.Log("remote asset URL is: " + remoteAssetUrl);

                
                loader.LoadAssetFromUrl(remoteAssetUrl, "cletter", OnAssetLoaded);
            }
            else
            {
                Debug.LogError("Failed to get metadata URL OR remote asset URL");
            }
        }));
    }

    private void OnAssetLoaded(GameObject loadedAsset)
    {
        if (loadedAsset != null)
        {
            // instantiate the gameobj on the mainscene
            var instantiate = Instantiate(loadedAsset, spawnPos, Quaternion.identity);
            instantiate.AddComponent<RotateObject>();
            
        }
        else
        {
            Debug.LogError("Failed to load and instantiate the asset.");
        }
    }

    IEnumerator GetMetadataAndRemoteAssetUrl(Action<string, string> callback)
    {
        string metadataUrl = string.Empty;
        string remoteAssetUrl = string.Empty;

        yield return StartCoroutine(GetMetadataFromNft((metadata) => metadataUrl = metadata));

        if (!string.IsNullOrEmpty(metadataUrl))
        {
            yield return StartCoroutine(GetRemoteAssetUrlFromJson(metadataUrl, (remoteAsset) => remoteAssetUrl = remoteAsset));
        }

        callback(metadataUrl, remoteAssetUrl);
    }

    IEnumerator GetMetadataFromNft(Action<string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(openSeaUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Request error: " + www.error);
                callback(string.Empty);
                yield break;
            }

            string json = www.downloadHandler.text;
            NftResponse response = JsonUtility.FromJson<NftResponse>(json);

            if (response == null || response.nft == null)
            {
                Debug.LogError("Failed to parse NftResponse.");
                callback(string.Empty);
                yield break;
            }

            string metadataURL = response.nft.metadata_url;
            callback(metadataURL);
        }
    }
    
    IEnumerator GetRemoteAssetUrlFromJson(string jsonUrl, Action<string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(jsonUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Request error for JSON URL: " + www.error);
                callback(string.Empty);
                yield break;
            }

            string json = www.downloadHandler.text;
            MetadataData assetData = JsonUtility.FromJson<MetadataData>(json);

            if (assetData == null)
            {
                Debug.LogError("Failed to parse AssetData from JSON.");
                callback(string.Empty);
                yield break;
            }

            string remoteAssetUrl = assetData.remote_asset; // Puoi scegliere quale campo utilizzare
            callback(remoteAssetUrl);
        }
    }
}
