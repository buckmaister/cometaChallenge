using System;

[Serializable]
public class NftResponse
{
    public NftData nft;
}

[Serializable]
public class NftData
{
    public string identifier;
    public string collection;
    public string contract;
    public string token_standard;
    public string name;
    public string description;
    public string image_url;
    public string metadata_url;
    public string created_at;
    public string updated_at;
    public bool is_disabled;
    public bool is_nsfw;
    public string animation_url;
    public bool is_suspicious;
    public string creator;
    public object traits;
    public Owner[] owners;
    public object rarity;
}

[Serializable]
public class Owner
{
    public string address;
    public int quantity;
}

[Serializable]
public class MetadataData
{
    public string name;
    public string description;
    public string image;
    public string animation_url;
    public string remote_asset;
}
