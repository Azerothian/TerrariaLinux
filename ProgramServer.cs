namespace Terraria
{
    using System;
    using System.IO;
    using System.Diagnostics;

    internal class ProgramServer
    {
        private static Terraria.Main Game;

        private static void Main(string[] args)
        {
            try
            {

                Trace.Listeners.Clear();

                TextWriterTraceListener twtl = new TextWriterTraceListener(Path.Combine(Path.GetTempPath(), AppDomain.CurrentDomain.FriendlyName));
                twtl.Name = "TextLogger";
                twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

                ConsoleTraceListener ctl = new ConsoleTraceListener(false);
                ctl.TraceOutputOptions = TraceOptions.DateTime;

                Trace.Listeners.Add(twtl);
                Trace.Listeners.Add(ctl);
                Trace.AutoFlush = true;

                Trace.WriteLine("The first line to be in the logfile and on the console.");



                Game = new Terraria.Main();
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].ToLower() == "-config")
                    {
                        i++;
                        Game.LoadDedConfig(args[i]);
                    }
                    if (args[i].ToLower() == "-port")
                    {
                        i++;
                        try
                        {
                            Netplay.serverPort = Convert.ToInt32(args[i]);
                        }
                        catch
                        {
                        }
                    }
                    if ((args[i].ToLower() == "-players") || (args[i].ToLower() == "-maxplayers"))
                    {
                        i++;
                        try
                        {
                            int mPlayers = Convert.ToInt32(args[i]);
                            Game.SetNetPlayers(mPlayers);
                        }
                        catch
                        {
                        }
                    }
                    if ((args[i].ToLower() == "-pass") || (args[i].ToLower() == "-password"))
                    {
                        i++;
                        Netplay.password = args[i];
                    }
                    if (args[i].ToLower() == "-world")
                    {
                        i++;
                        Game.SetWorld(args[i]);
                    }
                    if (args[i].ToLower() == "-worldname")
                    {
                        i++;
                        Game.SetWorldName(args[i]);
                    }
                    if (args[i].ToLower() == "-motd")
                    {
                        i++;
                        Game.NewMOTD(args[i]);
                    }
                    if (args[i].ToLower() == "-banlist")
                    {
                        i++;
                        Netplay.banFile = args[i];
                    }
                    if (args[i].ToLower() == "-autoshutdown")
                    {
                        Game.autoShut();
                    }
                    if (args[i].ToLower() == "-autocreate")
                    {
                        i++;
                        string newOpt = args[i];
                        Game.autoCreate(newOpt);
                    }
                }
                Game.DedServ();
            }
            catch (Exception exception)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter("crashlog.txt", true))
                    {
                        writer.WriteLine(DateTime.Now);
                        writer.WriteLine(exception);
                        writer.WriteLine("");
                    }
                    Trace.WriteLine("Server crash: " + DateTime.Now);
                    Trace.WriteLine(exception);
                    Trace.WriteLine("");
                    Trace.WriteLine("Please send crashlog.txt to support@terraria.org");
                }
                catch
                {
                }
            }
        }
    }
}

