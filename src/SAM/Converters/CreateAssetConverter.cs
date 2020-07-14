using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SAM.Converters
{
    public class CreateAssetConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var createAssetParams = value as DTO.SamCreateAssetParams;
            if (createAssetParams == null)
                return;

            writer.WriteStartObject();

            writer.WritePropertyName("author");
            if (createAssetParams.author_type == AssetAuthorType.custom)
            {
                serializer.Serialize(writer, createAssetParams.author);
            }
            else
            {
                writer.WriteValue(Enum.GetName(typeof(AssetAuthorType), createAssetParams.author_type));
            }

            if (createAssetParams.text != null)
            {
                writer.WritePropertyName("text");
                writer.WriteValue(createAssetParams.text);
            }

            if (createAssetParams.text != null)
            {
                writer.WritePropertyName("media_id");
                writer.WriteValue(createAssetParams.media_id);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DTO.SamCreateAssetParams);
        }
    }
}
