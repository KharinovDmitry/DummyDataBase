using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DummyDataBase
{
    class JsonElement
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "isPrimary")]
        public bool isPrimary { get; set; }
    }
    class JsonTable
    {

        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string TableName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "columns")]
        public List<JsonElement> Fields { get; set; }
    }

}
