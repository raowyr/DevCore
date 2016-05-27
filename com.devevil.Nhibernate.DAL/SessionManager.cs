using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Devevil.Blog.Nhibernate.DAL
{
    /// <summary>
    /// A static (singleton) class to manage NHibernate.
    /// Manage NHibernate sessions using <see cref="Session"/>
    /// </summary>
    public sealed class SessionManager
    {
        private const string SESSIONKEY = "NHIBERNATE.SESSION";
        [ThreadStatic]
        private static ISession _Session; //this session is not used in web
        private Configuration _configuration;
        private ISessionFactory _sessionFactory;

        #region Constructor

        #region Singleton
        public static SessionManager Instance
        {
            get
            {
                //#if !INSTANCE
                return Singleton.Instance;
                //#else
                //                return new SessionManager();
                //#endif
            }
        }

        //#if !INSTANCE
        private class Singleton
        {
            static Singleton() { }
            internal static readonly SessionManager Instance = new SessionManager();
        }
        //#endif
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        private SessionManager()
        {
            //_configuration = RestoreConfiguration();
            //if (_configuration == null)
            //{
            //    _configuration = new Configuration();
            //    BuildConfiguration();
            //}
            ////get the session factory
            ////_sessionFactory = Configuration.BuildSessionFactory();
            //_sessionFactory = Fluently.Configure(Configuration)
            //    .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            //    .BuildSessionFactory();
        }

        private void BuildConfiguration()
        {
            Configuration.Configure(); //configure from the app.config

            Configuration.AddAssembly(GetType().Assembly);//default- mapping is in this assembly
            //other examples:
            //Configuration.AddFile("file.hbm.xml"); //add files
            //Configuration.AddAssembly(assembly); //add assembly
            //Configuration.AddAssembly(assemblyName); //add assembly by name
            //foreach (string assemblyName in assemblyNames) //add enumerable of assemblies
            //    Configuration.AddAssembly(assemblyName);
#if !DEBUG
            SaveConfiguration(Configuration);
#endif
        }
        #endregion

        #region Configuration Serialization
        //specify a full path if required
        private const string _configurationFilePath = "Configuration.save";
        private static void SaveConfiguration(Configuration configuration)
        {
            IFormatter serializer = new BinaryFormatter();

            using (Stream stream = File.OpenWrite(_configurationFilePath))
            {
                try
                {
                    serializer.Serialize(stream, configuration);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("No write access to " + _configurationFilePath);
                }
            }
        }

        /// <summary>
        /// Optimization- you could deploy the serialization file.
        /// </summary>
        private static Configuration RestoreConfiguration()
        {
#if DEBUG
            return null;
#endif
            if (!File.Exists(_configurationFilePath)) return null;
            IFormatter serializer = new BinaryFormatter();

            using (Stream stream = File.OpenRead(_configurationFilePath))
            {
                return serializer.Deserialize(stream) as Configuration;
            }
        }
        #endregion

        #region NHibernate Setup
        /// <summary>
        /// Gets the <see cref="NHibernate.Cfg.Configuration"/>.
        /// </summary>
        internal Configuration Configuration { get { return _configuration; } }

        /// <summary>
        /// Gets the <see cref="NHibernate.ISessionFactory"/>
        /// </summary>
        internal ISessionFactory SessionFactory { get { return _sessionFactory; } }

        /// <summary>
        /// Closes the session factory.
        /// </summary>
        public void Close()
        {
            SessionFactory.Close();
        }

        public void Configure()
        {
            _configuration = RestoreConfiguration();
            if (_configuration == null)
            {
                _configuration = new Configuration();
                BuildConfiguration();
            }
            //get the session factory
            //_sessionFactory = Configuration.BuildSessionFactory();
            _sessionFactory = Fluently.Configure(Configuration)
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }

        public void BuildSchema()
        {
            SchemaExport se = new SchemaExport(Configuration);
            se.Drop(true, true);
            se.Execute(true, true,false);
        }

        internal static bool IsWeb { get { return (HttpContext.Current != null); } }
        #endregion

        #region NHibernate SessionContext (1.2+)

        /// <summary>
        /// Opens the conversation (if already existing, reuses it). Call this from Application_BeginPreRequestHandlerExecute
        /// </summary>
        /// <returns>A <see cref="NHibernate.ISession"/></returns>
        public ISession OpenConversation()
        {
            //you must set <property name="current_session_context_class">web</property> (or thread_static etc)
            ISession session = Session; //get the current session (or open one). We do this manually, not using SessionFactory.GetCurrentSession()
            //for session per conversation (otherwise remove)
            session.FlushMode = FlushMode.Never; //Only save on session.Flush() - because we need to commit on unbind in PauseConversation
            session.BeginTransaction(); //start a transaction
            CurrentSessionContext.Bind(session); //bind it
            return session;
        }

        /// <summary>
        /// Ends the conversation. If an exception occurs, rethrows it ensuring session is closed. Call this (or <see cref="PauseConversation"/> if session per conversation) from Application_PostRequestHandlerExecute
        /// </summary>
        public void EndConversation()
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            if (session == null) return;
            try
            {
                session.Flush();
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        /// <summary>
        /// Pauses the conversation. Call this (or <see cref="EndConversation"/>) from Application_EndRequest
        /// </summary>
        public void PauseConversation()
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            if (session == null) return;
            try
            {
                session.Transaction.Commit(); //with flushMode=Never, this closes connections but doesn't flush
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            //we don't close the session, and it's still in Asp SessionState
        }
        #endregion

        #region NHibernate Sessions

        /// <summary>
        /// Explicitly open a session. If you have an open session, close it first.
        /// </summary>
        /// <returns>The <see cref="NHibernate.ISession"/></returns>
        public ISession OpenSession()
        {
            ISession session = SessionFactory.OpenSession();
            if (IsWeb)
            {
                if (!HttpContext.Current.Items.Contains(SESSIONKEY))
                    HttpContext.Current.Items.Add(SESSIONKEY, session);
                else
                    HttpContext.Current.Items[SESSIONKEY] = session;
            }
            else
                _Session = session;
            return session;
        }

        /// <summary>
        /// Gets the current <see cref="NHibernate.ISession"/>. Although this is a singleton, this is specific to the thread/ asp session. If you want to handle multiple sessions, use <see cref="OpenSession"/> directly. If a session it not open, a new open session is created and returned.
        /// </summary>
        /// <value>The <see cref="NHibernate.ISession"/></value>
        public ISession Session
        {
            get
            {
                //use threadStatic or asp session.
                ISession session = IsWeb ? HttpContext.Current.Items[SESSIONKEY] as ISession : _Session;
                //if using CurrentSessionContext, SessionFactory.GetCurrentSession() can be used

                //if it's an open session, that's all
                if (session != null && session.IsOpen)
                    return session;

                //if not open, open a new session
                return OpenSession();
            }
        }
        #endregion

    }
}
