using System;
using TMPro;
using UnityEngine;

public class Reagent : MonoBehaviour
{
    public ReagentType reagentType = ReagentType.None;
    public Colyclone colyclone = Colyclone.None;
    public Erythrocyte erythrocyte = Erythrocyte.None;

    private Renderer _reagentRenderer;
    private Renderer _labelRenderer;

    [SerializeField] private TMP_Text labelText;
    [SerializeField] private Material[] colycloneMaterials;
    [SerializeField] private Material erythrocyteMaterial;

    [SerializeField] private Material defaultLabelMaterial;
    [SerializeField] private Material[] labelMaterials;

    private void Awake()
    {
        _reagentRenderer = GetComponent<Renderer>();
        _labelRenderer = transform.parent.GetComponent<Renderer>();

        DisplayReagent(); 
    }

    private void DisplayReagent()
    {
        switch (reagentType)
        {
            case ReagentType.Colyclone:
                switch (colyclone)
                {
                    case Colyclone.AntiA:
                        _reagentRenderer.material = colycloneMaterials[0];
                        labelText.text = "Анти-А";
                        break;
                    case Colyclone.AntiB:
                        _reagentRenderer.material = colycloneMaterials[1];
                        labelText.text = "Анти-В";
                        break;
                    case Colyclone.AntiD:
                        _reagentRenderer.material = colycloneMaterials[2];
                        labelText.text = "Анти-D";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case ReagentType.Erythrocyte:
                _reagentRenderer.material = erythrocyteMaterial;
                switch (erythrocyte)
                {
                    case Erythrocyte.ErythrocyteA:
                        labelText.text = "А";
                        break;
                    case Erythrocyte.ErythrocyteB:
                        labelText.text = "B";
                        break;
                    case Erythrocyte.ErythrocyteO:
                        labelText.text = "0";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
        }

        Material[] materials = _labelRenderer.materials;
        if (materials.Length > 1)
        {
            materials[1] = GetLabelMaterial();
            _labelRenderer.materials = materials;
        }
    }

    private Material GetLabelMaterial()
    {
        switch (reagentType)
        {
            case ReagentType.Colyclone:
                return labelMaterials[(int)colyclone - 1];
            case ReagentType.Erythrocyte:
                return labelMaterials[3];
            default:
                return defaultLabelMaterial;
        }
    }
}

public enum ReagentType { None, Colyclone, Erythrocyte }
public enum Colyclone { None, AntiA, AntiB, AntiD }
public enum Erythrocyte { None, ErythrocyteA, ErythrocyteB, ErythrocyteO }