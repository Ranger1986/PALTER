using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class NoiseScript
{
    public int width = 256;
    public int height = 256;
    // Start is called before the first frame update

    public float scale = 20f;
    public float offsetx = Random.Range(0,200);
    public float offsety = Random.Range(0,200);

    public NoiseScript(int width, int height, float scale){
        this.width=width;
        this.height=height;
        this.scale=scale;
    }

    public Texture2D GenerateTexture(){
        Texture2D texture = new Texture2D(width,height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x,y);
                texture.SetPixel(x,y,color);
            }
        }
        texture.Apply();
        return texture;

    }
    Color CalculateColor(int x, int y){
        float xCoord = (float) x / width  * scale + offsetx;
        float yCoord = (float) y / height * scale + offsety;
        float sample = Mathf.PerlinNoise(xCoord,yCoord);
        return new Color(sample,sample,sample);
    }
}
