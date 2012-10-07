using System;
using Raven.Client;
using Raven.Client.Embedded;

namespace RepoT.RavenDb.MvcSample.Models
{
    public class DataDocumentStore
    {
        private static IDocumentStore _instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("IDocumentStore has not been initialized.");
                return _instance;
            }
        }

        public static IDocumentStore Initialize()
        {
            _instance = new EmbeddableDocumentStore
            {
                ConnectionStringName = "RavenDB"
                ,UseEmbeddedHttpServer = true
            };
            _instance.Conventions.IdentityPartsSeparator = "-";
            _instance.Initialize();
            return _instance;
        }
    }
}