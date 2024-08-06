using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public ColorData colorData;
    public TypeColor color;
    [SerializeField] private Rigidbody rbBrick;
    [SerializeField] private Renderer render;
    [SerializeField] private BoxCollider boxCollider;

    [Header("Force Calculation")]
    public float minExplosionForce = 5f;  
    public float maxExplosionForce = 15f; 
    public float minExplosionRadius = 5f; 
    public float maxExplosionRadius = 10f; 
    public Transform tfrmBrick;
    private bool isCollected = false;
    public bool IsCollected
    {
        get { return isCollected; }
        set { isCollected = value; }
    }

    public void OnInit()
    {

    }
    public void ChangeColor(TypeColor colorType)
    {
        color = colorType;
        render.material = colorData.GetMat(colorType);
    }
    public void Explode(Vector3 pos,Transform parent)
    {
        rbBrick.isKinematic = false;
        tfrmBrick.SetParent(parent);
        ChangeColor(TypeColor.none);
        CalcForce();
        StartCoroutine(IDeleyEnabledBox());
       // rbBrick.AddExplosionForce(Random.Range(minExplosionForce, maxExplosionForce), pos, Random.Range(minExplosionRadius, maxExplosionRadius), 1, ForceMode.Impulse);
        Debug.Log("Explode: "+ pos);
    }
    private void CalcForce()
    {
        float explosionRadius = Random.Range(minExplosionRadius, maxExplosionRadius);
        float explosionForce = Random.Range(minExplosionForce, maxExplosionForce);
        Vector3 ranDirection = Random.onUnitSphere;
        ranDirection.y = 0;
        Vector3 forceDirection = ranDirection.normalized * explosionForce;
        rbBrick.AddForce(forceDirection*Time.deltaTime*10, ForceMode.Impulse);
    }
    IEnumerator IDeleyEnabledBox()
    {
        yield return new WaitForSeconds(0.1f);
        IsCollected = false;
        boxCollider.enabled = true;
    }
    public void Collect()
    {
        IsCollected = true;
        rbBrick.isKinematic = true;
        boxCollider.enabled = false;
    }
}
