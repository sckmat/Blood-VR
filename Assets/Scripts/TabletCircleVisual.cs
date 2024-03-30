using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCircleVisual : MonoBehaviour
{
    private Renderer _circleRenderer;
    private Dictionary<CircleState, List<Material>> _materials;

    private void Awake()
    {
        LoadMaterials();
        _circleRenderer = GetComponent<Renderer>(); 
    }

    private void LoadMaterials()
    {
        _materials = new Dictionary<CircleState, List<Material>>
        {
            { CircleState.Empty, new List<Material>(Resources.LoadAll<Material>("Materials/Empty")) },
            { CircleState.Serum, new List<Material>(Resources.LoadAll<Material>("Materials/Serum")) },
            { CircleState.FormedElements, new List<Material>(Resources.LoadAll<Material>("Materials/FormedElements")) },
            { CircleState.Colyclone, new List<Material>(Resources.LoadAll<Material>("Materials/Colyclone")) },
            { CircleState.Erythrocyte, new List<Material>(Resources.LoadAll<Material>("Materials/Erythrocyte")) },
            { CircleState.Agglutination,  new List<Material>(Resources.LoadAll<Material>("Materials/Agglutination"))},
            { CircleState.NoAgglutination, new List<Material>(Resources.LoadAll<Material>("Materials/NoAgglutination"))},
            { CircleState.FormedElementsReagentMix, new List<Material>(Resources.LoadAll<Material>("Materials/FormedElementsReagentMix"))},
            { CircleState.SerumReagentMix, new List<Material>(Resources.LoadAll<Material>("Materials/SerumReagentMix"))}
        };
    }

    public void UpdateMaterial(CircleState state, Colyclone? colyclone = null)
    {
        Material materialToUse = null;

        if (colyclone is not null && state == CircleState.Colyclone)
            materialToUse = FindMaterialForColyclone(colyclone.Value);

        if (materialToUse is null && _materials.TryGetValue(state, out var materialList))
        {
            if (materialList.Count > 0)
            {
                var randomIndex = Random.Range(0, materialList.Count);
                materialToUse = materialList[randomIndex];
            }
        }

        if (materialToUse is not null)
        {
            _circleRenderer.material = materialToUse;
        }
    }
    
    private Material FindMaterialForColyclone(Colyclone colyclone)
    {
        var materialName = $"Materials/Colyclone/{colyclone.ToString()}";
        return Resources.Load<Material>(materialName);
    }
}