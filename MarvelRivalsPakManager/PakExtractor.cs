using CUE4Parse.FileProvider;
using CUE4Parse.GameTypes.NetEase.MAR.Encryption.Aes;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;
using CUE4Parse.Utils;

namespace MarvelRivalsPakManager
{
    public static class PakExtractor
    {
        public static void ExtractPak(string pathToPaks, string AesKey, string outDir, bool isMod)
        {
            Console.WriteLine("Writing Files to: "+outDir);

            if(!Path.Exists(pathToPaks))
                throw new FileNotFoundException();

            Console.WriteLine("Looking for Files at: "+ pathToPaks);

            DefaultFileProvider provider = new(
                directory: pathToPaks, 
                searchOption: SearchOption.AllDirectories, 
                isCaseInsensitive: false,
                versions: new VersionContainer(EGame.GAME_MarvelRivals));

            provider.CustomEncryption=isMod?null:MarvelAes.MarvelDecrypt;

            provider.Initialize();
            provider.SubmitKey(guid: new FGuid(), key: new(AesKey));
            Console.WriteLine("Decrypting using AesKey: "+AesKey);
            
            Console.WriteLine(provider.Files.Count+" Files were found");

            int allFiles=provider.Files.Count;
            
            foreach (var entry in provider.Files)
            {
                string  localOutputPath = Path.DirectorySeparatorChar+entry.Key.Replace('/',Path.DirectorySeparatorChar).SubstringBeforeLast(delimiter: Path.DirectorySeparatorChar);
                string fileName=entry.Key.SubstringAfterLast(delimiter: '/');

                byte[] currentFile;
                try
                {
                    currentFile=provider.SaveAsset(path: localOutputPath+Path.DirectorySeparatorChar+fileName );
                }
                catch (Exception)
                {
                    allFiles--;
                    //Console.WriteLine("skipped file at: "+localOutputPath+Path.DirectorySeparatorChar+fileName+"\n"+e.ToString());
                    continue;
                }

                Directory.CreateDirectory(outDir+localOutputPath);

                File.WriteAllBytes(
                    path: outDir+localOutputPath+Path.DirectorySeparatorChar+fileName,
                    bytes: currentFile
                );

                //Console.WriteLine("Writing file to: "+outDir+localOutputPath+Path.DirectorySeparatorChar+fileName);
            }

            Console.WriteLine(allFiles+"/"+provider.Files.Count+"wurden hinzugefügt");
        }
    }
}
