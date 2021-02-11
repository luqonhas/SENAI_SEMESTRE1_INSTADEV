using System.Collections.Generic;
using System.IO;

namespace Project_Instadev.Models
{
    public class InstaDevBase
    {
        public void CreateFolderAndFile(string path)
        { // método geral que criará os arquivos CSV || o "path" será tipo um molde para conseguirmos chamar o PATH nas outras classes
            string folder = path.Split("/")[0]; // o folder está representando a nossa pasta Database

            // conferindo se existe alguma pasta(folder) chamada "Database" (se não existir, será criada):
            if (!Directory.Exists(folder))
            { // esta exclamação(!) corresponde um sinal de negação, que não existe (ou seja, se não existe um diretório tal, será criado)
                Directory.CreateDirectory(folder);
            }

            // conferindo se existe algum arquivo CSV (se não existir, será criada):
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        { // método em lista para fazer o código ler as linhas do CSV || o nome "ReadAllLinesCSV" é o nome do método, é o que vai ser puxado
            List<string> linesRead = new List<string>(); // o linesRead será o atributo que poderá ser puxado dentro deste contexto

            // o StreamReader vai ler (acho que posso dizer que ele vai conseguir "interpretar") as informações do CSV
            using (StreamReader file = new StreamReader(path))
            { // o "using" vai abrir e fechar determinado tipo de arquivo e conexão ||o StreamReader é uma classe presente na biblioteca System.IO
                string line; // esse será o atributo que representará cada linha do CSV

                while ((line = file.ReadLine()) != null)
                { // se enquanto a linha(line) é lido e memorizado pelo file(StreamReader) não for nulo (ou seja, se o CSV for nulo, não acontecerá nada || porém, se existir linha, ou seja, ser diferente de nulo, a linha será memorizada pelo StreamReader)
                    linesRead.Add(line); // a cada linha que for lida e memorizada pelo StreamReader, essa linha memorizada será ARMAZENADA na lista de linhas lidas(linesRead)
                }

                return linesRead; // esse atributo linesRead será retornado toda vez que o método ReadAllLinesCSV for chamado
            }
        }
        public void RewriteCSV(string path, List<string> line)
        {
            using (StreamWriter ouput = new StreamWriter(path))
            {
                foreach (var item in line)
                {
                    ouput.Write(item + '\n');
                }
            }
        }
    }
}