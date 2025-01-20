
using System.Data.SQLite;

namespace MarvelRivalsPakManager.ModArchive
{
    class ModRepository()
    {
        private const string inMemoryConnection="Data Source=mods.db;Version=3;";
        private readonly SQLiteConnection Connection=new(inMemoryConnection);

        public void OpenConnection(){
            Connection.Open();
        }

        public void Save(Mod mod){
            if(mod.Id==null)
            {
                Insert(mod);
            }
            else
            {
                Update(mod);
            }
        }

        private void Insert(Mod mod){
            /*const string insertQuery = "INSERT INTO Mods (Name, Version, FilePath, DateAdded) VALUES (@name, @version, @filePath, @dateAdded)";
            using var command= new SQLiteCommand(insertQuery,Connection);
            command.Parameters.AddWithValue("@name",mod.Name);
            command.ExecuteNonQuery();*/
        }

        private void Update(Mod mod){

        }

        public void Delete(Mod mod){

        }

        public void Delete(int id){

        }

        public void GetModById(int Id){

        }

        public void CreateModTable(){
            
        }
    }
}