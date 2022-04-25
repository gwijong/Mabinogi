using UnityEngine;

/// <summary> ���� ��������</summary>
public class Gauge
{
    private float _current;  //���� ��ġ
    private float _max;  //�ִ� ��ġ
    private float _fillableRate = 1.0f; // ä�� �� �ִ� �ִ� ����

    public Gauge(float value = 0.0f, float fillable = 1.0f)
    {
        _max = value;
        _current = fillable * _max;
    }

    /// <summary> ��ġ�� ������� üũ</summary>
    public bool IsEmpty { get { return _current <= 0; } } 

    /// <summary> ���� ���� ����</summary>
    public float Rate
    {
        get
        {
            return _current / _max;
        }
        set
        {
            if (value > _fillableRate) value = _fillableRate; //1�� ���� �� �ϵ��� ���� ó��

            value = Mathf.Clamp(value, 0, 1);
            _current = _max * value;
        }
    }
    /// <summary> ���� ��ġ</summary>
    public float Current
    {
        get
        {
            return _current;
        }

        set
        {
            _current = Mathf.Clamp(value, 0, _max * _fillableRate);
        }
    }

    /// <summary> �ִ� ��ġ</summary>
    public float Max
    {
        get
        {
            return _max;
        }

        set
        {
            _max = value;

            float calculateFillable = _max * _fillableRate;
            if (_current > calculateFillable) _current = calculateFillable; //���� �� �ϵ��� ���� ó��
        }
    }

    /// <summary> ä�� �� �ִ� ����</summary>
    public float FillableRate
    {
        get
        {
            return _fillableRate;
        }

        set
        {
            if (_fillableRate > 1.0f) _fillableRate = 1.0f;  //���� �� �ϵ��� ���� ó��
            else _fillableRate = value;

            float calculateFillable = _max * _fillableRate;
            if (_current > calculateFillable) _current = calculateFillable; //���� �� �ϵ��� ���� ó��
        }
    }
}