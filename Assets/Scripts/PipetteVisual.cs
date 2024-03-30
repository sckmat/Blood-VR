using System.Collections.Generic;
using UnityEngine;

public class PipetteVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer content;
    private Dictionary<PipetteState, Material> _materials;

    private void Awake()
    {
        LoadMaterials();
    }

    private void LoadMaterials()
    {
        _materials = new Dictionary<PipetteState, Material>
        {
            { PipetteState.Empty, Resources.Load<Material>("Materials/Empty/Empty") },
            { PipetteState.Serum, Resources.Load<Material>("Materials/Serum/Serum") },
            { PipetteState.FormedElements, Resources.Load<Material>("Materials/FormedElements/FormedElements") },
            { PipetteState.Reagent, Resources.Load<Material>("Materials/Colyclone/AntiD") }
        };
        UpdateMaterial(PipetteState.Empty);
    }

    public void UpdateMaterial(PipetteState state)
    {
        if (_materials.TryGetValue(state, out var material))
        {
            content.material = material;
        }
    }
}