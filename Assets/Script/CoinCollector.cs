using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour {

    public int[] coinsPerOrb = { 5 };
    
    public List<GameObject> orbs;
    private int orbsObtained = 1;
    private int coinsObtained = 0;

	// Initializes orbs list based on order of hierarchy traversal
	void Start ()
    {
        //disable all orbits except first one at start
        for (int i = orbsObtained; i < orbs.Count; i++)
        {
            orbs[i].SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            coinsObtained++;

            int coinsToNextJet = coinsPerOrb[Mathf.Min(orbsObtained - 1, coinsPerOrb.Length - 1)];
            if (coinsObtained == coinsToNextJet && orbsObtained < orbs.Count)
            {
                coinsObtained = 0;
                orbsObtained++;
                orbs[orbsObtained-1].SetActive(true);
            }
        }
    }
}
