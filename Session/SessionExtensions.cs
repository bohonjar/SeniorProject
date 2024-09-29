using Microsoft.AspNetCore.Http; // Importing ASP.NET Core HTTP functionalities
using System.Text.Json; // Importing JSON serialization functionalities

public static class SessionExtensions // Defining a static class for session extension methods
{
    // Method to store an object as JSON in the session
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // Serializing the object to a JSON string and storing it in the session under the specified key
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    // Method to retrieve an object from the session stored as JSON
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key); // Retrieving the JSON string from the session using the specified key
        // If the value is null, return the default value for the type; otherwise, deserialize the JSON string back to the specified type
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}