namespace UserApi.Data;

public interface IDBInitializer
{
    void initialize(UserApiContext userApiContext);
}