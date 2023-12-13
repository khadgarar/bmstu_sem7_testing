using Microsoft.Extensions.Configuration;
using System.IO;


namespace ComponentAccessToDB
{
    public static class Connection
    {
        public static string GetConnection(string perms)
        {
            var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("dbappsettings.json")
                   .Build();

            if (perms == "Notauth")
            {
                return config["Connections:ConnectAsNotauth"];
            }
            else if (perms == "User")
            {
                return config["Connections:ConnectAsUser"];
            }
            else if (perms == "Employee")
            {
                return config["Connections:ConnectAsEmployee"];
            }
            else if (perms == "Responsible")
            {
                return config["Connections:ConnectAsResponsible"];
            }
            else if (perms == "Manager")
            {
                return config["Connections:ConnectAsManager"];
            }
            else if (perms == "Founder")
            {
                return config["Connections:ConnectAsFounder"];
            }
            else
            {
                return config["Connections:ConnectAsHR"];
            }
        }
    }
}
