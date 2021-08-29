using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoStarter.Migrations
{
    public partial class PopulaDb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
                        
            mb.Sql("Insert into Starters(NomeStarter, Linguagem) Values('Maria','Java')");
            mb.Sql("Insert into Starters(NomeStarter, Linguagem) Values('Joana','CSHARP')");
            mb.Sql("Insert into Starters(NomeStarter, Linguagem) Values('Lucia','CSHARP')");
            
            mb.Sql("Insert into Avaliacoes(Projeto,Performance,Comportamento,StarterId) " +
            "Values('MVC', 9.50 ,'Bom',(Select StarterId from Starters where NomeStarter ='Maria'))");

            mb.Sql("Insert into Avaliacoes(Projeto,Performance,Comportamento,StarterId) " +
            "Values('API', 10 ,'Bom',(Select StarterId from Starters where NomeStarter ='Maria'))");

            mb.Sql("Insert into Avaliacoes(Projeto,Performance,Comportamento,StarterId) " +
            "Values('MVC', 7.50 ,'Regular',(Select StarterId from Starters where NomeStarter ='Joana'))");

            mb.Sql("Insert into Avaliacoes(Projeto,Performance,Comportamento,StarterId) " +
            "Values('API', 9.50 ,'Regular',(Select StarterId from Starters where NomeStarter ='Joana'))");

            mb.Sql("Insert into Avaliacoes(Projeto,Performance,Comportamento,StarterId) " +
            "Values('API', 7.50 ,'Excelente',(Select StarterId from Starters where NomeStarter ='Lucia'))");

            mb.Sql("Insert into Avaliacoes(Projeto,Performance,Comportamento,StarterId) " +
            "Values('MVC', 8.50 ,'Ótimo',(Select StarterId from Starters where NomeStarter ='Lucia'))");

          

        }
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Starters");
            mb.Sql("Delete from Avaliacoes");
            mb.Sql("Delete from Usuarios");

        }       
    }
}
