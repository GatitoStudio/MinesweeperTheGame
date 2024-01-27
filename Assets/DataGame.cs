using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGame 
{
    public static int w = 10; // this is the width
    public static int h = 13; // this is the height
    public static Element[,] elements = new Element[w, h];
    public static void uncoverMines()
    {
        foreach (Element elem in elements)
            if (elem.isMine) elem.ChangeTexture(0);
    }
    //Il y a une mine en x y ? 
    public static bool mineAt(int x, int y)
    {
        // Gestion des bord 
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].isMine;
        return false;
    }
    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) ++count; // top
        if (mineAt(x + 1, y + 1)) ++count; // top-right
        if (mineAt(x + 1, y)) ++count; // right
        if (mineAt(x + 1, y - 1)) ++count; // bottom-right
        if (mineAt(x, y - 1)) ++count; // bottom
        if (mineAt(x - 1, y - 1)) ++count; // bottom-left
        if (mineAt(x - 1, y)) ++count; // left
        if (mineAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        // Coordinates in Range?
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].ChangeTexture(adjacentMines(x, y));

            // Les bordure de ce qui affiche est quand on a plus de 1 voisin qui a une mine , c'est l'un des cas d'arret de la fonction recursive
            if (adjacentMines(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }
    public static bool isFinished()
    {
        // Try to find a covered element that is no mine
        foreach (Element elem in elements)
            if (elem.isCovered() && !elem.isMine)
                return false;
        // There are none => all are mines => game won.
        return true;
    }
}
