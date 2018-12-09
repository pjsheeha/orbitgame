using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour {

    public enum PointsType
    {
        Coins,
        Destructibles
    };

    public PointsType pointsType;
    public int[] pointsPerOrb = { };
    
    public List<GameObject> orbs;
    private int orbsObtained = 1;
    private int currentPoints = 0;
    private int totalPoints = 0;

	// Initializes orbs list based on order of hierarchy traversal
	void Start ()
    {
        if (pointsPerOrb.Length != 0)
        {
            //disable all orbits except first one at start
            for (int i = orbsObtained; i < orbs.Count; i++)
            {
                orbs[i].SetActive(false);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (pointsType == PointsType.Coins)
        {
            if (other.gameObject.CompareTag("coin"))
            {
                other.gameObject.SetActive(false);
                AddPoints();
            }
        }
    }

    public void AddPoints(int points = 1)
    {
        currentPoints += points;
        totalPoints += points;

        if (pointsPerOrb.Length > 0)
        {
            int pointsToNextJet = pointsPerOrb[Mathf.Min(orbsObtained - 1, pointsPerOrb.Length - 1)];
            if (currentPoints == pointsToNextJet && orbsObtained < orbs.Count)
            {
                currentPoints = 0;
                orbsObtained++;
                orbs[orbsObtained - 1].SetActive(true);
            }
        }

        Debug.Log("Player earned " + points + " points. Current: " + currentPoints + " Total: " + totalPoints);
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }
}
