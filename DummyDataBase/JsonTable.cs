using Newtonsoft.Json;

namespace DummyDataBase
{
    class JsonTableElement
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "referencedTable")]
        public string ReferencedTable { get; set; }
        [JsonProperty(PropertyName = "isPrimary")]
        public bool isPrimary { get; set; }
    }
    class JsonTable
    {

        [JsonProperty(PropertyName = "name")]
        public string TableName { get; set; }
        [JsonProperty(PropertyName = "columns")]
        public List<JsonTableElement> Fields { get; set; }
    }

}
