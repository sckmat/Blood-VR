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
        statistics = new Statistics();
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

        if (reader.TokenType != JsonToken.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.EndObject)
            {
                break;
            }

            string key = reader.Value.ToString();
            var parts = key.Split('_');
            if (parts.Length != 2)
            {
                throw new JsonException("Invalid key format");
            }

            BloodType bloodType;
            if (!Enum.TryParse(parts[0], out bloodType))
            {
                throw new JsonException("Invalid blood type");
            }

            RhesusFactor rhesusFactor;
            if (!Enum.TryParse(parts[1], out rhesusFactor))
            {
                throw new JsonException("Invalid rhesus factor");
            }

            reader.Read();
            var statistics = serializer.Deserialize<StatisticsData>(reader);

            dictionary[new BloodSample(bloodType, rhesusFactor)] = statistics;
        }

        return dictionary;
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

