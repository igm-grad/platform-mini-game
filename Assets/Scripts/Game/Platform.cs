using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Platform : GameBehaviour {

    [EnumMask]
    public Dimensions ActiveDimensions;
    
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private GameObject meshContainer;
    private SpriteCover spriteCover;

    protected override void Awake()
    {
        base.Awake();

        meshContainer = gameObject.FindChild("Mesh");
        spriteCover = gameObject.FindChild("Sprites").GetComponent<SpriteCover>();
    }

    public override void ShiftTo(Dimensions dimension)
    {
        base.ShiftTo(dimension);
        var active = (dimension & ActiveDimensions) == dimension;
        meshContainer.SetActive(active);

        spriteCover.Sprite = active ? activeSprite : inactiveSprite;
    }
}
