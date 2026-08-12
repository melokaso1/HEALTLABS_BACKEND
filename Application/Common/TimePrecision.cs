namespace Application.Common;

/// <summary>
/// Garantiza que las horas de agenda usen solo horas y minutos (sin segundos ni fracciones).
/// </summary>
public static class TimePrecision
{
    public static void EnsureHhMm(string fieldName, TimeOnly value)
    {
        if (value.Second != 0 || value.Millisecond != 0 || value.Microsecond != 0 || value.Nanosecond != 0)
            throw new ArgumentException($"{fieldName} debe usarse en formato HH:mm (sin segundos ni fracciones).");
    }

    public static void EnsureHhMm(params (string FieldName, TimeOnly Value)[] values)
    {
        foreach (var (fieldName, value) in values)
            EnsureHhMm(fieldName, value);
    }
}
