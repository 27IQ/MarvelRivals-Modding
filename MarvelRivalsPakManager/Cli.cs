using System.Security.Cryptography.X509Certificates;

namespace MarvelRivalsPakManager
{
    public static class Cli
    {
        const string AesKey="0x0C263D8C22DCB085894899C3A3796383E9BF9DE0CBFB08C9BF2DEF2E84F29D74";
        
        public static void Main(string[] args){
            string command=args[0];
            string gameDirPath=args[1];

            switch (command)
            {
                case "generate":

                    Console.WriteLine("Creating mod directory");

                    Directory.CreateDirectory(@".\mods");
                    Directory.CreateDirectory(@".\paks");

                    File.WriteAllLines(
                        path: @".\install.bat",
                        contents: new List<string>()
                        {
                            ".\\MarvelRivalsPakManager.exe remove \""+gameDirPath+"\"",
                            ".\\MarvelRivalsPakManager.exe install \""+gameDirPath+"\"",
                            "pause"
                        }
                    );

                    File.WriteAllLines(
                        path: @".\remove.bat",
                        contents: new List<string>()
                        {
                            ".\\MarvelRivalsPakManager.exe remove \""+gameDirPath+"\"",
                            "pause"
                        }
                    );

                    GetPaks(gameDirPath);
                    break;

                case "install":
                    if(File.Exists(gameDirPath+@"\MarvelGame\Marvel\Content\Paks\pakchunkCharacter-Windows.pak"))
                        GetPaks(gameDirPath);

                    PakExtractor.ExtractPak(
                    pathToPaks: @".\paks",
                    AesKey: AesKey,
                    outDir: gameDirPath+@"\MarvelGame",
                    isMod: false
                    );

                    PakExtractor.ExtractPak(
                    pathToPaks: @".\mods",
                    AesKey: AesKey,
                    outDir: gameDirPath+@"\MarvelGame",
                    isMod: true
                    );
                    break;

                case "remove":
                    if(!Directory.Exists(gameDirPath+@"\MarvelGame\Marvel\Content\Marvel")||!File.Exists(@".\paks\pakchunkCharacter-Windows.pak"))
                        return;

                    Console.WriteLine("removing mods from game");

                    UndoGetPaks(gameDirPath);  

                    DirectoryInfo dirInfo=new(gameDirPath+@"\MarvelGame\Marvel\Content\Marvel");
                    IEnumerable<DirectoryInfo> directories=dirInfo.EnumerateDirectories();

                    foreach (var directory in directories)
                    {

                        if(directory.Name.Equals("NonAssetsToCopy"))
                            continue;

                        Console.WriteLine("deleting directory: "+directory.Name);

                        directory.Delete(true);
                    }                     
                    break;

                default:
                    return;
            }
        }

        private static void GetPaks(string pathToGameDir){
            File.Move(pathToGameDir+@"\MarvelGame\Marvel\Content\Paks\pakchunkCharacter-Windows.pak",@".\paks\pakchunkCharacter-Windows.pak",true);
        }

        private static void UndoGetPaks(string pathToGameDir){
            File.Move(@".\paks\pakchunkCharacter-Windows.pak",pathToGameDir+@"\MarvelGame\Marvel\Content\Paks\pakchunkCharacter-Windows.pak",true);
        }
    }
}