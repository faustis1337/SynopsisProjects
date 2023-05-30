using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSql.Entities.Dtos;

public class UpdateClassDto
{
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    public String ClassName { get; set; }
    public String ClassInfo { get; set; }
}