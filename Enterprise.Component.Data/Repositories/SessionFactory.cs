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
        #region Private Static Fields
        ///// <summary>
        ///// The singleton instance of the database session factory.
        ///// </summary>
        //private static readonly DatabaseSessionFactory databaseSessionFactory = new DatabaseSessionFactory();
        #endregion

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

        internal SessionFactory(string configFileName)
        {
            if (sessionFactory == null)
            {
                try
                {
                    Configuration nhibernateConfig = new Configuration();
                    if (string.IsNullOrEmpty(configFileName))
                        nhibernateConfig.Configure();
                    else
                        nhibernateConfig.Configure(configFileName);

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
                try
                {
                    ISession result = session;
                    if (result != null && result.IsOpen)
                        return result;
                    return OpenSession();
                }
                catch
                { throw; }
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
