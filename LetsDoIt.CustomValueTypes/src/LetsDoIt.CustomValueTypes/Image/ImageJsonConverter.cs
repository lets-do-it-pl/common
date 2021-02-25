using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetsDoIt.CustomValueTypes.Image
{
    public class ImageJsonConverter : JsonConverter<Image>
    {
        public override Image Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            Image.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            Image image,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(image.ToString());
    }
}
