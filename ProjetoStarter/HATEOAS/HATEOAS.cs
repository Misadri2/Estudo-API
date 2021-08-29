using System;
using System.Collections.Generic;

namespace ProjetoStarter.HATEOAS
{
    public class HATEOAS
    {
        private string url;
        private string protocol = "https://";
        public List<Link> actions = new List<Link>();

        public HATEOAS(string url)
        {
            this.url = url;
        }

        public HATEOAS(string url, string protocol)
        {
            this.url = url;
            this.protocol = protocol;
        }

        public void AddAction(string rel, string method)
        {
            //https:// localhost:5001/api/v1/[controller]
            actions.Add(new Link(this.protocol + this.url, rel, method));
        }

        public Link[] GetActions(string sufix)
        {
            Link[] tempLinks = new Link[actions.Count];

            for (int i = 0; i < tempLinks.Length; i++)
            {     //Multiplas Entidades com HATEOAS - criando novos objetos dentro do array para listar
                tempLinks[i] = new Link(actions[i].Href, actions[i].Rel, actions[i].Method);
            }
            foreach (var link in tempLinks)
            {
                link.Href = link.Href + "/" + sufix;
            }
            return tempLinks;
        }

        internal Link[] GetActions(object p)
        {
            throw new NotImplementedException();
        }
    }
}