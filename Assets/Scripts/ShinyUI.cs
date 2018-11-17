using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShinyUI : MonoBehaviour
{
    [SerializeField]
    private CollectShinyThings shinyCollectScript;

    [SerializeField]
    private Image barPrefab;

    private List<GameObject> bars = new List<GameObject>();
    private Image barInstance;
    private float barX;
    private int barSpacing = 15;


    public void AddUI()
    {
        barInstance = Instantiate(barPrefab, new Vector2(transform.position.x - 130 + barX, transform.position.y), transform.rotation, this.gameObject.transform);
        bars.Add(barInstance.gameObject);
        barX += barSpacing;//every time we move it over a bit
    }

    public void DeleteUI()
    {
        GameObject barToDestroy = bars[bars.Count - 1];
        bars.Remove(barToDestroy);
        Destroy(barToDestroy);
        barX -= barSpacing;
    }
}
