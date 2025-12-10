using System;

namespace ExcelPatternTool
{
    using ExcelPatternTool.Contracts.Validations;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ValidationConverter : JsonConverter<IValidation>
    {
        public override bool CanWrite => false;

        public override IValidation ReadJson(JsonReader reader, Type objectType, IValidation existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var obj = JObject.Load(reader);

            var result = new Validation.Validation();
            serializer.Populate(obj.CreateReader(), result);
            return result;
        }

        public override void WriteJson(JsonWriter writer, IValidation value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not used because CanWrite = false.");
        }
    }
}



