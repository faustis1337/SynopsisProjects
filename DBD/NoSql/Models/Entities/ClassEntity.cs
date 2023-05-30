using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSql.Entities.Entities;

public class ClassEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    public String ClassName { get; set; }
    public String ClassInfo { get; set; }
    public string[] Students { get; set; }
}