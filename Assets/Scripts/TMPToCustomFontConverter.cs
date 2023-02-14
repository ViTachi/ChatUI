using System.Collections.Generic;
using UnityEngine;

public class TMPToCustomFontConverter : MonoBehaviour
{
    [SerializeField] private Font customFont;
    [SerializeField] private TMPro.TMP_FontAsset tmpFontAsset;
    [SerializeField] private Vector2 textureSize;

    public void Convert()
    {
        List<CharacterInfo> characterInfos = new List<CharacterInfo>();

        for (int i = 0; i < tmpFontAsset.characterTable.Count; i++)
        {
            CharacterInfo character = new CharacterInfo();
            TMPro.TMP_Character tMP_Character = tmpFontAsset.characterTable[i];
            UnityEngine.TextCore.Glyph glyph = tMP_Character.glyph;

            character.index = (int)tMP_Character.unicode;

            Vector2 charUVPosition = new Vector2(glyph.glyphRect.x / textureSize.x * 0.5f, glyph.glyphRect.y / textureSize.y);
            Vector2 charUVSize = new Vector2(glyph.glyphRect.width / textureSize.x * 0.5f, 
                glyph.glyphRect.height / textureSize.y);

            character.uvTopRight = charUVPosition;
            character.uvTopLeft = new Vector2(charUVPosition.x + charUVSize.x, charUVPosition.y);
            character.uvBottomLeft = new Vector2(charUVPosition.x , charUVPosition.y + charUVSize.y);
            character.uvBottomRight = new Vector2(charUVPosition.x + charUVSize.x, charUVPosition.y + charUVSize.y);

            character.vert = new Rect(
                0,
                0.5f * (glyph.metrics.horizontalBearingY - glyph.glyphRect.height),
                glyph.glyphRect.width * 0.5f,
                glyph.glyphRect.height * 0.5f
                );

            character.advance = glyph.glyphRect.width == 0 ? 20 : glyph.glyphRect.width / 2  + 5;        

            characterInfos.Add(character);
        }

        customFont.characterInfo = characterInfos.ToArray();

        Debug.Log("Convertion complete!");
    }
}
