using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBubble : MonoBehaviour
{
    public Item item;
    Material material;
    private void Awake()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up*5, ForceMode.Impulse);
        StartCoroutine(AvoidCollision());
        //material = new Material(Shader.Find("Standard"));
        //material.SetTexture("_MainTex", item.itemImage.texture);
        //transform.GetChild(0).GetComponent<MeshRenderer>().material = this.material;
        //material.SetTexture("_MainTex", textureFromSprite(item.itemImage));
        //transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
    }
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
    IEnumerator AvoidCollision()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        yield return new WaitForSeconds(1);
        collider.isTrigger = false;
        yield return null;

    }
    public void SetItem(Item item)
    {
        this.item = item;
        material = new Material(Shader.Find("Standard"));
        //material.SetTexture("_MainTex", item.itemImage.texture);
        //transform.GetChild(0).GetComponent<MeshRenderer>().material = this.material;
        Texture2D texture = textureFromSprite(item.itemImage);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = 0;
        material.SetTexture("_MainTex", texture); 
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<ItemContainer>().AddItem(item);
            Destroy(gameObject);
        }
    }
}
