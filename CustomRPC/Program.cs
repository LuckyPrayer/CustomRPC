using System;
using System.Diagnostics;
using Discord;

var CLIENT_ID = "1115004659384471714";

var discord = new Discord.Discord(Int64.Parse(CLIENT_ID), (UInt64)Discord.CreateFlags.Default);

var activityManager = discord.GetActivityManager();

discord.SetLogHook(Discord.LogLevel.Debug, (level, message) =>
{
    Console.WriteLine("Log[{0}] {1}", level, message);
});

discord.RunCallbacks();

var activity = new Discord.Activity
{
    Name = "Work",
    State = "Hardly working",
    Details = "Hard at work",
    Timestamps =
  {
      End = 1702557000,
  },
    Assets =
  {
      LargeImage = "desk-clipart-desk-job-12-4168606885", // Larger Image Asset Value
      LargeText = "Work", // Large Image Tooltip
      SmallImage = "foo smallImageKey", // Small Image Asset Value
      SmallText = "foo smallImageText", // Small Image Tooltip
  },
};

static void UpdateActivity(Discord.Discord discord, Discord.ActivityManager activityManager, Discord.Activity activity)
{

    activityManager.UpdateActivity(activity, (result) =>
    {
        if (result == Discord.Result.Ok)
        {
            Console.WriteLine("RPC Update Success!");
        }
        else
        {
            Console.WriteLine("RPC Update Failed");
        }
    });
}


static void ClearActivity(Discord.Discord discord, Discord.ActivityManager activityManager)
{
    activityManager.ClearActivity((result) =>
    {
        if (result == Discord.Result.Ok)
        {
            Console.WriteLine("RPC Clear Success!");
        }
        else
        {
            Console.WriteLine("RPC Clear Failed");
        }
    });
}

static bool CheckProcess()
{
    Process[] processes = Process.GetProcessesByName("CDViewer");
    if (processes.Length != 0)
    {
        Console.WriteLine("CDViewer detected");

        return true;
    }

    else { return false; }

}


while (true)
{
    discord.RunCallbacks();
    if  (CheckProcess())
    {
        UpdateActivity(discord, activityManager, activity);
        System.Threading.Thread.Sleep(20000);
    }
    else if (!CheckProcess())
    {
        ClearActivity(discord, activityManager);
        System.Threading.Thread.Sleep(5000);
    }

 
}

discord.Dispose();