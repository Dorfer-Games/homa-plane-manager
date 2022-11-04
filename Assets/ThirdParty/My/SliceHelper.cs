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

        if (target.TryGetComponent<Rigidbody>(out Rigidbody rigid) && rigid.isKinematic) // ���� ����� ���� ���� �����������, �� ��� �� ����
            rigid.isKinematic = false;

        target.parent = null; // ���� �������� ��������, �� ������ ����� ��� �������, �� ���������� ����� ����� ������� �������

        if (objectSliceMaterial == null)
            if(target.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer SMR))
                objectSliceMaterial = SMR.material;
            else objectSliceMaterial = target.GetComponent<MeshRenderer>().material;

        SlicedHull hull = target.gameObject.Slice(slicePosition, sliceVector, materialInSlice); //���������� � �������
        // � ������, ���� ������ ������� �� ��������� ����, �� ��������� ����� ����������
        // ���������� ����������� �� ��������� ����
        if (hull == null && target.TryGetComponent<Collider>(out Collider BC)) 
            hull = target.gameObject.Slice(BC.bounds.center, sliceVector, materialInSlice);
        // � ������, ���� ������ ������� �� ��������� ����, �� ��������� ����� ����
        // ���������� ����������� �� ��������� ����
        if (hull == null && target.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            hull = target.gameObject.Slice(mesh.bounds.center, sliceVector, materialInSlice);

        GameObject sait = hull.CreateUpperHull(target.gameObject, materialInSlice); //������� �����
        SlicedSait(sait, objectSliceMaterial, newRigidMass, forcePower, DisableShadow);

        GameObject _sait = hull.CreateLowerHull(target.gameObject, materialInSlice); // ������ �����
        SlicedSait(_sait, objectSliceMaterial, newRigidMass, -forcePower, DisableShadow);

        GameObject.Destroy(target.gameObject); //��, �� ���� ������� ��� �����, ���� ����������

        return new Transform[] { sait.transform, _sait.transform };
    }

    private static void SlicedSait(GameObject sait, Material objectSliceMaterial, float newRigidMass, float forcePower, bool DisableShadow)
    {
        var mesh = sait.GetComponent<MeshRenderer>();
        if (DisableShadow) // ����
            mesh.receiveShadows = false;
        if (objectSliceMaterial != null) //������ ���������, ���� ����
            mesh.sharedMaterial = objectSliceMaterial;

        var rigidbody = sait.AddComponent<Rigidbody>();
        rigidbody.mass = newRigidMass; // ��������� �����

        rigidbody.AddForce(Vector3.one * forcePower, ForceMode.Impulse); // ������ ����� ���������

        sait.AddComponent<BoxCollider>();
        sait.AddComponent<BoxCollider>().isTrigger = true;
    }*/
}
