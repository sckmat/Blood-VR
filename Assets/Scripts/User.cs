using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class User
{
    public string nickname { get; set; }
    
    // [JsonConverter(typeof(BloodGroupStatisticsConverter))]
    // public Dictionary<BloodSample, Statistics.StatisticsData> bloodGroupStatistics { get; set; }
    // public int levels;
    
    public Statistics statistics;

    public User(string nick)
    {
        nickname = nick;
        statistics = new Statistics(new Dictionary<BloodSample, StatisticsData>());
        // bloodGroupStatistics = Statistics.BloodGroupStatistics;
        // levels = Statistics.levelsCompleted;
    }
}

public class BloodGroupStatisticsConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Dictionary<BloodSample, StatisticsData>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var dictionary = new Dictionary<BloodSample, StatisticsData>();

        // Убедитесь, что JSON начинается с объекта
        if (reader.TokenType != JsonToken.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.EndObject)
            {
                break; // Завершение чтения JSON объекта
            }

            // Считываем ключ
            string key = reader.Value.ToString();
            reader.Read();

            // Считываем значение, соответствующее ключу
            StatisticsData statistics = serializer.Deserialize<StatisticsData>(reader);

            // Преобразуем строку ключа в объект BloodSample
            BloodSample bloodSample = ParseBloodSample(key);

            dictionary[bloodSample] = statistics;
        }

        return dictionary;
    }

    private BloodSample ParseBloodSample(string key)
    {
        // Разделяем строку на части по символу подчеркивания
        string[] parts = key.Split('_');
        if (parts.Length != 2)
        {
            throw new JsonException("Invalid key format");
        }

        // Преобразуем первую часть в BloodType
        if (!Enum.TryParse(parts[0], true, out BloodType bloodType))
        {
            throw new JsonException("Invalid blood type");
        }

        // Преобразуем вторую часть в RhesusFactor
        RhesusFactor rhesusFactor = parts[1].Equals("Negative", StringComparison.OrdinalIgnoreCase) 
            ? RhesusFactor.Negative 
            : RhesusFactor.Positive;

        return new BloodSample(bloodType, rhesusFactor);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var dictionary = (Dictionary<BloodSample, StatisticsData>)value;
        writer.WriteStartObject();
        foreach (var kvp in dictionary)
        {
            string key = $"{kvp.Key.bloodType}_{kvp.Key.rhesusFactor}";
            writer.WritePropertyName(key);
            serializer.Serialize(writer, kvp.Value);
        }
        writer.WriteEndObject();
    }
}

