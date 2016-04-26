using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Enterprise.Component.Nhiberate
{
    internal sealed class SessionFactory
    {

        #region Private Fields
        /// <summary>
        /// The session factory instance.
        /// </summary>
        private static ISessionFactory sessionFactory = null;
        /// <summary>
        /// The session instance.
        /// </summary>
        private ISession session = null;
        #endregion

        #region Constructors

        internal SessionFactory(string ConnectionString)
        {
            if (sessionFactory == null)
            {
                try
                {
                    var nhibernateConfig = new Configuration();
                    //Load from a specified section if provided
                    if (!string.IsNullOrEmpty(ConnectionString))
                        nhibernateConfig.Properties["connection.connection_string"] = ConnectionString;
                    nhibernateConfig.Configure();
                    sessionFactory = nhibernateConfig.BuildSessionFactory();
                }
                catch (Exception ex)
                {
                    throw new Exception("SessionFactory Error:" + ex.Message );
                }
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the singleton instance of the session. If the session has not been
        /// initialized or opened, it will return a newly opened session from the session factory.
        /// </summary>
        public ISession Session
        {
            get
            {
                if (session != null && session.IsOpen)
                    return session;
                return OpenSession();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Always opens a new session from the session factory.
        /// </summary>
        /// <returns>The newly opened session.</returns>
        /// <exception cref="Apworks.Repositories.RepositoryException">Occurs when failed to open the NHibernate session.</exception>
        public ISession OpenSession()
        {
            this.session = sessionFactory.OpenSession();
            return this.session;
        }
        #endregion

    }
}
