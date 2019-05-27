using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace EDOSwit.AbstraccionSP
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class, new()
    {
        #region Properties

        protected SqlDatabase Database
        {
            get { return _db; }
        }

        #endregion Properties

        #region Atributos

        private const int CommandTimeout = 0;
        private readonly SqlDatabase _db;
        private readonly DatabaseProviderFactory _dbFactory = new DatabaseProviderFactory();
        private readonly string _countStoredProcedure = string.Format("usp_Count_{0}", typeof (T).Name);
        private readonly string _deleteStoredProcedure = string.Format("usp_Delete_{0}", typeof (T).Name);
        private readonly string _insertStoredProcedure = string.Format("usp_Insert_{0}", typeof (T).Name);
        private readonly string _selectPagedStoredProcedure = string.Format("usp_SelectPaged_{0}", typeof (T).Name);
        private readonly string _selectStoredProcedure = string.Format("usp_Select_{0}", typeof (T).Name);
        private readonly string _singleStoredProcedure = string.Format("usp_Single_{0}", typeof (T).Name);
        private readonly string _updateStoredProcedure = string.Format("usp_Update_{0}", typeof (T).Name);

        #endregion Atributos

        #region Constructor

        protected BaseRepository()
        {
            _db = _dbFactory.CreateDefault() as SqlDatabase;
        }

        protected BaseRepository(string nameConnection)
        {
            _db = _dbFactory.Create(nameConnection) as SqlDatabase;
        }

        #endregion Constructor

        #region Miembros de IRepository<T>

        public IList<T> Get(params object[] parameters)
        {
            return Get(_selectStoredProcedure, null, parameters);
        }

        protected virtual IList<T> Get(string storedProcedure, params object[] parameters)
        {
            return GetGeneric<T>(storedProcedure, null, parameters);
        }

        protected virtual IList<T> Get(string storedProcedure, DbTransaction transaction, params object[] parameters)
        {
            return GetGeneric<T>(storedProcedure, transaction, parameters);
        }

        protected virtual IList<TG> GetGeneric<TG>(string storedProcedure, params object[] parameters)
            where TG : class, new()
        {
            return GetGeneric<TG>(storedProcedure, null, parameters);
        }

        protected virtual IList<TG> GetGeneric<TG>(string storedProcedure, DbTransaction transaction,
            params object[] parameters) where TG : class, new()
        {
            var entities = new List<TG>();

            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                LoadParameter(command, parameters);

                using (
                    var dataReader = transaction == null
                        ? _db.ExecuteReader(command)
                        : _db.ExecuteReader(command, transaction))
                {
                    while (dataReader.Read())
                    {
                        var entity = GetEntity<TG>(dataReader);
                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }

        public IList<T> GetPaged(params object[] parameters)
        {
            return Get(_selectPagedStoredProcedure, parameters);
        }

        public T Single(params object[] parameters)
        {
            return Single(_singleStoredProcedure, parameters);
        }

        public T Single(DbTransaction transaction, params object[] parameters)
        {
            return Single(_singleStoredProcedure, transaction, parameters);
        }

        protected virtual T Single(string storedProcedure, params object[] parameters)
        {
            return SingleGeneric<T>(storedProcedure, null, parameters);
        }

        protected virtual T Single(string storedProcedure, DbTransaction transaction, params object[] parameters)
        {
            return SingleGeneric<T>(storedProcedure, transaction, parameters);
        }

        protected virtual TG SingleGeneric<TG>(string storedProcedure, params object[] parameters)
            where TG : class, new()
        {
            return SingleGeneric<TG>(storedProcedure, null, parameters);
        }

        protected virtual TG SingleGeneric<TG>(string storedProcedure, DbTransaction transaction = null,
            params object[] parameters) where TG : class, new()
        {
            var entity = default(TG);

            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                LoadParameter(command, parameters);

                using (
                    var dataReader = transaction == null
                        ? _db.ExecuteReader(command)
                        : _db.ExecuteReader(command, transaction))
                {
                    if (dataReader.Read())
                    {
                        entity = GetEntity<TG>(dataReader);
                    }
                }
            }

            return entity;
        }

        public int Count(params object[] parameters)
        {
            return Count(_countStoredProcedure, parameters);
        }

        protected virtual int Count(string storedProcedure, params object[] parameters)
        {
            var count = 0;
            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                LoadParameter(command, parameters);

                using (var dr = _db.ExecuteReader(command))
                {
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }

            return count;
        }

        public T Add(T entity)
        {
            return Add(_insertStoredProcedure, entity);
        }

        protected virtual T Add(string storedProcedure, T entity, DbTransaction transaction = null)
        {
            return AddGeneric(storedProcedure, entity, transaction);
        }

        protected TG AddGeneric<TG>(string storedProcedure, TG entity, DbTransaction transaction = null)
            where TG : class, new()
        {
            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                _db.DiscoverParameters(command);

                var returnParameters = (from DbParameter parameter in command.Parameters where parameter.Direction == ParameterDirection.InputOutput select parameter.ParameterName.Replace("@", string.Empty)).ToList();

                var properties = entity.GetType().GetProperties();
                foreach (DbParameter parameterSql in command.Parameters)
                {
                    parameterSql.Value = DBNull.Value;
                    if (parameterSql.Direction == ParameterDirection.Output ||
                        parameterSql.Direction == ParameterDirection.InputOutput ||
                        parameterSql.Direction == ParameterDirection.ReturnValue)
                    {
                        continue;
                    }

                    var parameterName = parameterSql.ParameterName.Replace("@", string.Empty).ToUpper();
                    var propiedad = properties.FirstOrDefault(p => p.Name.ToUpper() == parameterName);
                    if (propiedad == null)
                    {
                        continue;
                    }

                    parameterSql.Value = propiedad.GetValue(entity, null);
                }

                if (transaction == null)
                {
                    _db.ExecuteNonQuery(command);
                }
                else
                {
                    _db.ExecuteNonQuery(command, transaction);
                }

                foreach (var parameter in returnParameters)
                {
                    var property =
                        entity.GetType().GetProperties().FirstOrDefault(p => p.Name.ToUpper() == parameter.ToUpper());
                    if (property == null)
                    {
                        continue;
                    }

                    property.SetValue(entity, _db.GetParameterValue(command, parameter), null);
                }
            }

            return entity;
        }

        public T Update(T entity)
        {
            return Update(_updateStoredProcedure, entity);
        }

        protected virtual T Update(string storedProcedure, T entity, DbTransaction transaction = null)
        {
            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                _db.DiscoverParameters(command);

                var returnParameters = (from DbParameter parameter in command.Parameters where parameter.Direction == ParameterDirection.InputOutput select parameter.ParameterName.Replace("@", string.Empty)).ToList();

                var properties = entity.GetType().GetProperties();
                foreach (DbParameter parameterSql in command.Parameters)
                {
                    parameterSql.Value = DBNull.Value;

                    if (parameterSql.Direction == ParameterDirection.Output ||
                        parameterSql.Direction == ParameterDirection.InputOutput ||
                        parameterSql.Direction == ParameterDirection.ReturnValue)
                    {
                        continue;
                    }

                    var parameterName = parameterSql.ParameterName.Replace("@", string.Empty).ToUpper();
                    var propiedad = properties.FirstOrDefault(p => p.Name.ToUpper() == parameterName.ToUpper());
                    if (propiedad == null)
                    {
                        continue;
                    }

                    parameterSql.Value = propiedad.GetValue(entity, null);
                }

                if (transaction == null)
                {
                    _db.ExecuteNonQuery(command);
                }
                else
                {
                    _db.ExecuteNonQuery(command, transaction);
                }

                foreach (var parameter in returnParameters)
                {
                    var property =
                        entity.GetType().GetProperties().FirstOrDefault(p => p.Name.ToUpper() == parameter.ToUpper());
                    if (property == null)
                    {
                        continue;
                    }

                    property.SetValue(entity, _db.GetParameterValue(command, parameter), null);
                }
            }

            return entity;
        }

        public void Delete(params object[] parameters)
        {
            Delete(_deleteStoredProcedure, parameters);
        }

        protected virtual void Delete(string storedProcedure, params object[] parameters)
        {
            Delete(storedProcedure, null, parameters);
        }

        protected virtual void Delete(string storedProcedure, DbTransaction transaction = null,
            params object[] parameters)
        {
            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                LoadParameter(command, parameters);

                if (transaction == null)
                {
                    _db.ExecuteNonQuery(command);
                }
                else
                {
                    _db.ExecuteNonQuery(command, transaction);
                }
            }
        }

        protected virtual object GetScalar(string storedProcedure, params object[] parameters)
        {
            object scalar = null;

            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                LoadParameter(command, parameters);

                using (var dr = _db.ExecuteReader(command))
                {
                    if (dr.Read())
                    {
                        scalar = dr.GetValue(0);
                    }
                }
            }

            return scalar;
        }

        protected virtual void ExecuteQuery(string storedProcedure, params object[] parameters)
        {
            using (var command = _db.GetStoredProcCommand(storedProcedure))
            {
                command.CommandTimeout = CommandTimeout;
                LoadParameter(command, parameters);

                _db.ExecuteNonQuery(command);
            }
        }

        #endregion Miembros de IRepository<T>

        #region Metodos

        private static DbType GetDbType(Type type)
        {
            var name = type.Name;
            var value = (DbType) Enum.Parse(typeof (DbType), name, true);
            return value;
        }

        private void LoadParameter(DbCommand command, params object[] parameters)
        {
            _db.DiscoverParameters(command);

            if (!parameters.Any())
            {
                return;
            }

            var index = 0;

            foreach (DbParameter parameterSql in command.Parameters)
            {
                parameterSql.Value = DBNull.Value;

                if (parameterSql.Direction == ParameterDirection.Output ||
                    parameterSql.Direction == ParameterDirection.ReturnValue)
                {
                    continue;
                }

                if (command.Parameters.Count == index)
                {
                    break;
                }

                var parameter = parameters[index++];
                if (parameter == null)
                {
                    continue;
                }

                parameterSql.DbType = GetDbType(parameter.GetType());
                parameterSql.Value = parameter;
            }
        }

        protected TQ GetEntity<TQ>(IDataReader dataReader) where TQ : class, new()
        {
            var entity = new TQ();
            var fieldCount = dataReader.FieldCount;
            var properties = entity.GetType().GetProperties();

            for (var i = 0; i < fieldCount; i++)
            {
                foreach (var property in properties)
                {
                    if (!property.CanWrite)
                    {
                        continue;
                    }

                    if (property.Name != dataReader.GetName(i))
                    {
                        continue;
                    }

                    var value = dataReader.GetValue(i);
                    if (value is DBNull)
                    {
                        continue;
                    }

                    property.SetValue(entity, value, null);
                    break;
                }
            }

            return entity;
        }

        protected IList<string> GetColumns(IDataReader dataReader)
        {
            var columns = new List<string>();
            using (var schema = dataReader.GetSchemaTable())
            {
                for (var i = 0; i < schema.Rows.Count; i++)
                {
                    columns.Add(schema.Rows[i]["ColumnName"].ToString());
                }
            }

            return columns;
        }

        protected TQ GetValue<TQ>(IDataReader dataReader, string columnName)
        {
            var ordinal = dataReader.GetOrdinal(columnName);

            return GetValue<TQ>(dataReader, ordinal);
        }

        protected TQ GetValue<TQ>(IDataReader dataReader, int columnIndex)
        {
            if (!dataReader.IsDBNull(columnIndex))
            {
                return (TQ) dataReader.GetValue(columnIndex);
            }

            return default(TQ);
        }

        #endregion Metodos
    }
}