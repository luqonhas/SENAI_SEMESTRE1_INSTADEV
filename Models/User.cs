using System;
using System.Collections.Generic;
using System.IO;
using Project_Instadev.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Project_Instadev.Models
{
    public class User : InstaDevBase, IUser
    {
        [TempData]
        public string Mensagem { get; set; }
        public int IdUser { get; set; } // Id de usuário
        public string CompleteName { get; set; } // Nome completo do usuário
        public string Photo { get; set; } // Foto de perfil do usuário
        public DateTime DateOfBirth { get; set; } // Data de nascimento/aniversário do usuário
        public int Following { get; set; } // CORRIGIR: Following - int[] id || Seguindo
        public string Email { get; set; } // Email do usuário
        public string UserName { get; set; } // O @ do usuário
        public string Password { get; set; } // Senha da conta do usuário
        [TempData]
        public string Message { get; set; }
        private const string PATH = "Database/register.csv"; // criação da pasta Database que armazanerá os arquivos CSV

        Random idRandom = new Random(); // está instanciando a classe que está dentro do método de geração de IDs
        public User()
        { // método construtor para criar os arquivos CSV
            CreateFolderAndFile(PATH); // método puxado de dentro da classe "InstaDevBase"
        }

        public string PrepareLinesCSV(User prepareLines)
        { // aqui será preparado o modo como será armazenado automaticamente as linhas dentro do CSV
            return $"{prepareLines.IdUser};{prepareLines.Email};{prepareLines.CompleteName};{prepareLines.UserName};{prepareLines.Password};{prepareLines.Photo}";
        }

        public int IdGenerator()
        { // esse método gerará números aleatórios de ID de usuário
            return idRandom.Next(); // direcionará para dentro do método de Register
        }

        public void Create(User newUser)
        { // criará um novo usuário cadastrado no CSV (ou seja, uma nova linha no CSV)
            string[] dataToLines = { PrepareLinesCSV(newUser) }; // o dataToLines(string array) irá memorizar os dados que forem preenchidos no cadastro de acordo com o método PrepareLinesCSV(que vai preparar a forma das informações nas linhas do CSV)
            File.AppendAllLines(PATH, dataToLines); // o arquivo CSV(File) chama um método já presente da biblioteca do System.IO que tem a função de anexar as os dados preenchidos pelo usuário que estão memorizadas no dataToLines || dentro do método AppendAllLines tem o PATH, que é onde ele vai anexar esses novos dados de cadastro; e o dataToLines(dados para linhas), que são os dados memorizados que serão "lançados" para dentro do PATH
        }

        public List<User> ReadAllItems()
        { // aqui será lido/interpretado todos os itens das linhas do CSV (Email, Nome Completo, Username e Senha)
            List<User> users = new List<User>(); // atributo da lista que poderá ser chamado no contexto
            string[] infoData = File.ReadAllLines(PATH); // infoData(dados de informação) é onde será consultado pelo File e com o seu método ReadAllLines(que já é presente da biblioteca System.IO) para logo logo ser separado cada dado do CSV em seu respectivo "lugar"

            foreach (var item in infoData)
            { // o foreach ficará repetindo a função de "recolher" os dados que estão sendo consultados do infoData
                string[] data = item.Split(";"); // o "data" é onde será memorizado onde cada dado está (para conseguir chamar pelos números [0], [1], [2] etc) || o Split(";") está separando as informações, entendendo que a cada ";" é um novo tipo de dado

                User user = new User(); // aqui apenas foi a classe sendo instanciada para possibilitar puxar os atributos dela(Email, CompleteName, Username e Password) e poder memorizar onde está cada dado nos números([1], [2], [3] etc)

                user.IdUser = Int32.Parse(data[0]);
                user.Email = data[1]; // aqui está começando a ser memorizado no "user" a posição que está cada dado
                user.CompleteName = data[2];
                user.UserName = data[3];
                user.Password = data[4];
                user.Photo = data[5];

                if (data[5] == "")
                {
                    user.Photo = "default.png";
                }
                else
                {
                    user.Photo = data[5];
                }

                users.Add(user); // aqui estamos adicionando o que foi instanciado para dentro da lista principal deste contexto
            }

            return users; // aqui foi retornado a lista principal do contexto que contém todas as informações dos dados separadadas
        }

        public void Update(User userUpdate)
        {
            List<string> lines = ReadAllLinesCSV(PATH);
            lines.RemoveAll(x => x.Split(";")[0] == userUpdate.IdUser.ToString());
            lines.Add(PrepareLinesCSV(userUpdate));
            RewriteCSV(PATH, lines);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            RewriteCSV(PATH, linhas);   // Funcionalidade: Edit Perfil. Oq faz: Deleta a conta definitivamente do usuário
        }
        public User SearchUserWithId(int id)
        {
            User searchUser = new User();

            List<string> csv = searchUser.ReadAllLinesCSV("Database/register.csv");

            var searchedLine =
            csv.Find(
                x =>
                x.Split(";")[0] == id.ToString()
            );

            var userLine = searchedLine.Split(";");
            User searchedUser = new User();
            searchedUser.IdUser = int.Parse(userLine[0]);
            searchedUser.Email = userLine[1];
            searchedUser.CompleteName = userLine[2];
            searchedUser.UserName = userLine[3];
            searchedUser.Password = userLine[4];
            searchedUser.Photo = userLine[5];
            if (userLine[5] == "")
            {
                searchedUser.Photo = "default.png";
            }
            else
            {
                searchedUser.Photo = userLine[5];
            }
            return searchedUser;
        }

        public void DeleteUser(int id){
            List<string> lines = ReadAllLinesCSV(PATH);
            lines.RemoveAll(x => x.Split(";")[0] == id.ToString());

            RewriteCSV(PATH, lines);
        }
    }
}