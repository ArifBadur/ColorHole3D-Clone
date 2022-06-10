using UnityEngine;
using UnityEngine.SceneManagement;

public class UndergroundCollision : MonoBehaviour
{
    public UIManager uIManager;

    void OnTriggerEnter(Collider other)
    {
        //Object or Obstacle is at the bottom of the Hole

        if (!Game.isGameover)
        {
            string tag = other.tag;
            //------------------------ O B J E C T --------------------------
            if (tag.Equals("Object"))
            {
                //Decrease in the number of objects
                if (!uIManager.halfLevel)
                {
                    Level.Instance.totalFirstObjectsCount--;
                }
                else
                {
                    Level.Instance.totalSecondObjectsCount--;
                }

                Level.Instance.objectsInScene--;

                //Slider progress
                UIManager.Instance.UpdateLevelProgress();

                //Make sure to remove this object from Magnetic field
                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);

                Destroy(other.gameObject);

                //check if win
                if (Level.Instance.objectsInScene == 0 && Level.Instance.gameStart == true)
                {
                    //win text
                    uIManager.completed.SetActive(true);

                    //money
                    uIManager.money += 100;
                    PlayerPrefs.SetInt("money", uIManager.money);
                    uIManager.totalMoney.text = uIManager.money.ToString();

                    //no more objects to collect (WIN)
                    Level.Instance.PlayWinFx();

                    //Load Next level after 2 seconds
                    Invoke("NextLevel", 2f);
                }
            }
            //---------------------- O B S T A C L E -----------------------
            if (tag.Equals("Obstacle"))
            {
                //Gameover
                Game.isGameover = true;
                Destroy(other.gameObject);
                //Restart Level
                Level.Instance.RestartLevel();
            }
        }
    }

    void NextLevel()
    {
        Level.Instance.LoadNextLevel();
    }
}
