using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class NetworkTextureDownloader
{
    public static IEnumerator DownloadImage(string MediaUrl, Action<Texture> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            callback?.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
        }
    }
}