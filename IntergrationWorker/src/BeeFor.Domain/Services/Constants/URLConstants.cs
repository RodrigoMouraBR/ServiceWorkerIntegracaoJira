using System;
using System.Collections.Generic;
using System.Linq;

namespace BeeFor.Domain.Services.Constants
{
    public static class URLConstants
    {
        #region EndPoints Jira

        public static string obterQuadroProjetosJira = "rest/agile/1.0/board";
        public static string obterProjetoQuadroTarefaJira = "rest/agile/1.0/board/{param}/epic/none/issue";
        public static string obterProjetosJira = "rest/api/3/project";
        public static string obterProjetosJiraPorId = "rest/api/3/project/{param}";
        public static string obterConfiguracaoQuadroJira = "rest/agile/1.0/board/{param}/configuration";
        public static string obterProjetoQuadroTarefaJira_Changelog = "rest/agile/1.0/board/{param}/epic/none/issue?&expand=changelog";
        

        #endregion

        #region Mensagens diversas
        public static string retornoErro = "OPS ! Não Foi Possivel obter o retorno fale com o administrador :( ";
        #endregion

        public static string metodoInt(string url, int vlr)
        {
            List<string> lista = new List<string>();
            lista.Add(url);
            var result = (from c in lista select c.Replace("{param}", Convert.ToString(vlr))).ToList();
            return result.First();
        }
    }
}
