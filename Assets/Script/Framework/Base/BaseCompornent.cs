using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCompornent : MonoBehaviour {

    //座標
	public Vector3 Position
    {
        set { transform.position = value; }
        get { return transform.position; }
    }
    public float PosX
    {
        set
        {
            Vector3 pos = Position;
            pos.x = value;
            Position = pos;
        }
        get { return Position.x; }
    }
    public float PosY
    {
        set
        {
            Vector3 pos = Position;
            pos.y = value;
            Position = pos;
        }
        get { return Position.y; }
    }
    public float PosZ
    {
        set
        {
            Vector3 pos = Position;
            pos.z = value;
            Position = pos;
        }
        get { return Position.z; }
    }

    public void AddPosition(float vx, float vy, float vz)
    {
        PosX += vx;
        PosY += vy;
        PosZ += vz;
    }

    public void AddPosition(float vx, float vy) { AddPosition(vx, vy, 0.0f); }

    public void SetPosition(float x, float y) { SetPosition(x, y, 0.0f); }
    public void SetPosition(float x, float y, float z) { Position.Set(x, y, z); }
    

    //拡縮
    public Vector3 Scale
    {
        set { transform.localScale = value; }
        get { return transform.localScale; }
    }
    public float ScaleX
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            Scale = scale;
        }
        get { return Scale.x; }
    }
    public float ScaleY
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.y = value;
            Scale = scale;
        }
        get { return Scale.y; }
    }
    public float ScaleZ
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.z = value;
            Scale = scale;
        }
        get { return Scale.z; }
    }

    public void SetScale(float x, float y) { Scale.Set(x, y, (x + y) / 2); }
    public void AddScale(float value)
    {
        ScaleX += value;
        ScaleY += value;
        ScaleZ += value;
    }
    public void MultiplyScale(float rate) { Scale *= rate; }

    public float ScaleValue
    {
        set
        {
            ScaleX = value;
            ScaleY = value;
        }
        get { return (ScaleX + ScaleY) / 2.0f; }
    }

    //回転（2D用）
    public Vector3 Angle
    {
        set { transform.eulerAngles = value; }
        get { return transform.eulerAngles; }
    }

    public float DegAngle
    {
        set
        {
            Vector3 angle = Angle;
            angle.z = value;
            Angle = angle;
        }
        get { return Angle.z; }
    }

    //左右どちらの方向を見ているか(右なら真)
    public bool GetDirection()
    {
        return Angle.y < 180.0f;
    }

    //左右反転
    public void InverseHorizontal()
    {
        Vector3 angle = Angle;
        angle.y = angle.y > 0.0f ? 0.0f : 180.0f;
        Angle = angle;
    }

    //上下反転
    public void InverseVertical()
    {
        Vector3 angle = Angle;
        angle.x = angle.x > 0.0f ? 0.0f : 180.0f;
        Angle = angle;
    }

    //アルファ値
    public float MaterialAlpha
    {
        set
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a = value;
            gameObject.GetComponent<Renderer>().material.color = color;
        }
        get
        {
            return gameObject.GetComponent<Renderer>().material.color.a;
        }
    }

    //色変更
    public Color MaterialColor
    {
        set
        {
            gameObject.GetComponent<Renderer>().material.color = value;
        }
        get
        {
            return gameObject.GetComponent<Renderer>().material.color;
        }
    }

    //指定のオブジェクトを探す
    public GameObject FindObject(string _objectName) { return GameObject.Find(_objectName); }

    //指定のオブジェクトのコンポーネントを取得
    public T GetComponent<T>(GameObject _obj) { return _obj.GetComponent<T>(); }
    public T GetComponent<T>(string _objectName)
    {
        var obj = GameObject.Find(_objectName);
        return obj.GetComponent<T>();
    }

    //自身の指定のコンポーネントを取得
    public T GetThisComponent<T>() { return gameObject.GetComponent<T>(); }
    

    //オブジェクト破棄
    public void Destroy()
    {
        Destroy(gameObject);
    }

    //指定のオブジェクトを破棄
    public void Destroy(string _objectName)
    {
        Destroy(GameObject.Find(_objectName));
    }
}
