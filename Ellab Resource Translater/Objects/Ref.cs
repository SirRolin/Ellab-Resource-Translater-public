using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ellab_Resource_Translater.Objects
{
    /// <summary>
    /// Since the buildin <see cref="Delegate"/> copies variables without being able to keep a reference.
    /// We can use this to pass a referenced variable.
    /// </summary>
    /// <param name="value"></param>
    [System.Text.Json.Serialization.JsonConverter(typeof(RefJsonConverterFactory))]
    [Newtonsoft.Json.JsonConverter(typeof(RefNewtonsoftConverter))]
    public class Ref<T>(T value)
    {
        public T value = value;
        public static implicit operator T(Ref<T> valRef) => valRef.value;
        public static implicit operator Ref<T>(T val) => new(val);
        override
        public string? ToString()
        {
            return value?.ToString();
        }
    }

    // So that we can automaticly serialise it as it's value instead of an object holding the value
    public class RefJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Ref<>);

        public override System.Text.Json.Serialization.JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type valueType = typeToConvert.GetGenericArguments()[0];
            Type converterType = typeof(RefJsonConverter<>).MakeGenericType(valueType);
            return (System.Text.Json.Serialization.JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }

    // System Json / Gson
    public class RefJsonConverter<T> : System.Text.Json.Serialization.JsonConverter<Ref<T>>
    {
        public override Ref<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            T value = System.Text.Json.JsonSerializer.Deserialize<T>(ref reader, options)!;
            return new Ref<T>(value);
        }

        public override void Write(Utf8JsonWriter writer, Ref<T> value, JsonSerializerOptions options)
        {
            System.Text.Json.JsonSerializer.Serialize(writer, value.value, options);
        }
    }
    // Newtonsoft.Json Converter
    public class RefNewtonsoftConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Ref<>);

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Type valueType = objectType.GetGenericArguments()[0];
            object? value = serializer.Deserialize(reader, valueType);
            return Activator.CreateInstance(objectType, value);
        }

        public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value?.GetType().GetGenericTypeDefinition().Equals(typeof(Ref<>)) ?? false)
            {
                
                var refObj = value.GetType().GetField(nameof(Ref<bool>.value))?.GetValue(value);
                serializer.Serialize(writer, refObj);
            }
        }
    }
}
