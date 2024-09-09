using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class GenerateurPot : MonoBehaviour
{
    [SerializeField]
    private PotFleur prototypePot;

    [SerializeField]
    private EmplacementPot[] emplacementsPot;

    private List<PotFleur> potsFleur;

    private void Awake()
    {
        potsFleur = new();
    }

    private void Start()
    {
        emplacementsPot = new EmplacementPot[transform.childCount];

        int indice = 0;

        foreach(Transform enfant in transform)
        {
           

            if(enfant.TryGetComponent(out EmplacementPot emplacement))
            {
                emplacementsPot[indice] = emplacement.GetComponent<EmplacementPot>();
                indice++;
            }
            else
            {
                Debug.LogError($"L'objet {enfant.name} n'a pas de script EmplacementPot");
            }

        }
    }
    public void CreerPot(InputAction.CallbackContext contexte)
    {
        bool aEteCree = false;
        
        if(potsFleur.Count >= emplacementsPot.Length || !contexte.started)
        {
            return;
        }

        while(!aEteCree)
        {

            EmplacementPot emplacement = emplacementsPot[Random.Range(0, emplacementsPot.Length)];

            if (emplacement.EstOccupe)
            {
                continue;
            }
            aEteCree = true;

            PotFleur nouveauPot = Instantiate(prototypePot);
            nouveauPot.transform.position = emplacement.transform.position;

            emplacement.EstOccupe = true;
            potsFleur.Add(nouveauPot);
        }
    }
}
