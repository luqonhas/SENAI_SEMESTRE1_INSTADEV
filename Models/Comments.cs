using System;
using System.Collections.Generic;
using System.IO;
using Project_Instadev.Interfaces;

namespace Project_Instadev.Models
{
    public class Comments : InstaDevBase, IComments
    {
        public int IdComment { get; set; } // Id do comentário
        public string Message { get; set; } // Id da mensagem do comentário
        public int IdUser { get; set; } // CORRIGIR: IdUser - int id - FK || Id do usuário do comentário
        public int IdPublication { get; set; } // CORRIGIR: IdPublication - int id - FK || Id da publicação do comentário
        private const string PATH = "Database/comments.csv";

        public Comments()
        {
            CreateFolderAndFile(PATH);
        }
        public string PrepareLinesCSV(Comments prepareLines)
        { // aqui será preparado o modo como será armazenado automaticamente as linhas dentro do CSV
            return $"{prepareLines.IdComment};{prepareLines.IdUser};{prepareLines.IdPublication};{prepareLines.Message}";
        }
        public void IdGenerator(Comments id)
        { // esse método gerará números aleatórios de ID
            var list = ReadAllItens();

            if (list.Count > 0)
            { // se existir mais que zero, vai gerar um número aleatório que será o id
                Random randomNumber = new Random();
                int idRandom = randomNumber.Next(); // o idRandom é só pra guardar o número aleatório

                id.IdComment = idRandom; // o número que tava no idRandom foi passado para o IdComment
            }
            else
            { // se não existir mais que zero, o valor do id padrão será zero
                id.IdComment = 0; // 
            }
            string[] idItem = { PrepareLinesCSV(id) }; // o array "idItem" guardará a preparação das linhas do CSV junto com o id(que teve o idRandom guardado)
            File.AppendAllLines(PATH, idItem); // o AppendAllLines irá acrescentar a informação do id no CSV em seu devido lugar([0], [1], [2] etc)

        }
        public void Create(Comments newComment)
        {
            IdGenerator(newComment); // o IdGenerator fará o mesmo que fez com o id, só que com o newComment
        }

        public List<Comments> ReadAllItens()
        { // aqui será lido/interpretado todos os itens das linhas do CSV (IdComment, IdUser, IdPublication e Message)
            List<Comments> comments = new List<Comments>(); // atributo da lista que poderá ser chamado no contexto
            string[] infoData = File.ReadAllLines(PATH); // infoData(dados de informação) é onde será consultado pelo File e com o seu método ReadAllLines(que já é presente da biblioteca System.IO) para logo logo ser separado cada dado do CSV em seu respectivo "lugar"

            foreach (var item in infoData)
            { // o foreach ficará repetindo a função de "recolher" os dados que estão sendo consultados do infoData
                string[] data = item.Split(";"); // o "data" é onde será memorizado onde cada dado está (para conseguir chamar pelos números [0], [1], [2] etc) || o Split(";") está separando as informações, entendendo que a cada ";" é um novo tipo de dado

                Comments comment = new Comments(); // aqui apenas foi a classe sendo instanciada para possibilitar puxar os atributos dela(IdComment, IdUser, IdPublication e Message) e poder memorizar onde está cada dado nos números([1], [2], [3] etc)

                comment.IdComment = Int32.Parse(data[0]); // aqui está começando a ser memorizado no "comment" a posição que está cada dado
                comment.IdUser = Int32.Parse(data[1]);
                comment.IdPublication = Int32.Parse(data[2]);
                comment.Message = data[3];

                comments.Add(comment); // aqui estamos adicionando o que foi instanciado para dentro da lista principal deste contexto
            }
            return comments; // aqui foi retornado a lista principal do contexto que contém todas as informações dos dados separadadas
        }

        public void Delete(int id)
        {
            List<string> lines = ReadAllLinesCSV(PATH); // todas as informações lidas pelo ReadAllLinesCSV serão guardadas na Lista "lines"
            lines.RemoveAll(item => item.Split(";")[0] == id.ToString()); // as informações da Lista lines serão separadas de acordo com o id em sua localização específica dentro do CSV([0])
            RewriteCSV(PATH, lines); // assim que encontrado, aquela linha que foi apaga será recriada com o ReWriteItemsCSV que pulará para a próxima linha
        }

        public void Update() { } // SEM UTILIDADE NO MOMENTO
    }
}