using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Platform : GameBehaviour {

    [EnumMask]
    public Dimensions ActiveDimensions;
    
    public Sprite activeSprite;
    public Sprite inactiveSprite;

	private Color Red = new Color(214/255.0f, 69/255.0f, 64/255.0f);
	private Color Green = new Color(57/255.0f, 163/255.0f, 65/255.0f);
	private Color Both = new Color(59/255.0f, 99/255.0f, 161/255.0f);

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
		spriteCover.Color = (ActiveDimensions == Dimensions.Red) ? Red : (ActiveDimensions == Dimensions.Green) ? Green : Both;
    }
}
