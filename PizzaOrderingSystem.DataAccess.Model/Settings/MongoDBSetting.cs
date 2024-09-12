namespace PizzaOrderSystem.DataAccess.Model.Settings;

public class MongoDBSetting
{
    public string DataBase { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }

    public string ConnectionString => $"mongodb+srv://{UserName}:{Password}@cluster0.c3mxa5q.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

}
