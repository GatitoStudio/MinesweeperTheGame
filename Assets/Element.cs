using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Element : MonoBehaviour
{
    public bool isMine = false;
    public Sprite[] emptyTexture;
    public Sprite mineTexture;
    // Start is called before the first frame update
    void Start()
    {
        isMine = Random.value < 0.15;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        //on remplis le tableau de l'objet atache au go (on a pas besoin d'avoir le go , on se detache d'unity )
        DataGame.elements[x, y] = this;
    }
    //lancement au clique de la texture, si c'est une mine on l'affiche (et c'est perdu) 
    //Sinon on affiche le nombre de mine adjacent et c'est facile avec le tableau on a juste a le faire avec l'index
    public void ChangeTexture(int nbMineAdjacent)
    {
        if (isMine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTexture[nbMineAdjacent];
        }
    }
    //Déja ouvert ?
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }
    void OnMouseUpAsButton()
    {
        // It's a mine
        if (isMine)
        {
            DataGame.uncoverMines();
            StartCoroutine(Again());
        }
        // It's not a mine
        else
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            ChangeTexture(DataGame.adjacentMines(x, y));
            DataGame.FFuncover(x, y, new bool[DataGame.w, DataGame.h]);
            if (DataGame.isFinished())
            {
                print("you win");
                StartCoroutine(Again());
            }
        }
    }
    IEnumerator Again()
    {
       yield  return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(0);
    }
}
