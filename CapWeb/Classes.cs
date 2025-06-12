using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CapWeb
{

    

    public class Pessoas
    {
        
        private string nome;
        private string telefone;

        public string Nome
        {
            get {  return nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode ser vazio.");
                nome = value.ToUpper(); 
            }
        }

        public string Telefone
        {
            get { return telefone; }
            set
            {
                if (value.Length < 8)
                    throw new ArgumentException("Telefone inválido.");
                telefone = value;
            }
        }
    }


    public class Endereco
    {
       
        private string logradouro;
        private string bairro;
        private string cidade;
        private string uf;
        private string cep;
        private int numero;
        private string nome_condominio;

        public string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }
        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        public string UF
        {
            get { return uf; }
            set { uf = value; }
        }

        public string CEP
        {
            get { return cep; }
            set { cep = value; }
        }
        public string Nome_Condominio
        {
            get { return nome_condominio; }
            set { nome_condominio = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
    }

    public class Imobiliaria
    {

        private string nome_imobiliaria;
        private string nome_responsavel;
        private string telefone_imobiliaria;
        private string valor_cobrado;

        public string Nome_Imobiliaria
        {
            get { return nome_imobiliaria; }
            set {  nome_imobiliaria = value;}
        }

        public string Nome_Responsavel
        {
            get { return nome_responsavel; }
            set { nome_responsavel = value; }
        }

        public string Valor_cobrado
        {
            get { return valor_cobrado; }
            set {  valor_cobrado = value; }
        }

        public string Telefone_Imobiliaria
        {
            get { return  telefone_imobiliaria;}
            set { telefone_imobiliaria = value; }
        }
    }


    public class Imovel
    {
       
        private string descricao;
        private string tipo_de_imovel;
        private string pretensao;
        private string comissao;
        private string complemento;
        private string util;
        private string construida;
        private string total;
        private string valor;
        private string iptu;
        private string valor_condominio;

        public string Descricao
        {
            get { return  descricao; }
            set { descricao = value; }
        }
        public string Tipo_de_imovel
        {
            get { return tipo_de_imovel;}
            set { tipo_de_imovel = value; }
        }
        public string Pretensao
        {
            get { return pretensao; }
            set { pretensao = value; }
        }
        public string Comissao
        {
            get { return comissao; }
            set { comissao = value; }
        }

        public string Complemento
        {
            get{ return complemento; }
            set { complemento = value; }
        }
        public string Valor
        {
            get { return valor;}
            set { valor = value; }
        }
        public string IPTU
        {
            get { return iptu;}
            set { iptu = value; }
        }

        public string Valor_Condominio
        {
            get { return valor_condominio; }
            set { valor_condominio = value; }
        }

        public string Util
        {
            get { return util; }
            set { util = value; }
        }
        public string Construida
        {
            get { return construida; }
            set { construida = value; }
        }

        public string Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
