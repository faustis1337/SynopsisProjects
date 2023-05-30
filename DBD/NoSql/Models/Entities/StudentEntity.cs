using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSql.Entities.Entities;

public class StudentEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public string[] Classes { get; set; }
}