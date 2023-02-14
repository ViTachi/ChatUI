using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class FontTextureSetuper : MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private Text text;
    [SerializeField] private Texture2D texture;

    private void Awake()
    {
        text.materialForRendering.mainTexture = atlas.GetSprite(texture.name).texture;
    }
}
