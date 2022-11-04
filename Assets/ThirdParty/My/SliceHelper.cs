using UnityEngine;
//using EzySlice;

public static class SliceHelper
{
 /*   public static Transform[] Slice(this Transform target, GameObject slicer, Material materialInSlice, Material objectSliceMaterial = null, float newRigidMass = 1, float forcePower = 0, bool DisableShadow = false)
    {
        return Slice(target, slicer.transform.up, slicer.transform.position, materialInSlice, objectSliceMaterial, newRigidMass, forcePower, DisableShadow);
    }
    public static Transform[] Slice(this GameObject target, GameObject slicer, Material materialInSlice, Material objectSliceMaterial = null, float newRigidMass = 1, float forcePower = 0, bool DisableShadow = false)
    {
        return Slice(target.transform, slicer.transform.up, slicer.transform.position, materialInSlice, objectSliceMaterial, newRigidMass, forcePower, DisableShadow);
    }
    public static Transform[] Slice(this GameObject target, Transform slicer, Material materialInSlice, Material objectSliceMaterial = null, float newRigidMass = 1, float forcePower = 0, bool DisableShadow = false)
    {
        return Slice(target.transform, slicer.up, slicer.position, materialInSlice, objectSliceMaterial, newRigidMass, forcePower, DisableShadow);
    }
    public static Transform[] Slice(this Transform target, Transform slicer, Material materialInSlice, Material objectSliceMaterial = null, float newRigidMass = 1, float forcePower = 0, bool DisableShadow = false)
    {
        return Slice(target, slicer.up, slicer.position, materialInSlice, objectSliceMaterial, newRigidMass, forcePower, DisableShadow);
    }
    public static Transform[] Slice(this Transform target, Vector3 sliceVector, Vector3 slicePosition, Material materialInSlice, Material objectSliceMaterial = null, float newRigidMass = 1, float forcePower = 0, bool DisableShadow = false)
    {
        if (!target)
        {
            Debug.LogWarning("target is null");
            return null;
        }

        if (target.TryGetComponent<Rigidbody>(out Rigidbody rigid) && rigid.isKinematic) // Если вдруг цель была кинематична, то был бы глюк
            rigid.isKinematic = false;

        target.parent = null; // если оставить родителя, то бывают глюки при нарезке, но порезанные куски можно вернуть обратно

        if (objectSliceMaterial == null)
            if(target.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer SMR))
                objectSliceMaterial = SMR.material;
            else objectSliceMaterial = target.GetComponent<MeshRenderer>().material;

        SlicedHull hull = target.gameObject.Slice(slicePosition, sliceVector, materialInSlice); //подготовка к нарезке
        // в случае, если вектор нарезки за пределами меша, то проверяем центр коллайдера
        // исправляем пересечение за пределами меша
        if (hull == null && target.TryGetComponent<Collider>(out Collider BC)) 
            hull = target.gameObject.Slice(BC.bounds.center, sliceVector, materialInSlice);
        // в случае, если вектор нарезки за пределами меша, то проверяем центр меша
        // исправляем пересечение за пределами меша
        if (hull == null && target.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            hull = target.gameObject.Slice(mesh.bounds.center, sliceVector, materialInSlice);

        GameObject sait = hull.CreateUpperHull(target.gameObject, materialInSlice); //верхний кусок
        SlicedSait(sait, objectSliceMaterial, newRigidMass, forcePower, DisableShadow);

        GameObject _sait = hull.CreateLowerHull(target.gameObject, materialInSlice); // нижний кусок
        SlicedSait(_sait, objectSliceMaterial, newRigidMass, -forcePower, DisableShadow);

        GameObject.Destroy(target.gameObject); //то, из чего сделали два куска, надо уничтожить

        return new Transform[] { sait.transform, _sait.transform };
    }

    private static void SlicedSait(GameObject sait, Material objectSliceMaterial, float newRigidMass, float forcePower, bool DisableShadow)
    {
        var mesh = sait.GetComponent<MeshRenderer>();
        if (DisableShadow) // тень
            mesh.receiveShadows = false;
        if (objectSliceMaterial != null) //замена материала, если надо
            mesh.sharedMaterial = objectSliceMaterial;

        var rigidbody = sait.AddComponent<Rigidbody>();
        rigidbody.mass = newRigidMass; // установка массы

        rigidbody.AddForce(Vector3.one * forcePower, ForceMode.Impulse); // толчок после нарезания

        sait.AddComponent<BoxCollider>();
        sait.AddComponent<BoxCollider>().isTrigger = true;
    }*/
}
