using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Platform : GameBehaviour {

    [EnumMask]
    public Dimensions ActiveDimensions;

    private GameObject meshContainer;

    protected override void Awake()
    {
        base.Awake();

        meshContainer = gameObject.FindChild("Mesh");
    }

    public override void ShiftTo(Dimensions dimension)
    {
        base.ShiftTo(dimension);

        meshContainer.SetActive((dimension & ActiveDimensions) == dimension);
    }
}
