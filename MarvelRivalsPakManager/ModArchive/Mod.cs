using System.Security.Cryptography;
using Blake3;

namespace MarvelRivalsPakManager.ModArchive
{
    class Mod(int? id, string name, string pathToSource, string? pathToArchive, DateTime addedDate, ModType modType)
    {
        public int? Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string PathToSource { get; set; } = pathToSource;
        public string? PathToArchive { get; set; } = pathToArchive;
        public bool IsCached { get; private set; } = false;
        public string? Hash { get; private set; } = ComputeHash(pathToSource, modType);
        public DateTime AddedDate { get; set; } = addedDate;
        public ModType ModType { get; set; } = modType;

        public void UpdateHash(){
            Hash=ComputeHash(PathToSource,ModType);
        }
        private static string? ComputeHash(string path, ModType modType){
            if(modType==ModType.PAK)
            {
                using FileStream stream = File.OpenRead(path);
                using var sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(stream);   
                return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
            }
            else if(modType==ModType.PAK)
            {
                return null;
            }

            return null;
        }
    }
    enum ModType
    {
        PAK,FOLDER
    }
}